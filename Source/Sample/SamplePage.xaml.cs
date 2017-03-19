using Xamarin.Forms;
using Gaugable.Forms.Plugin.Core;
namespace Sample
{
	public partial class SamplePage : ContentPage
	{
		public SamplePage()
		{
			this.BindingContext = new SampleViewModel();
			InitializeComponent();
		}
	}
}
