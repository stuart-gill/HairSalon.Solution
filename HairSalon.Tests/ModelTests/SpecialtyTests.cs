using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyTest : IDisposable
    {

        public SpecialtyTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stuart_gill_test;";
        }

        public void Dispose()
        {
            Stylist.ClearAll();
            Client.ClearAll();
            Specialty.ClearAll();
        }

        [TestMethod]
        public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
        {
            Specialty testSpecialty = new Specialty("perms");
            Assert.AreEqual(typeof(Specialty), testSpecialty.GetType());
        }

        [TestMethod]
        public void Save_SavesSpecialtyToDatabase_SpecialtyList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("perms");
            testSpecialty.Save();

            //Act
            List<Specialty> result = Specialty.GetAll();
            List<Specialty> testList = new List<Specialty> { testSpecialty };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToSpecialty_Id()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("perms");
            testSpecialty.Save();

            //Act
            Specialty savedSpecialty = Specialty.GetAll()[0];

            int result = savedSpecialty.GetId();
            int testId = testSpecialty.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            //Arrange

            string specialty1 = "perms";
            Specialty newSpecialty = new Specialty(specialty1);

            //Act
            string result = newSpecialty.GetName();

            //Assert
            Assert.AreEqual(specialty1, result);
        }

        [TestMethod]
        public void GetId_ReturnsSpecialtyId_Int()
        {
            //Arrange
            string name = "perms";
            Specialty newSpecialty = new Specialty(name);

            //Act
            int result = newSpecialty.GetId();

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
        {
            //Arrange
            string specialty1 = "perms";
            string specialty2 = "curls";
            Specialty newSpecialty1 = new Specialty(specialty1);
            newSpecialty1.Save();
            Specialty newSpecialty2 = new Specialty(specialty2);
            newSpecialty2.Save();
            List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

            //Act
            List<Specialty> result = Specialty.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Find_ReturnsSpecialtyInDatabase_Specialty()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("perms");
            testSpecialty.Save();

            //Act
            Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

            //Assert
            Assert.AreEqual(testSpecialty, foundSpecialty);
        }

        [TestMethod]
        public void GetAll_SpecialtiesEmptyAtFirst_List()
        {
            //Arrange, Act
            int result = Specialty.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfNamesAreTheSame_Specialty()
        {
            //Arrange, Act
            Specialty firstSpecialty = new Specialty("perms");
            Specialty secondSpecialty = new Specialty("perms");

            //Assert
            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void GetStylists_ReturnsAllSpecialtyStylists_StylistList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("perms");
            testSpecialty.Save();
            Stylist testStylist1 = new Stylist("jenny");
            testStylist1.Save();
            Stylist testStylist2 = new Stylist("henny");
            testStylist2.Save();

            //Act
            testSpecialty.AddStylist(testStylist1);
            List<Stylist> result = testSpecialty.GetStylists();
            List<Stylist> testList = new List<Stylist> { testStylist1 };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void AddStylist_AddsStylistToSpecialty_StylistList()
        {
            //Arrange
            Specialty testSpecialty = new Specialty("perms");
            testSpecialty.Save();
            Stylist testStylist = new Stylist("jenny");
            testStylist.Save();

            //Act
            testSpecialty.AddStylist(testStylist);

            List<Stylist> result = testSpecialty.GetStylists();
            List<Stylist> testList = new List<Stylist> { testStylist };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }




    }

}