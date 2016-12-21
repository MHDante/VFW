using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public ushort UShortField(ushort value)
		{
			return UShortField(string.Empty, value);
		}

		public ushort UShortField(string label, ushort value)
		{
			return UShortField(label, value, null);
		}

		public ushort UShortField(string label, string tooltip, ushort value)
		{
			return UShortField(label, tooltip, value, null);
		}

		public ushort UShortField(string label, ushort value, Layout option)
		{
			return UShortField(label, string.Empty, value, option);
		}

		public ushort UShortField(string label, string tooltip, ushort value, Layout option)
		{
			return UShortField(GetContent(label, tooltip), value, option);
		}

		public abstract ushort UShortField(GUIContent content, ushort value, Layout option);
	}
}
