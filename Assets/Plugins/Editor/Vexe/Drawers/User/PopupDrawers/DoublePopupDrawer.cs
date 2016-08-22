using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class DoublePopupDrawer : PopupDrawer<double, DoublePopupAttribute>
	{
		protected override double[] Empty
		{
			get { return new double[0] { }; }
		}

		protected override double Default
		{
			get { return 0; }
		}

		protected override double[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override double Convert(string stringValue)
		{
			double result = 0;
			double.TryParse(stringValue, out result);

			return result;
		}
	}
}
