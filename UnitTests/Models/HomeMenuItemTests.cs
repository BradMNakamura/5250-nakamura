using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Mine.Models;
namespace UnitTests.Models
{
    //Arrange 
    //Act
    //Reset
    //Assert
    [TestFixture]
    public class HomeMenuItemTests
    {
        /*
        [Test]
        public void ItemModel_
        {
          //Arrange 
            //Act
            //Reset
            //Assert
        }
        */
        public void HomeMenuItems_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange
            // Act
            var result = new HomeMenuItem();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void HomeMenuItems_Set_Get_Valid_Default_Should_Pass()
        {
            //Arrange 
            //Act
            var result = new HomeMenuItem();
            result.Id = MenuItemType.Items;
            result.Title = "Title";

            //Reset

            //Assert
            Assert.AreEqual(MenuItemType.Items, result.Id);
            Assert.AreEqual("Title", result.Title);
        }

        [Test]
        public void HomeMenuItems_Get_Valid_Default_Should_Pass() 
        {
            //Arrange 
            //Act
            var result = new HomeMenuItem();
            //Reset
            //Assert
            Assert.AreEqual(MenuItemType.Items, result.Id);
            Assert.AreEqual(null, result.Title);
        }
    }
}
