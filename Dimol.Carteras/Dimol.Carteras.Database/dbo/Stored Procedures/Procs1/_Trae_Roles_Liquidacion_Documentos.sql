CREATE PROCEDURE [dbo].[_Trae_Roles_Liquidacion_Documentos] (@codemp int, @rolId int, @sbcid int)
AS
BEGIN
		
	declare @query varchar(7000);
	set @query =
	'select
	cc.CCB_PCLID pclid, 
	cc.CCB_CTCID ctcid, 
	cc.CCB_CCBID ccbid
	from ROL_DOCUMENTOS rd
	inner join CARTERA_CLIENTES_CPBT_DOC cc
	on rd.RDC_CODEMP = cc.CCB_CODEMP
	and rd.RDC_CTCID = cc.CCB_CTCID
	and rd.RDC_CCBID = cc.CCB_CCBID
	and rd.RDC_PCLID = cc.CCB_PCLID
	join DEUDORES d
	on cc.CCB_CODEMP = d.CTC_CODEMP
	and cc.CCB_CTCID = d.CTC_CTCID
	left join SUBCARTERAS sc
	on cc.CCB_CODEMP = sc.SBC_CODEMP
	and cc.CCB_sbcid = sc.SBC_SBCID
	where rd.RDC_CODEMP = ' + CONVERT(VARCHAR,@codemp) + '
	and rd.RDC_ROLID = ' + CONVERT(VARCHAR,@rolId) + '' 
	
if @sbcid = 0
	begin  
		set @query = @query + ' and cc.CCB_SBCID is null';
	end 
	else
	begin 
		set @query = @query + ' and cc.CCB_SBCID = ' + CONVERT(VARCHAR,@sbcid) + '';
	end 
	
exec(@query)	
	
END
