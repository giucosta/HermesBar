using BLL.Event;
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
        private EventBLL _eventBLL = null;
        private EventBLL EventBLL
        {
            get
            {
                if (_eventBLL == null)
                    _eventBLL = new EventBLL();
                return _eventBLL;
            }
        }

        public List<EventModel> Get(EventModel evento, UsuarioModel user)
        {
            return EventBLL.Get(evento, user);
        }
    }
}