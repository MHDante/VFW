using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class UIntPopupDrawer : PopupDrawer<uint, UIntPopupAttribute>
	{
		protected override uint[] Empty
		{
			get { return new uint[0] { }; }
		}

		protected override uint Default
		{
			get { return 0; }
		}

		protected override uint[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override uint Convert(string stringValue)
		{
			uint result = 0;
			uint.TryParse(stringValue, out result);

			return result;
		}
	}
}
