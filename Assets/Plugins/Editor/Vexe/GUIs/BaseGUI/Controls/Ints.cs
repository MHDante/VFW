using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public int IntField(int value)
		{
			return IntField(string.Empty, value);
		}

		public int IntField(string label, int value)
		{
			return IntField(label, value, null);
		}

		public int IntField(string label, string tooltip, int value)
		{
			return IntField(label, tooltip, value, null);
		}

		public int IntField(string label, int value, Layout option)
		{
			return IntField(label, string.Empty, value, option);
		}

		public int IntField(string label, string tooltip, int value, Layout option)
		{
			return IntField(GetContent(label, tooltip), value, option);
		}

		public abstract int IntField(GUIContent content, int value, Layout option);
	}
}
