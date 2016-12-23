//#define DBG

using System;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Vexe.Editor.Drawers
{
	public abstract class BasicDrawer<T> : ObjectDrawer<T>
	{
		protected virtual T DoField(string text, T value)
		{
			throw new NotImplementedException();
		}

		public override void OnGUI()
		{
			memberValue = DoField(displayText, memberValue);
#if DBG
			var curValue = memberValue;
			var newValue = DoField(niceName, curValue);
			memberValue = newValue;
			if(!newValue.Equals(curValue))
				Debug.Log(newValue);
#endif
		}
	}

	public class IntDrawer : BasicDrawer<int>
	{
		protected override int DoField(string text, int value)
		{
			return gui.IntField(text, value);
		}
	}

	public class UIntDrawer : BasicDrawer<uint>
	{
		protected override uint DoField(string text, uint value)
		{
			return (uint) Math.Max(0, gui.IntField(text, (int) value));
		}
	}

	public class LongDrawer : BasicDrawer<long>
	{
		protected override long DoField(string text, long value)
		{
			return gui.LongField(text, value);
		}
	}

	public class ULongDrawer : BasicDrawer<ulong>
	{
		protected override ulong DoField(string text, ulong value)
		{
			return gui.ULongField(text, value);
		}
	}

	public class DoubleDrawer : BasicDrawer<double>
	{
		protected override double DoField(string text, double value)
		{
			return gui.DoubleField(text, value);
		}
	}

	public class FloatDrawer : BasicDrawer<float>
	{
		protected override float DoField(string text, float value)
		{
			return gui.FloatField(text, value);
		}
	}

	public class ByteDrawer : BasicDrawer<byte>
	{
		protected override byte DoField(string text, byte value)
		{
			return (byte) gui.IntField(text, value);
		}
	}

	public class SByteDrawer : BasicDrawer<sbyte>
	{
		protected override sbyte DoField(string text, sbyte value)
		{
			return (sbyte) gui.IntField(text, value);
		}
	}

	public class ShortDrawer : BasicDrawer<short>
	{
		protected override short DoField(string text, short value)
		{
			return (short) gui.IntField(text, value);
		}
	}

	public class UShortDrawer : BasicDrawer<ushort>
	{
		protected override ushort DoField(string text, ushort value)
		{
			return (ushort) gui.IntField(text, value);
		}
	}

	public class StringDrawer : BasicDrawer<string>
	{
		protected override string DoField(string text, string value)
		{
			return gui.TextField(text, value);
		}
	}

	public class CharDrawer : BasicDrawer<char>
	{
		protected override char DoField(string text, char value)
		{
			return (char) gui.IntField(text, value);
		}
	}

	public class Vector2Drawer : BasicDrawer<Vector2>
	{
		protected override Vector2 DoField(string text, Vector2 value)
		{
			return gui.Vector2Field(text, value);
		}
	}

	public class Vector3Drawer : BasicDrawer<Vector3>
	{
		protected override Vector3 DoField(string text, Vector3 value)
		{
			return gui.Vector3Field(text, value);
		}
	}

	public class Vector4Drawer : BasicDrawer<Vector4>
	{
		protected override Vector4 DoField(string text, Vector4 value)
		{
			return gui.Vector4Field(text, value);
		}
	}

	public class Matrix4x4Drawer : BasicDrawer<Matrix4x4>
	{
		protected override Matrix4x4 DoField(string text, Matrix4x4 value)
		{
			return gui.Matrix4x4Field(text, value);
		}
	}

	public class BoolDrawer : BasicDrawer<bool>
	{
		protected override bool DoField(string text, bool value)
		{
			return gui.Toggle(text, value);
		}
	}

	public class ColorDrawer : BasicDrawer<Color>
	{
		protected override Color DoField(string text, Color value)
		{
			return gui.ColorField(text, value);
		}
	}

	public class Color32Drawer : BasicDrawer<Color32>
	{
		protected override Color32 DoField(string text, Color32 value)
		{
			return gui.ColorField(text, value);
		}
	}

	public class BoundsDrawer : BasicDrawer<Bounds>
	{
		protected override Bounds DoField(string text, Bounds value)
		{
			return gui.BoundsField(text, value);
		}
	}

	public class RectDrawer : BasicDrawer<Rect>
	{
		protected override Rect DoField(string text, Rect value)
		{
			return gui.RectField(text, value);
		}
	}

	public class QuaternionDrawer : BasicDrawer<Quaternion>
	{
		protected override Quaternion DoField(string text, Quaternion value)
		{
			return gui.QuaternionField(text, value);
		}
	}

	public class AnimationCurveDrawer : BasicDrawer<AnimationCurve>
	{
		protected override AnimationCurve DoField(string text, AnimationCurve value)
		{
			return gui.CurveField(text, value);
		}
	}

	public class GradientDrawer : BasicDrawer<Gradient>
	{
		protected override Gradient DoField(string text, Gradient value)
		{
			return gui.GradientField(text, value);
		}
	}

	public class UnityObjectDrawer : BasicDrawer<UnityObject>
	{
		public override void OnGUI()
		{
			memberValue = gui.ObjectField(displayText, memberValue, memberType, !AssetDatabase.Contains(unityTarget));
		}
	}

	public class LayerMaskDrawer : BasicDrawer<LayerMask>
	{
		protected override LayerMask DoField(string text, LayerMask value)
		{
			return gui.LayerMaskField(text, value);
		}
	}

	public class EnumDrawer : BasicDrawer<Enum>
	{
		protected override Enum DoField(string text, Enum value)
		{
			return gui.EnumPopup(text, value);
		}
	}
}
