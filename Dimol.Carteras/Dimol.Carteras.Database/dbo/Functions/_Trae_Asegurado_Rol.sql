CREATE FUNCTION [dbo].[_Trae_Asegurado_Rol] (@codemp int, @ctcid int)
RETURNS varchar(8000)
AS
BEGIN

	DECLARE @ResultVar varchar(8000)
	
		SELECT @ResultVar = COALESCE(@ResultVar + ' // ', '') + SBC_NOMBRE from 
	( select distinct SBC_NOMBRE from ROL_DOCUMENTOS rd 
	inner join CARTERA_CLIENTES_CPBT_DOC cc 
	on cc.CCB_CODEMP = rd.RDC_CODEMP 
	and cc.CCB_CTCID = rd.RDC_CTCID 
	and cc.CCB_CCBID = rd.RDC_CCBID 
	inner join SUBCARTERAS sc 
	on sc.SBC_CODEMP = cc.CCB_CODEMP 
	and sc.SBC_SBCID = cc.CCB_SBCID 
	inner join ROL r 
	on rd.RDC_CODEMP = r.ROL_CODEMP
	and rd.RDC_ROLID = r.ROL_ROLID
	where rd.RDC_CODEMP = @codemp 
	and rd.RDC_ROLID = @ctcid ) x
	
	--SELECT @ResultVar = COALESCE(@ResultVar + ' // ', '') + SBC_NOMBRE from 
	--( select distinct SBC_NOMBRE from ROL_DOCUMENTOS rd 
	--inner join CARTERA_CLIENTES_CPBT_DOC cc 
	--on cc.CCB_CODEMP = rd.RDC_CODEMP 
	--and cc.CCB_CTCID = rd.RDC_CTCID 
	--and cc.CCB_CCBID = rd.RDC_CCBID 
	--inner join SUBCARTERAS sc 
	--on sc.SBC_CODEMP = cc.CCB_CODEMP 
	--and sc.SBC_SBCID = cc.CCB_SBCID 
	--inner join ROL r 
	--on rd.RDC_CODEMP = r.ROL_CODEMP
	--and rd.RDC_ROLID = r.ROL_ROLID
	--where cc.CCB_CODEMP = @codemp 
	--and cc.CCB_CTCID = @ctcid ) x
		
	RETURN @ResultVar

END
