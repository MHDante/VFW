using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class LongPopupDrawer : PopupDrawer<long, LongPopupAttribute>
	{
		protected override long[] Empty
		{
			get { return new long[0] { }; }
		}

		protected override long Default
		{
			get { return 0; }
		}

		protected override long[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override long Convert(string stringValue)
		{
			long result = 0;
			long.TryParse(stringValue, out result);

			return result;
		}
	}
}
