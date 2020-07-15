using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace DGPub.Application.Promotions.Handlers.Tests
{
    [TestClass()]
    public class FreeWaterWhenBrandyAndBeerPromotionTests
    {
        private readonly FreeWaterWhenBrandyAndBeerPromotion _handler;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IItemTabRepository> _mockItemTabRepository;

        private readonly Item beer;
        private readonly Item juice;
        private readonly Item brandy;
        private readonly Item water;

        public FreeWaterWhenBrandyAndBeerPromotionTests()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _mockItemTabRepository = new Mock<IItemTabRepository>();

            _handler = new FreeWaterWhenBrandyAndBeerPromotion(_mockItemRepository.Object, _mockItemTabRepository.Object);

            //repository
            beer = Item.ItemFactory.Create("cerveja", 5);
            _mockItemRepository.Setup(i => i.GetByName("cerveja"))
                .Returns(beer);

            juice = Item.ItemFactory.Create("suco", 50);
            _mockItemRepository.Setup(i => i.GetByName("suco"))
                .Returns(juice);

            brandy = Item.ItemFactory.Create("conhaque", 20);
            _mockItemRepository.Setup(i => i.GetByName("conhaque"))
                .Returns(brandy);

            water = Item.ItemFactory.Create("água", 70);
            _mockItemRepository.Setup(i => i.GetByName("água"))
                .Returns(water);
        }

        [TestMethod()]
        public async Task Promotion_HasFreeWater_DontAddWater()
        {
            //Arrange

            var tab = Tab.TabFactory.Create("ClearSale");
            var waterTab = ItemTab.ItemTabFactory.Create(tab.Id, water.Id, 0);
            tab.Items.Add(waterTab);

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }

        [TestMethod()]
        public async Task Promotion_HasLessThanThreeBrandy_DontAddWater()
        {
            //Arrange

            var tab = Tab.TabFactory.Create("ClearSale");
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }

        [TestMethod()]
        public async Task Promotion_HasLessThanTwoBeer_DontAddWater()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");
            var beerTab = ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 0);
            tab.Items.Add(beerTab);

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }

        [TestMethod()]
        public async Task Promotion_HasMinBeerMinBrandyDontRequestWater_DontAddWater()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 0));

            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value.Alert));
        }

        [TestMethod()]
        public async Task Promotion_HasMinBeerMinBrandyAndRequestWater_AddWater()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");            
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 0));
            
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, brandy.Id, 0));
            
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, water.Id, water.Price));

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsTrue(result.Value.Applied);
            _mockItemTabRepository.Verify(i => i.Update(It.Is<ItemTab>(a => a.Discount == water.Price)));
        }
    }
}
