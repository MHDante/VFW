using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public ushort UShort(ushort value)
		{
			return UShort(string.Empty, value);
		}

		public ushort UShort(string label, ushort value)
		{
			return UShort(label, value, null);
		}

		public ushort UShort(string label, string tooltip, ushort value)
		{
			return UShort(label, tooltip, value, null);
		}

		public ushort UShort(string label, ushort value, Layout option)
		{
			return UShort(label, string.Empty, value, option);
		}

		public ushort UShort(string label, string tooltip, ushort value, Layout option)
		{
			return UShort(GetContent(label, tooltip), value, option);
		}

		public abstract ushort UShort(GUIContent content, ushort value, Layout option);
	}
}
