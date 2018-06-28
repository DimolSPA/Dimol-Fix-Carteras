CREATE  PROCEDURE _Listar_Caja_Traspaso_Documentos_Excel
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
	pv.PCL_RUT RutCliente,
	d.CTC_NUMERO RutDeudor,
	d.CTC_DIGITO dvDeudor,
	UPPER(d.ctc_nomfant) Deudor,
	'''' Apepat,
	'''' ApeMat,
	(select COM_NOMBRE from COMUNA where COM_COMID = d.CTC_COMID) Comuna,
	UPPER(d.CTC_DIRECCION) Direccion1,
	'''' Direccion2,
	'''' Telefono1, '''' Telefono2, '''' Telefono3, '''' Telefono4, '''' Telefono5,
	'''' Celular1, '''' Celular2, '''' Celular3, '''' Celular4, '''' Celular5,
	'''' Fax, '''' Mail1, '''' Mail2, '''' Mail3,
	'''' TipoDocumento, '''' Numero, NULL FecDoc, NULL FecVenc, '''' MotivoCobranza,
	'''' CodigoCarga,
	mon.MON_NOMBRE Moneda,
	''1,00'' TipoCambio,
	crd.MONTO_INGRESO MontoAsignado,
	crd.MONTO_INGRESO Capital,
	crd.MONTO_INGRESO Saldo,
	''0'' GastoJud,
	''0'' GastoPre,
	'''' Banco,
	'''' RutGirador,
	'''' NombreGirador,
	'''' Negocio,
	'''' NumeroAgrupar,
	(Select SBC_RUT from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID) RutAsegurado,
	(Select SBC_NOMBRE from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID)NOMBREASEGURADO,
	''S'' As DOCORI,
	''N'' as DOCANT,
	'''' Comentario,
	'''' RutTercero,
	'''' NombreTercero,
	'''' IdCuenta,
	'''' DESC_CUENTA--,
	--crd.FEC_REGISTRO FecIngreso
	
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
where crdh.DOCUMENTO_ID NOT IN (select DOCUMENTO_ID
							from CAJA_RECEPCION_DOCUMENTOS_HISTORIAL_ESTATUS 
							where ESTATUS_ID = 3)'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END
