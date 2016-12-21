using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public char CharField(char value)
		{
			return CharField(string.Empty, value);
		}

		public char CharField(string label, char value)
		{
			return CharField(label, value, null);
		}

		public char CharField(string label, string tooltip, char value)
		{
			return CharField(label, tooltip, value, null);
		}

		public char CharField(string label, char value, Layout option)
		{
			return CharField(label, string.Empty, value, option);
		}

		public char CharField(string label, string tooltip, char value, Layout option)
		{
			return CharField(GetContent(label, tooltip), value, option);
		}

		public abstract char CharField(GUIContent content, char value, Layout option);
	}
}
