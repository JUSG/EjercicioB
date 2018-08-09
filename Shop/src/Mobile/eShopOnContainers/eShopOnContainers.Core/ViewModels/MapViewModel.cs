using eShopOnContainers.Core.Controls;
using eShopOnContainers.Core.Extensions;
using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace eShopOnContainers.Core.ViewModels
{
    /// <summary>
    /// MapView view model
    /// </summary>
    public class MapViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Store list
        /// </summary>
        private IList<CatalogStore> storeList;

        /// <summary>
        /// Catalog service
        /// </summary>
        private ICatalogService catalogService;        

        /// <summary>
        /// Stores pin list
        /// </summary>
        public ObservableCollection<Pin> StorePinList { get; set; }

        /// <summary>
        /// Selected store type
        /// </summary>
        private CatalogStore selectedStoreType;

        /// <summary>
        /// Selected store type
        /// </summary>
        public CatalogStore SelectedStoreType
        {
            get => selectedStoreType;
            set
            {
                selectedStoreType = value;
                RaisePropertyChanged(() => SelectedStoreType);
                ShowSelectedPinType();
            }
        }

        /// <summary>
        /// Store type list
        /// </summary>
        public IList<CatalogStore> StoreTypeList { get; private set; }

        /// <summary>
        /// Request to move to a region
        /// </summary>
        public MoveToRegionRequest MoveToRegionRequest { get => new MoveToRegionRequest(); }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="catalogService">Catalog service</param>
        public MapViewModel(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
            storeList = new List<CatalogStore>();
            StorePinList = new ObservableCollection<Pin>();
            StoreTypeList = new ObservableCollection<CatalogStore>();           
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initialize view model
        /// </summary>
        /// <param name="navigationData">Navigation data</param>
        /// <returns>Awaitable</returns>
        public override async Task InitializeAsync(object navigationData)
        {
            var storeDataList = await catalogService.GetCatalogStoreAsync();            

            SetStoreTypeList(storeDataList);
            SetPinList(storeDataList);            

            storeList.ClearAndAddRange(storeDataList);
        }

        /// <summary>
        /// Set the store type data list
        /// </summary>
        /// <param name="storeDataList">Stores list</param>
        private void SetStoreTypeList(List<CatalogStore> storeDataList)
        {
            StoreTypeList.Clear();
            StoreTypeList.Add(new CatalogStore { TypeIdentifier = "-1", Type = "All" });

            foreach (var storeData in storeDataList)
            {
                if(!StoreTypeList.Any(store => store.TypeIdentifier == storeData.TypeIdentifier))
                {
                    StoreTypeList.Add(storeData);
                }
            }
            
            SelectedStoreType = StoreTypeList.FirstOrDefault();           
        }

        /// <summary>
        /// Set pin on map from store data
        /// </summary>
        /// <param name="storeDataList">Pins store data list</param>
        private void SetPinList(List<CatalogStore> storeDataList)
        {
            StorePinList.Clear();

            foreach(var store in storeDataList)
            {
                StorePinList.Add(new Pin
                {
                    Tag = store.TypeIdentifier,
                    Type = PinType.Place,
                    Position = new Position(store.Latitude, store.Longitude),
                    Label = store.Name,
                    IsVisible = false,
                    Icon = BitmapDescriptorFactory.FromView(new PinControl())
                });
            }
        }

        /// <summary>
        /// Show pins from selected type
        /// </summary>
        private void ShowSelectedPinType()
        {
            foreach(var pin in StorePinList)
            {
                if (SelectedStoreType != null)
                {
                    if (selectedStoreType.TypeIdentifier == "-1")
                    {
                        pin.IsVisible = true;
                    }
                    else
                    {
                        pin.IsVisible = pin.Tag.ToString() == SelectedStoreType.TypeIdentifier;
                    }
                }
            }            
        }       

        #endregion Methods
    }
}
