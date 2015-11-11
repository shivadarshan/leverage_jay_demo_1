using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using leverage_jay_demo_1.Models;

namespace leverage_jay_demo_1.Controllers
{
    public class VehiclesController : Controller
    {
        private Vehicle_dmDBContext db = new Vehicle_dmDBContext();


        // GET : Vehicles/ImageUpload/5

        public ActionResult ImageUpload(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_dm vehicle_dm = db.Vehicle_dms.Find(id);
            if (vehicle_dm == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_dm);
        }



        // GET: Vehicles
        public ActionResult Index()
        {
            return View(db.Vehicle_dms.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_dm vehicle_dm = db.Vehicle_dms.Find(id);
            if (vehicle_dm == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_dm);
        }

        
       
        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Vehicles/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImageUpload(HttpPostedFileBase file, Vehicle_dm vehicle_dm)
        {
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(vehicle_dm.Registration_Number+file.FileName));
                        file.SaveAs(path);
                        vehicle_dm.Image_Path = vehicle_dm.Registration_Number + file.FileName;
                        ViewBag.Message = "File uploaded successfully with the file name " + vehicle_dm.Image_Path;

                        //let me save the data object to the database now that the image path has been updated
                        db.Entry(vehicle_dm).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
            return View();  
        }




        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Registration_Number,Manufacturer,Vehicle_Model,Covered,Image_Path,ReleaseDate")] Vehicle_dm vehicle_dm, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var error = "file exists";
            }

            else
            {
                var error = "file does not exist";
            }            

            if (ModelState.IsValid)
            {
                
               
                //saving the model object to the database
                
                db.Vehicle_dms.Add(vehicle_dm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle_dm);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_dm vehicle_dm = db.Vehicle_dms.Find(id);
            if (vehicle_dm == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_dm);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Registration_Number,Manufacturer,Vehicle_Model,Covered,Image_Path,ReleaseDate")] Vehicle_dm vehicle_dm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle_dm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle_dm);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle_dm vehicle_dm = db.Vehicle_dms.Find(id);
            if (vehicle_dm == null)
            {
                return HttpNotFound();
            }
            return View(vehicle_dm);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle_dm vehicle_dm = db.Vehicle_dms.Find(id);
            db.Vehicle_dms.Remove(vehicle_dm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //function that will check for uniqueness of the Registration_Number property


        public JsonResult IsRegistration_NumberExists(string Registration_Number)
        {
            //now checking if Registration_Number mathes the Registration_Number specified in the parameter
            //to achieve this, I am going to use the ANY extension method

            return Json(!db.Vehicle_dms.Any(x => x.Registration_Number == Registration_Number) , JsonRequestBehavior.AllowGet);
        }

    }//end of class definition
}
