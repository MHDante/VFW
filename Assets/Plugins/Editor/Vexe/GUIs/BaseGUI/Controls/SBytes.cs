using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public sbyte SByteField(sbyte value)
		{
			return SByteField(string.Empty, value);
		}

		public sbyte SByteField(string label, sbyte value)
		{
			return SByteField(label, value, null);
		}

		public sbyte SByteField(string label, string tooltip, sbyte value)
		{
			return SByteField(label, tooltip, value, null);
		}

		public sbyte SByteField(string label, sbyte value, Layout option)
		{
			return SByteField(label, string.Empty, value, option);
		}

		public sbyte SByteField(string label, string tooltip, sbyte value, Layout option)
		{
			return SByteField(GetContent(label, tooltip), value, option);
		}

		public abstract sbyte SByteField(GUIContent content, sbyte value, Layout option);
	}
}
