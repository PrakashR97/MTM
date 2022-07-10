using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thiru_Proj.DataLayer;
using Thiru_Proj.Models;

namespace Thiru_Proj.Controllers
{
    public class DistrictController : Controller
    {
        // GET: District
        public ActionResult Index()
        {
            District_Data_Layer _District = new District_Data_Layer();

            return View(_District.GetAllDistricts());
        }

        // GET: District/Details/5
        public ActionResult Details(int id)
        {

            District_Data_Layer _District = new District_Data_Layer();
            return View(_District.GetDistrictById(id));
        }

        // GET: District/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: District/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                District_Data_Layer _dis = new District_Data_Layer();
                _dis.Id = int.Parse(collection["Id"]);

                _dis.Name =collection["Name"];

                _dis.IsActive=bool.Parse (collection["IsActive"]);
                _dis.AddDistrict();
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: District/Edit/5
        public ActionResult Edit(int id)
        {

            District_Data_Layer _District = new District_Data_Layer();
            return View(_District.GetDistrictById(id));
            //return View();
        }

        // POST: District/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                District_Data_Layer _dis = new District_Data_Layer();
                _dis.Id = int.Parse(collection["Id"]);
                _dis.Name = collection["Name"];
                _dis.IsActive = bool.Parse(collection["IsActive"]);
                _dis.UpdateDistrict();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: District/Delete/5
        public ActionResult Delete(int id)
        {

            District_Data_Layer _dis = new District_Data_Layer();

            return View(_dis.GetDistrictById(id));
        }

        // POST: District/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                District_Data_Layer _dis = new District_Data_Layer();
                
                _dis.Id = id;
                _dis.DeleteDistrict();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}