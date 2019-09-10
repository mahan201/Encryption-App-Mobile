using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SyncfusionXamarinApp1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewFile : ContentPage
	{
		public ViewFile (string FilePath, string FileCont)
		{
			InitializeComponent ();
            PathLabel.Text = FilePath;
            ContLabel.Text = FileCont;
		}
	}
}