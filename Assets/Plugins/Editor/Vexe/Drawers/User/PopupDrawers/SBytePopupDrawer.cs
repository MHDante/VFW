using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class SBytePopupDrawer : PopupDrawer<sbyte, SBytePopupAttribute>
	{
		protected override sbyte[] Empty
		{
			get { return new sbyte[0] { }; }
		}

		protected override sbyte Default
		{
			get { return 0; }
		}

		protected override sbyte[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override sbyte Convert(string stringValue)
		{
			sbyte result = 0;
			sbyte.TryParse(stringValue, out result);

			return result;
		}
	}
}
