using Microsoft.AspNet.Identity;
using PhoonBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PhoonBook.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            /*

            PhoneBookDbEntities db = new PhoneBookDbEntities();
            var list = db.Person.ToList();
            List<personviewmodel> viewList = new List<personviewmodel>();
            foreach (var s in list)
            {
                personviewmodel obj = new personviewmodel();
                obj.AddedOn = s.AddedOn;
                obj.UpdateOn = Convert.ToDateTime(s.UpdateOn);
                obj.FirstName = s.FirstName;
                obj.LastName = s.LastName;
                obj.EmailId = s.EmailId;
                obj.HomeCity = s.HomeCity;
                viewList.Add(obj);
                
            }*/

            PhoneBookDbEntities db = new PhoneBookDbEntities();
            int s = 0;
            string user_id = User.Identity.GetUserId().ToString();
            var persons = db.Person.Where(x=>x.AddedBy==user_id);
            List<Person> pr = new List<Person>();
            foreach(var i in persons)
            {
                Person p = new Person();
                p.PersonId = i.PersonId;
                p.FirstName = i.FirstName;
                p.MiddleName = i.MiddleName;
                p.LastName = i.LastName;
                pr.Add(p);
                s = s + 1;

            }
            ViewBag.Greetings = s;

            return View(pr);



            //return View(viewList);
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            PhoneBookDbEntities db = new PhoneBookDbEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person contact = db.Person.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(personviewmodel collection)
        {
            
            
            try
            {
                // TODO: Add insert logic here
                string id = User.Identity.GetUserId().ToString();
                PhoneBookDbEntities db = new PhoneBookDbEntities();
                Person obj = new Person();
                // obj.PersonId = collection.PersonId;
                
                obj.AddedBy =id;
                obj.AddedOn = collection.AddedOn;
                obj.UpdateOn = collection.UpdateOn;
                obj.FirstName = collection.FirstName;
                obj.LastName = collection.LastName;
                obj.EmailId = collection.EmailId;
                obj.HomeCity = collection.HomeCity;
                db.Person.Add(obj);
               
                db.SaveChanges();
               


                return RedirectToAction("Index");
            }
            catch
            {
               
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            PhoneBookDbEntities db = new PhoneBookDbEntities();
             var person = db.Person.Where(x => x.PersonId == id);
            List<Person> per = new List<Person>();

            foreach (var i in person)
            {
                var pp = new Person()

                {

                    FirstName = i.FirstName,
                    MiddleName = i.MiddleName,
                    LastName = i.LastName,
                    DateOfBirth = i.DateOfBirth,
                    EmailId = i.EmailId,
                    HomeAddress = i.HomeAddress,
                    ImagePath = i.ImagePath,
                    HomeCity = i.HomeCity,


                };

                per.Add(pp);
            }

            ViewData["ff"] = per;



            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,personviewmodel collection)
        {
            try
            {
                PhoneBookDbEntities db = new PhoneBookDbEntities();
                var persons = db.Person.Where(x=>x.PersonId==id).First();
                persons.FirstName = collection.FirstName;
      
                persons.MiddleName = collection.MiddleName;
                persons.LastName = collection.LastName;
                persons.EmailId = collection.EmailId;

                persons.DateOfBirth = collection.DateOfBirth;




                db.SaveChanges();
                return RedirectToAction("Index");

                // TODO: Add update logic here




            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            //PhoneBookDbEntities db = new PhoneBookDbEntities();
            //var person = db.Person.Where(x => x.PersonId == id);
            //List<Person> per = new List<Person>();

            //foreach (var i in person)
            //{
            //    var pp = new Person()

            //    {

            //        FirstName = i.FirstName,
            //        MiddleName = i.MiddleName,
            //        LastName = i.LastName,
            //        DateOfBirth = i.DateOfBirth,
            //        EmailId = i.EmailId,
            //        HomeAddress = i.HomeAddress,
            //        ImagePath = i.ImagePath,
            //        HomeCity = i.HomeCity,


                //};

            //    per.Add(pp);
            //}
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,personviewmodel collection)
        {
            try
            {
                PhoneBookDbEntities db = new PhoneBookDbEntities();
                if (ModelState.IsValid)
                {
                    var del = (from c in db.Person
                               where c.PersonId == id
                               select c).FirstOrDefault();

                    db.Person.Remove(del);
                    db.SaveChanges();
                }
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
