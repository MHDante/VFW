using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public Color ColorField(Color value)
		{
			return ColorField(string.Empty, value);
		}

		public Color ColorField(string label, Color value)
		{
			return ColorField(label, value, null);
		}

		public Color ColorField(string label, string tooltip, Color value)
		{
			return ColorField(label, tooltip, value, null);
		}

		public Color ColorField(string label, Color value, Layout option)
		{
			return ColorField(label, string.Empty, value, option);
		}

		public Color ColorField(string label, string tooltip, Color value, Layout option)
		{
			return ColorField(GetContent(label, tooltip), value, option);
		}

		public abstract Color ColorField(GUIContent content, Color value, Layout option);
	}
}
