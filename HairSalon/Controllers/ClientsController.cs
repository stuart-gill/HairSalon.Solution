using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {

    [HttpGet("/clients/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
     Stylist stylist = Stylist.Find(stylistId);
     return View(stylist);
    }

    [HttpGet("/clients/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }
    
    // [HttpGet("/clients/{stylistId}/clients/{clientID}/delete")]
    // public ActionResult Delete(int stylistId, int clientId)
    // {
    //   Client client = Client.Find(clientId);
    //   Stylist stylist = Stylist.Find(stylistId);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   model.Add("client", client);
    //   model.Add("stylist", stylist);
    //   Client.Delete(clientId);
    //   return View(model);
    // }


    [HttpPost("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return View();
    }

    // [HttpGet("/clients/{stylistId}/clients/{clientId}/edit")]
    // public ActionResult Edit(int stylistId, int clientId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist stylist = Stylist.Find(stylistId);
    //   model.Add("stylist", stylist);
    //   Client client = Client.Find(clientId);
    //   model.Add("client", client);
    //   return View(model);
    // }

    [HttpPost("/clients/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int stylistId, int clientId, string newName, int newPhone)
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
