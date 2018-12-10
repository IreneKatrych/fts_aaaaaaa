using fts_aaaaaaa.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;

namespace fts_aaaaaaa.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ви можете з нами зв'язатись";

            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }


        public ActionResult Homeopathy()
        {
            return View();
        }

        public ActionResult Phytotherapy()
        {
            return View();
        }

        public ActionResult Temperature()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            DateTime compareDate = DateTime.Now.AddMonths(-1);

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 1 && t.CreatedDate >= compareDate).ToList();
                var newList = results.OrderBy(x => x.CreatedDate).ToList();
                return View(newList);
            }

        }

        [HttpGet]
        public ActionResult AddTemperature()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddTemperature([Bind(Include = "UserId,Value,CreatedDate")]MeasureResult measureResult)
        {

            var userid = User.Identity.GetUserId();
            if (measureResult.Value <= 28.0)
            {
                ModelState.AddModelError("Value", "Температура тіла не може бути нижче 28 градусів Цельсія");
            }
            else if (measureResult.Value >= 42.0)
            {
                ModelState.AddModelError("Value", "Температура тіла не може бути вище 42 градусів Цельсія");
            }

            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некорректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некорректна дата");
            }


            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 1;
                db.Entry(measureResult).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Temperature");
            }

            ViewBag.Message = "Запит не пройшов валідацію";
            return View(measureResult);
        }

        //View
        public async Task<ActionResult> AllTemperature()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 1).ToList();
                var newList = results.OrderByDescending(x => x.CreatedDate).ToList();

                return View(newList);
            }
        }


        //Edit
        [HttpGet]
        public ActionResult EditTemperature(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            MeasureResult measureResult = db.MeasureResults.Find(id);
            if (measureResult != null)
            {
                return View(measureResult);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditTemperature([Bind(Include = "Id,UserId,Value,CreatedDate")]MeasureResult measureResult)
        {
            var userid = User.Identity.GetUserId();
            var measureId = measureResult.Id;
            if (measureResult.Value <= 28.0)
            {
                ModelState.AddModelError("Value", "Температура тіла не може бути нижче 28 градусів Цельсія");
            }
            else if (measureResult.Value >= 42.0)
            {
                ModelState.AddModelError("Value", "Температура тіла не може бути вище 42 градусів Цельсія");
            }

            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некорректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некорректна дата");
            }


            if (ModelState.IsValid)
            {
                measureResult.Id = measureId;
                measureResult.UserId = userid;
                measureResult.MeasureId = 1;
                db.Entry(measureResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllTemperature");
            }
            return View(measureResult);
        }

        // Delete
        public async Task<ActionResult> DeleteTemperature(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            if (measureResult == null)
            {
                return HttpNotFound();
            }
            return View(measureResult);
        }

        [HttpPost, ActionName("DeleteTemperature")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedTemperature(int id)
        {
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            db.MeasureResults.Remove(measureResult);
            await db.SaveChangesAsync();
            return RedirectToAction("AllTemperature");
        }

        public ActionResult Weight()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            DateTime compareDate = DateTime.Now.AddMonths(-1);

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 2 && t.CreatedDate >= compareDate).ToList();
                var newList = results.OrderBy(x => x.CreatedDate).ToList();
                return View(newList);
            }

        }

        [HttpGet]
        public ActionResult AddWeight()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddWeight([Bind(Include = "UserId,Value,CreatedDate")]MeasureResult measureResult)
        {

            var userid = User.Identity.GetUserId();
            if (measureResult.Value <= 0.5)
            {
                ModelState.AddModelError("Value", "Вага не може бути менше 0.5 кг");
            }
            else if (measureResult.Value >= 300)
            {
                ModelState.AddModelError("Value", "Вага не може бути більше 300 кг");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 2;
                db.Entry(measureResult).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Weight");
            }
            return View(measureResult);
        }


        //View
        public async Task<ActionResult> AllWeight()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 2).ToList();
                var newList = results.OrderByDescending(x => x.CreatedDate).ToList();

                return View(newList);
            }
        }


        //Edit
        [HttpGet]
        public ActionResult EditWeight(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            MeasureResult measureResult = db.MeasureResults.Find(id);
            if (measureResult != null)
            {
                return View(measureResult);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditWeight([Bind(Include = "Id,UserId,Value,CreatedDate")]MeasureResult measureResult)
        {
            var userid = User.Identity.GetUserId();
            if (measureResult.Value <= 0.5)
            {
                ModelState.AddModelError("Value", "Вага не може бути менше 0.5 кг");
            }
            else if (measureResult.Value >= 300)
            {
                ModelState.AddModelError("Value", "Вага не може бути більше 300 кг");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 2;
                db.Entry(measureResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllWeight");
            }
            return View(measureResult);
        }

        // Delete
        public async Task<ActionResult> DeleteWeight(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            if (measureResult == null)
            {
                return HttpNotFound();
            }
            return View(measureResult);
        }

        [HttpPost, ActionName("DeleteWeight")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedWeight(int id)
        {
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            db.MeasureResults.Remove(measureResult);
            await db.SaveChangesAsync();
            return RedirectToAction("AllWeight");
        }

        public ActionResult HeartRate()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            DateTime compareDate = DateTime.Now.AddMonths(-1);

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 5 && t.CreatedDate >= compareDate).ToList();
                var newList = results.OrderBy(x => x.CreatedDate).ToList();
                return View(newList);
            }

        }

        [HttpGet]
        public ActionResult AddHeartRate()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddHeartRate([Bind(Include = "UserId,Value,CreatedDate")]MeasureResult measureResult)
        {

            var userid = User.Identity.GetUserId();
            double eps = 1E-14;
            double compareV = Math.Abs(measureResult.Value % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (measureResult.Value <= 35)
            {
                ModelState.AddModelError("Value", "Пульс не може бути менше 35 ударів за хвилину");
            }
            else if (measureResult.Value >= 110)
            {
                ModelState.AddModelError("Value", "Пульс не може бути більше 110 ударів за хвилину");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 5;
                db.Entry(measureResult).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("HeartRate");
            }
            return View(measureResult);
        }

        //View
        public async Task<ActionResult> AllHeartRate()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 5).ToList();
                var newList = results.OrderByDescending(x => x.CreatedDate).ToList();

                return View(newList);
            }
        }


        //Edit
        [HttpGet]
        public ActionResult EditHeartRate(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            MeasureResult measureResult = db.MeasureResults.Find(id);
            if (measureResult != null)
            {
                return View(measureResult);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditHeartRate([Bind(Include = "Id,UserId,Value,CreatedDate")]MeasureResult measureResult)
        {
            var userid = User.Identity.GetUserId();
            double eps = 1E-14;
            double compareV = Math.Abs(measureResult.Value % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (measureResult.Value <= 35)
            {
                ModelState.AddModelError("Value", "Пульс не може бути менше 35 ударів за хвилину");
            }
            else if (measureResult.Value >= 110)
            {
                ModelState.AddModelError("Value", "Пульс не може бути більше 110 ударів за хвилину");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 5;
                db.Entry(measureResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllHeartRate");
            }
            return View(measureResult);
        }

        // Delete
        public async Task<ActionResult> DeleteHeartRate(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            if (measureResult == null)
            {
                return HttpNotFound();
            }
            return View(measureResult);
        }

        [HttpPost, ActionName("DeleteHeartRate")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedHeartRate(int id)
        {
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            db.MeasureResults.Remove(measureResult);
            await db.SaveChangesAsync();
            return RedirectToAction("AllHeartRate");
        }

        public ActionResult BloodPressure()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            DateTime compareDate = DateTime.Now.AddMonths(-1);

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<BloodPressure> results = context.BloodPressures.Where(t => t.UserId == userid && t.CreatedDate >= compareDate).ToList();
                var newList = results.OrderBy(x => x.CreatedDate).ToList();
                return View(newList);
            }

        }

        [HttpGet]
        public ActionResult AddBloodPressure()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddBloodPressure([Bind(Include = "UserId,UpPressure,LowPressure,CreatedDate")]BloodPressure bloodPressure)
        {

            var userid = User.Identity.GetUserId();
            double eps = 1E-14;
            double compareV = Math.Abs(bloodPressure.LowPressure % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (bloodPressure.LowPressure <= 40)
            {
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути нижче 40 мм рт. ст.");
            }
            else if (bloodPressure.LowPressure >= 120)
            {
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути вище 120 мм рт. ст.");
            }
            double compareV1 = Math.Abs(bloodPressure.UpPressure % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (bloodPressure.UpPressure <= 60)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути нижче 60 мм рт. ст.");
            }
            else if (bloodPressure.UpPressure >= 180)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути вище 180 мм рт. ст.");
            }
            if(bloodPressure.LowPressure>=bloodPressure.UpPressure)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути нижче або дорівнювати діастолічному");
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути вище або дорівнювати систолічному");
            }
            if (bloodPressure.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (bloodPressure.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                bloodPressure.UserId = userid;
                db.Entry(bloodPressure).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("BloodPressure");
            }
            return View(bloodPressure);
        }

        //View
        public async Task<ActionResult> AllBloodPressure()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<BloodPressure> results = context.BloodPressures.Where(t => t.UserId == userid).ToList();
                var newList = results.OrderByDescending(x => x.CreatedDate).ToList();
                return View(newList);
            }
        }


        //Edit
        [HttpGet]
        public ActionResult EditBloodPressure(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            BloodPressure bloodPressure = db.BloodPressures.Find(id);
            if (bloodPressure != null)
            {
                return View(bloodPressure);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditBloodPressure([Bind(Include = "Id,UserId,UpPressure,LowPressure,CreatedDate")]BloodPressure bloodPressure)
        {
            var userid = User.Identity.GetUserId();
            double eps = 1E-14;
            double compareV = Math.Abs(bloodPressure.LowPressure % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (bloodPressure.LowPressure <= 40)
            {
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути нижче 40 мм рт. ст.");
            }
            else if (bloodPressure.LowPressure >= 120)
            {
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути вище 120 мм рт. ст.");
            }
            double compareV1 = Math.Abs(bloodPressure.UpPressure % 1);
            if (compareV > eps)
            { ModelState.AddModelError("Value", "Вводьте ціле число"); }
            if (bloodPressure.UpPressure <= 60)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути нижче 60 мм рт. ст.");
            }
            else if (bloodPressure.UpPressure >= 180)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути вище 180 мм рт. ст.");
            }
            if (bloodPressure.LowPressure >= bloodPressure.UpPressure)
            {
                ModelState.AddModelError("UpPressure", "Систолічний тиск не може бути нижче або дорівнювати діастолічному");
                ModelState.AddModelError("LowPressure", "Діастолічний тиск не може бути вище або дорівнювати систолічному");
            }
            if (bloodPressure.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (bloodPressure.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                bloodPressure.UserId = userid;
                db.Entry(bloodPressure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllBloodPressure");
            }
            return View(bloodPressure);
        }

        // Delete
        public async Task<ActionResult> DeleteBloodPressure(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodPressure bloodPressure = await db.BloodPressures.FindAsync(id);
            if (bloodPressure == null)
            {
                return HttpNotFound();
            }
            return View(bloodPressure);
        }

        [HttpPost, ActionName("DeleteBloodPressure")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedBloodPressure(int id)
        {
            BloodPressure bloodPressure = await db.BloodPressures.FindAsync(id);
            db.BloodPressures.Remove(bloodPressure);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBloodPressure");
        }

        public ActionResult BloodSugar()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            DateTime compareDate = DateTime.Now.AddMonths(-1);

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 4 && t.CreatedDate >= compareDate).ToList();
                var newList = results.OrderBy(x => x.CreatedDate).ToList();
                return View(newList);
            }

        }

        [HttpGet]
        public ActionResult AddBloodSugar()
        {
            var userid = User.Identity.GetUserId();
            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult AddBloodSugar([Bind(Include = "UserId,Value,CreatedDate")]MeasureResult measureResult)
        {

            var userid = User.Identity.GetUserId();
            if (measureResult.Value <= 2.8)
            {
                ModelState.AddModelError("Value", "Рівень цукру у крові не може бути менше 2.8 ммоль");
            }
            else if (measureResult.Value >= 7.8)
            {
                ModelState.AddModelError("Value", "Рівень цукру у крові не може бути більше 7.8 ммоль");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }

            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 4;
                db.Entry(measureResult).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("BloodSugar");
            }
            return View(measureResult);
        }

        //View
        public async Task<ActionResult> AllBloodSugar()
        {
            var context = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (userid == null)
            {
                return RedirectToAction("ErrorPage");
            }
            else
            {
                List<MeasureResult> results = context.MeasureResults.Where(t => t.UserId == userid && t.MeasureId == 4).ToList();
                var newList = results.OrderByDescending(x => x.CreatedDate).ToList();

                return View(newList);
            }
        }


        //Edit
        [HttpGet]
        public ActionResult EditBloodSugar(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            MeasureResult measureResult = db.MeasureResults.Find(id);
            if (measureResult != null)
            {
                return View(measureResult);
            }
            return HttpNotFound();
        }


        [HttpPost]
        public ActionResult EditBloodSugar([Bind(Include = "Id,UserId,Value,CreatedDate")]MeasureResult measureResult)
        {
            var userid = User.Identity.GetUserId();
            if (measureResult.Value <= 2.8)
            {
                ModelState.AddModelError("Value", "Рівень цукру у крові не може бути менше 2.8 ммоль");
            }
            else if (measureResult.Value >= 7.8)
            {
                ModelState.AddModelError("Value", "Рівень цукру у крові не може бути більше 7.8 ммоль");
            }
            if (measureResult.CreatedDate >= DateTime.Now)
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }
            else if (measureResult.CreatedDate <= DateTime.Now.AddMonths(-1))
            {
                ModelState.AddModelError("CreatedDate", "Некоректна дата");
            }


            if (ModelState.IsValid)
            {
                measureResult.UserId = userid;
                measureResult.MeasureId = 4;
                db.Entry(measureResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllBloodSugar");
            }
            return View(measureResult);
        }

        // Delete
        public async Task<ActionResult> DeleteBloodSugar(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            if (measureResult == null)
            {
                return HttpNotFound();
            }
            return View(measureResult);
        }

        [HttpPost, ActionName("DeleteBloodSugar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedBloodSugar(int id)
        {
            MeasureResult measureResult = await db.MeasureResults.FindAsync(id);
            db.MeasureResults.Remove(measureResult);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBloodSugar");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}