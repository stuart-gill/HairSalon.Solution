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
        public ActionResult Create(string stylistName)
        {
            Stylist newStylist = new Stylist(stylistName);
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
            List<Specialty> stylistSpecialties = Stylist.GetSpecialties(selectedStylist.GetId());
            List<Specialty> allSpecialties = Specialty.GetAll();
            model.Add("stylist", selectedStylist);
            model.Add("clients", stylistClients);
            model.Add("stylistSpecialties", stylistSpecialties);
            model.Add("allSpecialties", allSpecialties);
            return View(model);
        }

        //add specialty to stylists_specialties join table

        [HttpPost("/stylists/{stylistId}/addSpecialty")]
        public ActionResult AddSpecialty(int stylistId, int specialtyId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(stylistId);
            Specialty specialty = Specialty.Find(specialtyId);
            selectedStylist.AddSpecialty(specialty);
            List<Specialty> stylistSpecialties = Stylist.GetSpecialties(stylistId);
            List<Specialty> allSpecialties = Specialty.GetAll();
            List<Client> stylistClients = selectedStylist.GetClients();
            model.Add("stylistSpecialties", stylistSpecialties);
            model.Add("allSpecialties", allSpecialties);
            model.Add("stylist", selectedStylist);
            model.Add("clients", stylistClients);
            return View("Show", model);
        }

        //delete stylist and all their clients too
        [HttpGet("/stylists/{stylistId}/delete")]
        public ActionResult Delete(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist selectedStylist = Stylist.Find(stylistId);
            model.Add("stylist", selectedStylist);
            Stylist.DeleteThisStylistsClients(stylistId);
            Stylist.Delete(stylistId);
            return View(model);
        }


        //creates new Clients within a given Stylist:
        [HttpPost("/stylists/{id}")]
        public ActionResult Create(int id, string clientName, string clientPhone)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist foundStylist = Stylist.Find(id);
            Client newClient = new Client(clientName, clientPhone, id);
            newClient.Save();
            List<Client> stylistClients = foundStylist.GetClients();
            List<Specialty> stylistSpecialties = Stylist.GetSpecialties(foundStylist.GetId());
            List<Specialty> allSpecialties = Specialty.GetAll();
            model.Add("stylistSpecialties", stylistSpecialties);
            model.Add("clients", stylistClients);
            model.Add("stylist", foundStylist);
            model.Add("allSpecialties", allSpecialties);

            return View("Show", model);
        }



    }
}
