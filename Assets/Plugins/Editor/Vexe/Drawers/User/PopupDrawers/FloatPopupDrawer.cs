using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class FloatPopupDrawer : PopupDrawer<float, FloatPopupAttribute>
	{
		protected override float[] Empty
		{
			get { return new float[0] { }; }
		}

		protected override float Default
		{
			get { return 0; }
		}

		protected override float[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override float Convert(string stringValue)
		{
			float result = 0;
			float.TryParse(stringValue, out result);

			return result;
		}
	}
}
