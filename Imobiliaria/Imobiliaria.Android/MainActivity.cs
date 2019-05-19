using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using CarouselView.FormsPlugin.Android;


namespace Imobiliaria.Droid
{
    [Activity(Label = "Imobiliaria", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                                  .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                                  .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            base.OnCreate(savedInstanceState);

            CarouselViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            if ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
                 || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted)
                 || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessLocationExtraCommands) != Permission.Granted)
                 || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessMockLocation) != Permission.Granted)
                 || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessWifiState) != Permission.Granted)
                 || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessNetworkState) != Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] {
                    Manifest.Permission.AccessCoarseLocation ,
                    Manifest.Permission.AccessFineLocation,
                    Manifest.Permission.AccessLocationExtraCommands,
                    Manifest.Permission.AccessMockLocation,
                    Manifest.Permission.AccessWifiState,
                    Manifest.Permission.AccessNetworkState
                }, 0);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Permission Granted!!!");
            }
        

            LoadApplication(new App());
        }
    }

}