--use HermesBar
--EXEC SP_Gerencia_Tabelas_HMA
--DROP PROCEDURE SP_Gerencia_Tabelas_HMA
CREATE PROCEDURE SP_Gerencia_Tabelas_HMA
AS
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Login')
BEGIN
	EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
	DROP TABLE Usuario;
	DROP TABLE Login;
	DROP TABLE Perfil;
	DROP TABLE Atracoes;
	DROP TABLE Funcionario;
	DROP TABLE ConfigEstabelecimento_Estabelecimento;
	DROP TABLE ConfigEstabelecimento;
	DROP TABLE Estabelecimento;
	DROP TABLE TipoFuncionario;
	DROP TABLE Produto;
	DROP TABLE TipoProduto;
	DROP TABLE Fornecedor;
	DROP TABLE Endereco;
	DROP TABLE Contato;
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Perfil')
BEGIN
	DROP TABLE Perfil;
	CREATE TABLE Perfil( Perfil varchar(100) not null, Id_Perfil INT IDENTITY(1,1) PRIMARY KEY);
END
ELSE 
BEGIN
	CREATE TABLE Perfil( Perfil varchar(100) not null, Id_Perfil INT IDENTITY(1,1) PRIMARY KEY);
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Login')
BEGIN
	DROP TABLE Login;
	CREATE TABLE Login(Id_Login INT IDENTITY(1,1) PRIMARY KEY, Login varchar(20) not null, Senha varchar(100) not null, DataUltimoLogin DATETIME);
END
ELSE 
BEGIN
	CREATE TABLE Login(Id_Login INT IDENTITY(1,1) PRIMARY KEY, Login varchar(20) not null, Senha varchar(100) not null, DataUltimoLogin DATETIME);
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Usuario')
BEGIN
	DROP TABLE Usuario;
	CREATE TABLE Usuario(Id_Usuario INT IDENTITY(1,1) PRIMARY KEY,Id_Login INT,Id_Perfil INT,Nome varchar(100) not null,Status varchar(1) not null, Email varchar(100), FOREIGN KEY(Id_Login)REFERENCES Login(Id_Login),FOREIGN KEY(Id_Perfil)REFERENCES Perfil(Id_Perfil));
END
ELSE 
BEGIN
	CREATE TABLE Usuario(Id_Usuario INT IDENTITY(1,1) PRIMARY KEY,Id_Login INT,Id_Perfil INT,Nome varchar(100) not null,Status varchar(1) not null,FOREIGN KEY(Id_Login)REFERENCES Login(Id_Login),FOREIGN KEY(Id_Perfil)REFERENCES Perfil(Id_Perfil));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Contato')
BEGIN
	DROP TABLE Contato;
	CREATE TABLE Contato(Id_Contato INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Telefone VARCHAR(15),Celular VARCHAR(15),Email VARCHAR(100),Site VARCHAR(100));
END
ELSE 
BEGIN
	CREATE TABLE Contato(Id_Contato INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Telefone VARCHAR(15),Celular VARCHAR(15),Email VARCHAR(100),Site VARCHAR(100));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Atracoes')
BEGIN
	DROP TABLE Atracoes;
	CREATE TABLE Atracoes(Id_Atracoes INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Estilo VARCHAR(100),Id_Contato INT, Ultimo_Valor_Cobrado VARCHAR(10), Tempo_Show VARCHAR(10),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
END
ELSE 
BEGIN
	CREATE TABLE Atracoes(Id_Atracoes INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Estilo VARCHAR(100),Id_Contato INT, Ultimo_Valor_Cobrado VARCHAR(10), Tempo_Show VARCHAR(10),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'TipoFuncionario')
BEGIN
	DROP TABLE TipoFuncionario;
	CREATE TABLE TipoFuncionario(Id_TipoFuncionario INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100));
END
ELSE 
BEGIN
	CREATE TABLE TipoFuncionario(Id_TipoFuncionario INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Endereco')
BEGIN
	DROP TABLE Endereco;
	CREATE TABLE Endereco(Id_Endereco INT IDENTITY(1,1) PRIMARY KEY,Rua VARCHAR(100),Numero VARCHAR(10),Complemento VARCHAR(100) NULL,Bairro VARCHAR(100),Cep VARCHAR(10),Cidade VARCHAR(100),Estado VARCHAR(2),TipoEndereco VARCHAR(40));
END
ELSE 
BEGIN
	CREATE TABLE Endereco(Id_Endereco INT IDENTITY(1,1) PRIMARY KEY,Rua VARCHAR(100),Numero VARCHAR(10),Complemento VARCHAR(100),Bairro VARCHAR(100),Cep VARCHAR(10),Cidade VARCHAR(100),Estado VARCHAR(2),TipoEndereco VARCHAR(40));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Funcionario')
BEGIN
	DROP TABLE Funcionario;
	CREATE TABLE Funcionario(Id_Funcionario INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Cpf VARCHAR(15),Rg VARCHAR(15),DataNascimento DATETIME,CarteiraTrabalho VARCHAR(20),Serie VARCHAR(10),Id_Endereco INT,Id_TipoFuncionario INT,Id_Contato INT,DataAdmissao DATE,FOREIGN KEY(Id_Contato) REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco) REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_TipoFuncionario) REFERENCES TipoFuncionario(Id_TipoFuncionario));
