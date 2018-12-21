using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    //view form to add client
    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
     Stylist stylist = Stylist.Find(stylistId);
     return View(stylist);
    }

    //view details on a particular client
    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client client = Client.Find(clientId);
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }
    
    [HttpGet("/stylists/{stylistId}/clients/{clientID}/delete")]
    public ActionResult Delete(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Stylist stylist = Stylist.Find(stylistId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("client", client);
      model.Add("stylist", stylist);
      Client.Delete(clientId);
      return View(model);
    }


    [HttpGet("/clients/deleteAll")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return View();
    }

    [HttpGet("/clients")]
    public ActionResult Index()
    {
        List<Client> allClients = Client.GetAll();
        return View(allClients);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Client client = Client.Find(clientId);
      model.Add("client", client);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string newName, string newPhone)
    {
      Client client = Client.Find(clientId);
      client.Edit(newName, newPhone);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      model.Add("client", client);
      return View("Show", model);
    }
  }
}
