using DGPub.Domain.Core;
using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Handlers;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace DGPub.Domain.Tests.Tabs.Handlers
{
    [TestClass]
    public class AddItemTabHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IItemTabRepository> _mockItemTabRepository;
        private readonly Mock<ITabRepository> _mockTabRepository;

        private readonly AddItemTabHandler _handler;

        public AddItemTabHandlerTests()
        {
            _mockTabRepository = new Mock<ITabRepository>();
            _mockUow = new Mock<IUnitOfWork>();
            _mockItemRepository = new Mock<IItemRepository>();
            _mockItemTabRepository = new Mock<IItemTabRepository>();

            _handler = new AddItemTabHandler(
                 _mockUow.Object,
                _mockItemRepository.Object,
                _mockItemTabRepository.Object,
                _mockTabRepository.Object);


            _mockUow.Setup(s => s.Commit()).Returns(true);
        }


        [TestMethod()]
        public async Task AddItem_CommandInvalid_Invalid()
        {
            //Arrange                     

            //Act
            var result = await _handler.Handler(new AddItemTabCommand(Guid.Empty, Guid.Empty));

            //Assert
            Assert.IsFalse(result.Valid);
        }

        [TestMethod()]
        public void AddItem_ItemNotFound_Invalid()
        {
            //Arrange
            var itemId = Guid.NewGuid();
            var tabId = Guid.NewGuid();

            _mockItemRepository.Setup(i => i.FindById(itemId)).Returns((Item)null);

            //Act
            var result = _handler.AddItem(new AddItemTabCommand(tabId, itemId));

            //Assert
            Assert.IsFalse(result.Valid);
        }

        [TestMethod()]
        public void AddItem_Valid()
        {
            //Arrange            
            Guid tabId = Guid.NewGuid();
            Item item = MockValidItem();

            //Act
            var result = _handler.AddItem(new AddItemTabCommand(tabId, item.Id));

            //Assert
            Assert.IsTrue(result.Valid);
            _mockItemTabRepository.Verify(i => i.Add(It.Is<ItemTab>
                 (v =>
                 v.TabId == tabId &&
                 v.ItemId == item.Id &&
                 v.UnitPrice == item.Price)));

        }

        private Item MockValidItem()
        {
            var item = Item.ItemFactory.Create("ClearSale", 50);
            _mockItemRepository.Setup(i => i.FindById(item.Id)).Returns(item);
            return item;
        }

        [TestMethod()]
        public void AddItem_Command_Invalid()
        {
            //Arrange            
            var itemId = Guid.NewGuid();
            var tabId = Guid.NewGuid();

            _mockItemRepository.Setup(i => i.FindById(itemId)).Returns((Item)null);

            //Act
            var result = _handler.AddItem(new AddItemTabCommand(tabId, itemId));

            //Assert
            Assert.IsFalse(result.Valid);

        }

        [TestMethod()]
        public async Task AddItem_Command_Valid()
        {
            //Arrange                      
            Item item = MockValidItem();

            var tab = Tab.TabFactory.Create("ClearSale");

            _mockTabRepository.Setup(s => s.FindById(tab.Id)).Returns(tab);

            //Act
            var result = await _handler.Handler(new AddItemTabCommand(tab.Id, item.Id));

            //Assert
            Assert.IsTrue(result.Valid);
            Assert.AreEqual(result.Value.Id, tab.Id);

        }
    }
}