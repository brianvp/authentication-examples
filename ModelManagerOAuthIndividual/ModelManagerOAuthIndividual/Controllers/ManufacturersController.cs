using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ModelManagerOAuthIndividual.Models;

namespace ModelManagerOAuthIndividual.Controllers
{
    public class ManufacturersController : ApiController
    {
        private BikeStoreContext db = new BikeStoreContext();

        // GET: api/Manufacturers
        public IQueryable<ManufacturerListDTO> GetManufacturers()
        {
            var manufacturerList = from manufacturer in db.Manufacturers
                                   select new ManufacturerListDTO
                                   {
                                       ManufacturerName = manufacturer.Name,
                                       ManufacturerId = manufacturer.ManufacturerId
                                   };

            return manufacturerList;
        }

        // GET: api/Manufacturers/5
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult GetManufacturer(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return Ok(manufacturer);
        }

        // PUT: api/Manufacturers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutManufacturer(int id, Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }

            db.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult PostManufacturer(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [ResponseType(typeof(Manufacturer))]
        public IHttpActionResult DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            db.Manufacturers.Remove(manufacturer);
            db.SaveChanges();

            return Ok(manufacturer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManufacturerExists(int id)
        {
            return db.Manufacturers.Count(e => e.ManufacturerId == id) > 0;
        }
    }
}