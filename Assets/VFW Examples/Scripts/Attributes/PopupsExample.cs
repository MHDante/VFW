using System;
using Vexe.Runtime.Types;

namespace VFWExamples
{
	public class PopupsExample : BaseBehaviour
	{
		[StringPopup("String1", "String2", "String3")]
		public string strings;

		// populate from the property 'Factors' - which returns a string[]
		// (access modifier on the method doesn't matter)
		// also use a filter to quickly select values
		// we also tell it to use a text field so we can input values that are not
		// in the popup should we want that
		[StringPopup("Factors", TextField = true, Filter = true)]
		public string factor;

		// PerItem indicates that the attributes are applied per element, and not on the array
		// in this case, Tags and OnChanged will be applied on each element
		// if the value of any element changes, LogFactor is calling with the new value
		[PerItem, Tags, OnChanged("Log")]
		public string[] enemyTags;

		[Tags]
		public string playerTag;

		// get values from static member from the type 'ItemsDB'
		[PerItem, StringPopup("ItemsDB.getItemNames", CaseSensitive = false)]
		public string[] items;

		[IntPopup(int.MinValue, 0, int.MaxValue)]
		public int ints;

		[UIntPopup(uint.MinValue, 1u, uint.MaxValue)]
		public uint uints;

		[FloatPopup(float.MinValue, 0, float.MaxValue)]
		public float floats;

		[DoublePopup(double.MinValue, 0, double.MaxValue)]
		public double doubles;

		[BytePopup(byte.MinValue, 1, byte.MaxValue)]
		public byte bytes;

		[SBytePopup(sbyte.MinValue, 0, sbyte.MaxValue)]
		public sbyte sbytes;

		[LongPopup(long.MinValue, 0, long.MaxValue)]
		public long longs;

		[CharPopup('A', 'B', 'C')]
		public char chars;

		public SomeStruct someObj;

		private string[] Factors
		{
			get { return new[] { "Ork", "Human", "Undead", "Elves" }; }
		}
	}

	public static class ItemsDB // doesn't have to be static
	{
		public static string[] GetItemNames()
		{
			// code that loads items from disk for ex
			// ...
			return new string[] { "Handgun", "LodgeF2Key", "ManholeOpener", "etc" };
		}
	}

	[Serializable]
	public struct SomeStruct
	{
		[StringPopup("target.Factors")] // get the popup values from the UnityEngine.Object target (in this case, PopupsExample script)
		public string factor;
	}
}
