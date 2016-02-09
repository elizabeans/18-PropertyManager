using System.Collections.Generic;
using PropertyManager.Models;

namespace PropertyManager.Domain
{
    public class Tenant
    {
        public Tenant()
        {

        }

        public Tenant(TenantModel model)
        {
            this.Update(model);
        }
        public int TenantId { get; set; }
        public int? AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        
        public void Update(TenantModel model)
        {
            AddressId = model.AddressId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Telephone = model.Telephone;
            EmailAddress = model.EmailAddress;
        }
    }
}