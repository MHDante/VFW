using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class CharPopupDrawer : PopupDrawer<char, CharPopupAttribute>
	{
		protected override char[] Empty
		{
			get { return new char[0] { }; }
		}

		protected override char Default
		{
			get { return '\0'; }
		}

		protected override char[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override char Convert(string stringValue)
		{
			char result = '\0';

			if (stringValue.Length == 1)
				result = stringValue[0];

			return result;
		}
	}
}
