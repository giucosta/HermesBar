--use master;
--EXEC GerenciaBancoHMA;
-- DROP PROCEDURE GerenciaBancoHMA;
CREATE PROCEDURE GerenciaBancoHMA
AS
	IF EXISTS(SELECT * FROM sys.databases WHERE name = 'HermesBar')
	BEGIN
		DROP DATABASE HermesBar;
		CREATE DATABASE HermesBar;
	END
	ELSE
	BEGIN
		CREATE DATABASE HermesBar;
	END