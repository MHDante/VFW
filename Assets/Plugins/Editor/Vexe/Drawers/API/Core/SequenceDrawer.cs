using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Vexe.Editor.Types;
using Vexe.Editor.Windows;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Helpers;
using Vexe.Runtime.Types;
using UnityObject = UnityEngine.Object;

namespace Vexe.Editor.Drawers
{
	public abstract class SequenceDrawer<TSequence, TElement> : ObjectDrawer<TSequence> where TSequence : IList<TElement>
	{
		private static readonly GUIStyle _bodyStyle = new GUIStyle(GUIStyles.HelpBox);
		private static readonly GUIStyle _headerStyle = new GUIStyle(GUIStyles.ToolbarButton);
		private static readonly GUIStyle _footerStyle = new GUIStyle(GUIStyles.ToolbarButton);

		private readonly Type _elementType;
		private readonly List<EditorMember> _elements;

		private SequenceOptions _options;
		private bool _shouldDrawAddingArea;
		private int _newSize;
		private int _advancedKey;
		private int _lastUpdatedCount = -1;
		private Attribute[] _perItemAttributes;
		private TextFilter _filter;
		private string _originalDisplay;

		public bool UpdateCount = true;

		private bool isAdvancedChecked
		{
			get { return prefs[_advancedKey]; }
			set { prefs[_advancedKey] = value; }
		}

		protected abstract TSequence GetNew();

		public SequenceDrawer()
		{
			_elementType = typeof(TElement);
			_elements = new List<EditorMember>();
		}

		public override void OnGUI()
		{
			this.gui.Space(2f);

			if (memberValue == null)
				memberValue = GetNew();

			member.CollectionCount = memberValue.Count;
			var showAdvanced = _options.Advanced && !_options.Readonly;

			if (UpdateCount && _lastUpdatedCount != memberValue.Count)
			{
				_lastUpdatedCount = memberValue.Count;
				displayText = Regex.Replace(_originalDisplay, @"\$count", _lastUpdatedCount.ToString());
			}

			using (gui.Vertical(_bodyStyle))
			{
				this.gui.BeginCheck();

				using (gui.Horizontal(_headerStyle))
				{
					gui.Space(10f);
					DrawHeader(showAdvanced);
					gui.Space(-4f);
				}

				if (foldout)
				{
					if (memberValue.IsEmpty())
					{
						gui.Space(4f);

						using (gui.Indent())
							gui.HelpBox("Sequence is empty");
					}
					else
					{
						DrawElements(showAdvanced);
					}

					DrawFooter();
				}

				this.gui.HasChanged();
			}
		}

		protected override void Initialize()
		{
			_headerStyle.fixedHeight = 15f;
			_headerStyle.margin.top = -1;
			_footerStyle.stretchWidth = true;

			var readOnlyAttr = attributes.GetAttribute<ReadOnlyAttribute>();
			var displayAttr = attributes.GetAttribute<DisplayAttribute>();
			var displayOption = Seq.None;

			if (displayAttr != null)
				displayOption = displayAttr.SeqOpt;

			if (readOnlyAttr != null)
				displayOption |= Seq.Readonly;

			_options = new SequenceOptions(displayOption);

			if (_options.Readonly)
				displayText += " (Readonly)";

			_advancedKey = RuntimeHelper.CombineHashCodes(id, "advanced");
			_shouldDrawAddingArea = !_options.Readonly && _elementType.IsA<UnityObject>();

			var perItem = attributes.GetAttribute<PerItemAttribute>();

			if (perItem != null)
			{
				if (perItem.ExplicitAttributes == null)
					_perItemAttributes = attributes.Where(x => !(x is PerItemAttribute)).ToArray();
				else _perItemAttributes = attributes.Where(x => perItem.ExplicitAttributes.Contains(x.GetType().Name.Replace("Attribute", ""))).ToArray();
			}

			if (_options.Filter)
				_filter = new TextFilter(null, id, true, prefs, null);

			_originalDisplay = displayText;

			if (memberValue == null)
				memberValue = GetNew();

			member.CollectionCount = memberValue.Count;
		}

		private void DrawHeader(bool showAdvanced)
		{
			foldout = gui.Foldout(displayText, foldout, Layout.Auto);

			if (_options.Filter)
				_filter.Field(gui, 70f);

			gui.FlexibleSpace();

			if (showAdvanced)
				isAdvancedChecked = gui.CheckButton(isAdvancedChecked, "advanced mode");

			if (_options.Readonly)
				return;

			using (gui.State(memberValue.Count > 0))
			{
				if (gui.ClearButton("list"))
				{
					RecordUndo("Clear");
					Clear();
				}

				if (gui.RemoveButton("last element"))
				{
					RecordUndo("Remove Last");
					RemoveLast();
				}
			}

			if (gui.AddButton("element", MiniButtonStyle.Right))
			{
				RecordUndo("Add");
				AddValue();
			}
		}

