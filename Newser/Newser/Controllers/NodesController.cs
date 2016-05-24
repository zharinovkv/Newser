using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newser.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace Newser.Controllers
{
    public class NodeC
    {
        public Node Node { get; set; }
        public int CommentCount { get; set; }
              

        public DateTime? LastCommentDate { get; set; }
    }

    public class NodesController : Controller
    {
        // 4 урок. инициализация класса работы с бд
        private ApplicationDbContext db = new ApplicationDbContext();

        // 6 урок. доавление в ноду ссылки на автора
        private UserManager<ApplicationUser> userManager;
        public NodesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }


        // к странице допусткаются только юзеры с ролю Админ
        // GET: Nodes
        //public ActionResult Index()
        //{
        //    // делаем Лэзи лоадинг
        //    // сразу получаем ноды с комментами
        //    //return View(db.Nodes.Include(x => x.Comments).ToList());

        //    // так загружает только кол-во комментов
        //    return View(db.Nodes.Select(x => new NodeC { Node = x, CommentCount = x.Comments.Count }).ToList());
        //    //return View();
        //}



        // реализация пейджинга отсюда http://metanit.com/sharp/mvc5/17.7.php
        public ActionResult Index(int? page)
        {
            List<NodeC> ln = new List<NodeC>();
            ln = db.Nodes.Select(x => new NodeC { Node = x, CommentCount = x.Comments.Count }).ToList();

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(ln.ToPagedList(pageNumber, pageSize));
        }


        // GET: Nodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        [Authorize(Roles = "admin")]
        // GET: Nodes/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View();
        }

        // POST: Nodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,CreateDate")] Node node, HttpPostedFileBase image)
        {
            String flag = CustomServerValidation.ValidResult(node.Title);

            if (flag == "InValid")
            {
                ModelState.AddModelError("", "Заголовок должен начинаться с одной заглавной буквы");
            }


            if (node.Title.StartsWith("TTT"))
            {
                ModelState.AddModelError("", "Заголовок должен начинаться с одной большой буквы");
                return View(node);
            }

            // создание картинки

            if (image != null)
            {
            Image img = new Models.Image();
            img.Ext = Newser.Models.Image.GetExt(image.FileName);

            db.Images.Add(img);
            db.SaveChanges();

            node.Image = img;

            if (image != null)
            {
                image.SaveAs(Server.MapPath(Url.Content("~/ImagesUpload/" + img.Id +"."+img.Ext)));
            }
            }
            // конец создания картинки

            if (ModelState.IsValid)
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    node.User = userManager.FindByName(User.Identity.Name);
                }

                db.Nodes.Add(node);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(node);
        }

        // GET: Nodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,CreateDate")] Node node)
        {
            String flag = CustomServerValidation.ValidResult(node.Title);

            if (flag == "InValid")
            {
                ModelState.AddModelError("", "Заголовок должен начинаться с одной заглавной буквы");
            }

            //if (node.Title.StartsWith(" "))
            //{
            //    ModelState.AddModelError("", "Заголовок должен начинаться с буквы");
            //    return View(node);
            //}

            if (ModelState.IsValid)
            {
                db.Entry(node).State = EntityState.Modified;
                db.SaveChanges();

                // в ручную написанный код - позволяет избежать потери данных
                Node nodePrimary = db.Nodes.Find(node.Id);
                nodePrimary.Body = node.Body;
                nodePrimary.Title = node.Title;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(node);
        }

        // GET: Nodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Node node = db.Nodes.Find(id);
            db.Nodes.Remove(node);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                //userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
