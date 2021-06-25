
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace BookManager.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult ListBook()
        {
            BookManagerContext con = new BookManagerContext();
            var ListBook = con.Books.ToList();
            return View(ListBook);
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            BookManagerContext con = new BookManagerContext();
            Book book = con.Books.SingleOrDefault(p => p.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult create()
        {
            return View();
        }
        [Authorize]
        [HttpPost, ActionName("create")]
        [ValidateAntiForgeryToken]
        public ActionResult create([Bind(Include = "ID, Title, Description, Author, ImageCover, Price")] Book book)
        {
            BookManagerContext con = new BookManagerContext();
            List<Book> listBook = con.Books.ToList();
            if (ModelState.IsValid)
            {
                con.Books.AddOrUpdate(book);
                con.SaveChanges();

            }
            return RedirectToAction("ListBook", listBook);
        }

        public ActionResult Edit(int? id)
        {
            BookManagerContext con = new BookManagerContext();
            List<Book> listBook = con.Books.ToList();
            Book book = con.Books.Find(id);
            if (id == null)
            {
                return HttpNotFound();
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        [Authorize]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Title, Description,Author, ImageCover, Price")] Book book)
        {
            BookManagerContext con = new BookManagerContext();
            List<Book> listBook = con.Books.ToList();
            if (ModelState.IsValid)
            {
                con.Books.AddOrUpdate(book);
                con.SaveChanges();

            }
            return RedirectToAction("ListBook", listBook);
        }

        public ActionResult Delete(int? id)
        {
            BookManagerContext context = new BookManagerContext();
            List<Book> listBook = context.Books.ToList();
            Book book = context.Books.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(BookManagerContext.BadRequest);
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            BookManagerContext context = new BookManagerContext();
            List<Book> listBook = context.Books.ToList();
            Book book = context.Books.Find(id);
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("ListBook", listBook);
        }


    }
}