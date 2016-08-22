//#define PROFILE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Helpers;
using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public abstract class PopupDrawer<T, A> : AttributeDrawer<T, A>
		where A : PopupAttribute
		where T : IComparable, IComparable<T>, IEquatable<T>
	{
		protected int? CurrentIndex;

		private MethodCaller<object, object> _populateMethod;
		private MemberGetter<object, object> _populateMember;
		private bool _populateFromTarget, _populateFromType;
		private const string kOwnerTypePrefix = "target";
		private bool _showUpdateButton = true, _changed;
		private TextFilter _filter;
		private string[] _displayStrings;
		private T[] _cacheValues;

		protected T[] Values;

		protected abstract T[] Empty { get; }

		protected abstract T Default { get; }

		protected abstract T[] GetAttributeValues();

		protected abstract T Convert(string stringValue);

		protected virtual T DropDown(string displayText, T currentValue, string[] displayValues)
		{
			var value = gui.TextFieldDropDown(displayText, currentValue.ToString(), displayValues);

			return Convert(value);
		}

		protected virtual string[] GetDisplayStrings(T[] values)
		{
			var displayStrings = new string[values.Length];

			for (var i = 0; i < values.Length; ++i)
				displayStrings[i] = values[i].ToString();

			return displayStrings;
		}

		protected virtual TextFilter GetTextFilter(T[] values, int id, EditorRecord prefs)
		{
			return null;
		}

		public sealed override void OnGUI()
		{
			if (memberValue == null)
				memberValue = Default;

			if (Values == null)
			{
				UpdateValues();

				if (attribute.Filter)
					_filter = GetTextFilter(Values, id, prefs);
			}

			if (TryCache())
				_displayStrings = GetDisplayStrings(Values);

			T newValue = Default;
			T currentValue = memberValue;

			using (gui.Horizontal())
			{
				if (attribute.TextField)
				{
#if PROFILE
					Profiler.BeginSample("PopupDrawer TextFieldDrop");
#endif

					newValue = DropDown(displayText, memberValue, _displayStrings);

					if (!currentValue.Equals(newValue))
						_changed = true;

#if PROFILE
					Profiler.EndSample();
#endif
				}
				else
				{
#if PROFILE
					Profiler.BeginSample("PopupDrawer TextFieldDrop");
#endif

					if (!CurrentIndex.HasValue)
					{
						if (attribute.TakeLastPathItem)
							CurrentIndex = Values.IndexOf(x => GetActualValue(ref x).Equals(currentValue));
						else
							CurrentIndex = Values.IndexOf(currentValue);
					}

					if (CurrentIndex == -1)
					{
						CurrentIndex = 0;
						if (Values.Length > 0)
							SetValue(Values[0]);
					}

					gui.BeginCheck();

					int selection = gui.Popup(displayText, CurrentIndex.Value, _displayStrings);
					if (gui.HasChanged() && Values.Length > 0)
					{
						CurrentIndex = selection;
						_changed = true;
						newValue = Values[selection];
					}

#if PROFILE
					Profiler.EndSample();
#endif
				}

				if (attribute.Filter && _filter != null)
					_filter.OnGUI(gui, 45f);

				if (_changed)
				{
					_changed = false;
					SetValue(newValue);
				}

				if (_showUpdateButton && gui.MiniButton("U", "Update popup values", MiniButtonStyle.Right))
					UpdateValues();
			}
		}

		public void UpdateValues()
		{
			object target;
			if (_populateFromTarget)
				target = unityTarget;
			else if (_populateFromType)
				target = null;
			else target = rawTarget;

			if (_populateMember != null)
			{
				var pop = _populateMember(target);
				if (pop != null)
					Values = ProcessPopulation(pop);
			}
			else if (_populateMethod != null)
			{
				var pop = _populateMethod(target, null);
				if (pop != null)
					Values = ProcessPopulation(pop);
			}
			else Values = Empty;
		}

		protected sealed override void Initialize()
		{
			string fromMember = attribute.PopulateFrom;

			if (fromMember.IsNullOrEmpty())
			{
				Values = GetAttributeValues();

				_showUpdateButton = false;
			}
			else
			{
				_showUpdateButton = !attribute.HideUpdate;
				Type populateFrom;
				var split = fromMember.Split('.');
				if (split.Length == 1)
				{
					populateFrom = targetType;
				}
				else
				{
					if (split[0].ToLower() == kOwnerTypePrefix) // populate from unityTarget
					{
						populateFrom = unityTarget.GetType();
						_populateFromTarget = true;
					}
					else // populate from type (member should be static)
					{
						var typeName = split[0];
						populateFrom = ReflectionHelper.CachedGetRuntimeTypes()
													   .FirstOrDefault(x => x.Name == typeName);

						if (populateFrom == null)
							throw new InvalidOperationException("Couldn't find type to populate the popup from " + typeName);

						_populateFromType = true;
					}

					fromMember = split[1];
				}

				var all = populateFrom.GetAllMembers(typeof(object));
				var popMember = all.FirstOrDefault(x => attribute.CaseSensitive ? x.Name == fromMember : x.Name.ToLower() == fromMember.ToLower());
				if (popMember == null)
					ErrorHelper.MemberNotFound(populateFrom, fromMember);

				var field = popMember as FieldInfo;
				if (field != null)
					_populateMember = (popMember as FieldInfo).DelegateForGet();
				else
				{
					var prop = popMember as PropertyInfo;
					if (prop != null)
						_populateMember = (popMember as PropertyInfo).DelegateForGet();
					else
					{
						var method = popMember as MethodInfo;
						if (method == null)
							throw new Exception("{0} is not a field, nor a property nor a method!".FormatWith(fromMember));

						_populateMethod = (popMember as MethodInfo).DelegateForCall();
					}
				}
			}

			if (TryCache())
				_displayStrings = GetDisplayStrings(Values);

			if (attribute.Filter)
				_filter = GetTextFilter(Values, id, prefs);
		}

		protected virtual T GetActualValue(ref T value)
		{
			return value;
		}

		protected void SetValue(T value)
		{
			if (!attribute.TextField && attribute.TakeLastPathItem && attribute.Filter)
				CurrentIndex = Values.IndexOf(value);

			memberValue = GetActualValue(ref value);
		}

		protected T[] ProcessPopulation(object obj)
		{
			var arr = obj as T[];
			if (arr != null)
				return arr;

			var list = obj as List<T>;
			if (list != null)
				return list.ToArray();

			return Empty;
		}

		private bool TryCache()
		{
			if (Values == null)
				return false;

			if (_cacheValues == null ||
				_cacheValues.Length != Values.Length)
			{
				_cacheValues = new T[Values.Length];

				for (var i = 0; i < Values.Length; ++i)
					_cacheValues[i] = Values[i];

				return true;
			}

			var cached = false;

			for (var i = 0; i < Values.Length; ++i)
			{
				if (!_cacheValues[i].Equals(Values[i]))
				{
					_cacheValues[i] = Values[i];
					cached = true;
				}
			}

			return cached;
		}
	}
}
