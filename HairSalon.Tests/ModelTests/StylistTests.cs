using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {

    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stuart_gill_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist testStylist = new Stylist("Jenny", "perms");
      Assert.AreEqual(typeof(Stylist), testStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name01 = "Jenny";
      string specialty1 = "perms";
      Stylist newStylist = new Stylist(name01, specialty1);

      //Act
      string result = newStylist.GetName();

      //Assert
      Assert.AreEqual(name01, result);
    }

    // [TestMethod]
    // public void GetId_ReturnsStylistId_Int()
    // {
    //   //Arrange
    //   string name = "Test Stylist";
    //   Stylist newStylist = new Stylist(name);
    //
    //   //Act
    //   int result = newStylist.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      //Arrange
      string name01 = "Jenny";
      string name02 = "Kenny";
      string specialty1 = "perms";
      string specialty2 = "curls";
      Stylist newStylist1 = new Stylist(name01, specialty1);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02, specialty2);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      //Act
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jenny", "perms");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyClientList_ClientList()
    {
      //Arrange
      string name = "Jenny";
      string specialty = "perms";
      Stylist newStylist = new Stylist(name, specialty);
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = newStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_CategoriesEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Jenny", "perms");
      Stylist secondStylist = new Stylist("Jenny", "perms");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jenny", "perms");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jenny", "perms");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("Jenny", "perms");
      testStylist.Save();
      Client firstClient = new Client("George", "2065555555", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Soraya", "2085555555", testStylist.GetId());
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    // [TestMethod]

    // public void DeleteClients_DeletesAllClientsWithStylist_ClientList()
    // {
    //   //Arrange, Act
    //   Stylist testStylist = new Stylist("Jenny", "perms");
    //   testStylist.Save();
    //   Client firstClient = new Client("George", 2065555555, testStylist.GetId());
    //   firstClient.Save();
    //   Client secondClient = new Client("Soraya", 2085555555, testStylist.GetId());
    //   secondClient.Save();
    //   List<Client> testClientList = new List<Client> {firstClient, secondClient};
    //   testStylist.DeleteClients();
    //   string result = firstClient.GetName();
    //   string deletedDescription="";

    //   //Assert
    //   Assert.AreEqual(deletedDescription , result);

    // }

    // [TestMethod]
    // public void Delete_DeletesNameInDatabase_String()
    // {
    //   //Arrange
    //   Stylist testStylist = new Stylist("Jenny", "perms");
    //   testStylist.Save();
    //   string deletedName = "";

    //   //Act
    //   testStylist.Delete();
    //   string result = Stylist.Find(testStylist.GetId()).GetName();
      
    //   //Assert
    //   Assert.AreEqual(deletedName, result);
    // }

  }
}