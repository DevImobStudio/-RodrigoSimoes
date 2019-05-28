using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Imobiliaria.Views.Menu
{
    public class Menu : MenuContainerPage
    {
        public Menu()
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Spacing = 10,
                Children = {
                    new Button{
                        Text ="Exibir Menu",
                        Command = new Command(()=>{
                            this.ShowMenu();
                        })
                    },
                    new Button{
                        Text ="Esconder Menu",
                        Command = new Command(()=>{
                            this.HideMenu();
                        })
                    },
                }
            };

      //      this.SlideMenu = new MenuPesquisa(this);
        }

    
    }
}
