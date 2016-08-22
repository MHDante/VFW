using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a long with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class LongPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly long[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of longs</param>
		public LongPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public LongPopupAttribute(params long[] values)
		{
			this.values = values;
		}
	}
}
