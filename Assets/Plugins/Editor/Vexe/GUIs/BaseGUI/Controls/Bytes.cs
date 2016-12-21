using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public byte ByteField(byte value)
		{
			return ByteField(string.Empty, value);
		}

		public byte ByteField(string label, byte value)
		{
			return ByteField(label, value, null);
		}

		public byte ByteField(string label, string tooltip, byte value)
		{
			return ByteField(label, tooltip, value, null);
		}

		public byte ByteField(string label, byte value, Layout option)
		{
			return ByteField(label, string.Empty, value, option);
		}

		public byte ByteField(string label, string tooltip, byte value, Layout option)
		{
			return ByteField(GetContent(label, tooltip), value, option);
		}

		public abstract byte ByteField(GUIContent content, byte value, Layout option);
	}
}
