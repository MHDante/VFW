using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Vexe.Editor.Types;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Helpers;
using Vexe.Runtime.Types;
using UnityObject = UnityEngine.Object;

namespace Vexe.Editor.Drawers
{
	public class IDictionaryDrawer<TKey, TValue> : ObjectDrawer<IDictionary<TKey, TValue>>
	{
		private static readonly GUIStyle _bodyStyle = new GUIStyle(GUIStyles.HelpBox);
		private static readonly GUIStyle _headerStyle = new GUIStyle(GUIStyles.ToolbarButton);
		private static readonly GUIStyle _footerStyle = new GUIStyle(GUIStyles.ToolbarButton);

		private readonly Type _keyType;
		private readonly Type _valueType;
		private readonly List<EditorMember> _keyElements;
		private readonly List<EditorMember> _valueElements;

		private DictionaryOptions _options;
		private string _formatPairPattern;
		private bool _invalidKeyType;
		private string _originalDisplay;
		private TextFilter _filter;
		private Attribute[] _perKeyAttributes;
		private Attribute[] _perValueAttributes;
		private EditorMember _tempKeyMember;
		private TKey _tempKey;
		private int _lastUpdatedCount = -1;

		public bool UpdateCount = true;

		private Func<KeyValuePair<TKey, TValue>, string> _formatPair;

		private Func<KeyValuePair<TKey, TValue>, string> formatPair
		{
			get
			{
				return _formatPair ?? (_formatPair = new Func<KeyValuePair<TKey, TValue>, string>(pair => {
					var key = pair.Key;
					var value = pair.Value;

					var result = _formatPairPattern;
					result = Regex.Replace(result, @"\$keytype", key == null ? "null" : key.GetType().GetNiceName());
					result = Regex.Replace(result, @"\$valuetype", value == null ? "null" : value.GetType().GetNiceName());
					result = Regex.Replace(result, @"\$key", GetObjectString(key));
					result = Regex.Replace(result, @"\$value", GetObjectString(value));

					return result;
				}).Memoize());
			}
		}

		public IDictionaryDrawer()
		{
			_keyType = typeof(TKey);
			_valueType = typeof(TValue);
			_keyElements = new List<EditorMember>();
			_valueElements = new List<EditorMember>();
		}

		protected override void OnAfterUndoRedo()
		{
			base.OnAfterUndoRedo();

			_tempKey = default(TKey);
		}

		public override void OnGUI()
		{
			gui.Space(2f);

			using (gui.Vertical(_bodyStyle))
			{
				gui.BeginCheck();

				using (gui.Horizontal(_headerStyle))
				{
					gui.Space(10f);
					DrawHeader();
					gui.Space(-4f);
				}

				if (_invalidKeyType)
				{
					using (gui.Indent())
					{
						gui.HelpBox("Without [{0}] attribute, only value types and string are supported as Key.\n\n"
										.FormatWith(Dict.TempKey.ToString()) +
										"Unsupported Key type: {0}."
										.FormatWith(typeof(TKey)), MessageType.Error);
					}
				}
				else if (foldout || _options.ForceExpand)
				{
					DrawTempKey();

					if (memberValue == null || memberValue.Count == 0)
					{
						gui.Space(4f);

						using (gui.Indent())
							gui.HelpBox("Dictionary is empty");
					}
					else
					{
						DrawElements();
					}
				}

				gui.HasChanged();
			}
		}