		private void DrawElements(bool showAdvanced)
		{
			// body
			using (gui.Vertical(_options.GuiBox ? GUI.skin.box : GUIStyle.none))
			{
				// advanced area
				if (isAdvancedChecked)
				{
					gui.Space(-2f);

					using (gui.Indent((GUI.skin.box)))
					{
						gui.Space(4f);
						using (gui.Horizontal())
						{
							_newSize = gui.IntField("New size", _newSize);
							if (gui.MiniButton("c", "Commit", MiniButtonStyle.ModRight))
							{
								if (_newSize != memberValue.Count)
								{
									RecordUndo("Adjust Size");
									memberValue.AdjustSize(_newSize, RemoveAt, AddValue);
								}
							}
						}

						using (gui.Horizontal())
						{
							gui.Label("Commands");

							if (gui.MiniButton("Shuffle", "Shuffle list (randomize the order of the list's elements", (Layout) null))
							{
								RecordUndo("Shuffle");
								Shuffle();
							}

							if (gui.MoveDownButton())
							{
								RecordUndo("Shift Down");
								memberValue.Shift(true);
							}

							if (gui.MoveUpButton(_elementType.IsValueType ? MiniButtonStyle.Right : MiniButtonStyle.Mid))
							{
								RecordUndo("Shift Up");
								memberValue.Shift(false);
							}

							if (!_elementType.IsValueType && gui.MiniButton("N", "Filter null values", MiniButtonStyle.Right))
							{
								for (int i = memberValue.Count - 1; i > -1; i--)
								{
									if (memberValue[i] == null)
									{
										RecordUndo("Remove At");
										RemoveAt(i);
									}
								}
							}
						}
					}
				}

				gui.Space(4f);

				using (gui.Indent(_options.GuiBox ? GUI.skin.box : GUIStyle.none))
				{
#if PROFILE
					Profiler.BeginSample("Sequence Elements");
#endif

					for (int i = 0; i < memberValue.Count; i++)
					{
						var elementValue = memberValue[i];

						if (_filter != null && elementValue != null)
						{
							string elemStr = elementValue.ToString();

							if (!_filter.IsMatch(elemStr))
								continue;
						}

						using (gui.Horizontal())
						{
							if (_options.LineNumbers)
								gui.NumericLabel(i);
							else
								gui.MiniLabel("=");

							var previous = elementValue;
							var changed = false;

							var element = GetElement(i);

							using (gui.If(!_options.Readonly && _elementType.IsNumeric(), gui.LabelWidth(15f)))
							{
								changed = gui.Member(element, @ignoreComposition: _perItemAttributes == null);
							}

							if (changed)
							{
								if (_options.Readonly)
								{
									memberValue[i] = previous;
								}
								else if (_options.UniqueItems)
								{
									int occurances = 0;

									for (int k = 0; k < memberValue.Count; k++)
									{
										if (memberValue[i].GenericEquals(memberValue[k]))
										{
											occurances++;

											if (occurances > 1)
											{
												memberValue[i] = previous;
												break;
											}
										}
									}
								}
							}

							var midStyle = !_options.Readonly && ( _options.PerItemRemove || _options.PerItemDuplicate);

							if (isAdvancedChecked)
							{
								var c = elementValue as Component;
								var go = c == null ? elementValue as GameObject : c.gameObject;
								if (go != null)
									gui.InspectButton(go);

								if (showAdvanced)
								{
									if (gui.MoveDownButton())
									{
										RecordUndo("Move Down");
										MoveElementDown(i);
									}

									if (gui.MoveUpButton(midStyle ? MiniButtonStyle.Mid : MiniButtonStyle.Right))
									{
										RecordUndo("Move Up");
										MoveElementUp(i);
									}
								}
							}

							if (!_options.Readonly && _options.PerItemRemove && gui.RemoveButton("element", MiniButtonStyle.ModRight))
							{
								RecordUndo("Remove At");
								RemoveAt(i);
							}

							// Only valid for Classes implementing ICloneable
							if (typeof(ICloneable).IsAssignableFrom(_elementType) && elementValue != null)
							{
								if (!_options.Readonly && _options.PerItemDuplicate && gui.AddButton("element", MiniButtonStyle.ModRight))
								{
									ICloneable _elementToClone = (ICloneable) elementValue;
									TElement cloned = (TElement) _elementToClone.Clone();
									RecordUndo("Add");
									AddValue(cloned);
								}
							}
						}

						if (i < memberValue.Count - 1)
						{
							this.gui.Space(6f);
						}
					}
#if PROFILE
						Profiler.EndSample();
#endif
				}
			}
		}

