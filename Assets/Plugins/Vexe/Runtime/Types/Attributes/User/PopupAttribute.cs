using System;

namespace Vexe.Runtime.Types
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public abstract class PopupAttribute : DrawnAttribute
	{
		/// <summary>
		/// Use this if you want to dynamically generate the popup values instead of having to hardcode them
		/// This could be a method with no parameters, or a field/property, with the right return type
		/// </summary>
		public string PopulateFrom;

		/// <summary>
		/// Is the 'PopulateFrom' member name case sensitive? (Defaults to false)
		/// </summary>
		public bool CaseSensitive;

		/// <summary>
		/// Use a text field dropdown/popup? (same one used setting up Mecanim's conditions)
		/// </summary>
		public bool TextField;

		/// <summary>
		/// Take the last entry in a path string? e.g. One/Two/Three -> Three
		/// </summary>
		public bool TakeLastPathItem;

		/// <summary>
		/// Hide update 'U' button?
		/// </summary>
		public bool HideUpdate;

		/// <summary>
		/// Show a search text box to filter values?
		/// </summary>
		public bool Filter;

		protected PopupAttribute()
		{ }

		protected PopupAttribute(string populateFrom)
		{
			PopulateFrom = populateFrom;
		}
	}
}
