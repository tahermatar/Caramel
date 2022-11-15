using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Userpermissionview
    {
        public int UserId { get; set; }
        public int? BusinessUnitId { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int ModuleId { get; set; }
        public string ModuleKey { get; set; }
        public short A1 { get; set; }
        public short A2 { get; set; }
        public short A3 { get; set; }
        public short A4 { get; set; }
        public short A5 { get; set; }
        public int A6 { get; set; }
    }
}
