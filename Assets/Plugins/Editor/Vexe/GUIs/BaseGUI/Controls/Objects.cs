using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Vexe.Editor.GUIs
{
	public abstract partial class BaseGUI
	{
		public UnityObject DraggableObjectField<T>(T value) where T : UnityObject
		{
			return DraggableObjectField(string.Empty, value);
		}

		public UnityObject DraggableObjectField<T>(string label, T value) where T : UnityObject
		{
			return DraggableObjectField(label, string.Empty, value);
		}

		public UnityObject DraggableObjectField<T>(string label, string tooltip, T value) where T : UnityEngine.Object
		{
			value = ObjectField(label, tooltip, value, true) as T;
			RegisterFieldForDrag(LastRect, value);
			return value;
		}

		public T ObjectField<T>(T value) where T : UnityObject
		{
			return ObjectField(string.Empty, value);
		}

		public T ObjectField<T>(string label, T value) where T : UnityObject
		{
			return ObjectField(label, string.Empty, value);
		}

		public T ObjectField<T>(string label, string tooltip, T value) where T : UnityObject
		{
			return ObjectField(label, tooltip, value, true);
		}

		public T ObjectField<T>(string label, T value, bool allowSceneObjects) where T : UnityObject
		{
			return ObjectField(label, string.Empty, value, allowSceneObjects);
		}

		public T ObjectField<T>(string label, string tooltip, T value, bool allowSceneObjects) where T : UnityObject
		{
			return ObjectField(GetContent(label, tooltip), value, typeof(T), allowSceneObjects, null) as T;
		}

		public UnityObject ObjectField(UnityObject value)
		{
			return ObjectField(string.Empty, value);
		}

		public UnityObject ObjectField(UnityObject value, Type type)
		{
			return ObjectField(string.Empty, value, type);
		}

		public UnityObject ObjectField(UnityObject value, Type type, bool allowSceneObjects)
		{
			return ObjectField(string.Empty, value, type, allowSceneObjects);
		}

		public UnityObject ObjectField(string label, UnityObject value)
		{
			return ObjectField(label, value, typeof(UnityObject));
		}

		public UnityObject ObjectField(string label, UnityObject value, Type type)
		{
			return ObjectField(label, value, type, null);
		}

		public UnityObject ObjectField(UnityObject value, Layout option)
		{
			return ObjectField(string.Empty, value, option);
		}

		public UnityObject ObjectField(UnityObject value, bool allowSceneObjects, Layout option)
		{
			return ObjectField(string.Empty, value, allowSceneObjects, option);
		}

		public UnityObject ObjectField(string label, UnityObject value, Layout option)
		{
			return ObjectField(label, value, typeof(UnityObject), option);
		}

		public UnityObject ObjectField(string label, UnityObject value, bool allowSceneObjects, Layout option)
		{
			return ObjectField(label, value, typeof(UnityObject), allowSceneObjects, option);
		}

		public UnityObject ObjectField(string label, UnityObject value, Type type, Layout option)
		{
			return ObjectField(label, value, type, true, option);
		}

		public UnityObject ObjectField(string label, UnityObject value, Type type, bool allowSceneObjects)
		{
			return ObjectField(label, value, type, allowSceneObjects, null);
		}

		public UnityObject ObjectField(string label, UnityObject value, Type type, bool allowSceneObjects, Layout option)
		{
			return ObjectField(label, string.Empty, value, type, allowSceneObjects, option);
		}

		public UnityObject ObjectField(string label, string tooltip, UnityObject value, Type type, Layout option)
		{
			return ObjectField(GetContent(label, tooltip), value, type, true, option);
		}

		public UnityObject ObjectField(string label, string tooltip, UnityObject value, Type type, bool allowSceneObjects, Layout option)
		{
			return ObjectField(GetContent(label, tooltip), value, type, allowSceneObjects, option);
		}

		public abstract UnityObject ObjectField(GUIContent content, UnityObject value, Type type, bool allowSceneObjects, Layout option);
	}
}
