CREATE PROCEDURE [dbo].[_Listar_Caja_Traspaso_Finanzas_Documentos_Grilla]
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
  
set @query = @query + 'select distinct
	crd.DOCUMENTO_ID DocumentoId,  
	crd.REC NumeroDocumento, 
	pv.PCL_RUT RutCliente,
	pv.PCL_NOMFANT Cliente,
	d.ctc_rut RutDedor,
	d.ctc_nomfant Deudor,
	(Select SBC_RUT from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID) RutAsegurado,
	(Select SBC_NOMBRE from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID)Asegurado,
	crd.FEC_REGISTRO FecIngreso,
	mon.MON_NOMBRE Moneda,
	crd.MONTO_INGRESO MontoIngreso,
	crd.pclid,
	crd.ctcid,
	crd.sbcid,
	est.ESTATUS_ID EstatusId,
	crd.CRITERIO_ID CriterioId,
	crd.OBSERVACIONES,
	crd.MONTO_FACTURAR MontoFacturar,
	crd.VALOR_INGRESO ValorIngreso
from 
CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS crdh
join CAJA_RECEPCION_DOCUMENTOS crd
on crdh.DOCUMENTO_ID = crd.DOCUMENTO_ID
join PROVCLI pv
on crd.CODEMP = pv.PCL_CODEMP
and crd.PCLID = pv.PCL_PCLID
join DEUDORES d
on crd.CODEMP = d.CTC_CODEMP 
and crd.CTCID = d.CTC_CTCID 
join MONEDAS mon
on crd.CODEMP = mon.MON_CODEMP
and crd.CODMON = mon.MON_CODMON
join CAJA_DOCUMENTOS_ESTATUS est
on crd.ESTATUS_ID = est.ESTATUS_ID
where crdh.ESTATUS_ID = 4 
 and crdh.DOCUMENTO_ID NOT IN (select DOCUMENTO_ID
							from CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS 
							where ESTATUS_ID = 8)'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END
