using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public ulong ULongField(ulong value)
		{
			return ULongField(string.Empty, value);
		}

		public ulong ULongField(string label, ulong value)
		{
			return ULongField(label, value, null);
		}

		public ulong ULongField(string label, string tooltip, ulong value)
		{
			return ULongField(label, tooltip, value, null);
		}

		public ulong ULongField(string label, ulong value, Layout option)
		{
			return ULongField(label, string.Empty, value, option);
		}

		public ulong ULongField(string label, string tooltip, ulong value, Layout option)
		{
			return ULongField(GetContent(label, tooltip), value, option);
		}

		public abstract ulong ULongField(GUIContent content, ulong value, Layout option);
	}
}
