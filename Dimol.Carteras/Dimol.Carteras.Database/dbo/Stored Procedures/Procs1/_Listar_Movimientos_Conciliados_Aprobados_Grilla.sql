CREATE PROCEDURE [dbo].[_Listar_Movimientos_Conciliados_Aprobados_Grilla]
(
@codemp int,
@pclid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select 
	cmd.CONCILIACION_ID ConciliacionId,
	cmd.MOVIMIENTO_ID MovimientoId,
	cmd.CUSTODIA_ID CustodiaId,
	cmd.PCLID Pclid,
	cmd.CTCID Ctcid,
	cmd.GESTORID GestorId,
	cmd.FEC_REGISTRO FechaConciliacion,
	cmd.NUM_COMPROBANTE NumComprobante,
	cli.PCL_RUT RutCliente,
	cli.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	Rebajado.TotalSaldo Capital,
	Rebajado.TotalInteres Interes,
	Rebajado.TotalHonorarios Honorarios,
	Rebajado.TotalGastos OtrosGastos,
	Rebajado.Total MontoRecuperado
from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
join CARTOLA_MOVIMIENTOS ct
on cmd.MOVIMIENTO_ID = ct.MOVIMIENTO_ID
join PROVCLI cli
on cmd.PCLID = cli.PCL_PCLID
and ct.CODEMP = cli.PCL_CODEMP
join DEUDORES d
on cmd.CTCID = d.CTC_CTCID
and ct.CODEMP = d.CTC_CODEMP
join (SELECT ConciliacionId, SUM(saldo) TotalSaldo, SUM(interes) TotalInteres, SUM(honorario) TotalHonorarios, 
	SUM(gastopre) + SUM(gastojud) TotalGastos, SUM(subtotal) Total
		FROM (
		select conciliacion_id ConciliacionId, saldo, interes, honorario, gastopre, gastojud, 
			saldo + interes + honorario + gastopre + gastojud as subtotal 
		from CONCILIACION_DOCUMENTO_IMPUTADO) GrandTotal
		group by ConciliacionId) Rebajado
on  cmd.CONCILIACION_ID = Rebajado.ConciliacionId
where ct.CODEMP = ' + CONVERT(VARCHAR,@codemp) + '
 and cmd.CONCILIACION_ESTADO_ID = 2
 and ct.NUM_CUENTA != ''1''
  and cmd.CONCILIACION_ID not in (SELECT DISTINCT CONCILIACION_ID FROM 
								REMESA_DETALLE)'

 if @pclid is not null
 begin
 set @query = @query + ' and cmd.PCLID = ' + CONVERT(VARCHAR,@pclid) + ''
 end
 
set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
