using System;
using System.Collections.Generic;

namespace SarayaAdmin.Entity.Model
{
    public partial class Role
    {
        public Role()
        {
            MenuRoleMap = new HashSet<MenuRoleMap>();
            RoleMap = new HashSet<RoleMap>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<MenuRoleMap> MenuRoleMap { get; set; }
        public virtual ICollection<RoleMap> RoleMap { get; set; }
    }
}
