using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class UShortPopupDrawer : PopupDrawer<ushort, UShortPopupAttribute>
	{
		protected override ushort[] Empty
		{
			get { return new ushort[0] { }; }
		}

		protected override ushort Default
		{
			get { return 0; }
		}

		protected override ushort[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override ushort Convert(string stringValue)
		{
			ushort result = 0;
			ushort.TryParse(stringValue, out result);

			return result;
		}
	}
}
