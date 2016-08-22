using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a double with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class DoublePopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly double[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of doubles</param>
		public DoublePopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public DoublePopupAttribute(params double[] values)
		{
			this.values = values;
		}
	}
}
