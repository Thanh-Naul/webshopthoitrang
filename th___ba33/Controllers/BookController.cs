using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using th___ba33.Models;

namespace th___ba33.Controllers
{
    public class BookController : Controller
    {
        
        // GET: Book
       public ActionResult Listbook()
        {
            BookManagerContext context= new BookManagerContext();
            var listbook = context.Books.ToList();
            return View(listbook);
        }
        [Authorize]
        public ActionResult Buy(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.SingleOrDefault(p => p.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            BookManagerContext context = new BookManagerContext();
            context.Books.AddOrUpdate(book); 
            context.SaveChanges();
            return RedirectToAction("Listbook");
        }
        public ActionResult Edit(int? id)
        {
            BookManagerContext context = new BookManagerContext();
            List<Book> listBook = context.Books.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = listBook.Find(s => s.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    BookManagerContext context = new BookManagerContext();
                    List<Book> listBook = context.Books.ToList();

                    Book dbUpdate = context.Books.FirstOrDefault(p => p.ID == book.ID);
                    dbUpdate.Title = book.Title;
                    dbUpdate.Author = book.Author;
                    dbUpdate.Description = book.Description;
                    dbUpdate.Price = book.Price;
                    dbUpdate.Images = book.Images;
                    if (dbUpdate != null)
                    {
                        context.Books.AddOrUpdate(book);
                        context.SaveChanges();
                    }
                    return View("ListBook", listBook);

                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                ModelState.AddModelError("", "Input Model Not Valide!");
                return View(book);
            }
        }
        [Authorize]
        public ActionResult delete(int id)
        {
            BookManagerContext context = new BookManagerContext();
           var book = context.Books.Find(id);
            return View (book);

        }
        [HttpPost]
        public ActionResult delete(int id, Book model)
        {
            BookManagerContext context = new BookManagerContext();
            var book = context.Books.Find(id);
            if(book == null)
            {
                return HttpNotFound();
                
            }
            context.Books.Remove(book);
            context.SaveChanges();
            return RedirectToAction("Listbook");

        }
    }
}