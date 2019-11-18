using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DATA.BookWorm.EF;

namespace UI.BookWorm.MVC.Controllers
{
    public class BookSeriesController : Controller
    {
        private BookWormEntities db = new BookWormEntities();

        // GET: BookSeries
        public ActionResult Index()
        {
            var bookSeries = db.BookSeries.Include(b => b.Author);
            return View(bookSeries.ToList());
        }

        // GET: BookSeries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeries bookSeries = db.BookSeries.Find(id);
            if (bookSeries == null)
            {
                return HttpNotFound();
            }
            return View(bookSeries);
        }

        // GET: BookSeries/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName");
            return View();
        }

        // POST: BookSeries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AuthorId,Title")] BookSeries bookSeries)
        {
            if (ModelState.IsValid)
            {
                db.BookSeries.Add(bookSeries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName", bookSeries.AuthorId);
            return View(bookSeries);
        }

        // GET: BookSeries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeries bookSeries = db.BookSeries.Find(id);
            if (bookSeries == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName", bookSeries.AuthorId);
            return View(bookSeries);
        }

        // POST: BookSeries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Title")] BookSeries bookSeries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookSeries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName", bookSeries.AuthorId);
            return View(bookSeries);
        }

        // GET: BookSeries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeries bookSeries = db.BookSeries.Find(id);
            if (bookSeries == null)
            {
                return HttpNotFound();
            }
            return View(bookSeries);
        }

        // POST: BookSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookSeries bookSeries = db.BookSeries.Find(id);

            try {
                db.BookSeries.Remove(bookSeries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch {
                ViewBag.Error = "A problem occurred trying to delete.";
                if (bookSeries == null)
                {
                    return HttpNotFound();
                }
                return View(bookSeries); // TO DO 
            }
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
