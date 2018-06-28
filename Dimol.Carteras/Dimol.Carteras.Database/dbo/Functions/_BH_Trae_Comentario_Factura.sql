
-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_BH_Trae_Comentario_Factura] 
(
	@codemp int ,
	@tpcid int ,
	@numero int,
	@numero_boleta varchar(30)
)
RETURNS varchar(198)
AS
BEGIN

declare @pclid int,
@ctcid int,
@ccbid int,
@rolid int

declare @cliente varchar(200) ='',
@tipo varchar(3) ='',
@comentario varchar(198) ='',
@asegurado varchar(198) ='',
@codigo_carga varchar(198) ='',
@numero_cpbt varchar(30) ='',
@rut_deudor varchar(12) ='',
@nombre_deudor varchar(200) ='',
@detalle varchar(200) =''

select  distinct top 1 @rolid = DCR_ROLID 
from DETALLE_COMPROBANTES_ROL
where DCR_CODEMP = @codemp
and DCR_TPCID = @tpcid
and DCR_NUMERO = @numero

select @cliente = vr.pcl_nomfant from VIEW_ROL vr where vr.rol_codemp = @codemp and vr.rol_rolid = @rolid

set @comentario = @cliente

if @tpcid = 35 begin set @tipo = 'BHM' end
if @tpcid = 59 begin set @tipo = 'BHE' end

set @comentario = @comentario + ', ' + @tipo +': ' +@numero_boleta

select top 1  @pclid = rd.rdc_pclid, 
@ctcid  = rd.RDC_CTCID,
@ccbid = rd.RDC_CCBID 
from ROL_DOCUMENTOS rd 
where rd.RDC_CODEMP = @codemp
and rd.RDC_ROLID = @rolid


select @asegurado = ', A: ' + s.sbc_nombre, 
@codigo_carga =  ', COD: ' + PCC_NOMBRE,
@numero_cpbt = ', OP: ' +CCB_NUMERO, 
@rut_deudor = dbo._Formato_Rut( d.CTC_RUT),
@nombre_deudor = d.CTC_NOMFANT
from CARTERA_CLIENTES_CPBT_DOC ccc
left join SUBCARTERAS s
on ccc.CCB_CODEMP = s.SBC_CODEMP
and ccc.CCB_SBCID = s.SBC_SBCID
left join PROVCLI_CODIGO_CARGA
on ccc.CCB_CODEMP = PCC_CODEMP
and ccc.ccb_pclid = pcc_pclid
and ccc.CCB_CODID = PCC_CODID
left join DEUDORES d
on ccc.CCB_CODEMP = d.CTC_CODEMP
and ccc.CCB_CTCID = d.CTC_CTCID
where ccc.CCB_CODEMP = @codemp
and ccc.CCB_PCLID = @pclid
and ccc.CCB_CTCID = @ctcid
and ccc.CCB_CCBID = @ccbid

set @comentario = @comentario + ', D:' + @rut_deudor +', ' + @nombre_deudor 

select top 1 @detalle = ', DET: '+i.INS_NOMBRE 
from DETALLE_COMPROBANTES dc 
inner join INSUMOS i
on i.INS_CODEMP = dc.DCC_CODEMP
and i.INS_INSID = dc.DCC_INSID
where dc.DCC_CODEMP = @codemp
and dc.DCC_TPCID = @tpcid
and dc.DCC_NUMERO = @numero

set @comentario = @comentario + @detalle

--select @comentario

select @comentario =
case  
when @pclid = 90 then (@comentario + @asegurado)
when @pclid =86 then (@comentario + @codigo_carga)
when @pclid =424 then (@comentario + @numero_cpbt)
else (@comentario) end

return  @comentario



END
