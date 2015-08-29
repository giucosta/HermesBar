﻿using BLL.Commom;
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
namespace BLL.Client
{
    public class ClientBLL
    {
        private ClientDAO _clientDAO = null;
        private ClientDAO ClientDAO
        {
            get
            {
                if (_clientDAO == null)
                    _clientDAO = new ClientDAO();
                return _clientDAO;
            }
        }
        private ContatoBLL _contatoBLL = null;
        private ContatoBLL ContatoBLL
        {
            get
            {
                if (_contatoBLL == null)
                    _contatoBLL = new ContatoBLL();
                return _contatoBLL;
            }
        }

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
                var con = ContatoBLL.ConvertModelToEntity(client.Contato, user);
                return Convert.ToInt32(ClientDAO.Insert(cli, con).Rows[0]["SUCCESS"]) != 0;
                
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
                var result = ClientDAO.Get(ConvertModelToEntity(client, user));

                var cli = result.Tables[0].DataTableToList<HMA_CLI>();
                var con = result.Tables[1].DataTableToList<HMA_CON>();

                var list = new List<ClientModel>();

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
                return Convert.ToInt32(ClientDAO.Inactive(ConvertModelToEntity(client, user)).Rows[0]["SUCCESS"]) != 0;
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
                return Convert.ToInt32(ClientDAO.Active(ConvertModelToEntity(client, user)).Rows[0]["SUCCESS"]) != 0;
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
                var con = ContatoBLL.ConvertModelToEntity(client.Contato, user);
                return Convert.ToInt32(ClientDAO.Update(cli, con).Rows[0]["SUCCESS"]) != 0;
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
                var entity = new HMA_CLI();
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
                var model = new ClientModel();
                model.Id = cli._ID;
                model.StatusSelected = cli._ATV.ToString();
                model.DataNascimento = cli.NASC;
                model.Contato = ContatoBLL.ConvertEntityToModel(con);
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
