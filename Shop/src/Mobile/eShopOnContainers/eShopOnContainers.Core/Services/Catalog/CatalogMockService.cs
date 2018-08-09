using eShopOnContainers.Core.Extensions;
using eShopOnContainers.Core.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eShopOnContainers.Core.Services.Catalog
{
    public class CatalogMockService : ICatalogService
    {
        private ObservableCollection<CatalogBrand> MockCatalogBrand = new ObservableCollection<CatalogBrand>
        {
            new CatalogBrand { Id = 1, Brand = "Azure" },
            new CatalogBrand { Id = 2, Brand = "Visual Studio" }
        };

        private ObservableCollection<CatalogType> MockCatalogType = new ObservableCollection<CatalogType>
        {
            new CatalogType { Id = 1, Type = "Mug" },
            new CatalogType { Id = 2, Type = "T-Shirt" }
        };

        private ObservableCollection<CatalogItem> MockCatalog = new ObservableCollection<CatalogItem>
        {
            new CatalogItem { Id = Common.Common.MockCatalogItemId01, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_01.png" : "Assets/fake_product_01.png", Name = ".NET Bot Blue Sweatshirt (M)", Price = 19.50M, CatalogBrandId = 2, CatalogBrand = "Visual Studio", CatalogTypeId = 2, CatalogType = "T-Shirt" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId02, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_02.png" : "Assets/fake_product_02.png", Name = ".NET Bot Purple Sweatshirt (M)", Price = 19.50M, CatalogBrandId = 2, CatalogBrand = "Visual Studio", CatalogTypeId = 2, CatalogType = "T-Shirt" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId03, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_03.png" : "Assets/fake_product_03.png", Name = ".NET Bot Black Sweatshirt (M)", Price = 19.95M, CatalogBrandId = 2, CatalogBrand = "Visual Studio", CatalogTypeId = 2, CatalogType = "T-Shirt" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId04, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_04.png" : "Assets/fake_product_04.png", Name = ".NET Black Cupt", Price = 17.00M, CatalogBrandId = 2, CatalogBrand = "Visual Studio", CatalogTypeId = 1, CatalogType = "Mug" },
            new CatalogItem { Id = Common.Common.MockCatalogItemId05, PictureUri = Device.RuntimePlatform != Device.UWP ? "fake_product_05.png" : "Assets/fake_product_05.png", Name = "Azure Black Sweatshirt (M)", Price = 19.50M, CatalogBrandId = 1, CatalogBrand = "Azure", CatalogTypeId = 2, CatalogType = "T-Shirt" }
        };

        /// <summary>
        /// Store catalog
        /// </summary>
        private List<CatalogStore> MockCatalogStore = new List<CatalogStore>
        {
            new CatalogStore{ Identifier = "1", Name = "VS Store 1", TypeIdentifier = "1", Type = "Shirt Store", Latitude = 19.432606, Longitude = -99.133206, Icon = ""  },
            new CatalogStore{ Identifier = "2", Name = "VS Store 2", TypeIdentifier = "1", Type = "Shirt Store", Latitude = 19.429633, Longitude = -99.130537, Icon = ""  },
            new CatalogStore{ Identifier = "3", Name = "TFS Store 1", TypeIdentifier = "2", Type = "Mug Store", Latitude = 19.434135, Longitude = -99.135504, Icon = ""  },
            new CatalogStore{ Identifier = "4", Name = "TFS Store 2", TypeIdentifier = "2", Type = "Mug Store", Latitude = 19.435278, Longitude = -99.134989, Icon = ""  }
        };

        public async Task<ObservableCollection<CatalogItem>> GetCatalogAsync()
        {
            await Task.Delay(10);

            return MockCatalog;
        }

        public async Task<ObservableCollection<CatalogItem>> FilterAsync(int catalogBrandId, int catalogTypeId)
        {
            await Task.Delay(10);

            return MockCatalog
                .Where(c => c.CatalogBrandId == catalogBrandId &&
                c.CatalogTypeId == catalogTypeId)
                .ToObservableCollection();
        }

        public async Task<ObservableCollection<CatalogBrand>> GetCatalogBrandAsync()
        {
            await Task.Delay(10);

            return MockCatalogBrand;
        }

        public async Task<ObservableCollection<CatalogType>> GetCatalogTypeAsync()
        {
            await Task.Delay(10);

            return MockCatalogType;
        }

        /// <summary>
        /// Get stores catalog
        /// </summary>
        /// <returns>Awaitable</returns>
        public async Task<List<CatalogStore>> GetCatalogStoreAsync()
        {
            await Task.Delay(10);
            return MockCatalogStore;
        }
    }
}