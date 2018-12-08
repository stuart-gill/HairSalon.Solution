using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System;
using System.Collections.Generic;
 
namespace HairSalon.Tests
{
 [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hair_salon_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("Soraya", 2065555555, 1);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Soraya";
      Client newClient = new Client(name, 2065555555, 1);

      //Act
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      //Arrange
      string name = "Soraya";
      Client newClient = new Client(name, 2065555555, 1);

      //Act
      string updatedName = "Do the dishes";
      newClient.SetName(updatedName);
      string result = newClient.GetName();

      //Assert
      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      string name01 = "Soraya";
      string name02 = "Nathaniel";
      int phone01 = 2065555555;
      int phone02 = 2085555555;
      Client newClient1 = new Client(name01, phone01, 1);
      newClient1.Save();
      Client newClient2 = new Client(name02, phone02, 1);
      newClient2.Save();
      List<Client> newList = new List<Client> { newClient1, newClient2 };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Soraya", 2065555555, 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("Soraya", 2065555555, 1);
      Client secondClient = new Client("Soraya", 2065555555, 1);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Soraya", 2065555555, 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Soraya", 2065555555, 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    // [TestMethod]
    // public void Edit_UpdatesClientInDatabase_String()
    // {
    //   //Arrange
    //   Client testClient = new Client("Soraya", 2065555555, 1);
    //   testClient.Save();
    //   string secondName = "Nathaniel";

    //   //Act
    //   testClient.Edit(secondName);
    //   string result = Client.Find(testClient.GetId()).GetName();

    //   //Assert
    //   Assert.AreEqual(secondName, result);
    // }

    // [TestMethod]
    // public void Delete_DeletesNameInDatabase_String()
    // {
    //   //Arrange
    //   Client testClient = new Client("Soraya", 2065555555, 1);
    //   testClient.Save();
    //   string deletedName = "";

    //   //Act
    //   testClient.Delete();
    //   string result = Client.Find(testClient.GetId()).GetName();
      
    //   //Assert
    //   Assert.AreEqual(deletedName, result);
    // }

  

    

    // [TestMethod]
    // public void GetCategoryId_ReturnsClientsParentCategoryId_Int()
    // {
    //   //Arrange
    //   Category newCategory = new Category("Home Tasks");
    //   Client newClient = new Client("Soraya", 1, newCategory.GetId());
    //
    //   //Act
    //   int result = newClient.GetCategoryId();
    //
    //   //Assert
    //   Assert.AreEqual(newCategory.GetId(), result);
    // }

  }
}
