using DAO.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Comum
{
    public class EnderecoBLL
    {
        private EnderecoDAO _enderecoDAO = null;
        public EnderecoDAO DAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }

        public EnderecoModel Salvar(EnderecoModel endereco)
        {
            return DAO.Salvar(endereco);
        }
    }
}
