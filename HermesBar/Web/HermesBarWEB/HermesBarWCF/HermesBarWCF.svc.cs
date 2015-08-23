﻿using HermesBarWCF.DataContracts;
using HermesBarWCF.IContract;
using MODEL.Establishment;
using MODEL.Product;
using MODEL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HermesBarWCF
{
    public class ProductService : IProduct
    {
        private Produtos _produto = null;
        public Produtos Produtos
        {
            get
            {
                if (_produto == null)
                    _produto = new Produtos();
                return _produto;
            }
        }

        public bool Insert(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Insert(produto, usuario);
        }
        public List<ProdutoModel> Get()
        {
            return Produtos.Get();
        }
        public ProdutoModel GetId(int id)
        {
            return Produtos.GetId(id);
        }
        public bool Active(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Active(produto, usuario);
        }
        public bool Inactive(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Inactive(produto, usuario);
        }
        public int GetNextCode()
        {
            return Produtos.GetNextCode();
        }
        public bool Update(ProdutoModel produto, UsuarioModel usuario)
        {
            return Produtos.Update(produto, usuario);
        }
    }
    public class LoginService : ILogin
    {
        private Login _login = null;
        private Login Login
        {
            get
            {
                if (_login == null)
                    _login = new Login();
                return _login;
            }
        }

        public UsuarioModel EfetuarLogin(UsuarioModel usuario)
        {
            return Login.EfetuarLogin(usuario);
        }
    }
    public class EstabelecimentoService : IEstablishment
    {
        private Establishment _estabelecimento = null;
        private Establishment Estabelecimento
        {
            get
            {
                if (_estabelecimento == null)
                    _estabelecimento = new Establishment();
                return _estabelecimento;
            }
        }
        public List<EstablishmentModel> Get(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Get(estabelecimento, usuario);
        }
        public bool Insert(EstablishmentModel estabelecimento, UsuarioModel usuario)
        {
            return Estabelecimento.Insert(estabelecimento, usuario);
        }
    }
}
