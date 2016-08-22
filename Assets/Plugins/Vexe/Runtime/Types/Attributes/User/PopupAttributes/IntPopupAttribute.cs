using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate an int with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class IntPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly int[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of ints</param>
		public IntPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public IntPopupAttribute(params int[] values)
		{
			this.values = values;
		}
	}
}
