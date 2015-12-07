using DAO.Utils;
using MODEL.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTIL;

namespace DAO.Estoque
{
    public class EstoqueDAO
    {
        public bool Salvar(EstoqueModel estoque)
        {
            try
            {
                AccessObject<EstoqueModel> AO = new AccessObject<EstoqueModel>();
                AO.CreateDataInsert();
                AO.GetCommand();
                AO.InsertParameter("Produto", estoque.Produto.Id);
                AO.InsertParameter("QuantidadeEstoque", estoque.QuantidadeEstoque);
                AO.InsertParameter("EstoqueMinimo", estoque.EstoqueMinimo);
                AO.InsertParameter("EstoqueIdeal", estoque.EstoqueIdeal);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Salvar", "EstoqueDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
        public bool Editar(EstoqueModel estoque)
        {
            try
            {
                AccessObject<EstoqueModel> AO = new AccessObject<EstoqueModel>();
                AO.CreateSpecificQuery("UPDATE Estoque SET QuantidadeEstoque = @QuantidadeEstoque FROM Estoque E INNER JOIN Produto P ON E.Id_Produto = P.Id_Produto WHERE E.Id_Produto = @Id_Produto");
                AO.GetCommand();
                AO.InsertParameter("QuantidadeEstoque", estoque.QuantidadeEstoque);
                AO.InsertParameter("Id_Produto", estoque.Produto.Id);

                return AO.ExecuteCommand();
            }
            catch (Exception e)
            {
                Log.Log.GravarLog("Editar", "EstoqueDAO", e.Message, Constantes.ATipoMetodo.INSERT);
                throw e;
            }
        }
    }
}
