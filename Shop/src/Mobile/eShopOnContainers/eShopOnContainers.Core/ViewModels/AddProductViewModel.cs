using eShopOnContainers.Core.Extensions;
using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace eShopOnContainers.Core.ViewModels
{
    /// <summary>
    /// Add product page view model
    /// </summary>
    public class AddProductViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Catalog service
        /// </summary>
        private ICatalogService catalogService;

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
        /// Selected brand
        /// </summary>
        private CatalogBrand selectedBrand;

        /// <summary>
        /// Selected brand
        /// </summary>
        public CatalogBrand SelectedBrand
        {
            get => selectedBrand;
            set
            {
                selectedBrand = value;
                RaisePropertyChanged(() => SelectedBrand);
            }
        }

        /// <summary>
        /// Selected type
        /// </summary>
        private CatalogType selectedType;

        /// <summary>
        /// Selected type
        /// </summary>
        public CatalogType SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                RaisePropertyChanged(() => SelectedType);
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
        public ICommand AddToCatalogCommand => new Command(async () => await AddItemToCatalogAsync());

        /// <summary>
        /// Brand list
        /// </summary>
        public IList<CatalogBrand> BrandList { get; private set; }

        /// <summary>
        /// Type product list
        /// </summary>
        public IList<CatalogType> TypeList { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        public AddProductViewModel(ICatalogService catalogService)
        {
            this.catalogService = catalogService;

            BrandList = new ObservableCollection<CatalogBrand>();
            TypeList = new ObservableCollection<CatalogType>();            
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
            PictureUri = "default_product.png";

            BrandList.ClearAndAddRange(await catalogService.GetCatalogBrandAsync());
            TypeList.ClearAndAddRange(await catalogService.GetCatalogTypeAsync());
           
            SelectedBrand = BrandList.FirstOrDefault();
            SelectedType = TypeList.FirstOrDefault();
        }

        /// <summary>
        /// Add item to main catalog
        /// </summary>
        /// <returns>Awaitable</returns>
        private async Task  AddItemToCatalogAsync()
        {
            if(!string.IsNullOrWhiteSpace(ProductName) 
                && !string.IsNullOrWhiteSpace(ProductDescription))
            {
                await DialogService.ShowAlertAsync("Product added",
                                               "Add product",
                                               "Ok");

                MessagingCenter.Send(this,
                                     MessageKeys.AddProductCatalog,
                                     new CatalogItem
                                     {
                                         PictureUri = PictureUri,
                                         Name = ProductName,
                                         Description = ProductDescription,
                                         Price = ProductPrice,
                                         CatalogBrandId = SelectedBrand.Id,
                                         CatalogBrand = SelectedBrand.Brand,
                                         CatalogTypeId = SelectedType.Id,
                                         CatalogType = SelectedType.Type
                                     });

                await NavigationService.RemoveCurrentAsync();
            }                               
        }

        #endregion Methods
    }
}
