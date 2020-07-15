using DGPub.Application.Promotions.Handlers;
using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace DGPub.Application.Tests.Promotions.Handlers
{
    [TestClass]
    public class BeerWithJuicePromotionTests
    {
        private readonly BeerWithJuicePromotion _handler;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IItemTabRepository> _mockItemTabRepository;

        private readonly Item beer;
        private readonly Item juice;

        public BeerWithJuicePromotionTests()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _mockItemTabRepository = new Mock<IItemTabRepository>();
            _handler = new BeerWithJuicePromotion(_mockItemRepository.Object, _mockItemTabRepository.Object);

            //repository
            beer = Item.ItemFactory.Create("cerveja", 5);
            _mockItemRepository.Setup(i => i.GetByName("cerveja"))
                .Returns(beer);

            juice = Item.ItemFactory.Create("suco", 5);
            _mockItemRepository.Setup(i => i.GetByName("suco"))
                .Returns(juice);
        }

        [TestMethod]
        public void Promotion_WithBeerAndJuice_ChangePrice()
        {
            //Arrange      

            var tab = Tab.TabFactory.Create("ClearSale");

            var beerTab = ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, beer.Price);
            tab.Items.Add(beerTab);
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));

            //Act
            _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert            
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Once);
        }

        [TestMethod]
        public void Promotion_OnlyBeer_NotChangePrice()
        {
            //Arrange
            var beer = Item.ItemFactory.Create("cerveja", 5);
            _mockItemRepository.Setup(i => i.GetByName("cerveja"))
                .Returns(beer);

            var tab = Tab.TabFactory.Create("ClearSale");

            var beerTab = ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, beer.Price);
            tab.Items.Add(beerTab);

            //Act
            _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }

        [TestMethod]
        public void Promotion_OnlyJuice_NotChangePrice()
        {
            //Arrange 
            var tab = Tab.TabFactory.Create("ClearSale");

            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));

            //Act
            _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }

        [TestMethod]
        public void Promotion_TabEmpty()
        {
            //Arrange 
            var tab = Tab.TabFactory.Create("ClearSale");

            //Act
            _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }

        [TestMethod()]
        public async Task Promotion_CommandInvalid_DontRun()
        {
            //Arrange                     

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(null));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }
    }
}
