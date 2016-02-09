using PropertyManager.Models;
using System;

namespace PropertyManager.Domain
{
    public enum Priorities
    {
        Critical = 1,
        Major = 2,
        High = 3,
        Medium = 4,
        Low = 5
    }

    public class WorkOrder
    {
        public WorkOrder()
        {

        }

        public WorkOrder(WorkOrderModel model)
        {
            this.Update(model);
        }

        public int WorkOrderId { get; set; }
        public int PropertyId { get; set; }
        public int? TenantId { get; set; }
        public string Description { get; set; }
        public Priorities Priority { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime ClosedDate { get; set; }

        public virtual Property Property { get; set; }
        public virtual Tenant Tenant { get; set; }

        public void Update(WorkOrderModel model)
        {
            PropertyId = model.PropertyId;
            TenantId = model.TenantId;
            Description = model.Description;
            Priority = model.Priority;
            OpenedDate = model.OpenedDate;
            ClosedDate = model.ClosedDate;
        }
    }
}