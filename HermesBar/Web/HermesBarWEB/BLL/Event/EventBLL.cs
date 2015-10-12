using DAO.Event;
using ENTITY.Event;
using MODEL.Event;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;

namespace BLL.Event
{
    public class EventBLL
    {
        #region Singleton
        private EventDAO EventDAO = Singleton<EventDAO>.Instance();
        #endregion

        public List<EventModel> Get(EventModel evento, UsuarioModel user)
        {
            try
            {
                var result = EventDAO.Get(ConvertModelToEntity(evento, user)).DataTableToList<HMA_AGE>();
                if (result != null)
                {
                    var list = new List<EventModel>();
                    foreach (var item in result)
                        list.Add(ConvertEntityToModel(item));

                    return list;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert(EventModel evento, UsuarioModel user)
        {
            try
            {
                if (string.IsNullOrEmpty(evento.Observacao))
                    evento.Observacao = "";
                return EventDAO.Insert(ConvertModelToEntity(evento, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(EventModel evento, UsuarioModel user)
        {
            try
            {
                return EventDAO.Update(ConvertModelToEntity(evento, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private HMA_AGE ConvertModelToEntity(EventModel eve, UsuarioModel user)
        {
            try
            {
                var ent = new HMA_AGE();
                ent._ID = eve.Id;
                ent._ATV = Convert.ToInt32(eve.StatusSelected);
                ent._ID_CLI = Convert.ToInt32(eve.ClienteSelected);
                ent._USR = user.Id;
                ent.OBS = eve.Observacao;
                ent.DATA_RESER = eve.Data;
                ent.QUANT_RESR = eve.QuantidadePessoas;

                return ent;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private EventModel ConvertEntityToModel(HMA_AGE age)
        {
            try
            {
                var model = new EventModel();
                model.ClienteNome = age.CLI_NOM;
                model.ClienteSelected = age._ID_CLI.ToString();
                model.Data = age.DATA_RESER;
                model.Id = age._ID;
                model.Observacao = age.OBS;
                model.QuantidadePessoas = age.QUANT_RESR;
                model.StatusSelected = age._ATV.ToString();

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
