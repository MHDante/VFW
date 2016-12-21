using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public AnimationCurve CurveField(AnimationCurve value)
		{
			return CurveField(string.Empty, value);
		}

		public AnimationCurve CurveField(string label, AnimationCurve value)
		{
			return CurveField(label, value, null);
		}

		public AnimationCurve CurveField(string label, string tooltip, AnimationCurve value)
		{
			return CurveField(label, tooltip, value, null);
		}

		public AnimationCurve CurveField(string label, AnimationCurve value, Layout option)
		{
			return CurveField(label, string.Empty, value, option);
		}

		public AnimationCurve CurveField(string label, string tooltip, AnimationCurve value, Layout option)
		{
			return CurveField(GetContent(label, tooltip), value, option);
		}

		public abstract AnimationCurve CurveField(GUIContent content, AnimationCurve value, Layout option);
	}
}
