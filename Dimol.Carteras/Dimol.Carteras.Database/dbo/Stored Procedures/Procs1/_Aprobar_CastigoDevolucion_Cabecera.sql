CREATE PROCEDURE [dbo].[_Aprobar_CastigoDevolucion_Cabecera]
(
@codemp int,
@folio int,
@tipoComprobanteid int,
@pclid int,
@estado varchar(1)
)
AS
BEGIN
	update CABACERA_COMPROBANTES
	set CBT_ESTADO = @estado
	where CBC_CODEMP = @codemp
	and CBC_NUMERO = @folio
	and CBC_TPCID = @tipoComprobanteid
	and CBC_PCLID = @pclid

END
