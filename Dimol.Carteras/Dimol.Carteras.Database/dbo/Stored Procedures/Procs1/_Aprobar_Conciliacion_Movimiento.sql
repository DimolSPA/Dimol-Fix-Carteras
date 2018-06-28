CREATE PROCEDURE [dbo].[_Aprobar_Conciliacion_Movimiento]
(
--declare
@codemp int,
@conciliacionId int
)
AS
BEGIN
	
	declare @montoTotal decimal(15,2) = 0, @total decimal(15,2)= 0, @monto decimal(15,2) = 0
	SELECT @total = isnull(SUM(subtotal),0) 
	FROM (
	select conciliacion_id ConciliacionId, saldo + interes + honorario + gastopre + gastojud as subtotal 
	from CONCILIACION_DOCUMENTO_IMPUTADO
	where conciliacion_id = @conciliacionId) GrandTotal

	select @monto = ct.MONTO 
	from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
	join CARTOLA_MOVIMIENTOS ct
	on cmd.MOVIMIENTO_ID = ct.MOVIMIENTO_ID
	where cmd.CONCILIACION_ID = @conciliacionId

	set @montoTotal = @monto - @total

	if (@montoTotal = 0)
	begin 
		update CONCILIACION_MOVIMIENTOS_DOCUMENTOS
		set CONCILIACION_ESTADO_ID = 2 --Aprobado
		where CONCILIACION_ID = @conciliacionId

		select 2 proceso
	end
	else
	begin 
		update CONCILIACION_MOVIMIENTOS_DOCUMENTOS
		set CONCILIACION_ESTADO_ID = 1 --Pendiente
		where CONCILIACION_ID = @conciliacionId

		select 1 proceso
	end
END
