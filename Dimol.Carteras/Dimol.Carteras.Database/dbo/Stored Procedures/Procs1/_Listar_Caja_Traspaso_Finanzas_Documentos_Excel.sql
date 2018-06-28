CREATE PROCEDURE [dbo].[_Listar_Caja_Traspaso_Finanzas_Documentos_Excel]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
select distinct
	crd.REC NumeroDocumento, 
	pv.PCL_RUT RutCliente,
	pv.PCL_NOMFANT Cliente,
	d.ctc_rut RutDedor,
	d.ctc_nomfant Deudor,
	(Select SBC_RUT from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID) RutAsegurado,
	(Select SBC_NOMBRE from SUBCARTERAS where SBC_CODEMP = crd.CODEMP and SBC_SBCID = crd.SBCID)Asegurado,
	crd.FEC_REGISTRO FecIngreso,
	mon.MON_NOMBRE Moneda,
	crd.VALOR_INGRESO ValorIngreso,
	crd.MONTO_INGRESO MontoIngreso,
	(select descripcion from CAJA_CRITERIO_FACTURACION where CRITERIO_ID = crd.CRITERIO_ID) Criterio,
	crd.OBSERVACIONES,
	crd.MONTO_FACTURAR MontoFacturar
	
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
							where ESTATUS_ID = 8)
END
