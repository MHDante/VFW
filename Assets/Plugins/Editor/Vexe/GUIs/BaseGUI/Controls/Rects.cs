using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public Rect RectField(Rect value)
		{
			return RectField(string.Empty, value);
		}

		public Rect RectField(string label, Rect value)
		{
			return RectField(label, value, kMultifieldOption);
		}

		public Rect RectField(string label, string tooltip, Rect value)
		{
			return RectField(label, tooltip, value, kMultifieldOption);
		}

		public Rect RectField(string label, Rect value, Layout option)
		{
			return RectField(label, string.Empty, value, option);
		}

		public Rect RectField(string label, string tooltip, Rect value, Layout option)
		{
			return RectField(new GUIContent(label, tooltip), value, option);
		}

		public abstract Rect RectField(GUIContent content, Rect value, Layout option);
	}
}
