﻿using DGPub.Application.Promotions.Handlers;
using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGPub.Application.Tests.Promotions.Handlers
{
    [TestClass]
    public class BeerWithJuicePromotionTest
    {
        private BeerWithJuicePromotion _handler;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IItemTabRepository> _mockItemTabRepository;

        private Item beer;
        private Item juice;

        public BeerWithJuicePromotionTest()
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

            var tab = Tab.TabFactory.Create("Caio");

            var beerTab = ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 2, beer.Price);
            tab.Items.Add(beerTab);
            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, 2, juice.Price));

            //Act
            _handler.Calcule(tab);

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

            var tab = Tab.TabFactory.Create("Caio");

            var beerTab = ItemTab.ItemTabFactory.Create(tab.Id, beer.Id, 2, beer.Price);
            tab.Items.Add(beerTab);

            //Act
            _handler.Calcule(tab);

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }

        [TestMethod]
        public void Promotion_OnlyJuice_NotChangePrice()
        {
            //Arrange 
            var tab = Tab.TabFactory.Create("Caio");

            tab.Items.Add(ItemTab.ItemTabFactory.Create(tab.Id, juice.Id, 2, juice.Price));

            //Act
            _handler.Calcule(tab);

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }

        [TestMethod]
        public void Promotion_Empty()
        {
            //Arrange 
            var tab = Tab.TabFactory.Create("Caio");

            //Act
            _handler.Calcule(tab);

            //Assert
            _mockItemTabRepository.Verify(s => s.Update(It.IsAny<ItemTab>()), Times.Never);
        }
    }
}