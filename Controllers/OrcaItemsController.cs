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
    public class OrcaItemsController : ApiController
    {
        private BDBudgetEntities db = new BDBudgetEntities();

        // GET: api/OrcaItems
        public IQueryable<OrcaItem> GetOrcaItem()
        {
            return db.OrcaItem;
        }

        // GET: api/OrcaItems/5
        [ResponseType(typeof(OrcaItem))]
        public async Task<IHttpActionResult> GetOrcaItem(int id)
        {
            OrcaItem orcaItem = await db.OrcaItem.FindAsync(id);
            if (orcaItem == null)
            {
                return NotFound();
            }

            return Ok(orcaItem);
        }

        // PUT: api/OrcaItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrcaItem(int id, OrcaItem orcaItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orcaItem.idorcamento)
            {
                return BadRequest();
            }

            db.Entry(orcaItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrcaItemExists(id))
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

        // POST: api/OrcaItems
        [ResponseType(typeof(OrcaItem))]
        public async Task<IHttpActionResult> PostOrcaItem(OrcaItem orcaItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrcaItem.Add(orcaItem);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrcaItemExists(orcaItem.idorcamento))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orcaItem.idorcamento }, orcaItem);
        }

        // DELETE: api/OrcaItems/5
        [ResponseType(typeof(OrcaItem))]
        public async Task<IHttpActionResult> DeleteOrcaItem(int id)
        {
            OrcaItem orcaItem = await db.OrcaItem.FindAsync(id);
            if (orcaItem == null)
            {
                return NotFound();
            }

            db.OrcaItem.Remove(orcaItem);
            await db.SaveChangesAsync();

            return Ok(orcaItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrcaItemExists(int id)
        {
            return db.OrcaItem.Count(e => e.idorcamento == id) > 0;
        }
    }
}