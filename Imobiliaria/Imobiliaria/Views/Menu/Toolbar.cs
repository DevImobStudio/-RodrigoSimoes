using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Imobiliaria.Views.Menu
{
    public partial class Toolbar : ContentPage
    {
        public Toolbar(){
            ToolbarItems.Add(
                new ToolbarItem {
                    Icon="home.png"
                }
            );
        }
        
    }
}
