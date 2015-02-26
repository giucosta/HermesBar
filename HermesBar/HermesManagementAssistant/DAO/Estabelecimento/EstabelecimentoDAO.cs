using DAO.Comum;
using DAO.Connections;
using MODEL.Estabelecimento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;
using DAO.Utils;

namespace DAO.Estabelecimento
{
    public class EstabelecimentoDAO
    {
        private EnderecoDAO _enderecoDAO = null;
        public EnderecoDAO EnderecoDAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }
        private ContatoDAO _contatoDAO = null;
        public ContatoDAO ContatoDAO
        {
            get
            {
                if (_contatoDAO == null)
                    _contatoDAO = new ContatoDAO();
                return _contatoDAO;
            }
        }
        public EstabelecimentoModel Salvar(EstabelecimentoModel estabelecimento)
        {
            try
            {
                AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
                AO.CreateDataInsert();
                var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());
                comando.Parameters.AddWithValue("@RazaoSocial",estabelecimento.RazaoSocial);
                comando.Parameters.AddWithValue("@NomeFantasia",estabelecimento.NomeFantasia);
                comando.Parameters.AddWithValue("@Cnpj",estabelecimento.Cnpj);
                comando.Parameters.AddWithValue("@InscricaoEstadual",estabelecimento.InscEstadual);
                comando.Parameters.AddWithValue("@Endereco",estabelecimento.Endereco.Id);
                comando.Parameters.AddWithValue("@Contato",estabelecimento.Contato.Id);

                if(Connection.ExecutarComando(comando))
                    return RetornaUltimoEstabelecimentoSalvo();
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar","EstabelecimentoDAO",e.StackTrace,Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        private int RetornaUltimoId()
        {
            try
            {
                AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
                AO.CreateSpecificQuery("SELECT MAX(Id_Estabelecimento) AS Id FROM Estabelecimento");
                
                var comando = new SqlCommand(AO.ReturnQuery(), Connection.GetConnection());
                var dataTable = Connection.getDataTable(comando);
                if(dataTable != null)
                    return (int)dataTable.Rows[0]["Id"];
                return 0;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaUltimoId","EstabelecimentoDAO",e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return 0;
            }
            
        }
        private EstabelecimentoModel RetornaUltimoEstabelecimentoSalvo()
        {
            var idEstabelecimento = RetornaUltimoId();
            if (idEstabelecimento == 0)
                return null;

            AccessObject<EstabelecimentoModel> AO = new AccessObject<EstabelecimentoModel>();
            AO.CreateSelectAll();
            AO.InsertParameter(ConstantesDAO.WHERE, "Id_Estabelecimento", ConstantesDAO.EQUAL, "@Id_Estabelecimento");
            
            var comando = new SqlCommand(AO.ReturnQuery(),Connection.GetConnection());
            comando.Parameters.AddWithValue("Id_Estabelecimento",idEstabelecimento);

            return PreencheEstabelecimento(Connection.getDataTable(comando));
        }
        private EstabelecimentoModel PreencheEstabelecimento(DataTable data)
        {
            if (data != null)
            {
                var estabelecimento = new EstabelecimentoModel();
                estabelecimento.Id = (int)data.Rows[0]["Id_Estabelecimento"];
                estabelecimento.RazaoSocial = data.Rows[0]["RazaoSocial"].ToString();
                estabelecimento.NomeFantasia = data.Rows[0]["NomeFantasia"].ToString();
                estabelecimento.Cnpj = data.Rows[0]["Cnpj"].ToString();
                estabelecimento.InscEstadual = data.Rows[0]["InscricaoEstadual"].ToString();
                estabelecimento.Endereco = EnderecoDAO.RecuperaEnderecoPeloId((int)data.Rows[0]["Id_Endereco"]);
                estabelecimento.Contato = ContatoDAO.RecuperaContatoPeloId((int)data.Rows[0]["Id_Contato"]);
                
                return estabelecimento;
            }
            return null;
        }
    }
}
