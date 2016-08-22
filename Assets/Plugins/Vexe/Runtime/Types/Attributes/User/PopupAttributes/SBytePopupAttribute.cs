using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a sbyte with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class SBytePopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly sbyte[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of sbytes</param>
		public SBytePopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public SBytePopupAttribute(params sbyte[] values)
		{
			this.values = values;
		}
	}
}
