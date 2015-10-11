using BLL.Commom;
using DAO.Client;
using ENTITY.Client;
using ENTITY.Commom;
using MODEL.Client;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UTIL;
using HELPER;
namespace BLL.Client
{
    public class ClientBLL
    {
        #region Singleton
        private ClientDAO ClientDAO = Singleton<ClientDAO>.Instance();
        private ContactBLL ContactBLL = Singleton<ContactBLL>.Instance();
        #endregion

        public bool Insert(ClientModel client, UsuarioModel user, bool cadastroRapido = false)
        {
            try
            {
                ProcessDataForInsert(ref client);
                var cli = ConvertModelToEntity(client, user);
                if (!cadastroRapido)
                {
                    if(ClientDAO.Get(cli).Tables[0].Rows.Count != 0)
                        throw new Exceptions("Cliente já cadastrado com mesmo RG");
                }
                var con = ContactBLL.ConvertModelToEntity(client.Contato, user);
                return ClientDAO.Insert(cli, con).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ClientModel> Get(ClientModel client, UsuarioModel user)
        {
            try
            {
                var list = new List<ClientModel>();

                var result = ClientDAO.Get(ConvertModelToEntity(client, user));
                var cli = result.Tables[0].DataTableToList<HMA_CLI>();
                if (cli.Count() == 0)
                    return list;
                var con = result.Tables[1].DataTableToList<HMA_CON>();

                for (int i = 0; i < cli.Count; i++)
                    list.Add(ConvertEntityToModel(cli[i], con[i]));

                return list.OrderBy(m => m.Contato.Nome).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Inactive(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.Inactive(ConvertModelToEntity(client, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Active(ClientModel client, UsuarioModel user)
        {
            try
            {
                return ClientDAO.Active(ConvertModelToEntity(client, user)).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update(ClientModel client, UsuarioModel user)
        {
            try
            {
                ProcessDataForInsert(ref client);
                var cli = ConvertModelToEntity(client, user);
                var con = ContactBLL.ConvertModelToEntity(client.Contato, user);
                
                return ClientDAO.Update(cli, con).GetResults();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods
        private void ProcessDataForInsert(ref ClientModel client)
        {
            try
            {
                if (!string.IsNullOrEmpty(client.Contato.Telefone))
                    client.Contato.Telefone = client.Contato.Telefone.Replace("(", "").Replace(")", "").Replace("-", "");
                else 
                    client.Contato.Telefone = "";

                if (!string.IsNullOrEmpty(client.Contato.Celular))
                    client.Contato.Celular = client.Contato.Celular.Replace("(", "").Replace(")", "").Replace("-", "");
                else
                    client.Contato.Celular = "";

                if (string.IsNullOrEmpty(client.Contato.Email))
                    client.Contato.Email = "";

                if (string.IsNullOrEmpty(client.RG))
                    client.RG = "0";

                if (client.DataNascimento == DateTime.MinValue)
                    client.DataNascimento = DateTime.Now;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private HMA_CLI ConvertModelToEntity(ClientModel client, UsuarioModel user)
        {
            try
            {
                var entity = Singleton<HMA_CLI>.Instance();

                entity._ID = client.Id;
                entity.NASC = client.DataNascimento;
                entity._ATV = Convert.ToInt32(client.StatusSelected);
                entity._USR = user.Id;
                entity.RG = client.RG == null ? "0" : client.RG; 

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ClientModel ConvertEntityToModel(HMA_CLI cli, HMA_CON con)
        {
            try
            {
                var model = Singleton<ClientModel>.Instance();

                model.Id = cli._ID;
                model.StatusSelected = cli._ATV.ToString();
                model.DataNascimento = cli.NASC;
                model.Contato = ContactBLL.ConvertEntityToModel(con);
                model.RG = cli.RG;

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
