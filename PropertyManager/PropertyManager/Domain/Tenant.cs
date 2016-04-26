using System.Collections.Generic;
using PropertyManager.Api.Models;
using System.Collections.ObjectModel;

namespace PropertyManager.Api.Domain
{
    public class Tenant
    {
        public Tenant()
        {
            Leases = new Collection<Lease>();
            WorkOrders = new Collection<WorkOrder>();
        }

        public Tenant(TenantModel model)
        {
            this.Update(model);
        }
        public int TenantId { get; set; }
        public string UserId { get; set; }

        public int? PropertyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

        public virtual Property Property { get; set; }
        public virtual PropertyManagerUser User { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        
        public void Update(TenantModel model)
        {
            PropertyId = model.PropertyId;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Telephone = model.Telephone;
            EmailAddress = model.EmailAddress;
        }
    }
}