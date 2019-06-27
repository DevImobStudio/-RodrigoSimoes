using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Erro : ContentPage
	{
		public Erro (string mensagem)
		{
			InitializeComponent ();
            Mensagem1.Text = mensagem;
		}
	}
}