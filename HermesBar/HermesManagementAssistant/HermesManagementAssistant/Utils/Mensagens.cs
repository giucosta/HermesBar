﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Utils.Mensagens
{
    public static class Mensagens
    {
        public static bool GeraMensagens(string titulo, string mensagemErro, List<String> erros, string tipo)
        {
            var msg = "";
            if (erros == null)
                erros = new List<string>();
            foreach (string x in erros)
                msg += x + " - ";

            if (tipo == TIPOS_MENSAGENS.SUCESSO)
                MessageBox.Show(mensagemErro + Environment.NewLine + Environment.NewLine + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
            if (tipo == TIPOS_MENSAGENS.ALERTA)
                MessageBox.Show(mensagemErro + Environment.NewLine + Environment.NewLine + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
            if (tipo == TIPOS_MENSAGENS.ERRO)
                MessageBox.Show(mensagemErro + Environment.NewLine + Environment.NewLine + msg, titulo, MessageBoxButton.OK, MessageBoxImage.Error);
            if (tipo == TIPOS_MENSAGENS.QUESTAO)
            {
                MessageBoxResult result = MessageBox.Show(mensagemErro + Environment.NewLine + Environment.NewLine + msg, titulo, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                    return true;
            }
            return false;
                
        }
    }
    public static class TIPOS_MENSAGENS
    {
        public const string ERRO = "Erro";
        public const string ALERTA = "Alerta";
        public const string SUCESSO = "Sucesso";
        public const string QUESTAO = "Questão";
    }

    public static class MENSAGEM
    {
        public const string USUARIO_CADASTRADO = "Usuário já cadastrado";
        public const string SENHA_IDENTICA = "A senha e a confirmação devem ser idênticas";
        public const string CAMPOS_OBRIGATORIOS = "Campos obrigatórios não preenchidos";
        public const string LOGIN_INVALIDO = "Login e/ou senha inválido";
        public const string NOVA_SENHA_EMAIL = "Sua nova senha foi enviada para o email cadastrado!";
        public const string ERRO_ENVIA_EMAIL = "Ocorreu um erro ao enviar email, favor consultar o administrador do sistema";
        public const string PREENCHE_CAMPO_LOGIN = "Favor preencher o campo LOGIN";
        public const string FECHAR_TELA_CONFIRMA = "Deseja fechar esta janela?";
        //Login
        public const string ERRO_GRAVAR_LOGIN = "Erro ao gravar o login";
        public const string SEM_PERMISSAO = "PERMISSÃO INVÁLIDA";
        //CADASTRO USUARIO
        public const string USUARIO_CADASTRO_SUCESSO = "Usuário cadastrado com sucesso!";
        public const string USUARIO_CADASTRO_ERRO = "Erro ao cadastrar usuário!";
        //CADASTRO ATRACOES
        public const string ATRACOES_CADASTRO_SUCESSO = "Atração cadastrada com sucesso!";
        public const string ATRACOES_CADASTRO_ERRO = "Erro ao cadastrar atração";
        public const string ATRACOES_EXCLUIR_CERTEZA = "Confirma exclusão da atração?";
        public const string ATRACOES_EXCLUIR_SUCESSO = "Atração excluida com sucesso!";
        public const string ATRACOES_EXCLUIR_ERRO = "Erro ao excluir atração";
        public const string ATRACOES_EDITAR_SUCESSO = "Atração editada com sucesso!";
        public const string ATRACOES_EDITAR_ERRO = "Erro ao editar atração";
        //CADASTRO FUNCIONARIO
        public const string FUNCIONARIO_CADASTRO_SUCESSO = "Funcionário cadastrado com sucesso!";
        public const string FUNCIONARIO_CADASTRO_ERRO = "Erro ao cadastrar funcionário";
        public const string CERTEZA_EXCLUIR_FUNCIONARIO = "Confirma exclusão do funcionário?";
        public const string FUNCIONARIO_EXCLUIR_SUCESSO= "Funcionário excluído com sucesso!";
        public const string FUNCIONARIO_EXCLUIR_ERRO = "Erro ao excluir funcionário!";
        public const string FUNCIONARIO_EDITAR_SUCESSO = "Funcionário editado com sucesso!";
        public const string FUNCIONARIO_EDITAR_ERRO = "Erro ao editar funcionário!";
        //CADASTRO ENDEREÇO
        public const string ENDERECO_CADASTRO_ERRO = "Erro ao cadastrar endereço";
        //CADASTRO CONTATO
        public const string CONTATO_CADASTRO_ERRO = "Erro ao salvar contato";
        //CADASTRO ESTABELECIMENTO
        public const string ESTABELECIMENTO_CADASTRO_SUCESSO = "Estabelecimento salvo com sucesso!";
        public const string ESTABELECIMENTO_CADASTRO_ERRO = "Erro ao salvar o estabelecimento!";
        //IMPORTACAO XML
        public const string ARQUIVO_JA_EXPORTADO = "Arquivo já exportado!";
        //TIPO PRODUTO
        public const string TIPOPRODUTO_CADASTRO_SUCESSO = "Tipo de produto cadastrado com sucesso!";
        public const string TIPOPRODUTO_CADASTRO_ERRO = "Erro ao cadastrar o tipo de produto";
        public const string TIPOPRODUTO_EXCLUIR_ERRO = "Erro ao excluir o tipo de produto";
        public const string TIPOPRODUTO_EXCLUIR_SUCESSO = "Tipo de produto excluído com sucesso!";
        public const string TIPOPRODUTO_EXCLUIR_CONFIRMA = "Confirma exclusão do tipo de produto?";
        public const string TIPOPRODUTO_EDITAR_SUCESSO = "Tipo de produto editado com sucesso!";
        public const string TIPOPRODUTO_EDITAR_ERRO = "Erro ao editar o tipo de produto";
        //PRODUTO
        public const string PRODUTO_CADASTRO_SUCESSO = "Produto cadastrado com sucesso!!";
        public const string PRODUTO_CADASTRO_ERRO = "Erro ao cadastrar produto!";
        public const string PRODUTO_EXCLUIR_SUCESSO = "Produto excluído com sucesso!";
        public const string PRODUTO_EXCLUIR_ERRO = "Erro ao excluir produto";
        public const string PRODUTO_EXCLUIR_CONFIRMA = "Confirma a exclusão do produto?";
        public const string PRODUTO_EDITAR_SUCESSO = "Produto editado com sucesso!";
        public const string PRODUTO_EDITAR_ERRO = "Erro ao editar produto!";
    }
}
