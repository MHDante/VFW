using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public float FloatField(float value)
		{
			return FloatField(string.Empty, value);
		}

		public float FloatField(string label, float value)
		{
			return FloatField(label, value, null);
		}

		public float FloatField(string label, string tooltip, float value)
		{
			return FloatField(label, tooltip, value, null);
		}

		public float FloatField(string label, float value, Layout option)
		{
			return FloatField(label, string.Empty, value, option);
		}

		public float FloatField(string label, string tooltip, float value, Layout option)
		{
			return FloatField(GetContent(label, tooltip), value, option);
		}

		public abstract float FloatField(GUIContent content, float value, Layout option);
	}
}
