using eShopOnContainers.Core.Models.Catalog;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System;

namespace eShopOnContainers.Core.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        private ObservableCollection<CatalogItem> _products;
        private ObservableCollection<CatalogBrand> _brands;
        private CatalogBrand _brand;
        private ObservableCollection<CatalogType> _types;
        private CatalogType _type;
        private ICatalogService _productsService;

        public CatalogViewModel(ICatalogService productsService)
        {
            _productsService = productsService;
        }

        public ObservableCollection<CatalogItem> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
        }

        public ObservableCollection<CatalogBrand> Brands
        {
            get { return _brands; }
            set
            {
                _brands = value;
                RaisePropertyChanged(() => Brands);
            }
        }

        public CatalogBrand Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                RaisePropertyChanged(() => Brand);
                RaisePropertyChanged(() => IsFilter);
            }
        }

        public ObservableCollection<CatalogType> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                RaisePropertyChanged(() => Types);
            }
        }

        public CatalogType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(() => Type);
                RaisePropertyChanged(() => IsFilter);
            }
        }

        public bool IsFilter { get { return Brand != null || Type != null; } }
        
        public ICommand FilterCommand => new Command(async () => await FilterAsync());

		public ICommand ClearFilterCommand => new Command(async () => await ClearFilterAsync());

        /// <summary>
        /// Show page to add products
        /// </summary>
        public ICommand AddProductCommand => new Command(async () => await ShowAddProductPageAsync());
        
        /// <summary>
        /// Show catalog item detail command
        /// </summary>
        public ICommand ShowCatalogItemDetailCommand => new Command<CatalogItem>(async (item) => await ShowCatalogItemDetailAsync(item));
        
        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            // Get Catalog, Brands and Types
            Products = await _productsService.GetCatalogAsync();
            Brands = await _productsService.GetCatalogBrandAsync();
            Types = await _productsService.GetCatalogTypeAsync();

            MessagingCenter.Unsubscribe<AddProductViewModel, CatalogItem>(this, MessageKeys.AddProductCatalog);
            MessagingCenter.Subscribe<AddProductViewModel, CatalogItem>(this, MessageKeys.AddProductCatalog, (sender, arg) => AddCatalogItem(arg));

            IsBusy = false;
        }

        /// <summary>
        /// Add item to the current catalog
        /// </summary>
        /// <param name="catalogItem">Item to add</param>
        private void AddCatalogItem(CatalogItem catalogItem)
        {
            catalogItem.Id = (Convert.ToInt32(Products.Max(product => product.Id)) + 1).ToString();
            Products.Add(catalogItem);
        }

        /// <summary>
        /// Show catalog item detail
        /// </summary>
        /// <param name="catalogItem">Catalgog item to show</param>
        /// <returns>Awaitable</returns>
        private async Task ShowCatalogItemDetailAsync(CatalogItem catalogItem)
        {
            await NavigationService.NavigateToAsync<CatalogDetailViewModel>(catalogItem);
        }

        /// <summary>
        /// Show the add product page
        /// </summary>
        /// <returns>Awaitable</returns>
        private async Task ShowAddProductPageAsync()
        {
            await NavigationService.NavigateToAsync<AddProductViewModel>();
        }

        private async Task FilterAsync()
        {
            if (Brand == null || Type == null)
            {
                return;
            }

            IsBusy = true;

            // Filter catalog products
            MessagingCenter.Send(this, MessageKeys.Filter);
            Products = await _productsService.FilterAsync(Brand.Id, Type.Id);

            IsBusy = false;
        }

        private async Task ClearFilterAsync()
        {
            IsBusy = true;

            Brand = null;
            Type = null;
            Products = await _productsService.GetCatalogAsync();

            IsBusy = false;
        }
    }
}