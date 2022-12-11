using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProjetoBudget.Models;

namespace ProjetoBudget.Controllers
{
    public class OrcamentosController : ApiController
    {
        private BDBudgetEntities db = new BDBudgetEntities();

        // GET: api/Orcamentos
        public IQueryable<Orcamento> GetOrcamento()
        {
            return db.Orcamento;
        }

        // GET: api/Orcamentos/5
        [ResponseType(typeof(Orcamento))]
        public async Task<IHttpActionResult> GetOrcamento(int id)
        {
            Orcamento orcamento = await db.Orcamento.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }

            return Ok(orcamento);
        }

        // PUT: api/Orcamentos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrcamento(int id, Orcamento orcamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orcamento.idOrcamento)
            {
                return BadRequest();
            }

            db.Entry(orcamento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrcamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orcamentos
        [ResponseType(typeof(Orcamento))]
        public async Task<IHttpActionResult> PostOrcamento(Orcamento orcamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orcamento.Add(orcamento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = orcamento.idOrcamento }, orcamento);
        }

        // DELETE: api/Orcamentos/5
        [ResponseType(typeof(Orcamento))]
        public async Task<IHttpActionResult> DeleteOrcamento(int id)
        {
            Orcamento orcamento = await db.Orcamento.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }

            db.Orcamento.Remove(orcamento);
            await db.SaveChangesAsync();

            return Ok(orcamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrcamentoExists(int id)
        {
            return db.Orcamento.Count(e => e.idOrcamento == id) > 0;
        }
    }
}