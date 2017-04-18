using System;
using System.Windows.Input;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace ArcTouchTMDb.Droid
{
	public class EditTextImeActionBinding : MvxAndroidTargetBinding
	{
		private bool subscribed;
		private ICommand _command;

		protected EditText EditText
		{
			get { return Target as EditText; }
		}

		public static void Register(IMvxTargetBindingFactoryRegistry registry)
		{
			registry.RegisterCustomBindingFactory<EditText>("ImeAction", view => new EditTextImeActionBinding(view));
		}

		public EditTextImeActionBinding(EditText view) : base(view)
		{
			if (view == null)
				Mvx.Trace(MvxTraceLevel.Error, "TextViewImeActionBinding : view is null");

			if (view != null)
			{
				EditText.EditorAction += ViewOnEditorAction;
				subscribed = true;
			}
		}

		private void ViewOnEditorAction(object sender, TextView.EditorActionEventArgs args)
		{
			FireValueChanged(args.ActionId);

			//args.ActionId
			args.Handled = true;
			if (_command == null)
				return;

			CloseKeyboard((EditText)sender);
			_command.Execute(((EditText)sender).Text);
		}

		private void CloseKeyboard(EditText editText)
		{
			InputMethodManager imm = (InputMethodManager)editText.Context.GetSystemService(Context.InputMethodService);
			imm.HideSoftInputFromWindow(editText.WindowToken, 0);
		}

		public override Type TargetType => typeof(ICommand);

		protected override void SetValueImpl(object target, object value)
		{
			_command = (ICommand)value;
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.TwoWay; }
		}

		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				var view = EditText;
				if (view != null && subscribed)
				{
					view.EditorAction -= ViewOnEditorAction;
				}
			}
			base.Dispose(isDisposing);
		}
	}
}
