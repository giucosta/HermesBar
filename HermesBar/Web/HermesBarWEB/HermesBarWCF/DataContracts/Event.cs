using BLL.Event;
using HELPER;
using MODEL.Event;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class Event
    {
        private EventBLL EventBLL;
        public Event()
        {
            this.EventBLL = Singleton<EventBLL>.Instance();
        }

        public List<EventModel> Get(EventModel evento, UsuarioModel user)
        {
            return EventBLL.Get(evento, user);
        }
        public bool Insert(EventModel evento, UsuarioModel user)
        {
            return EventBLL.Insert(evento, user);
        }
        public bool Update(EventModel evento, UsuarioModel user)
        {
            return EventBLL.Update(evento, user);
        }
    }
}