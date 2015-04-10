using DAO.Cliente;
using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace BLL.Cliente
{
    public class ClienteBLL
    {
        private ClienteDAO _clienteDAO = null;
        public ClienteDAO ClienteDAO
        {
            get
            {
                if (_clienteDAO == null)
                    _clienteDAO = new ClienteDAO();
                return _clienteDAO;
            }
        }

        public bool Salvar(ClienteModel cliente)
        {
            try
            {
                return ClienteDAO.Salvar(cliente);
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return false;
            }
        }
        public ClienteModel RecuperaUltimoCliente()
        {
            try
            {
                return ClienteDAO.RecuperaUltimoCadastro().DataTableToSimpleObject<ClienteModel>();
            }
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
    }
}