		protected override void Initialize()
		{
			_headerStyle.fixedHeight = 15f;
			_headerStyle.margin.top = -1;
			_footerStyle.stretchWidth = true;

			var readOnlyAttr = attributes.GetAttribute<ReadOnlyAttribute>();
			var displayAttr = attributes.GetAttribute<DisplayAttribute>();
			var displayOption = Dict.None;

			if (displayAttr != null)
				displayOption = displayAttr.DictOpt;

			if (readOnlyAttr != null)
				displayOption |= Dict.Readonly;

			_options = new DictionaryOptions(displayOption);

			if (displayAttr != null)
				_formatPairPattern = displayAttr.FormatKVPair;

			if (_formatPairPattern.IsNullOrEmpty())
				_formatPairPattern = "[$key, $value]";

			if (_options.Readonly)
				displayText += " (Readonly)";

			_originalDisplay = displayText;
			_invalidKeyType = !_options.TempKey && !typeof(TKey).IsValueType && typeof(TKey) != typeof(string);

			if (_invalidKeyType)
			{
				displayText = Regex.Replace(_originalDisplay, @"\$count", "Null");
				return;
			}

			var perKey = attributes.GetAttribute<PerKeyAttribute>();

			if (perKey != null)
			{
				if (perKey.ExplicitAttributes == null)
					_perKeyAttributes = attributes.Where(x => !(x is PerKeyAttribute)).ToArray();
				else
					_perKeyAttributes = attributes.Where(x => perKey.ExplicitAttributes.Contains(x.GetType().Name.Replace("Attribute", ""))).ToArray();
			}

			var perValue = attributes.GetAttribute<PerValueAttribute>();

			if (perValue != null)
			{
				if (perValue.ExplicitAttributes == null)
					_perValueAttributes = attributes.Where(x => !(x is PerValueAttribute)).ToArray();
				else
					_perValueAttributes = attributes.Where(x => perValue.ExplicitAttributes.Contains(x.GetType().Name.Replace("Attribute", ""))).ToArray();
			}

			if (_options.Filter)
				_filter = new TextFilter(null, id, true, prefs, null);

			if (memberValue == null && !_options.ManualAlloc)
				memberValue = memberType.Instance<IDictionary<TKey, TValue>>();

			if (memberValue != null)
				member.CollectionCount = memberValue.Count;

			if (_options.TempKey)
			{
				_tempKeyMember = EditorMember.WrapMember(GetType().GetField("_tempKey", Flags.InstanceAnyVisibility),
						this, unityTarget, RuntimeHelper.CombineHashCodes(id, "temp"), null);
				_tempKeyMember.DisplayText = "New Key";
				_tempKey = GetNewKey(memberValue);
			}
		}

		private void DrawHeader()
		{
			if (_options.HideHeader)
				return;

			if (_invalidKeyType)
			{
				gui.Label(displayText);
				return;
			}

			if (_options.ForceExpand)
				gui.Label(displayText);
			else
				foldout = gui.Foldout(displayText, foldout, Layout.Auto);

			if (_options.Filter)
				_filter.Field(gui, 70f);

			if (memberValue == null)
			{
				displayText = Regex.Replace(_originalDisplay, @"\$count", "Null");

				if (_options.ManualAlloc)
				{
					if (!_options.Readonly && gui.Button("New", GUIStyles.MiniRight, Layout.sFit()))
					{
						memberValue = memberType.Instance<IDictionary<TKey, TValue>>();
					}
				}
			}

			if (memberValue == null)
				return;

			member.CollectionCount = memberValue.Count;

			if (UpdateCount && _lastUpdatedCount != memberValue.Count)
			{
				_lastUpdatedCount = memberValue.Count;
				displayText = Regex.Replace(_originalDisplay, @"\$count", _lastUpdatedCount.ToString());
			}

			if (_options.Readonly)
				return;

			gui.FlexibleSpace();

			if (_options.HideButtons)
				return;

			using (gui.State(memberValue.Count > 0))
			{
				if (gui.ClearButton("dictionary"))
				{
					RecordUndo("Clear");

					if (memberValue != null)
						memberValue.Clear();
				}

				if (gui.RemoveButton("last added pair", !_options.TempKey ? MiniButtonStyle.ModMid : MiniButtonStyle.Right))
				{
					RecordUndo("Remove Last Added Pair");
					RemoveLastAddedPair();
				}
			}

			if (!_options.TempKey && gui.AddButton("pair", MiniButtonStyle.ModRight))
			{
				Add();
			}
		}

		public void DrawTempKey()
		{
			if (!_options.TempKey || _options.Readonly)
				return;

			gui.Space(4f);

			using (gui.Horizontal())
			{
				gui.Space(8f);

				gui.MemberField(_tempKeyMember);
				var e = Event.current;

				var controlName = "TempAddButton";
				GUI.SetNextControlName(controlName);

				if ((e.type == EventType.KeyUp && e.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl() == controlName)
					|| gui.AddButton("key", MiniButtonStyle.ModRight))
				{
					Add();
					EditorGUI.FocusTextInControl(controlName);
				}
			}
		}

