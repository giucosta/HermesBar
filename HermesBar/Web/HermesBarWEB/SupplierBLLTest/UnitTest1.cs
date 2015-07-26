using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Supplier;
using ENTITY.Supplier;
using MODEL.Supplier;
using MODEL.User;

namespace SupplierBLLTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetResultSuccess()
        {
            FornecedorBLL forn = new FornecedorBLL();
            var successResult = forn.Get(new FornecedorModel() { Id = 1 }, new UsuarioModel() { Id = 1 });
            Assert.IsNotNull(successResult);
        }
        [TestMethod]
        public void GetResultError()
        {
            FornecedorBLL forn = new FornecedorBLL();
            var errorResult = forn.Get(new FornecedorModel() { Id = 5 }, new UsuarioModel() { Id = 2 });
            Assert.AreEqual(0, errorResult.Count);
        }
    }
}
