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
    public class itensOrcamentariosController : ApiController
    {
        private BDBudgetEntities db = new BDBudgetEntities();

        // GET: api/itensOrcamentarios
        public IQueryable<itensOrcamentarios> GetitensOrcamentarios()
        {
            return db.itensOrcamentarios;
        }

        // GET: api/itensOrcamentarios/5
        [ResponseType(typeof(itensOrcamentarios))]
        public async Task<IHttpActionResult> s(int id)
        {
            itensOrcamentarios itensOrcamentarios = await db.itensOrcamentarios.FindAsync(id);
            if (itensOrcamentarios == null)
            {
                return NotFound();
            }

            return Ok(itensOrcamentarios);
        }

        // PUT: api/itensOrcamentarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutitensOrcamentarios(int id, itensOrcamentarios itensOrcamentarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itensOrcamentarios.idItemOrcamentario)
            {
                return BadRequest();
            }

            db.Entry(itensOrcamentarios).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itensOrcamentariosExists(id))
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

        // POST: api/itensOrcamentarios
        [ResponseType(typeof(itensOrcamentarios))]
        public async Task<IHttpActionResult> PostitensOrcamentarios(itensOrcamentarios itensOrcamentarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.itensOrcamentarios.Add(itensOrcamentarios);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = itensOrcamentarios.idItemOrcamentario }, itensOrcamentarios);
        }

        // DELETE: api/itensOrcamentarios/5
        [ResponseType(typeof(itensOrcamentarios))]
        public async Task<IHttpActionResult> DeleteitensOrcamentarios(int id)
        {
            itensOrcamentarios itensOrcamentarios = await db.itensOrcamentarios.FindAsync(id);
            if (itensOrcamentarios == null)
            {
                return NotFound();
            }

            db.itensOrcamentarios.Remove(itensOrcamentarios);
            await db.SaveChangesAsync();

            return Ok(itensOrcamentarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool itensOrcamentariosExists(int id)
        {
            return db.itensOrcamentarios.Count(e => e.idItemOrcamentario == id) > 0;
        }
    }
}