using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class ShortPopupDrawer : PopupDrawer<short, ShortPopupAttribute>
	{
		protected override short[] Empty
		{
			get { return new short[0] { }; }
		}

		protected override short Default
		{
			get { return 0; }
		}

		protected override short[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override short Convert(string stringValue)
		{
			short result = 0;
			short.TryParse(stringValue, out result);

			return result;
		}
	}
}
