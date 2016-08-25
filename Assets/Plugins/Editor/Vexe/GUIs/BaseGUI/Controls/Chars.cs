using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public char Char(char value)
		{
			return Char(string.Empty, value);
		}

		public char Char(string label, char value)
		{
			return Char(label, value, null);
		}

		public char Char(string label, string tooltip, char value)
		{
			return Char(label, tooltip, value, null);
		}

		public char Char(string label, char value, Layout option)
		{
			return Char(label, string.Empty, value, option);
		}

		public char Char(string label, string tooltip, char value, Layout option)
		{
			return Char(GetContent(label, tooltip), value, option);
		}

		public abstract char Char(GUIContent content, char value, Layout option);
	}
}
