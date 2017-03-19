using System;
using Xamarin.Forms;
namespace Sample
{
	public class SampleViewModel : BindableObject
	{
		public SampleViewModel()
		{
		}

		public static readonly BindableProperty ProgressProperty =
		  BindableProperty.Create(propertyName: nameof(Progress),
			  returnType: typeof(int),
			  declaringType: typeof(SampleViewModel),
			  defaultValue: 0);

		public int Progress
		{
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}
	}
}
