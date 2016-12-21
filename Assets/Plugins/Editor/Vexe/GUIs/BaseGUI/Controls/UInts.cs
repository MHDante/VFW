using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public uint UIntField(uint value)
		{
			return UIntField(string.Empty, value);
		}

		public uint UIntField(string label, uint value)
		{
			return UIntField(label, value, null);
		}

		public uint UIntField(string label, string tooltip, uint value)
		{
			return UIntField(label, tooltip, value, null);
		}

		public uint UIntField(string label, uint value, Layout option)
		{
			return UIntField(label, string.Empty, value, option);
		}

		public uint UIntField(string label, string tooltip, uint value, Layout option)
		{
			return UIntField(GetContent(label, tooltip), value, option);
		}

		public abstract uint UIntField(GUIContent content, uint value, Layout option);
	}
}
