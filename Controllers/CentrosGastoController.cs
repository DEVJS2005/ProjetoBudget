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
    public class CentrosGastoController : ApiController
    {
        private BDBudgetEntities db = new BDBudgetEntities();

        // GET: api/CentrosGasto
        public IQueryable<CentroGasto> GetCentroGasto()
        {
            return db.CentroGasto;
        }

        // GET: api/CentrosGasto/5
        [ResponseType(typeof(CentroGasto))]
        public async Task<IHttpActionResult> GetCentroGasto(int id)
        {
            CentroGasto centroGasto = await db.CentroGasto.FindAsync(id);
            if (centroGasto == null)
            {
                return NotFound();
            }

            return Ok(centroGasto);
        }

        // PUT: api/CentrosGasto/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCentroGasto(int id, CentroGasto centroGasto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != centroGasto.idCentroGasto)
            {
                return BadRequest();
            }

            db.Entry(centroGasto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroGastoExists(id))
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

        // POST: api/CentrosGasto
        [ResponseType(typeof(CentroGasto))]
        public async Task<IHttpActionResult> PostCentroGasto(CentroGasto centroGasto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CentroGasto.Add(centroGasto);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = centroGasto.idCentroGasto }, centroGasto);
        }

        // DELETE: api/CentrosGasto/5
        [ResponseType(typeof(CentroGasto))]
        public async Task<IHttpActionResult> DeleteCentroGasto(int id)
        {
            CentroGasto centroGasto = await db.CentroGasto.FindAsync(id);
            if (centroGasto == null)
            {
                return NotFound();
            }

            db.CentroGasto.Remove(centroGasto);
            await db.SaveChangesAsync();

            return Ok(centroGasto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CentroGastoExists(int id)
        {
            return db.CentroGasto.Count(e => e.idCentroGasto == id) > 0;
        }
    }
}