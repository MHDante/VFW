using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a short with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class ShortPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly short[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of shorts</param>
		public ShortPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public ShortPopupAttribute(params short[] values)
		{
			this.values = values;
		}
	}
}
