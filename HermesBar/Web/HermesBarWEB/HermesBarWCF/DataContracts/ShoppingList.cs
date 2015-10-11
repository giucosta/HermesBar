using BLL.ShoppingList;
using HELPER;
using MODEL.ListaCompras;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HermesBarWCF.DataContracts
{
    [DataContract]
    public class ShoppingList
    {
        private ShoppingListBLL ShoppingListBLL;
        public ShoppingList()
        {
            this.ShoppingListBLL = Singleton<ShoppingListBLL>.Instance();
        }

        public bool Insert(List<ListaComprasModel> shoppingListModel, UsuarioModel user)
        {
            try 
	        {	        
		        return ShoppingListBLL.Insert(shoppingListModel, user);
	        }
	        catch (Exception)
	        {
		        throw;
	        }
        }
    }
}