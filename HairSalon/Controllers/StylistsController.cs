using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName, string specialty)
    {
      Stylist newStylist = new Stylist(stylistName, specialty);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      return View(model);
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      // Dictionary<string, object> model = new Dictionary<string, object>();
      // Stylist selectedStylist = Stylist.Find(stylistId);
      // model.Add("stylist", selectedStylist);
      // model.Add("clients", stylistClients);
      Stylist.DeleteClients(stylistId);
      Stylist.Delete(stylistId);
      return View();
    }


    //This one creates new Clients within a given Stylist, not new Stylists:
    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName, int clientPhone)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, clientPhone, stylistId);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      return View("Show", model);
    }
  }
}
