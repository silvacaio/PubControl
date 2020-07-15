using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace DGPub.Application.Promotions.Handlers.Tests
{
    [TestClass()]
    public class LimitJuicePromotionTests
    {
        private readonly LimitJuicePromotion _handler;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IItemTabRepository> _mockItemTabRepository;
        
        private readonly Item juice;

        public LimitJuicePromotionTests()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _mockItemTabRepository = new Mock<IItemTabRepository>();

            _handler = new LimitJuicePromotion(_mockItemRepository.Object, _mockItemTabRepository.Object);

            //repository   
            juice = Item.ItemFactory.Create("suco", 50);
            _mockItemRepository.Setup(i => i.GetByName("suco"))
                .Returns(juice);        
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

        [TestMethod()]
        public async Task Promotion_WithoutJuice_DontRemoveJuice()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");                        

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }

        [TestMethod()]
        public async Task Promotion_LessThanFourJuice_DontRemoveJuice()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");            
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.IsFalse(result.Value.Applied);
        }

        [TestMethod()]
        public async Task Promotion_FiveJuice_RemoveOne()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.AreEqual(tab.Items.Count, 3);
            Assert.IsTrue(result.Value.Applied);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value.Alert));
            _mockItemTabRepository.Verify(a => a.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [TestMethod()]
        public async Task Promotion_ManyJuice_RemoveAtThree()
        {
            //Arrange
            var tab = Tab.TabFactory.Create("ClearSale");
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, juice.Price));

            var itemsRemoved = tab.Items.Count - 3;

            //Act
            var result = await _handler.Handler(new Domain.Promotions.Commands.PromotionRunCommand(tab));

            //Assert
            Assert.AreEqual(tab.Items.Count, 3);
            Assert.IsTrue(result.Value.Applied);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Value.Alert));
            _mockItemTabRepository.Verify(a => a.Delete(It.IsAny<Guid>()), Times.AtLeast(itemsRemoved));
        }
    }
}