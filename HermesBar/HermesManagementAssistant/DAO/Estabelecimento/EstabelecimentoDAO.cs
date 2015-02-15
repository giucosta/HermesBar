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
using DAO;

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
                var sql = @"INSERT INTO Estabelecimento VALUES(@RazaoSocial,@NomeFantasia,@Cnpj,@InscricaoEstadual,@Id_Endereco,@Id_Contato)";
                var comando = new SqlCommand(sql,Connection.GetConnection());
                comando.Parameters.AddWithValue("@RazaoSocial",estabelecimento.RazaoSocial);
                comando.Parameters.AddWithValue("@NomeFantasia",estabelecimento.NomeFantasia);
                comando.Parameters.AddWithValue("@Cnpj",estabelecimento.Cnpj);
                comando.Parameters.AddWithValue("@InscricaoEstadual",estabelecimento.InscEstadual);
                comando.Parameters.AddWithValue("@Id_Endereco",estabelecimento.Endereco.Id);
                comando.Parameters.AddWithValue("@Id_Contato",estabelecimento.Contato.Id);

                Connection.ExecutarComando(comando);
                return RetornaUltimoEstabelecimentoSalvo();
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
                var sql = "SELECT MAX(Id_Estabelecimento) as Id FROM Estabelecimento";
                var comando = new SqlCommand(sql, Connection.GetConnection());
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

            var sql = "SELECT * FROM Estabelecimento WHERE Id_Estabelecimento = @Id";
            var comando = new SqlCommand(sql,Connection.GetConnection());
            comando.Parameters.AddWithValue("Id",idEstabelecimento);

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
