using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate a field/property with this attribute and setup a method to execute when the value of that field/property changes.
	/// Note that when applying it on collections (list, array, dictionary) it will give you a callback when the collection count changes
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class OnChangedNoArgAttribute : CompositeAttribute
	{
		/// <summary>
		/// The name of a method to be called when the value changes
		/// It has to have a void return, and no argument
		/// It doesn't matter what the access modifier on the method is
		/// It could be instance, or static
		/// </summary>
		public string Call { get; set; }

		public OnChangedNoArgAttribute(int id, string call) : base(id)
		{
			Call = call;
		}

		public OnChangedNoArgAttribute(string call) : this(-1, call)
		{
		}

		public OnChangedNoArgAttribute() : this(string.Empty)
		{
		}
	}
}
