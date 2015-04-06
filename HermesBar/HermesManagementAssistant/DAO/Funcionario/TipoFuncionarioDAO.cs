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
using UTIL;

namespace DAO.Funcionario
{
    public class TipoFuncionarioDAO
    {
        public TipoFuncionarioModel Salvar(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSpecificQuery("INSERT INTO TipoFuncionario VALUES (@TipoFuncionario)");
                AO.GetCommand();
                AO.InsertParameter("TipoFuncionario", tipoFuncionario.Tipo);
                
                if(AO.ExecuteCommand())
                    return RetornaTipo(tipoFuncionario);
                return null;
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "TipoFuncionarioDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public DataTable PesquisarTipoid(int id)
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Id_TipoFuncionario", ConstantesDAO.EQUAL, id);
                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("PesquisarTipoid", "TipoFuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
        public TipoFuncionarioModel RetornaTipo(TipoFuncionarioModel tipoFuncionario)
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();
                AO.InsertParameter(ConstantesDAO.WHERE, "Tipo",ConstantesDAO.EQUAL, tipoFuncionario.Tipo);
                
                return CarregaTipoFuncionario(AO.GetDataTable());
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("RetornaTipo", "TipoFuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
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
                    tipo.Tipo = data.Rows[0]["Tipo"].ToString();

                    return tipo;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }   
        }
        public DataTable TiposFuncionarios()
        {
            try
            {
                AccessObject<TipoFuncionarioModel> AO = new AccessObject<TipoFuncionarioModel>();
                AO.CreateSelectAll();
                AO.GetCommand();

                return AO.GetDataTable();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("TiposFuncionarios", "TipoFuncionarioDAO", e.Message, Constantes.ATipoMetodo.SELECT);
                throw e;
            }
        }
    }
}
