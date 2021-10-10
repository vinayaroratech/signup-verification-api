using System;

namespace VA.Identity.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = "Test";

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
