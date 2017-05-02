using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaAltTran.Data.Models
{
    public partial class Member
    {
        public Member()
        {
            this.Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string AlexaUserId { get; set; }
        public int RequestCount { get; set; }
        public System.DateTime LastRequestDate { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