		private void DrawElements()
		{
			using (gui.Vertical())
			{
				gui.Space(4f);

				using (gui.Indent())
				{
					var keys = memberValue.Keys.ToList();
					var values = memberValue.Values.ToList();

					for (var i = 0; i < keys.Count; ++i)
					{
						var key = keys[i];
						var value = values[i];
						var entryKey = RuntimeHelper.CombineHashCodes(id, i, "entry");

						if (_filter != null)
						{
							var pairStr = FormatPair(key, value);
							var match = _filter.IsMatch(pairStr);

							if (!match)
								continue;
						}

						using (gui.Horizontal())
						{
							gui.MiniLabel("=");

							if (_options.HorizontalPairs)
							{
								using (gui.Horizontal())
								{
									DrawKey(keys, values, i, entryKey + 1);
									DrawValue(keys, values, i, entryKey + 2);
								}
							}
							else
							{
								using (gui.Vertical())
								{
									DrawKey(keys, values, i, entryKey + 1);
									DrawValue(keys, values, i, entryKey + 2);
								}
							}
						}

						if (i < keys.Count - 1)
						{
							this.gui.Space(6f);
						}
					}
				}
			}
		}

		private void DrawKey(List<TKey> keys, List<TValue> values, int index, int id)
		{
			var keyMember = GetElement(_keyType, _keyElements, keys, index, id + 1);

			using (gui.If(!_options.Readonly && _keyType.IsNumeric(), gui.LabelWidth(15f)))
			{
				if (gui.MemberField(keyMember, @ignoreComposition: _perKeyAttributes == null) && !_options.Readonly)
				{
					keys[index] = (TKey) keyMember.Value;
					Write(keys, values);
				}
			}
		}

		private void DrawValue(List<TKey> keys, List<TValue> values, int index, int id)
		{
			var valueMember = GetElement(_valueType, _valueElements, values, index, id + 2);

			using (gui.If(!_options.Readonly && _valueType.IsNumeric(), gui.LabelWidth(15f)))
			{
				if (gui.MemberField(valueMember, @ignoreComposition: _perValueAttributes == null) && _options.Readonly)
				{
					values[index] = (TValue) valueMember.Value;
					Write(keys, values);
				}
			}
		}

		private void Add()
		{
			if (memberValue == null && !_options.ManualAlloc)
			{
				RecordUndo("Add");
				memberValue = memberType.Instance<IDictionary<TKey, TValue>>();
				AddNewPair();
			}
			else if (memberValue != null)
			{
				RecordUndo("Add");
				AddNewPair();
			}
		}

		private void AddNewPair()
		{
			var key = _options.TempKey ? _tempKey : GetNewKey(memberValue);

			try
			{
				var value = default(TValue);

				if (!_options.AddFirst)
					memberValue.Add(key, value);
				else
				{
					var tempMember = memberType.Instance<IDictionary<TKey, TValue>>();
					tempMember.Add(key, value);

					foreach (var kv in memberValue)
					{
						tempMember.Add(kv.Key, kv.Value);
					}

					memberValue.Clear();

					foreach (var kv in tempMember)
					{
						memberValue.Add(kv.Key, kv.Value);
					}
				}

				var eKey = RuntimeHelper.CombineHashCodes(id, (memberValue.Count - 1), "entry");
				prefs[eKey] = true;
				foldout = true;

				if (_options.TempKey)
					_tempKey = GetNewKey(memberValue);
			}
			catch (ArgumentException)
			{
				if (_options.TempKey)
				{
					_tempKey = GetNewKey(memberValue);
				}

				Debug.Log("Key already exists: " + key);
			}
		}

