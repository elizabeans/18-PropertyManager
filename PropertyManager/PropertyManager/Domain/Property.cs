using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PropertyManager.Models;

namespace PropertyManager.Domain
{
    public class Property
    {
        public Property()
        {
        }

        public Property(PropertyModel model)
        {
            this.Update(model);
        }

        public int PropertyId { get; set; }
        public int? AddressId { get; set; }
        public string PropertyName { get; set; } 
        public int? SquareFeet { get; set; }        // ? after type means nullable, don't have to put after strings because strings are nullable by default
        public int? NumberOfBedrooms { get; set; }
        public float? NumberOfBathrooms { get; set; }
        public int? NumberOfVehicles { get; set; }
        
        public virtual Address Address { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        public void Update(PropertyModel model)
        {
            AddressId = model.AddressId;
            PropertyName = model.PropertyName;
            SquareFeet = model.SquareFeet;
            NumberOfBedrooms = model.NumberOfBedrooms;
            NumberOfBathrooms = model.NumberOfBathrooms;
            NumberOfVehicles = model.NumberOfVehicles;
        }
    }
}