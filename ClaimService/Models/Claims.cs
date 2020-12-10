using System;
using System.Collections.Generic;

namespace ClaimService.Models
{
    public partial class Claims
    {
        public int ClaimId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Address { get; set; }
        public string DamagedItem { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Incidence { get; set; }
    }
}