		private TKey GetNewKey(IDictionary<TKey, TValue> from)
		{
			TKey key;

			if (typeof(TKey) == typeof(string))
			{
				string prefix;
				int postfix;

				if (from.Count > 0)
				{
					prefix = from.Last().Key as string;
					string postfixStr = "";
					int i = prefix.Length - 1;

					for (; i >= 0; i--)
					{
						char c = prefix[i];
						if (!char.IsDigit(c))
							break;
						postfixStr = postfixStr.Insert(0, c.ToString());
					}

					if (int.TryParse(postfixStr, out postfix))
						prefix = prefix.Remove(i + 1, postfixStr.Length);
				}
				else
				{
					prefix = "New Key ";
					postfix = 0;
				}

				while (from.Keys.Contains((TKey) (object) (prefix + postfix)))
					postfix++;

				key = (TKey) (object) (prefix + postfix);
			}
			else if (typeof(TKey) == typeof(int))
			{
				var n = 0;

				while (from.Keys.Contains((TKey) (object) (n)))
					n++;

				key = (TKey) (object) n;
			}
			else if (typeof(TKey).IsEnum)
			{
				var values = Enum.GetValues(typeof(TKey)) as TKey[];
				var result = values.Except(from.Keys).ToList();

				if (result.Count == 0)
					return default(TKey);

				key = result[0];
			}
			else
				key = default(TKey);

			return key;
		}

		private string GetObjectString(object from)
		{
			if (from.IsObjectNull())
				return "null";

			var obj = from as UnityObject;
			if (obj != null)
				return obj.name + " (" + obj.GetType().Name + ")";

			var toStr = from.ToString();
			return toStr == null ? "null" : toStr;
		}

		private string FormatPair(TKey key, TValue value)
		{
			string format = formatPair(new KeyValuePair<TKey, TValue>(key, value));
			return format;
		}

		private void Write(List<TKey> keys, List<TValue> values)
		{
			var rollbackKeys = memberValue.Keys.ToList();
			var rollbackValues = memberValue.Values.ToList();

			try
			{
				memberValue.Clear();

				for (var i = 0; i < keys.Count; ++i)
				{
					memberValue.Add(keys[i], values[i]);
				}
			}
			catch
			{
				memberValue.Clear();

				for (var i = 0; i < rollbackKeys.Count; ++i)
				{
					memberValue.Add(rollbackKeys[i], rollbackValues[i]);
				}
			}
		}

		private void RemoveLastAddedPair()
		{
			var keys = memberValue.Keys.ToList();
			var values = memberValue.Values.ToList();

			if (_options.AddFirst)
			{
				keys.RemoveAt(0);
				values.RemoveAt(0);
			}
			else
			{
				keys.RemoveAt(keys.Count - 1);
				values.RemoveAt(values.Count - 1);
			}

			Write(keys, values);
		}

		private EditorMember GetElement<T>(Type typeofT, List<EditorMember> elements, List<T> source, int index, int id)
		{
			if (index >= elements.Count)
			{
				Attribute[] attrs;
				if (typeofT == _keyType)
					attrs = _perKeyAttributes;
				else
					attrs = _perValueAttributes;

				var element = EditorMember.WrapIListElement(
					@elementName: string.Empty,
					@elementType: typeofT,
					@elementId: RuntimeHelper.CombineHashCodes(id, index),
					@attributes: attrs
				);
				element.InitializeIList(source, index, rawTarget, unityTarget);
				elements.Add(element);
				return element;
			}

			try
			{
				var e = elements[index];
				e.InitializeIList(source, index, rawTarget, unityTarget);
				return e;
			}
			catch (ArgumentOutOfRangeException)
			{
				Log("DictionaryDrawer: Accessing element out of range. Index: {0} Count {1}. This shouldn't really happen. Please report it with information on how to replicate it".FormatWith(index, elements.Count));
				return null;
			}
		}

		private struct DictionaryOptions
		{
			public readonly bool Readonly;
			public readonly bool ForceExpand;
			public readonly bool HideHeader;
			public readonly bool HorizontalPairs;
			public readonly bool Filter;
			public readonly bool AddFirst;
			public readonly bool TempKey;
			public readonly bool ManualAlloc;
			public readonly bool HideButtons;

			public DictionaryOptions(Dict options)
			{
				Readonly = options.HasFlag(Dict.Readonly);
				ForceExpand = options.HasFlag(Dict.ForceExpand);
				HideHeader = options.HasFlag(Dict.HideHeader);
				HorizontalPairs = options.HasFlag(Dict.HorizontalPairs);
				Filter = options.HasFlag(Dict.Filter);
				AddFirst = options.HasFlag(Dict.AddFirst);
				TempKey = options.HasFlag(Dict.TempKey);
				ManualAlloc = options.HasFlag(Dict.ManualAlloc);
				HideButtons = options.HasFlag(Dict.HideButtons);
			}
		}
	}
}
