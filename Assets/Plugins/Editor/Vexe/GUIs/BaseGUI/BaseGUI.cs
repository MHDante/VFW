using System;
using UnityEditor;
using UnityEngine;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public static readonly float kHeight = EditorGUIUtility.singleLineHeight;
		public static readonly float kMiniHeight = kHeight - 3f;
		public static readonly float kVSpacing = 1f;
		public static readonly float kHSpacing = 3.5f;
		public static readonly Rect kDummyRect = new Rect(0, 0, 1, 1);

		public static readonly Layout kMultifieldOption = Layout.sHeight(kHeight * 3f);
		public static readonly Layout kFoldoutOption = Layout.sWidth(kFoldoutWidth);
		public static readonly Layout kDefaultMiniOption = Layout.sWidth(kDefaultMiniWidth).Height(kDefaultMiniHeight);
		public static readonly Layout kNumericLabel = new Layout { width = kNumericLabelWidth };

		public const int kMinScreenWidth = 352;
		public const float kIndentAmount = 7.5f;
		public const float kNumericLabelWidth = 20f;
		public const float kDefaultMiniWidth = 20f;
		public const float kDefaultMiniHeight = 16f;
		public const float kFoldoutWidth = 10f;

		public const MiniButtonStyle kDefaultMiniStyle = MiniButtonStyle.Mid;
		public const MiniButtonStyle kDefaultModStyle = MiniButtonStyle.ModMid;

		public const string kNoneText = "None";

		private IndentBlock indentBlock;
		private StateBlock stateBlock;
		private GUIColorBlock colorBlock;
		private ContentColorBlock contentColorBlock;
		private LabelWidthBlock labelWidthBlock;

		protected EditorRecord prefs;
		protected bool hasReachedMinScreenWidth;

		public Action OnBeginLayout;

		public abstract Rect LastRect { get; }

		public ScrollViewBlock ScrollView { get; private set; }

		public BaseGUI()
		{
			stateBlock = new StateBlock();
			colorBlock = new GUIColorBlock();
			labelWidthBlock = new LabelWidthBlock();
			contentColorBlock = new ContentColorBlock();
			indentBlock = new IndentBlock(this);
			ScrollView = new ScrollViewBlock(this);
		}

		public void RequestResetIfRabbit()
		{
			var rabbit = this as RabbitGUI;
			if (rabbit != null)
				rabbit.RequestReset();
		}

		public VerticalBlock Vertical()
		{
			return Vertical(GUIStyle.none);
		}

		public VerticalBlock Vertical(GUIStyle style)
		{
			return BeginVertical(style);
		}

		public HorizontalBlock Horizontal()
		{
			return Horizontal(GUIStyle.none);
		}

		public HorizontalBlock Horizontal(GUIStyle style)
		{
			return BeginHorizontal(style);
		}

		protected abstract HorizontalBlock BeginHorizontal(GUIStyle style);

		protected abstract VerticalBlock BeginVertical(GUIStyle style);

		protected abstract void EndHorizontal();

		protected abstract void EndVertical();

		public abstract void OnGUI(Action guiCode, Vector4 padding, int targetId);

		public virtual void OnEnable()
		{
		}

		public virtual void OnDisable()
		{
		}

		public abstract IDisposable If(bool condition, IDisposable body);

		public void BeginCheck()
		{
			EditorGUI.BeginChangeCheck();
		}

		public bool HasChanged()
		{
			return EditorGUI.EndChangeCheck();
		}

		public LabelWidthBlock LabelWidth(float newWidth)
		{
			return labelWidthBlock.Begin(newWidth);
		}

		public IndentBlock Indent()
		{
			return Indent(GUIStyle.none);
		}

		public IndentBlock Indent(float amount)
		{
			return Indent(GUIStyle.none, amount);
		}

		public IndentBlock Indent(GUIStyle style, bool doIndent)
		{
			return Indent(style, doIndent ? kIndentAmount : 0f);
		}

		public IndentBlock Indent(GUIStyle style)
		{
			return Indent(style, kIndentAmount);
		}

		public IndentBlock Indent(GUIStyle style, float amount)
		{
			return indentBlock.Begin(style, amount);
		}

		public StateBlock State(bool newState)
		{
			return stateBlock.Begin(newState);
		}

		public bool BeginToggleGroup(string text, bool value)
		{
			return BeginToggleGroup(text, value, kIndentAmount);
		}

		public bool BeginToggleGroup(string text, bool value, float indentAmount)
		{
			value = ToggleLeft(text, value);
			State(value);
			Indent(indentAmount);
			return value;
		}

		public void EndToggleGroup()
		{
			indentBlock.Dispose();
			stateBlock.Dispose();
		}

		public GUIColorBlock ColorBlock(Color newColor)
		{
			return colorBlock.Begin(newColor);
		}

		public GUIColorBlock ColorBlock(Color? newColor)
		{
			return newColor.HasValue ? ColorBlock(newColor.Value) : null;
		}

		public ContentColorBlock ContentColor(Color color)
		{
			return contentColorBlock.Begin(color);
		}

		public ContentColorBlock ContentColor(Color? color)
		{
			return color.HasValue ? ContentColor(color.Value) : null;
		}

		public void Cursor(Rect rect, MouseCursor mouse)
		{
			EditorGUIUtility.AddCursorRect(rect, mouse);
		}

		public void LastCursor(MouseCursor mouse)
		{
			Cursor(LastRect, mouse);
		}

		public static BaseGUI Create(Type guiType)
		{
			return guiType.Instance<BaseGUI>();
		}

		public static GUIContent GetContent(string text)
		{
			return GetContent(text, string.Empty);
		}

		public static float GetHeight(ControlType control)
		{
			switch (control)
			{
				case ControlType.MiniButton: return kMiniHeight;
				default: return kHeight;
			}
		}

		public static float GetHSpacing(ControlType control)
		{
			switch (control)
			{
				case ControlType.MiniButton: return 0f;
				default: return kVSpacing;
			}
		}

		public static float GetVSpacing(ControlType controlType)
		{
			return kVSpacing;
		}

		public static GUIContent GetContent(string text, string tooltip)
		{
			// TODO: Pool
			return new GUIContent(text, tooltip);
		}

		public void assert(bool expression, string msg)
		{
			if (!expression)
				throw new Exception(string.Format("Assertion `{0}` failed", msg));
		}

		public static sbyte SByteField(GUIContent content, ref sbyte value, Layout option)
		{
			int intValue = value;
			intValue = EditorGUILayout.IntField(content, intValue, option);

			if (intValue < sbyte.MinValue)
				return sbyte.MinValue;

			if (intValue > sbyte.MaxValue)
				return sbyte.MaxValue;

			unchecked
			{
				return (sbyte) intValue;
			}
		}

		public static byte ByteField(GUIContent content, ref byte value, Layout option)
		{
			int intValue = value;
			intValue = EditorGUILayout.IntField(content, intValue, option);

			if (intValue < byte.MinValue)
				return byte.MinValue;

			if (intValue > byte.MaxValue)
				return byte.MaxValue;

			unchecked
			{
				return (byte) intValue;
			}
		}

		public static short ShortField(GUIContent content, ref short value, Layout option)
		{
			int intValue = value;
			intValue = EditorGUILayout.IntField(content, intValue, option);

			if (intValue < short.MinValue)
				return short.MinValue;

			if (intValue > short.MaxValue)
				return short.MaxValue;

			unchecked
			{
				return (short) intValue;
			}
		}

		public static ushort UShortField(GUIContent content, ref ushort value, Layout option)
		{
			int intValue = value;
			intValue = EditorGUILayout.IntField(content, intValue, option);

			if (intValue < ushort.MinValue)
				return ushort.MinValue;

			if (intValue > ushort.MaxValue)
				return ushort.MaxValue;

			unchecked
			{
				return (ushort) intValue;
			}
		}

		public static uint UIntField(GUIContent content, ref uint value, Layout option)
		{
			long longValue = value;
			longValue = EditorGUILayout.LongField(content, longValue, option);

			if (longValue < uint.MinValue)
				return uint.MinValue;

			if (longValue > uint.MaxValue)
				return uint.MaxValue;

			unchecked
			{
				return (uint) longValue;
			}
		}

		public static ulong ULongField(GUIContent content, ref ulong value, Layout option)
		{
			var ulongStr = value.ToString();
			decimal ulongValue;

			try
			{
				if (!decimal.TryParse(EditorGUILayout.DelayedTextField(content, ulongStr), out ulongValue))
				{
					ulongValue = value;
				}
			}
			catch
			{
				ulongValue = value;
			}

			if (ulongValue < ulong.MinValue)
				return ulong.MinValue;

			if (ulongValue > ulong.MaxValue)
				return ulong.MaxValue;

			unchecked
			{
				return (ulong) ulongValue;
			}
		}

		public static char CharField(GUIContent content, ref char value, Layout option)
		{
			string stringValue = value.ToString();
			stringValue = EditorGUILayout.TextField(content, stringValue, option);

			if (stringValue.Length < 1)
				return char.MinValue;

			return stringValue[0];
		}

		public struct ControlData
		{
			public GUIContent content;
			public GUIStyle style;
			public Layout option;
			public ControlType type;

			public ControlData(GUIContent content, GUIStyle style, Layout option, ControlType type)
			{
				this.content = content;
				this.style = style;
				this.option = option;
				this.type = type;
			}
		}

		public enum ControlType
		{
			Button,
			Label,
			PrefixLabel,
			TextField,
			ObjectField,
			IntField,
			FloatField,
			Popup,
			FlexibleSpace,
			HorizontalBlock,
			VerticalBlock,
			MiniButton,
			Vector3Field,
			Toggle,
			Space,
			MaskField,
			Slider,
			Vector2Field,
			HelpBox,
			Foldout,
			Box,
			EnumPopup,
			RectField,
			BoundsField,
			TextArea,
			ColorField,
			CurveField,
			GradientField,
			TextFieldDropDown,
			DoubleField,
			LongField
		}
	}
}
