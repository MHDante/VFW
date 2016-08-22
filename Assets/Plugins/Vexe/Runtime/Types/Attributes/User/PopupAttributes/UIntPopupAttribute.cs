using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a uint with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class UIntPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly uint[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of uints</param>
		public UIntPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public UIntPopupAttribute(params uint[] values)
		{
			this.values = values;
		}
	}
}