		private void DrawFooter()
		{
			if (!_shouldDrawAddingArea)
				return;

			Action<UnityObject> addOnDrop = obj => {
				var go = obj as GameObject;
				object value;

				if (go == null)
					value = obj;
				else
					value = _elementType == typeof(GameObject) ? (UnityObject) go : go.GetComponent(_elementType);

				RecordUndo("Add");
				AddValue((TElement) value);
			};

			using (gui.Indent())
			{
				gui.Space(4f);
				gui.DragDropArea<UnityObject>(
					@label: "+Drag-Drop+",
					@labelSize: 14,
					@style: _footerStyle,
					@canSetVisualModeToCopy: dragObjects => dragObjects.All(obj => {
						var go = obj as GameObject;
						var isGo = go != null;
						if (_elementType == typeof(GameObject))
							return isGo;

						return isGo ? go.GetComponent(_elementType) != null : obj.GetType().IsA(_elementType);
					}),
					@cursor: MouseCursor.Link,
					@onDrop: addOnDrop,
					@onMouseUp: () => SelectionWindow.Show(new Tab<UnityObject>(
						@getValues: () => UnityObject.FindObjectsOfType(_elementType),
						@getCurrent: () => null,
						@setTarget: item => {
							RecordUndo("Add");
							AddValue((TElement) (object) item);
						},
						@getValueName: value => value.name,
						@title: _elementType.Name + "s")),
					@preSpace: 0f,
					@postSpace: 6f,
					@height: 15f
				);
			}

			gui.Space(4f);
		}

		private EditorMember GetElement(int index)
		{
			if (index >= _elements.Count)
			{
				var element = EditorMember.WrapIListElement(
					@attributes: _perItemAttributes,
					@elementName: string.Empty,
					@elementType: _elementType,
					@elementId: RuntimeHelper.CombineHashCodes(id, index)
				);

				element.InitializeIList(memberValue, index, rawTarget, unityTarget);
				_elements.Add(element);
				return element;
			}

			var e = _elements[index];
			e.InitializeIList(memberValue, index, rawTarget, unityTarget);
			return e;
		}

		#region List ops

		protected abstract void Clear();

		protected abstract void RemoveAt(int atIndex);

		protected abstract void Insert(int index, TElement value);

		private void Shuffle()
		{
			memberValue.Shuffle();
		}

		private void MoveElementDown(int i)
		{
			memberValue.MoveElementDown(i);
		}

		private void MoveElementUp(int i)
		{
			memberValue.MoveElementUp(i);
		}

		private void RemoveLast()
		{
			RemoveAt(memberValue.Count - 1);
		}

		private void AddValue(TElement value)
		{
			Insert(memberValue.Count, value);
		}

		private void AddValue()
		{
			AddValue((TElement) _elementType.GetDefaultValueEmptyIfString());
		}

		#endregion List ops

		private struct SequenceOptions
		{
			public readonly bool Readonly;
			public readonly bool Advanced;
			public readonly bool LineNumbers;
			public readonly bool PerItemRemove;
			public readonly bool PerItemDuplicate;
			public readonly bool GuiBox;
			public readonly bool UniqueItems;
			public readonly bool Filter;

			public SequenceOptions(Seq options)
			{
				Readonly = options.HasFlag(Seq.Readonly);
				Advanced = options.HasFlag(Seq.Advanced);
				LineNumbers = options.HasFlag(Seq.LineNumbers);
				PerItemRemove = options.HasFlag(Seq.PerItemRemove);
				PerItemDuplicate = options.HasFlag(Seq.PerItemDuplicate);
				GuiBox = options.HasFlag(Seq.GuiBox);
				UniqueItems = options.HasFlag(Seq.UniqueItems);
				Filter = options.HasFlag(Seq.Filter);
			}
		}
	}

	public class ArrayDrawer<T> : SequenceDrawer<T[], T>
	{
		protected override T[] GetNew()
		{
			return new T[0];
		}

		protected override void RemoveAt(int atIndex)
		{
			memberValue = memberValue.ToList().RemoveAtAndGet(atIndex).ToArray();
		}

		protected override void Clear()
		{
			memberValue = memberValue.ToList().ClearAndGet().ToArray();
		}

		protected override void Insert(int index, T value)
		{
			memberValue = memberValue.ToList().InsertAndGet(index, value).ToArray();
			foldout = true;
		}
	}

	public class ListDrawer<T> : SequenceDrawer<List<T>, T>
	{
		protected override List<T> GetNew()
		{
			return new List<T>();
		}

		protected override void RemoveAt(int index)
		{
			memberValue.RemoveAt(index);
		}

		protected override void Clear()
		{
			memberValue.Clear();
		}

		protected override void Insert(int index, T value)
		{
			memberValue.Insert(index, value);
			foldout = true;
		}
	}
}
