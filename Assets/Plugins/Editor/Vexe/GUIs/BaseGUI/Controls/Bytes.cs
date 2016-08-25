using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public byte Byte(byte value)
		{
			return Byte(string.Empty, value);
		}

		public byte Byte(string label, byte value)
		{
			return Byte(label, value, null);
		}

		public byte Byte(string label, string tooltip, byte value)
		{
			return Byte(label, tooltip, value, null);
		}

		public byte Byte(string label, byte value, Layout option)
		{
			return Byte(label, string.Empty, value, option);
		}

		public byte Byte(string label, string tooltip, byte value, Layout option)
		{
			return Byte(GetContent(label, tooltip), value, option);
		}

		public abstract byte Byte(GUIContent content, byte value, Layout option);
	}
}
