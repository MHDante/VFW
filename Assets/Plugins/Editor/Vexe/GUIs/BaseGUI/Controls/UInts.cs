using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public uint UInt(uint value)
		{
			return UInt(string.Empty, value);
		}

		public uint UInt(string label, uint value)
		{
			return UInt(label, value, null);
		}

		public uint UInt(string label, string tooltip, uint value)
		{
			return UInt(label, tooltip, value, null);
		}

		public uint UInt(string label, uint value, Layout option)
		{
			return UInt(label, string.Empty, value, option);
		}

		public uint UInt(string label, string tooltip, uint value, Layout option)
		{
			return UInt(GetContent(label, tooltip), value, option);
		}

		public abstract uint UInt(GUIContent content, uint value, Layout option);
	}
}
