using UnityEngine;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public double DoubleField(double value)
		{
			return DoubleField(string.Empty, value);
		}

		public double DoubleField(string label, double value)
		{
			return DoubleField(label, value, null);
		}

		public double DoubleField(string label, string tooltip, double value)
		{
			return DoubleField(label, tooltip, value, null);
		}

		public double DoubleField(string label, double value, Layout option)
		{
			return DoubleField(label, string.Empty, value, option);
		}

		public double DoubleField(string label, string tooltip, double value, Layout option)
		{
			return DoubleField(GetContent(label, tooltip), value, option);
		}

		public abstract double DoubleField(GUIContent content, double value, Layout option);
	}
}
