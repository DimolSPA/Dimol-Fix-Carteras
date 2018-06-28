CREATE PROCEDURE [dbo].[_Valida_Monto_Ingreso_Documento](
--declare
@documentoId int,
@montoIngreso decimal(15,2)
)
AS
BEGIN
 declare @montoActual decimal(15,2) = 0;
 set @montoActual = isnull((select VALOR_INGRESO from CAJA_RECEPCION_DOCUMENTOS where DOCUMENTO_ID = @documentoId),0)
 --print @montoActual
 --print @montoIngreso
 if (@montoActual != @montoIngreso)
 begin
	select 1 Actualizar
 end
 else
 begin
 select 0 Actualizar
 end

END
