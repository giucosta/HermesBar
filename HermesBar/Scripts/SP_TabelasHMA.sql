use HermesBar
CREATE PROCEDURE TabelasHMA

--Verifica se já existe o banco criado
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'HermesBar')
BEGIN
	PRINT 'Banco já criado!';
END
ELSE
BEGIN
	CREATE DATABASE HermesBar;
	GO
	USE HermesBar;

	--Verifica se já existe a tabela perfil no banco de dados!
	IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Perfil')
	BEGIN
		PRINT 'Tabela Perfil já existe';
	END
	ELSE 
	BEGIN
		CREATE TABLE Perfil( Perfil varchar(100) not null, Id_Perfil INT IDENTITY(1,1) PRIMARY KEY);
		INSERT INTO Perfil VALUES ('Administrador');
	END
	--Verifica se já existe a tabela Login no banco de dados!
	IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Login')
	BEGIN
		PRINT 'Tabela Login já existe';
	END
	ELSE 
	BEGIN
		CREATE TABLE Login(Id_Login INT IDENTITY(1,1) PRIMARY KEY, Login varchar(20) not null, Senha varchar(100) not null, DataUltimoLogin DATETIME);
		INSERT INTO Login VALUES('Administrador','Admin',NULL);
	END
END