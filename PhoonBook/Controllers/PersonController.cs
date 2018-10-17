using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PhoneBook2.Models;
namespace PhoneBook2.Controllers
{

    [Authorize]
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId().ToString();
            PhoneBookDbEntities db = new PhoneBookDbEntities();
            List<Person> list = db.People.ToList();
            List<PersonViewModel> ViewList = new List<PersonViewModel>();
            foreach (Person p in list)
            {
                if (p.AddedBy == id)
                {
                    PersonViewModel obj = new PersonViewModel();
                    obj.PersonId = p.PersonId;
                    obj.FirstName = p.FirstName;
                    obj.MiddleName = p.MiddleName;
                    obj.LastName = p.LastName;
                    obj.AddedBy = p.AddedBy;
                    obj.AddedOn = p.AddedOn;
                    obj.DateOfBirth = p.DateOfBirth;
                    obj.EmailId = p.EmailId;
                    obj.FaceBookAccountId = p.FaceBookAccountId;
                    obj.HomeAddress = p.HomeAddress;
                    obj.LinkedInId = p.LinkedInId;
                    obj.ImagePath = p.ImagePath;
                    obj.TwitterId = p.TwitterId;
                    obj.UpdateOn = p.UpdateOn;
                    obj.HomeCity = p.HomeCity;
                    ViewList.Add(obj);
                }
               
            }
            return View(ViewList);
        }
         

        // GET: Person
        public ActionResult TotalPersons()
        {
            int s = 0;
            string id = User.Identity.GetUserId().ToString();
            PhoneBookDbEntities db = new PhoneBookDbEntities();
            List<Person> list = db.People.ToList();
            foreach (Person p in list)
            {
                if (p.AddedBy == id)
                {
                    s = s + 1;
                }
               
            }
            return Content(s.ToString());
        }
        public ActionResult UpcomingBirthdays()
        {
            
            string id = User.Identity.GetUserId().ToString();
            PhoneBookDbEntities db = new PhoneBookDbEntities();
            var list = db.People.Where(x => x.AddedBy == id);
            List<Person> ViewList = new List<Person>();
            foreach (var p in list)
            {
                    if (p.DateOfBirth.Value.Day == DateTime.Now.Day && p.DateOfBirth.Value.Month == DateTime.Now.Month ||
                        p.DateOfBirth.Value.Day == DateTime.Now.AddDays(1).Day && p.DateOfBirth.Value.Month == DateTime.Now.Month)
                    {
                        Person obj = new Person();
                        obj.PersonId = p.PersonId;
                        obj.FirstName = p.FirstName;
                        obj.MiddleName = p.MiddleName;
                        obj.LastName = p.LastName;
                        obj.AddedBy = p.AddedBy;
                        obj.DateOfBirth = p.DateOfBirth;
                        obj.EmailId = p.EmailId;
                        obj.FaceBookAccountId = p.FaceBookAccountId;
                        obj.HomeAddress = p.HomeAddress;
                        obj.LinkedInId = p.LinkedInId;
                        obj.ImagePath = p.ImagePath;
                        obj.TwitterId = p.TwitterId;
                        obj.UpdateOn = p.UpdateOn;
                        obj.HomeCity = p.HomeCity;
                        ViewList.Add(obj);
                    }
                

            }
            return View(ViewList);
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            PhoneBookDbEntities db = new PhoneBookDbEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person p = db.People.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }
       

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                string id = User.Identity.GetUserId().ToString();
                PhoneBookDbEntities db = new PhoneBookDbEntities();
                Person obj = new Person();
                
                obj.FirstName = collection.FirstName;
                obj.MiddleName = collection.MiddleName;
                obj.LastName = collection.LastName;
                obj.AddedBy = id;
                obj.AddedOn = DateTime.Now;
                obj.DateOfBirth = collection.DateOfBirth;
                obj.EmailId = collection.EmailId;
                obj.FaceBookAccountId = collection.FaceBookAccountId;
                obj.HomeAddress = collection.HomeAddress;
                obj.LinkedInId = collection.LinkedInId;
                obj.ImagePath = collection.ImagePath;
                obj.TwitterId = collection.TwitterId;
                obj.UpdateOn = collection.UpdateOn;
                obj.HomeCity = collection.HomeCity;
                db.People.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            PhoneBookDbEntities db = new PhoneBookDbEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person p = db.People.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            Person pp = new Person();
            {
                pp.FirstName = p.FirstName;
                pp.MiddleName = p.MiddleName;
                pp.LastName = p.LastName;
                pp.HomeAddress = p.HomeAddress;
                pp.HomeCity = p.HomeCity;
                pp.EmailId = p.EmailId;
                pp.DateOfBirth = p.DateOfBirth;
            }
            return View(pp);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person collection)
        {
            try
            {
                // TODO: Add update logic here

                PhoneBookDbEntities db = new PhoneBookDbEntities();
                Person obj = db.People.Find(id);
                obj.FirstName = collection.FirstName;
                obj.MiddleName = collection.MiddleName;
                obj.LastName = collection.LastName;
                obj.DateOfBirth = collection.DateOfBirth;
                obj.EmailId = collection.EmailId;
                obj.FaceBookAccountId = collection.FaceBookAccountId;
                obj.HomeAddress = collection.HomeAddress;
                obj.LinkedInId = collection.LinkedInId;
                obj.ImagePath = collection.ImagePath;
                obj.TwitterId = collection.TwitterId;
                obj.UpdateOn = DateTime.Now;
                obj.HomeCity = collection.HomeCity;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            PhoneBookDbEntities db = new PhoneBookDbEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person p = db.People.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Person collection)
        {
            try
            {
               PhoneBookDbEntities db = new PhoneBookDbEntities();
                Person p = db.People.Find(id);
                db.People.Remove(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
