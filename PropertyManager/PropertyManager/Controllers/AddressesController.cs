﻿using System;
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
    public class AddressesController : ApiController
    {
        private PropertyManagerDataContext db = new PropertyManagerDataContext();

        // GET: api/Addresses
        public IEnumerable<AddressModel> GetAddresses()
        {
            return Mapper.Map<IEnumerable<AddressModel>>(db.Addresses.Where(a => a.User.UserName == User.Identity.Name));
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(AddressModel))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = db.Addresses.FirstOrDefault(a => a.User.UserName == User.Identity.Name && a.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AddressModel>(address));
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, AddressModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.AddressId)
            {
                return BadRequest();
            }

            // find address in database by ID and assign it to dbAddress variable
            Address dbAddress = db.Addresses.FirstOrDefault(a => a.User.UserName == User.Identity.Name && a.AddressId == id);

            if(dbAddress == null)
            {
                return BadRequest();
            }
            // updates dbAddress with information passed in by address
            dbAddress.Update(address);

            // changes entity state to modified to let database know it has been changed 
            db.Entry(dbAddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [ResponseType(typeof(AddressModel))]
        public IHttpActionResult PostAddress(AddressModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbAddress = new Address(address);

            dbAddress.User = (PropertyManagerUser)db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            db.Addresses.Add(dbAddress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dbAddress.AddressId }, Mapper.Map<AddressModel>(address));
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(AddressModel))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            db.Addresses.Remove(address);
            db.SaveChanges();

            return Ok(Mapper.Map<AddressModel>(address));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return db.Addresses.Count(e => e.AddressId == id) > 0;
        }
    }
}