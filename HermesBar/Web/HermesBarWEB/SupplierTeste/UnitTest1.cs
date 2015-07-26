using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO.Supplier;
using ENTITY.Supplier;
using System.Data;
using System.Xml;

namespace SupplierTeste
{
    [TestClass]
    public class UnitTest1
    {
        string result = string.Empty;
        [TestMethod]
        public void Get()
        {
            FornecedorDAO dao = new FornecedorDAO();
            HMA_FOR forn = new HMA_FOR();
            forn._ID = 0;
            result = dao.Get(forn).Tables[1].Rows[0]["RAZ"].ToString();
            Assert.AreEqual("Giuliano", result);   
        }
    }
}
