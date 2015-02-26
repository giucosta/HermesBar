﻿using DAO.Comum;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Comum
{
    public class EnderecoBLL
    {
        private EnderecoDAO _enderecoDAO = null;
        public EnderecoDAO DAO
        {
            get
            {
                if (_enderecoDAO == null)
                    _enderecoDAO = new EnderecoDAO();
                return _enderecoDAO;
            }
        }

        public EnderecoModel Salvar(EnderecoModel endereco)
        {
            return DAO.Salvar(VerificaCamposNulos(endereco));
        }
        public List<String> CarregaEstados()
        {
            string[] ufs = {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO" };
            return ufs.ToList();
        }
        private EnderecoModel VerificaCamposNulos(EnderecoModel endereco)
        {
            if (endereco.Bairro == null)
                endereco.Bairro = "";
            if (endereco.Cep == null)
                endereco.Cep = "";
            if (endereco.Cidade == null)
                endereco.Cidade = "";
            if (endereco.Complemento == null)
                endereco.Complemento = "";
            if (endereco.Estado == null)
                endereco.Estado = "";
            if (endereco.Numero == null)
                endereco.Numero = "";
            if (endereco.Rua == null)
                endereco.Rua = "";
            if (endereco.Tipo.Tipo == null)
                endereco.Tipo.Tipo = "";

            return endereco;
        }
        public bool Excluir(EnderecoModel endereco)
        {
            return DAO.Excluir(endereco);
        }
    }
}
