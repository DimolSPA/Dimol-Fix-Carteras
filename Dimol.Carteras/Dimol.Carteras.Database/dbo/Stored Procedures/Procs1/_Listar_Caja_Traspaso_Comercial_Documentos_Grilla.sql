CREATE PROCEDURE [dbo].[_Listar_Caja_Traspaso_Comercial_Documentos_Grilla]
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
CAJA_RECEPCION_DOCUMENTOS crd
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
where crd.CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
AND crd.ESTATUS_ID in (3,6,2)
 AND crd.DOCUMENTO_ID not in (select DOCUMENTO_ID
							from CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS 
							where ESTATUS_ID IN (4,5,7,8))
 and crd.PCLID = 90
 OR crd.CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
 and crd.FEC_REGISTRO > DATEADD(day, DATEDIFF(day, 0, GETDATE() -1), 0)
 and crd.PCLID != 90
 AND crd.ESTATUS_ID in (3,6,2)
  AND crd.DOCUMENTO_ID not in (select DOCUMENTO_ID
							from CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS 
							where ESTATUS_ID IN (4,5,7,8))'


set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END