END
ELSE 
BEGIN
	CREATE TABLE Funcionario(Id_Funcionario INT IDENTITY(1,1) PRIMARY KEY,Nome VARCHAR(100),Cpf VARCHAR(15),Rg VARCHAR(15),DataNascimento DATETIME,CarteiraTrabalho VARCHAR(20),Serie VARCHAR(10),Id_Endereco INT,Id_TipoFuncionario INT,Id_Contato INT,DataAdmissao DATE,FOREIGN KEY(Id_Contato) REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco) REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_TipoFuncionario) REFERENCES TipoFuncionario(Id_TipoFuncionario));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'ConfigEstabelecimento')
BEGIN
	DROP TABLE ConfigEstabelecimento;
	CREATE TABLE ConfigEstabelecimento(Id_ConfigEstabelecimento int IDENTITY(1,1) PRIMARY KEY,AgruparItensQuantidade varchar(1),TipoSistema varchar(1),QuantidadeMesa varchar(2),TaxaServico varchar(2));
END
ELSE 
BEGIN
	CREATE TABLE ConfigEstabelecimento(Id_ConfigEstabelecimento int IDENTITY(1,1) PRIMARY KEY,AgruparItensQuantidade varchar(1),TipoSistema varchar(1),QuantidadeMesa varchar(2),TaxaServico varchar(2));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Estabelecimento')
BEGIN
	DROP TABLE Estabelecimento;
	CREATE TABLE Estabelecimento(Id_Estabelecimento int IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100),NomeFantasia VARCHAR(100),Cnpj VARCHAR(30),InscricaoEstadual VARCHAR(30),Id_Endereco int,Id_Contato int,FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
END
ELSE 
BEGIN
	CREATE TABLE Estabelecimento(Id_Estabelecimento int IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100),NomeFantasia VARCHAR(100),Cnpj VARCHAR(30),InscricaoEstadual VARCHAR(30),Id_Endereco int,Id_Contato int,FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco),FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'ConfigEstabelecimento_Estabelecimento')
BEGIN
	DROP TABLE ConfigEstabelecimento_Estabelecimento;
	CREATE TABLE ConfigEstabelecimento_Estabelecimento(Id_Estabelecimento int,Id_ConfigEstabelecimento int,FOREIGN KEY(Id_Estabelecimento)REFERENCES Estabelecimento(Id_Estabelecimento),FOREIGN KEY(Id_ConfigEstabelecimento)REFERENCES ConfigEstabelecimento(Id_ConfigEstabelecimento));
END
ELSE 
BEGIN
	CREATE TABLE ConfigEstabelecimento_Estabelecimento(Id_Estabelecimento int,Id_ConfigEstabelecimento int,FOREIGN KEY(Id_Estabelecimento)REFERENCES Estabelecimento(Id_Estabelecimento),FOREIGN KEY(Id_ConfigEstabelecimento)REFERENCES ConfigEstabelecimento(Id_ConfigEstabelecimento));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'TipoProduto')
BEGIN
	DROP TABLE TipoProduto;
	CREATE TABLE TipoProduto(Id_TipoProduto INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100),Descricao VARCHAR(200));
END
ELSE 
BEGIN
	CREATE TABLE TipoProduto(Id_TipoProduto INT IDENTITY(1,1) PRIMARY KEY,Tipo VARCHAR(100),Descricao VARCHAR(200));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Fornecedor')
BEGIN
	DROP TABLE Fornecedor;
	CREATE TABLE Fornecedor(Id_Fornecedor INT IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100) NOT NULL,Cpj VARCHAR(20),InscricaoEstadual VARCHAR(20),Id_Contato INT,Id_Endereco INT,FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco));
END
ELSE 
BEGIN
	CREATE TABLE Fornecedor(Id_Fornecedor INT IDENTITY(1,1) PRIMARY KEY,RazaoSocial VARCHAR(100) NOT NULL,Cpj VARCHAR(20),InscricaoEstadual VARCHAR(20),Id_Contato INT,Id_Endereco INT,FOREIGN KEY(Id_Contato)REFERENCES Contato(Id_Contato),FOREIGN KEY(Id_Endereco)REFERENCES Endereco(Id_Endereco));
END
--
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Produto')
BEGIN
	DROP TABLE Produto;
	CREATE TABLE Produto(Id_Produto INT IDENTITY(1,1) PRIMARY KEY,CodigoOriginal VARCHAR(20),CodigoBarras VARCHAR(50),Nome VARCHAR(100),NomeReduzido VARCHAR(100),Id_TipoProduto INT,Marca VARCHAR(100),Unidade VARCHAR(10),Id_Fornecedor INT,QuantidadeEstoque INT,EstoqueMinimo INT,EstoqueIdeal INT,ValorCusto VARCHAR(10),ValorVenda VARCHAR(10),Observacao VARCHAR(200),FOREIGN KEY(Id_TipoProduto)REFERENCES TipoProduto(Id_TipoProduto),FOREIGN KEY(Id_Fornecedor)REFERENCES Fornecedor(Id_Fornecedor));
END
ELSE 
BEGIN
	CREATE TABLE Produto(Id_Produto INT IDENTITY(1,1) PRIMARY KEY,CodigoOriginal VARCHAR(20),CodigoBarras VARCHAR(50),Nome VARCHAR(100),NomeReduzido VARCHAR(100),Id_TipoProduto INT,Marca VARCHAR(100),Unidade VARCHAR(10),Id_Fornecedor INT,QuantidadeEstoque INT,EstoqueMinimo INT,EstoqueIdeal INT,ValorCusto VARCHAR(10),ValorVenda VARCHAR(10),Observacao VARCHAR(200),FOREIGN KEY(Id_TipoProduto)REFERENCES TipoProduto(Id_TipoProduto),FOREIGN KEY(Id_Fornecedor)REFERENCES Fornecedor(Id_Fornecedor));
END