using DGPub.Domain.Core;
using DGPub.Domain.Helpers.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Promotions.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace DGPub.Application.Tabs.Handlers.Tests
{
    [TestClass()]
    public class TabHandlerTests
    {
        private readonly Mock<ITabRepository> _mockTabRepository;
        private readonly Mock<IPromotionHandler> _mockPromotionHandler;
        private readonly Mock<IItemCache> _mockItemCache;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<IItemTabRepository> _itemTabRepository;

        private readonly TabHandler _handler;

        public TabHandlerTests()
        {
            _mockTabRepository = new Mock<ITabRepository>();
            _mockPromotionHandler = new Mock<IPromotionHandler>();
            _mockItemCache = new Mock<IItemCache>();
            _mockUow = new Mock<IUnitOfWork>();
            _itemRepository = new Mock<IItemRepository>();
            _itemTabRepository = new Mock<IItemTabRepository>();

            _handler = new TabHandler(
                _mockTabRepository.Object,
                _mockUow.Object,
                _itemRepository.Object,
                _itemTabRepository.Object,
                _mockPromotionHandler.Object,
                _mockItemCache.Object);


            _mockUow.Setup(s => s.Commit()).Returns(true);
        }

        [TestMethod()]
        public async Task CreateTab_CommandInvalid_Invalid()
        {
            //Arrange                     

            //Act
            var result = await _handler.Handler(new CreateTabCommand(null));

            //Assert
            Assert.IsFalse(result.Valid);
        }

        [TestMethod()]
        public async Task CreateTab_Valid()
        {
            //Arrange                     
            var command = new CreateTabCommand("ClearSale");
            //Act
            var result = await _handler.Handler(command);

            //Assert
            Assert.IsTrue(result.Valid);
            Assert.AreEqual(command.CustomerName, result.Value.CustomerName);
            _mockTabRepository.Verify(t => t.Add(It.Is<Tab>(tab => tab.CustomerName == command.CustomerName)));
        }

        [TestMethod()]
        public async Task ResetTab_CommandInvalid_Invalid()
        {
            //Arrange                     

            //Act
            var result = await _handler.Handler(new ResetTabCommand(Guid.Empty));

            //Assert
            Assert.IsFalse(result.Valid);
        }

        [TestMethod()]
        public async Task ResetTab_Valid()
        {
            //Arrange                     

            var name = "ClearSale";
            var tab = Tab.TabFactory.Create(name);
            _mockTabRepository.Setup(s => s.FindByIdWithItems(tab.Id)).Returns(tab);

            //Act
            var result = await _handler.Handler(new ResetTabCommand(tab.Id));

            //Assert
            Assert.IsTrue(result.Valid);
            Assert.AreEqual(tab.Id, result.Value.Id);
            _mockTabRepository.Verify(s => s.RemoveAllItem(tab.Id));
        }

        [TestMethod()]
        public async Task CloseTab_CommandInvalid_Invalid()
        {
            //Arrange                     

            //Act
            var result = await _handler.Handler(new CloseTabCommand(Guid.Empty));

            //Assert
            Assert.IsFalse(result.Valid);
        }

        [TestMethod()]
        public async Task CloseTab_WithoutItem_Close()
        {
            //Arrange                    

            var name = "ClearSale";
            var tab = Tab.TabFactory.Create(name);
            _mockTabRepository.Setup(s => s.FindByIdWithItems(tab.Id)).Returns(tab);

            //Act
            var result = await _handler.Handler(new CloseTabCommand(tab.Id));

            //Assert
            Assert.IsTrue(result.Valid);
            Assert.IsFalse(tab.Open);
            Assert.AreEqual(result.Value.TabId, tab.Id);
            _mockTabRepository.Verify(v => v.Update(It.Is<Tab>(t => t.Id == tab.Id)));
        }

        [TestMethod()]
        public async Task CloseTab_WithItem_Close()
        {
            //Arrange                                 
            var name = "ClearSale";
            var tab = Tab.TabFactory.Create(name);
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, Guid.NewGuid(), 50));
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, Guid.NewGuid(), 22));
            _mockTabRepository.Setup(s => s.FindByIdWithItems(tab.Id)).Returns(tab);

            var total = tab.Total;
            var totalDiscount = tab.TotalDiscount;

            //Act
            var result = await _handler.Handler(new CloseTabCommand(tab.Id));

            //Assert
            Assert.IsTrue(result.Valid);
            Assert.IsFalse(tab.Open);
            Assert.AreEqual(result.Value.TabId, tab.Id);
            Assert.AreEqual(result.Value.Total, total);
            Assert.AreEqual(result.Value.TotalDiscount, totalDiscount);
            _mockTabRepository.Verify(v => v.Update(It.Is<Tab>(t => t.Id == tab.Id)));
        }
    }
}

