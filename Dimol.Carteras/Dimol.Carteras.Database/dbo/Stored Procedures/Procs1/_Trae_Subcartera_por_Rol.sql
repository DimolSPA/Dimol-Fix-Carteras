-- =============================================
-- Author:		FMO
-- Create date: 23-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Subcartera_por_Rol](@codemp int, @rolid int)
AS
BEGIN
	SET NOCOUNT ON;

declare @rut varchar(20), @nombre varchar(250), @rut_asegurado varchar(20), @nombre_asegurado varchar(250),@codigo_carga varchar(120)

select @rut=PCL_RUT, @nombre=PCL_NOMFANT
from ROL_DOCUMENTOS inner join CARTERA_CLIENTES_CPBT_DOC on RDC_CODEMP = CCB_CODEMP and RDC_PCLID = CCB_PCLID and RDC_CTCID = CCB_CTCID and RDC_CCBID = CCB_CCBID
inner join PROVCLI on CCB_CODEMP = PCL_CODEMP and CCB_PCLID = PCL_PCLID
where RDC_CODEMP = @codemp
and RDC_ROLID = @rolid

select @rut_asegurado= SBC_RUT,@nombre_asegurado= SBC_NOMBRE
from ROL_DOCUMENTOS inner join CARTERA_CLIENTES_CPBT_DOC on RDC_CODEMP = CCB_CODEMP and RDC_PCLID = CCB_PCLID and RDC_CTCID = CCB_CTCID and RDC_CCBID = CCB_CCBID
inner join [SUBCARTERAS] on CCB_CODEMP = SBC_CODEMP and CCB_SBCID = SBC_SBCID
where RDC_CODEMP = @codemp 
and RDC_ROLID = @rolid

set @codigo_carga = (select top 1 PCC_NOMBRE from (select distinct PCC_NOMBRE
from ROL_DOCUMENTOS 
inner join CARTERA_CLIENTES_CPBT_DOC on RDC_CODEMP = CCB_CODEMP and RDC_PCLID = CCB_PCLID and RDC_CTCID = CCB_CTCID and RDC_CCBID = CCB_CCBID
inner join PROVCLI_CODIGO_CARGA on CCB_CODEMP = PCC_CODEMP and CCB_PCLID = PCC_PCLID and CCB_CODID = PCC_CODID
where RDC_CODEMP = @codemp
and RDC_ROLID = @rolid) as t)

declare @numeros varchar(8000)
select @numeros = coalesce(@numeros + ', ' + CCB_NUMERO, CCB_NUMERO) 
from ROL_DOCUMENTOS 
inner join CARTERA_CLIENTES_CPBT_DOC on RDC_CODEMP = CCB_CODEMP and RDC_PCLID = CCB_PCLID and RDC_CTCID = CCB_CTCID and RDC_CCBID = CCB_CCBID
where RDC_CODEMP = @codemp
and RDC_ROLID = @rolid

select  @rut rut, @nombre nombre, @rut_asegurado rut_asegurado, @nombre_asegurado nombre_asegurado,@codigo_carga codigo_carga, @numeros numero

end
      