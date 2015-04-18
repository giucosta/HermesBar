using DAO.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

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
            try
            {
                var data = DAO.Salvar(VerificaCamposNulos(endereco));
                var enderecoModel = data.DataTableToSimpleObject<EnderecoModel>();
                enderecoModel.Id = (int)data.Rows[0]["Id_Endereco"];

                return enderecoModel;
}
            catch (Exception e)
            {
                UTIL.Session.MensagemErro = e.Message;
                return null;
            }
        }
        public List<String> CarregaEstados()
        {
            string[] ufs = {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO" };
            return ufs.ToList();
        }
        public EnderecoModel RecuperaEnderecoId(int id)
        {
            if (id != 0)
                return DAO.RecuperaEnderecoPeloId(id);
            return null;
        }
        private EnderecoModel VerificaCamposNulos(EnderecoModel endereco)
        {
            if (endereco.Bairro == null)
                endereco.Bairro = "";
            if (endereco.Cidade == null)
                endereco.Cidade = "";
            if (endereco.Complemento == null)
                endereco.Complemento = "";
            if (endereco.Estado == null)
                endereco.Estado = "";
            if (endereco.Numero == null)
                endereco.Numero = "";
            if (endereco.Rua == null)
                endereco.Rua = "";
            if (endereco.Tipo.Tipo == null)
                endereco.Tipo.Tipo = "";

            return endereco;
        }
        public bool Excluir(EnderecoModel endereco)
        {
            if(endereco.Id != 0)
                return DAO.Excluir(endereco);
            return false;
        }
        public bool Editar(EnderecoModel endereco)
        {
            return DAO.Editar(VerificaCamposNulos(endereco));
        }
    }
}
