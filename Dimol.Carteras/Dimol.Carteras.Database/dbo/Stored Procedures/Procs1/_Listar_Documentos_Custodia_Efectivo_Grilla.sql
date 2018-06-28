CREATE PROCEDURE [dbo].[_Listar_Documentos_Custodia_Efectivo_Grilla]
(
@codemp int,
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
    dc.CUSTODIA_ID CustodiaId,
	dc.FEC_DOC FecDoc,
	cli.PCL_RUT RutCliente,
	cli.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	dc.MONTO Monto,
	ges.GES_NOMBRE Gestor,
	dc.RECIBE GiradoA,
	ban.BCO_NOMBRE TipoBanco,
	dc.num_documento NumDocumento,
	tipoEstado.DESCRIPCION Estado,
	dc.FEC_PRORROGA FecProrroga,
	dc.PCLID Pclid,
	dc.CTCID Ctcid,
	dc.GESTORID GestorId,
	dc.BANCO_ID BancoId,
	dc.TIPO_ESTADO_BANCO_ID EstadoId,
	cmd.CONCILIACION_ID ConciliacionId,
	isnull(cmd.MOVIMIENTO_ID,0) MovimientoId,
	cmd.NUM_COMPROBANTE NumComprobante,
	''Pago efectivo'' MotivoSistema,
	dc.MONTO - isnull(Rebajado.Total, 0) Saldo,
	ctipo.DESCRIPCION Tipoconciliacion,
	ce.DESCRIPCION EstadoLiquidacion,
	cmd.FEC_REGISTRO FechaConciliacion,
	ce.CONCILIACION_ESTADO_ID EstadoConciliacionId
from DOCUMENTOS_CUSTODIA dc 
join CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
on dc.CUSTODIA_ID = cmd.CUSTODIA_ID 
join PROVCLI cli
on dc.PCLID = cli.PCL_PCLID
and dc.CODEMP = cli.PCL_CODEMP
join DEUDORES d
on dc.CTCID = d.CTC_CTCID
and dc.CODEMP = d.CTC_CODEMP
join GESTOR ges
on dc.CODEMP = ges.GES_CODEMP
and dc.GESTORID = ges.GES_GESID
join BANCOS ban
on dc.CODEMP = ban.BCO_CODEMP
and dc.BANCO_ID = ban.BCO_BCOID
join TESORERIA_TIPO_ESTADO_BANCO tipoEstado
on dc.TIPO_ESTADO_BANCO_ID = tipoEstado.TIPO_ESTADO_BANCO_ID
join CONCILIACION_ESTADO ce
on cmd.CONCILIACION_ESTADO_ID = ce.CONCILIACION_ESTADO_ID
join CONCILIACION_TIPO ctipo
on cmd.CONCILIACION_TIPO_ID = ctipo.CONCILIACION_TIPO_ID
left join (SELECT ConciliacionId, SUM(subtotal) Total
		FROM (
		select conciliacion_id ConciliacionId, saldo + interes + honorario + gastopre + gastojud as subtotal 
		from CONCILIACION_DOCUMENTO_IMPUTADO) GrandTotal
		group by ConciliacionId) Rebajado
on  cmd.CONCILIACION_ID = Rebajado.ConciliacionId
where dc.CODEMP = ' + CONVERT(VARCHAR,@codemp) + '
and (dc.TIPO_ESTADO_BANCO_ID = 2 or ce.CONCILIACION_ESTADO_ID = 1)
AND dc.TIPODOCUMENTO = 2'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END