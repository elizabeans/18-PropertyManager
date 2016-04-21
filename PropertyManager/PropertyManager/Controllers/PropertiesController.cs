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
using PropertyManager.Api.Infrastructure;
using PropertyManager.Api.Domain;
using PropertyManager.Api.Models;
using AutoMapper;

namespace PropertyManager.Api.Controllers
{
    [Authorize]
    public class PropertiesController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/Properties
        public IEnumerable<PropertyModel> GetProperties()
        {
            return Mapper.Map<IEnumerable<PropertyModel>>(
                db.Properties.Where(p => p.User.UserName == User.Identity.Name)
                );
        }

        // GET: api/Properties/5
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult GetProperty(int id)
        {
            //Property property = db.Properties.Find(id);

            Property property = db.Properties.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.PropertyId == id);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PropertyModel>(property));
        }

        // GET: api/Properties/5/leases
        [Route("api/properties/{PropertyId}/leases")]
        public IEnumerable<LeaseModel> GetLeasesForProperty(int propertyId)
        {
            var leases = db.Leases.Where(l => l.PropertyId == propertyId);

            return Mapper.Map<IEnumerable<LeaseModel>>(leases);
        }

        // GET: api/Properties/5/WorkOrders
        [Route("api/properties/{PropertyId}/workorders")]
        public IEnumerable<WorkOrderModel> GetWorkOrdersForProperty(int propertyId)
        {
            var workOrders = db.WorkOrders.Where(wo => wo.PropertyId == propertyId);

            return Mapper.Map<IEnumerable<WorkOrderModel>>(workOrders);
        }

        // PUT: api/Properties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProperty(int id, PropertyModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.PropertyId)
            {
                return BadRequest();
            }

            // find address in database by ID and assign it to dbAddress variable
            // var dbProperty = db.Properties.Find(id);

            Property dbProperty = db.Properties.FirstOrDefault(p => p.User.UserName == User.Identity.Name && p.PropertyId == id);

            if(dbProperty == null)
            {
                return BadRequest();
            }

            // updates dbProperty with information passed in by property
            dbProperty.Update(property);

            // changes entity state to modified to let database know it has been changed 
            db.Entry(dbProperty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult PostProperty(PropertyModel property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbProperty = new Property(property);

            dbProperty.User = (PropertyManagerUser) db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            db.Properties.Add(dbProperty);
            db.SaveChanges();

            property.PropertyId = dbProperty.PropertyId;

            var address = new Address
            {
                Address1 = dbProperty.Address.Address1,
                Address2 = dbProperty.Address.Address2,
                Address3 = dbProperty.Address.Address3,
                Address4 = dbProperty.Address.Address4,
                Address5 = dbProperty.Address.Address5,
                City = dbProperty.Address.City,
                Region = dbProperty.Address.Region,
                PostCode = dbProperty.Address.PostCode,
                UserId = dbProperty.UserId
            };

            property.AddressId = address.AddressId; 

            db.Addresses.Add(address);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dbProperty.PropertyId }, Mapper.Map<PropertyModel>(property));
        }

        // DELETE: api/Properties/5
        [ResponseType(typeof(PropertyModel))]
        public IHttpActionResult DeleteProperty(int id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(Mapper.Map<PropertyModel>(property));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(int id)
        {
            return db.Properties.Count(e => e.PropertyId == id) > 0;
        }
    }
}