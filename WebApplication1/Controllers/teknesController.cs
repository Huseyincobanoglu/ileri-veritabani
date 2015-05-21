using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class teknesController : Controller
    {
        private MARİNE_TAKİPEntities1 db = new MARİNE_TAKİPEntities1();
        

        // GET: teknes
        public ActionResult Index()
        {
            MARİNE_TAKİPEntities1 db = new MARİNE_TAKİPEntities1();
            return View(db.teknes.ToList());
        }
        [HttpPost]
        public ActionResult Search2(string name, string distance = "0")
        {
            MARİNE_TAKİPEntities1 TD = new MARİNE_TAKİPEntities1();
            //var result = GE.Geos.Where(x => x.name.StartsWith(name)).ToList();
            double dd;
            if (distance == "") dd = 0;
            else dd = Convert.ToDouble(distance.ToString()) * 1000;

            
            string myQuery = "select g2.* "; 
            myQuery += " from tekne g1,tekne g2 ";
            myQuery += " where g1.tekne_ad LIKE '" + name + "%'  and g1.konum.STDistance(g2.konum) < " + dd + " order by g1.konum.STDistance(g2.konum) DESC";
           

            var result =TD.teknes.SqlQuery(myQuery).ToList<tekne>();



            List<mekan> lM = new List<mekan>();

            foreach (tekne myL in result)
            {
                mekan nM = new mekan()
                {
                    ID = myL.id,
                    name = myL.tekne_ad,
                    lat = Convert.ToDouble(myL.konum.Latitude),
                    lng = Convert.ToDouble(myL.konum.Longitude),
                };

                lM.Add(nM);
            }

            //var sso=Json(result, JsonRequestBehavior.AllowGet);
            var sso = Json(lM, JsonRequestBehavior.AllowGet);


            return sso;
        }
        public ActionResult Search4(string name, string distance = "0")
        {
            MARİNE_TAKİPEntities1 TD = new MARİNE_TAKİPEntities1();
            //var result = GE.Geos.Where(x => x.name.StartsWith(name)).ToList();
            double dd;
            if (distance == "") dd = 0;
            else dd = Convert.ToDouble(distance.ToString()) * 1000;

            
            string myQuery = "select g2.* ";
            myQuery += " from tekne g1,tekne g2 ";
            myQuery += " where g1.tekne_ad LIKE '" + name + "%'  and g1.konum.STDistance(g2.konum) < " + dd + " order by g1.konum.STDistance(g2.konum) DESC";


            var result = TD.teknes.SqlQuery(myQuery).ToList<tekne>();



            List<mekan> lM = new List<mekan>();

            foreach (tekne myL in result)
            {
                mekan nM = new mekan()
                {
                    ID = myL.id,
                    name = myL.tekne_ad,
                    lat = Convert.ToDouble(myL.konum.Latitude),
                    lng = Convert.ToDouble(myL.konum.Longitude),
                };

                lM.Add(nM);
            }

            //var sso=Json(result, JsonRequestBehavior.AllowGet);
            var sso = Json(lM, JsonRequestBehavior.AllowGet);


            return sso;
        }
        //[HttpPost]
        //public ActionResult Search3(string name, double distance)
        //{
        //   // MARİNE_TAKİPEntities1 TD = new MARİNE_TAKİPEntities1();
        //    //var result = TD.teknes.Where(x => x.tekne_ad.StartsWith(name)).ToList();

            
        //    string myquery = "select  g2.*,g1.konum.STDistance(g2.konum) as mesafe";

        //    myquery += "from db.teknes g1,db.teknes g2 ";

        //    myquery += "where g1.tekne_ad=" + name + "and g1.konum.STDistance(g2.konum)>" + distance + " order by mesafe desc";


        //    var result = db.teknes.SqlQuery(myquery).ToList<tekne>();


        //    List<mekan> lM = new List<mekan>();

        //    foreach (tekne myL in result)
        //    {
        //        mekan nM = new mekan()
        //        {
        //            ID = myL.id,
        //            name = myL.tekne_ad,
        //            lat = Convert.ToDouble(myL.konum.Latitude),
        //            lng = Convert.ToDouble(myL.konum.Longitude),
        //        };

        //        lM.Add(nM);
        //    }

        //    //var sso=Json(result, JsonRequestBehavior.AllowGet);
        //    var sso = Json(lM, JsonRequestBehavior.AllowGet);


        //    return sso;

        //}
        [HttpPost]
        public ActionResult Search(string Location)
        {
            MARİNE_TAKİPEntities1 TD = new MARİNE_TAKİPEntities1();
            var result = TD.teknes.Where(x => x.tekne_ad.StartsWith(Location)).ToList();
            List<mekan> lM = new List<mekan>();

            foreach (tekne myL in result)
            {
                mekan nM = new mekan()
                {
                    ID = myL.id,
                    name = myL.tekne_ad,
                    lat = Convert.ToDouble(myL.konum.Latitude),
                    lng = Convert.ToDouble(myL.konum.Longitude),
                };

                lM.Add(nM);
            }



            //var sso=Json(result, JsonRequestBehavior.AllowGet);
            var sso = Json(lM, JsonRequestBehavior.AllowGet);


            return sso;

        }
        // GET: teknes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tekne tekne = db.teknes.Find(id);
            if (tekne == null)
            {
                return HttpNotFound();
            }
            return View(tekne);
        }

        // GET: teknes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: teknes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tekne_ad,marine,konum")] tekne tekne)
        {
            if (ModelState.IsValid)
            {
                db.teknes.Add(tekne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tekne);
        }

        // GET: teknes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tekne tekne = db.teknes.Find(id);
            if (tekne == null)
            {
                return HttpNotFound();
            }
            return View(tekne);
        }

        // POST: teknes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tekne_ad,marine,konum")] tekne tekne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tekne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tekne);
        }

        // GET: teknes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tekne tekne = db.teknes.Find(id);
            if (tekne == null)
            {
                return HttpNotFound();
            }
            return View(tekne);
        }

        // POST: teknes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tekne tekne = db.teknes.Find(id);
            db.teknes.Remove(tekne);
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
    }
}
