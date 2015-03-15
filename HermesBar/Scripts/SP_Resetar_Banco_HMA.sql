--use HermesBar
-- EXEC SP_Resetar_Banco_HMA
-- DROP PROCEDURE SP_Resetar_Banco_HMA
CREATE PROCEDURE SP_Resetar_Banco_HMA
AS
	EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
	GO
	EXEC sp_MSForEachTable '
	IF OBJECTPROPERTY(object_id(''?''), ''TableHasForeignRef'') = 1
	DELETE FROM ?
	else
	TRUNCATE TABLE ?
	'
	GO
	EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
	GO