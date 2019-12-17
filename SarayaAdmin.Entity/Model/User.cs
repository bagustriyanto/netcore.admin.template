using System;
using System.Collections.Generic;

namespace SarayaAdmin.Entity.Model
{
    public partial class User
    {
        public long Id { get; set; }
        public long CredentialId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedHost { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedHost { get; set; }

        public virtual Credentials Credential { get; set; }
    }
}
