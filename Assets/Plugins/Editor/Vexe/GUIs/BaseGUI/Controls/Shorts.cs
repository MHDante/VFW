using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public short Short(short value)
		{
			return Short(string.Empty, value);
		}

		public short Short(string label, short value)
		{
			return Short(label, value, null);
		}

		public short Short(string label, string tooltip, short value)
		{
			return Short(label, tooltip, value, null);
		}

		public short Short(string label, short value, Layout option)
		{
			return Short(label, string.Empty, value, option);
		}

		public short Short(string label, string tooltip, short value, Layout option)
		{
			return Short(GetContent(label, tooltip), value, option);
		}

		public abstract short Short(GUIContent content, short value, Layout option);
	}
}
