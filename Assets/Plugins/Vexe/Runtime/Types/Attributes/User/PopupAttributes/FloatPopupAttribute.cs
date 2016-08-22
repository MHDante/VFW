using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a float with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class FloatPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly float[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of floats</param>
		public FloatPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public FloatPopupAttribute(params float[] values)
		{
			this.values = values;
		}
	}
}
