using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class IntPopupDrawer : PopupDrawer<int, IntPopupAttribute>
	{
		protected override int[] Empty
		{
			get { return new int[0] { }; }
		}

		protected override int Default
		{
			get { return 0; }
		}

		protected override int[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override int Convert(string stringValue)
		{
			int result = 0;
			int.TryParse(stringValue, out result);

			return result;
		}
	}
}
