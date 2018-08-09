using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;

namespace eShopOnContainers.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyD3fE8bfSIfqBAS3haIrBq6uT_jdI6unm8");

            LoadApplication(new App());

            SlideOverKit.iOS.SlideOverKit.Init();
            CachedImageRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
