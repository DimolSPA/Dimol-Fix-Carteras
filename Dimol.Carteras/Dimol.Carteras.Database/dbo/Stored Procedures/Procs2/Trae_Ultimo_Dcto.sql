

create  Procedure [dbo].[Trae_Ultimo_Dcto](@ccb_codemp integer,@ccb_pclid integer,	@ccb_ctcid  integer) as
  SELECT ccb_ccbid,
		 ccb_numero,
		 ccb_estcpbt,
		 ccb_codid,
		 ccb_estid 
    FROM dbo.CARTERA_CLIENTES_CPBT_DOC  
   WHERE ( ccb_codemp = @ccb_codemp ) AND  
         ( ccb_pclid = @ccb_pclid ) AND
		ccb_ctcid=@ccb_ctcid
order by ccb_numero desc
