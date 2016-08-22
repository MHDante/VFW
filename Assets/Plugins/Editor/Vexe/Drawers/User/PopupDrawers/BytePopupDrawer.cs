using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public sealed class BytePopupDrawer : PopupDrawer<byte, BytePopupAttribute>
	{
		protected override byte[] Empty
		{
			get { return new byte[0] { }; }
		}

		protected override byte Default
		{
			get { return 0; }
		}

		protected override byte[] GetAttributeValues()
		{
			return attribute.values;
		}

		protected override byte Convert(string stringValue)
		{
			byte result = 0;
			byte.TryParse(stringValue, out result);

			return result;
		}
	}
}
