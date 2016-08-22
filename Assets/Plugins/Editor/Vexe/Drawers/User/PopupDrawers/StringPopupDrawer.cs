using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class StringPopupDrawer : PopupDrawer<string, StringPopupAttribute>
	{
		protected override string[] Empty
		{
			get { return new string[1] { "--empty--" }; }
		}

		protected override string Default
		{
			get { return string.Empty; }
		}

		protected override string Convert(string stringValue)
		{
			return stringValue;
		}

		protected override TextFilter GetTextFilter(string[] values, int id, EditorRecord prefs)
		{
			return new TextFilter(values, id, false, prefs, SetValue);
		}

		protected override string DropDown(string displayText, string currentValue, string[] values)
		{
			return gui.TextFieldDropDown(displayText, memberValue, values);
		}

		protected override string[] GetDisplayStrings(string[] values)
		{
			return values;
		}

		protected override string GetActualValue(ref string value)
		{
			string result = value;

			if (attribute.TakeLastPathItem && !string.IsNullOrEmpty(value))
			{
				int lastPathIdx = value.LastIndexOf('/') + 1;

				if (lastPathIdx != -1)
					result = value.Substring(lastPathIdx);
			}

			return result;
		}

		protected override string[] GetAttributeValues()
		{
			return attribute.values;
		}
	}
}
