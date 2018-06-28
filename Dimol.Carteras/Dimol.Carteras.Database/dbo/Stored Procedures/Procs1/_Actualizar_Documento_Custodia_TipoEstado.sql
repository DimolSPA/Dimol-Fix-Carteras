CREATE PROCEDURE [dbo].[_Actualizar_Documento_Custodia_TipoEstado](
@codemp int,
@custodiaId int,
@estadoId int,
@userId int)
AS
BEGIN
	
	--Actualizar Movimientos CARTOLA_MOVIMIENTOS
	update DOCUMENTOS_CUSTODIA
	set TIPO_ESTADO_BANCO_ID = @estadoId
	where CODEMP = @codemp and CUSTODIA_ID = @custodiaId

	--Ingresar al Historial
	exec dbo._Insertar_Documento_Custodia_Historial @codemp, @custodiaId, @userId
	
END

select * from DOCUMENTOS_CUSTODIA
