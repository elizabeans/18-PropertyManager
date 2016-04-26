using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PropertyManager.Api.Models;
using System.Collections.ObjectModel;

namespace PropertyManager.Api.Domain
{
    public class Property
    {
        public Property()
        {
            Leases = new Collection<Lease>();
            WorkOrders = new Collection<WorkOrder>();
        }

        public Property(PropertyModel model)
        {
            this.Update(model);
        }

        public int PropertyId { get; set; }
        public string UserId { get; set; }

        public string PropertyName { get; set; } 
        public int? SquareFeet { get; set; }        // ? after type means nullable, don't have to put after strings because strings are nullable by default
        public int? NumberOfBedrooms { get; set; }
        public float? NumberOfBathrooms { get; set; }
        public int? NumberOfVehicles { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public bool International { get; set; }

        public virtual PropertyManagerUser User { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }

        public void Update(PropertyModel model)
        {
            PropertyName = model.PropertyName;
            SquareFeet = model.SquareFeet;
            NumberOfBedrooms = model.NumberOfBedrooms;
            NumberOfBathrooms = model.NumberOfBathrooms;
            NumberOfVehicles = model.NumberOfVehicles;
        }
    }
}