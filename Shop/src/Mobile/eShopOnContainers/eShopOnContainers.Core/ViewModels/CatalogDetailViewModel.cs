using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    /// <summary>
    /// Catalog detail page view model
    /// </summary>
    public class CatalogDetailViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Current catalog item
        /// </summary>
        private CatalogItem catalogItem;

        /// <summary>
        /// Picture Uri
        /// </summary>
        private string pictureUri;

        /// <summary>
        /// Picture Uri
        /// </summary>
        public string PictureUri
        {
            get => pictureUri;
            set
            {
                pictureUri = value;
                RaisePropertyChanged(() => PictureUri);
            }
        }

        /// <summary>
        /// Product name
        /// </summary>
        private string productName;

        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                RaisePropertyChanged(() => ProductName);
            }
        }

        /// <summary>
        /// Product description
        /// </summary>
        private string productDescription;

        /// <summary>
        /// Product description
        /// </summary>
        public string ProductDescription
        {
            get => productDescription;

            set
            {
                productDescription = value;
                RaisePropertyChanged(() => ProductDescription);
            }
        }

        /// <summary>
        /// Product brand
        /// </summary>
        private string productBrand;

        /// <summary>
        /// Product brand
        /// </summary>
        public string ProductBrand
        {
            get => productBrand;
            set
            {
                productBrand = value;
                RaisePropertyChanged(() => ProductBrand);
            }
        }

        /// <summary>
        /// Product type
        /// </summary>
        private string productType;

        /// <summary>
        /// Product type
        /// </summary>
        public string ProductType
        {
            get => productType;
            set
            {
                productType = value;
                RaisePropertyChanged(() => ProductType);
            }
        }

        /// <summary>
        /// Product price
        /// </summary>
        private decimal productPrice;

        /// <summary>
        /// Product price
        /// </summary>
        public decimal ProductPrice
        {
            get => productPrice;
            set
            {
                productPrice = value;
                RaisePropertyChanged(() => ProductPrice);
            }
        }

        /// <summary>
        /// Add catalog item to car command
        /// </summary>
        public ICommand AddToCarCommand => new Command(async () => await AddToCarCatalogItemToCarAsync());

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize view model
        /// </summary>
        /// <param name="navigationData">Navigation data</param>
        /// <returns>Awaitable</returns>
        public override async Task InitializeAsync(object navigationData)
        {
            if(navigationData is CatalogItem catalogItem)
            {
                this.catalogItem = catalogItem;
                InitializeData(this.catalogItem);                
            }              
        }

        /// <summary>
        /// Initialize view model data
        /// </summary>
        /// <param name="catalogItem">Catalog data</param>
        private void InitializeData(CatalogItem catalogItem)
        {
            PictureUri = catalogItem.PictureUri;
            ProductName = catalogItem.Name;
            ProductDescription = catalogItem.Description;
            ProductBrand = catalogItem.CatalogBrand;
            ProductType = catalogItem.CatalogType;
            ProductPrice = catalogItem.Price;
        }

        /// <summary>
        /// Add item to the current car
        /// </summary>
        /// <returns>Awaitable</returns>
        private async Task AddToCarCatalogItemToCarAsync()
        {
            await DialogService.ShowAlertAsync("Product added to the car",
                                               "Add product",
                                               "Ok");

            MessagingCenter.Send(this, MessageKeys.AddProduct, catalogItem);
            await NavigationService.RemoveCurrentAsync();
        }

        #endregion Methods
    }
}
