using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace eShopOnContainers.Core.Views
{	
    /// <summary>
    /// Mapview code behind
    /// </summary>
	public partial class MapView : ContentPage
	{
        /// <summary>
        /// Class constructor
        /// </summary>
		public MapView ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// On appearing override method
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //México city position
            var defaultPosition = new Position(19.432606, -99.133206);
            var defaultKmDistance = 2;

            StoreMap.MoveToRegion(MapSpan.FromCenterAndRadius(defaultPosition, 
                                                              Distance.FromKilometers(defaultKmDistance)));
        }
    }
}