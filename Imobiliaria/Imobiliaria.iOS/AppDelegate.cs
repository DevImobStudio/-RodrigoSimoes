using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using SegmentedControl.FormsPlugin.iOS;
using UIKit;

namespace Imobiliaria.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            Plugin.InputKit.Platforms.iOS.Config.Init();
            SegmentedControlRenderer.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyCgA1ifQK-1Kn4EsTTyU8hGrKn1jQM9bus");
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                                  .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                                  .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            //SlideOverKit.iOS.SlideOverKit.Init();
            CarouselViewRenderer.Init();
            new FreshEssentials.iOS.AdvancedFrameRendereriOS();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
