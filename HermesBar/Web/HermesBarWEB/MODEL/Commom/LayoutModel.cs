using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Commom
{
    public class LayoutModel
    {
        public NameValueCollection Headers { get; set; }
        public string ContentType { get; set; }
        public DateTime UtcDateTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }

        public int EmailCount { get; set; }
        public List<LayoutModel> List { get; set; }
    }
}
