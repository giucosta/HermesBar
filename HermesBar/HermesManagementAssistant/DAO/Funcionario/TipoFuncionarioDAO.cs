using DAO.Connections;
using DAO.Utils;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTILS;

namespace DAO.Funcionario
{
    public class TipoFuncionarioDAO
    {
        public TipoFuncionarioModel Salvar(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSpecificQuery(@"INSERT INTO TipoFuncionario VALUES (@TipoFuncionario)");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@TipoFuncionario", tipoFuncionario.TipoFuncionario);

                if(Connection.ExecutarComando())
                    return RetornaTipo(tipoFuncionario);
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "TipoFuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.INSERT);
                return null;
            }
        }
        public TipoFuncionarioModel RetornaTipo(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSelectAll();
                AO.InsertParameter(ConstantesDAO.WHERE, "Tipo",ConstantesDAO.EQUAL, "@TipoFuncionario");
                Connection.GetCommand(AO.ReturnQuery());
                Connection.AddParameter("@TipoFuncionario",tipoFuncionario.TipoFuncionario);
                
                return CarregaTipoFuncionario(Connection.getDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaTipo", "TipoFuncionarioDAO", e.StackTrace, Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
        private TipoFuncionarioModel CarregaTipoFuncionario(DataTable data)
        {
            try
            {
                if (data.Rows.Count != 0)
                {
                    var tipo = new TipoFuncionarioModel();
                    tipo.Id = (int)data.Rows[0]["Id_TipoFuncionario"];
                    tipo.TipoFuncionario = data.Rows[0]["Tipo"].ToString();

                    return tipo;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }   
        }
        public List<String> TiposFuncionarios()
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSelectWithSimpleParameter("Tipo");
                Connection.GetCommand(AO.ReturnQuery());
                var dataTable = Connection.getDataTable();
                var listaTipos = new List<String>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    listaTipos.Add(dataTable.Rows[i]["Tipo"].ToString());

                return listaTipos;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("TiposFuncionarios","TipoFuncionarioDAO",e.StackTrace,Constantes.ATipoMetodo.SELECT);
                return null;
            }
        }
    }
}
