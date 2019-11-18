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
    public class BooksController : Controller
    {
        private BookWormEntities db = new BookWormEntities();
        string[] validExts = { ".jpg", ".jpeg", ".png", ".gif" };


        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.BookSeries);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }
            else {
                ViewBag.RelatedBooks = GetRelatedBooks(book);
            }

            return View(book);
        }

        public List<Book> GetRelatedBooks(Book currentBook) {
            int authorId = currentBook.AuthorId;
            int? seriesId = currentBook.BookSeriesId;

            List <Book> books = (from b in db.Books
                     where b.AuthorId == authorId && b.BookSeriesId == seriesId && b.Id != currentBook.Id
                     select b).Take(5).ToList();

            return books;
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var seriesSelectList = new List<SelectListItem>();
            seriesSelectList.Add(new SelectListItem() { Text = "- Select a Book Series -", Value = string.Empty });

            var BookSeries = db.BookSeries.ToList();
            foreach(var bs in BookSeries) {
                seriesSelectList.Add(new SelectListItem() { Text = bs.Title, Value = bs.Id.ToString() });
            };

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName");
            ViewBag.BookSeriesId = seriesSelectList;
            return View();
        }


        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AuthorId,Title,Description,StarRating,BookSeriesId,CoverArt")] Book book, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                string fileName = "no-image-icon.png";

                if (file != null)
                {
                    fileName = file.FileName;
                    string ext = fileName.Substring(fileName.LastIndexOf("."));

                    // if user submitted file extension is in list, 
                    // assign a GUID for filname and concatenate the ext
                    if (validExts.Contains(ext.ToLower()))
                    {
                        fileName = Guid.NewGuid() + ext;

                        // Save the file to the webserver
                        file.SaveAs(Server.MapPath("~/Images/" + fileName));
                    }
                    else
                    {
                        fileName = "no-image-icon.png";
                    }
                }

                book.CoverArt = fileName;

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.BookSeriesId = new SelectList(db.BookSeries, "Id", "Title", book.BookSeriesId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var seriesSelectList = new List<SelectListItem>();
            var BookSeries = db.BookSeries.ToList();

            seriesSelectList.Add(new SelectListItem() { Text = "- Select a Book Series -", Value = string.Empty });

            foreach (var bs in BookSeries)
            {
                seriesSelectList.Add(new SelectListItem() {
                    Text = bs.Title,
                    Value = bs.Id.ToString(),
                    Selected = bs.Id == book.Id ? true : false

                });
            };
            ViewBag.BookSeriesId = seriesSelectList;
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FullName", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Title,Description,StarRating,BookSeriesId,CoverArt")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string imageName = file.FileName;
                    string ext = imageName.Substring(imageName.LastIndexOf("."));
                    
                    if (validExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;

                        // Save the file to the webserver 
                        file.SaveAs(Server.MapPath("~/Images/" + imageName));

                        book.CoverArt = imageName;
                    }
                }

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.BookSeriesId = new SelectList(db.BookSeries, "Id", "Title", book.BookSeriesId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
