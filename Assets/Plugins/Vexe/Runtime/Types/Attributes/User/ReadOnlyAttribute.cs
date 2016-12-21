using System;

namespace Vexe.Runtime.Types
{
	/// <summary>
	/// Annotate fields and properties of any type with this attribute to make it readonly
	/// and cannot be modified in inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
	public class ReadOnlyAttribute : CompositeAttribute
	{
		public ReadOnlyAttribute()
		{
		}
	}
}
