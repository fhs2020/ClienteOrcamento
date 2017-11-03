using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FFCJardinagens.Models;

namespace FFCJardinagens.Controllers
{
    public class OrcamentosController : Controller
    {
        private FFCJardinagensContext db = new FFCJardinagensContext();

        // GET: Orcamentos
        public ActionResult Index()
        {
            return View(db.Orcamentoes.ToList());
        }

        // GET: Orcamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orcamento orcamento = db.Orcamentoes.Find(id);
            if (orcamento == null)
            {
                return HttpNotFound();
            }
            return View(orcamento);
        }

        // GET: Orcamentos/Create
        public ActionResult Create()
        {
            var clientes = db.Clientes.ToList();

            var clienteLista = db.Clientes.AsEnumerable().Select(c => new
            {
                ID = c.ID,
                NomeCliente = string.Format("{0} - {1} ", c.Empresa, c.Nome)
            }).ToList();

            ViewBag.Clientes = new SelectList(clienteLista, "ID", "NomeCliente");

            @ViewBag.cartCount = 0;

            return View();
        }

        // POST: Orcamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Orcamento orcamento)
        {

            var clienteLista = db.Clientes.AsEnumerable().Select(c => new
            {
                ID = c.ID,
                NomeCliente = string.Format("{0} - {1} ", c.Empresa, c.Nome)
            }).ToList();

            ViewBag.Clientes = new SelectList(clienteLista, "ID", "NomeCliente");

            if (ModelState.IsValid)
            {
                orcamento.ValorTotal = orcamento.ProdutoTotal;
                orcamento.DataOrcamento = DateTime.Now;

                db.Orcamentoes.Add(orcamento);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }

            var contador = AddCart(orcamento.ClienteID);

            @ViewBag.cartCount = contador;

            return View(orcamento);
        }


        public IList<Orcamento> getAllItems()
        {
            IList<Orcamento> orcamentos = new List<Orcamento>();
            orcamentos = db.Orcamentoes.ToList();
            return orcamentos;
        }

        public int AddCart(int ClienteID)
        {

            var orcamentoLista = db.Orcamentoes.Where(x => x.ClienteID == ClienteID).ToList();

            int count = orcamentoLista.Count();

            decimal valorTotal = 0;

            foreach (var item in orcamentoLista)
            {
                valorTotal += item.ProdutoTotal;
            }

            TotalOrcamento orcamentoTotal = new TotalOrcamento()
            {
                DataOrcamento = DateTime.Now,
                ClienteID = ClienteID,
                ValorTotal = valorTotal
            };

            db.TotalOrcamentoes.Add(orcamentoTotal);
            db.SaveChanges();

            //int count = db.Carts.Where(s => s.ClienteID == ClienteID).Count();

            return count;
        }


        public ActionResult GetCartItems(int ClienteID)
        {

            decimal sum = 0;
            var GetItemsId = db.Carts.Where(s => s.ClienteID == ClienteID).Select(u => u.OrcamentoID).ToList();
            var GetCartItem = from itemList in db.Orcamentoes where GetItemsId.Contains(itemList.ClienteID) select itemList;

            foreach (var totalsum in GetCartItem)
            {
                sum = sum + totalsum.ProdutoTotal;
            }

            ViewBag.ValorTotal = sum;

            //return PartialView("_cartItem", GetCartItem);

            return Json(GetCartItem);

        }

        public ActionResult DeleteCart(int clienteID)
        {
            Cart removeCart = db.Carts.FirstOrDefault(s => s.ClienteID == clienteID);
            db.Carts.Remove(removeCart);
            db.SaveChanges();

            return GetCartItems(clienteID);
        }




        // GET: Orcamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orcamento orcamento = db.Orcamentoes.Find(id);
            if (orcamento == null)
            {
                return HttpNotFound();
            }
            return View(orcamento);
        }

        // POST: Orcamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteID,Quantidade,Descriminação,ProdutoUnidade,ProdutoTotal,ValorTotal")] Orcamento orcamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orcamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orcamento);
        }

        // GET: Orcamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orcamento orcamento = db.Orcamentoes.Find(id);
            if (orcamento == null)
            {
                return HttpNotFound();
            }
            return View(orcamento);
        }

        // POST: Orcamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orcamento orcamento = db.Orcamentoes.Find(id);
            db.Orcamentoes.Remove(orcamento);
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
