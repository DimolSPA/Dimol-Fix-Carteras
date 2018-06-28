

create Procedure [dbo].[Trae_Deudor_Judicial](@ccb_codemp integer,@ccb_pclid integer,	@ccb_ctcid  integer) as

declare 
@judicial int

set @judicial =( SELECT count(  ccb_ccbid)
    FROM dbo.CARTERA_CLIENTES_CPBT_DOC  
   WHERE ( ccb_codemp = @ccb_codemp ) AND  
         ( ccb_pclid = @ccb_pclid ) AND
		ccb_ctcid=@ccb_ctcid
		and ccb_estcpbt = 'J')

if @judicial = 0 
set @judicial =(Select count(@ccb_ctcid) from view_rol where rol_codemp =@ccb_codemp
             and eci_idid = @ccb_codemp
             and tci_idid = @ccb_codemp
             and mji_idid = @ccb_codemp
            and rol_pclid =@ccb_pclid
            and rol_ctcid = @ccb_ctcid
			and rol_estid not in (71,46,144,170,174) )


select @judicial as judicial