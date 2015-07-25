using BLL.User;
using MODEL.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.UTIL
{
    public static class GetEmail
    {
        public static LayoutModel Get()
        {
            var email = new EmailBLL("pop.gmail.com", 995, "giuliano.costa.drive@gmail.com", "dde79204041", true);
            email.Connect();
            var emails = email.FetchEmailList(1, 5);
            var model = new LayoutModel();
            model.List = new List<LayoutModel>();
            foreach (var item in emails)
            {
                model.List.Add(new LayoutModel()
                {
                    Headers = item.Headers,
                    Subject = item.Subject,
                    To = item.To,
                    From = item.From,
                    UtcDateTime = item.UtcDateTime
                });
            }
            model.EmailCount = email.GetEmailCount();

            return model;   
        }
    }
}
