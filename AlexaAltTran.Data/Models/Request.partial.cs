using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaAltTran.Data.Models
{
    public partial class Request
    {
        private List<KeyValuePair<string, string>> slotsList = new List<KeyValuePair<string, string>>();

        public List<KeyValuePair<string, string>> SlotsList
        {
            get
            {
                return slotsList;
            }
            set
            {
                slotsList = value;

                var slots = new StringBuilder();

                slotsList.ForEach(s => slots.AppendFormat("{0}|{1},", s.Key, s.Value));

                Slots = slots.ToString().TrimEnd(',');
            }
        }
    }
}
