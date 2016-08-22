using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a byte with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class BytePopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly byte[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of bytes</param>
		public BytePopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public BytePopupAttribute(params byte[] values)
		{
			this.values = values;
		}
	}
}
