-- =============================================
-- Author:		FM
-- Create date: 09-10-2014
-- Description:	Proceso aceptar comprobantes
-- =============================================
CREATE PROCEDURE [dbo].[_BH_Salida_Facturacion] (@codemp int , @desde datetime , @hasta datetime)
AS
BEGIN
	SET NOCOUNT ON;
	
select distinct 
'HP' [TipoDeDocumento]
,'N' [BoletaDirector]
,'N' [BoletaAnulada]
,case cc.CBC_TPCID when 35 then '01TALONARIO' when 59 then '02ELECTRONICA' end [TipoDeBoleta]
,CONVERT(VARCHAR(10),cc.CBC_FECCPBT,105) [FechaDelDocumento]
,CONVERT(VARCHAR(10),cc.CBC_FECCPBT,105) [FechaLibro]
,cc.CBC_NUMPROVCLI [NumeroDelDocumento]
,CONVERT(VARCHAR(10),cc.CBC_FECCPBT,105) [FechaDeContabilizacion]
,dbo._Formato_Rut_zero(p.PCL_RUT) [IdentificadorPrestadorEmisor]
, dbo._BH_Trae_Comentario_Factura(cc.CBC_CODEMP,cc.CBC_TPCID,cc.CBC_NUMERO, cc.CBC_NUMPROVCLI) [Comentario]
,'HP' [TipoDeHonorario]
,'DIMGAS000000000' [IdentificadorCentroDeNegocios]
,cast(cc.CBC_NETO as int) [MontoBruto]
, '' [AnalisisTasaDeCambioBruto]
, '' [AnalisisPorMonedaValorBruto]	
, '' [AnalisisPorFichaValorBruto]	
, '' [AnalisisPorClasificadorN1ValorBruto]	
, '' [AnalisisPorClasificadorN2ValorBruto]	
, 'M' [TipoRetencion]	
, cast(cc.CBC_RETENIDO as int) [MontoRetencion]	
, '' [AnalisisTasaDeCambioRetencion]	
, '' [AnalisisPorMonedaValorRetencion]	
, '' [AnalisisPorFichaValorRetencion]	
, '' [AnalisisPorClasificadorN1ValorRetencion]	
, '' [AnalisisPorClasificadorN2ValorRetencion]	
, cast(cc.CBC_FINAL as int) [Total]	
, '' [AnalisisTasaDeCambioTotal]	
, '' [AnalisisPorMonedaValorTotal]	
, dbo._Formato_Rut_zero(p.PCL_RUT) [AnalisisPorFichaValorTotal]	
, '' [AnalisisPorClasificadorN1ValorTotal]	
, '' [AnalisisPorClasificadorN2ValorTotal]	
, dbo._Formato_Rut_zero(p.PCL_RUT) [CodigoLegal]	
, p.PCL_NOMFANT [NombrePersona]	
,u.CIU_NOMBRE [Ciudad]	
,s.PCS_DIRECCION [DireccionPersona]
from CABACERA_COMPROBANTES cc  (nolock)
inner join DETALLE_COMPROBANTES dc (nolock)
on cc.CBC_CODEMP = dc.DCC_CODEMP
and cc.CBC_TPCID= dc.DCC_TPCID
and cc.CBC_NUMERO = dc.DCC_NUMERO
inner join PROVCLI p  (nolock)
on cc.CBC_CODEMP = p.PCL_CODEMP
and cc.CBC_PCLID = p.PCL_PCLID
inner join PROVCLI_SUCURSAL s (nolock)
on p.PCL_CODEMP = s.PCS_CODEMP
and p.PCL_PCLID = s.PCS_PCLID
inner join COMUNA c  (nolock)
on s.PCS_COMID = c.COM_COMID
inner join CIUDAD u (nolock)
on c.COM_CIUID = u.CIU_CIUID

where cc.CBC_TPCID in (35, 59)
and cc.CBC_CODEMP = @codemp
and (select COUNT(CBE_CODEMP) from CABACERA_COMPROBANTES_ESTADOS
where CBC_CODEMP = CBE_CODEMP
and CBC_TPCID = CBE_TPCID
and CBC_NUMERO = CBE_NUMERO
and CBE_ESTADO = 'F'
and CBE_FECHA > @desde
and CBE_FECHA < dateadd(day,1, @hasta ) )> 0

END
