using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public short ShortField(short value)
		{
			return ShortField(string.Empty, value);
		}

		public short ShortField(string label, short value)
		{
			return ShortField(label, value, null);
		}

		public short ShortField(string label, string tooltip, short value)
		{
			return ShortField(label, tooltip, value, null);
		}

		public short ShortField(string label, short value, Layout option)
		{
			return ShortField(label, string.Empty, value, option);
		}

		public short ShortField(string label, string tooltip, short value, Layout option)
		{
			return ShortField(GetContent(label, tooltip), value, option);
		}

		public abstract short ShortField(GUIContent content, short value, Layout option);
	}
}
