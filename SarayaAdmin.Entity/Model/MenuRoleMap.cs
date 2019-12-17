using System;
using System.Collections.Generic;

namespace SarayaAdmin.Entity.Model
{
    public partial class MenuRoleMap
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long RoleId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
