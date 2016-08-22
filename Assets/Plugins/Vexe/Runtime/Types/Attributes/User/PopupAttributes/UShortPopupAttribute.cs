using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a ushort with this attribute to have its value selected from a popup
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class UShortPopupAttribute : PopupAttribute
	{
		/// <summary>
		/// The popup values
		/// </summary>
		public readonly ushort[] values;

		/// <summary>
		///
		/// </summary>
		/// <param name="populateFrom">A method with no parameters, or a field/property, with the return type of an array of ushorts</param>
		public UShortPopupAttribute(string populateFrom) : base(populateFrom)
		{ }

		public UShortPopupAttribute(params ushort[] values)
		{
			this.values = values;
		}
	}
}
