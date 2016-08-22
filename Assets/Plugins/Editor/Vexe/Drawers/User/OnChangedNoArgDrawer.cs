using System.Linq;
using System.Reflection;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Helpers;
using Vexe.Runtime.Types;

namespace Vexe.Editor.Drawers
{
	public class OnChangedNoArgDrawer : CompositeDrawer<object, OnChangedNoArgAttribute>
	{
		private MethodCaller<object, object> _onChanged;
		private object _previousValue;
		private int _previousCollectionCount;

		protected override void Initialize()
		{
			string call = attribute.Call;

			if (!call.IsNullOrEmpty())
			{
				try
				{
					var methods = targetType.GetAllMembers(typeof(object)).OfType<MethodInfo>();
					_onChanged = (from method in methods
								  where method.Name == call
								  where method.ReturnType == typeof(void)
								  let args = method.GetParameters()
								  where args.Length == 0
								  select method).FirstOrDefault().DelegateForCall();
				}
				catch
				{
					ErrorHelper.MemberNotFound(targetType, call);
				}
			}

			_previousValue = memberValue;

			if (member.CollectionCount != -1)
				_previousCollectionCount = member.CollectionCount;
		}

		public override void OnLowerGUI()
		{
			var current = memberValue;

			bool changed;
			if (member.CollectionCount != -1 && member.CollectionCount != _previousCollectionCount)
			{
				_previousCollectionCount = member.CollectionCount;
				changed = true;
			}
			else
				changed = !current.GenericEquals(_previousValue);

			if (changed)
			{
				_previousValue = current;
				_onChanged.SafeInvoke(rawTarget);
			}
		}
	}
}
