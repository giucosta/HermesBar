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
        public List<String> CarregaEstados()
        {
            string[] ufs = {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO" };
            return ufs.ToList();
        }
    }
}
