using MODEL.ListaCompras;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace HermesBarWCF.IContract
{
    [ServiceContract]
    public interface IShoppingList
    {
        bool Insert(List<ListaComprasModel> shoppingListModel, UsuarioModel user);
        List<ListaComprasModel> Get();
        List<ListaComprasModel> GetId(int id);
        bool InsertPurchase(ListaComprasModel shoppingListModel, UsuarioModel user, int idList);
    }
}