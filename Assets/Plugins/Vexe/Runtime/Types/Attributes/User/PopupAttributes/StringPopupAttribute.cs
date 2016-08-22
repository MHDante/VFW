using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a string with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class StringPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly string[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of strings</param>
		public StringPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public StringPopupAttribute(params string[] values)
		{
			this.values = values;
		}
	}
}
