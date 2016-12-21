using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public long LongField(long value)
		{
			return LongField(string.Empty, value);
		}

		public long LongField(string label, long value)
		{
			return LongField(label, value, null);
		}

		public long LongField(string label, string tooltip, long value)
		{
			return LongField(label, tooltip, value, null);
		}

		public long LongField(string label, long value, Layout option)
		{
			return LongField(label, string.Empty, value, option);
		}

		public long LongField(string label, string tooltip, long value, Layout option)
		{
			return LongField(GetContent(label, tooltip), value, option);
		}

		public abstract long LongField(GUIContent content, long value, Layout option);
	}
}
