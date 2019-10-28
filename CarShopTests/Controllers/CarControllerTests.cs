using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarShop.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using CarShop.BL.Models;

namespace CarShop.Controllers.Tests
{
    [TestClass()]
    public class CarControllerTests
    {
        [TestMethod()]
        public void CarControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateCategoryTest()
        {
            Category category = new Category();
            string name = Guid.NewGuid().ToString();
            category.Name = name;
            Assert.AreEqual(name, category.Name);
            Assert.AreSame(name, category.Name);
        }

       
        [TestMethod()]
        public void DetailsCarTest()
        {





            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteCategoryTest()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 14,
                    Name = Guid.NewGuid().ToString()
                },

            };
            Assert.IsNotNull(categories);
            categories.Remove(categories[0]);
            //Assert.IsNull(categories);


        }

        [TestMethod()]
        public void DeleteCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditCategoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditCategoryTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditCarTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateCarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateCarTest1()
        {
            Assert.Fail();
        }
    }
}