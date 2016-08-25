using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public sbyte SByte(sbyte value)
		{
			return SByte(string.Empty, value);
		}

		public sbyte SByte(string label, sbyte value)
		{
			return SByte(label, value, null);
		}

		public sbyte SByte(string label, string tooltip, sbyte value)
		{
			return SByte(label, tooltip, value, null);
		}

		public sbyte SByte(string label, sbyte value, Layout option)
		{
			return SByte(label, string.Empty, value, option);
		}

		public sbyte SByte(string label, string tooltip, sbyte value, Layout option)
		{
			return SByte(GetContent(label, tooltip), value, option);
		}

		public abstract sbyte SByte(GUIContent content, sbyte value, Layout option);
	}
}
