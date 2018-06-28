CREATE Procedure [dbo].[Update_Cartera_Clientes_Cpbt_Doc_Estados]
(@ccb_codemp integer, 
@ccb_pclid numeric (15),
@ccb_ctcid numeric (15), 
@ccb_ccbid integer, 
@ccb_estid integer, 
@ccb_estcpbt char(1) ) 
as  
begin  
declare @estij int = 0, @estingresoj int = 0;
select @estij=EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @ccb_codemp and EMC_EMCID = 66
select @estingresoj=EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @ccb_codemp and EMC_EMCID = 68
 if @ccb_estid =@estij or @ccb_estid =@estingresoj---APROBACION TRASPASO JUDICIAL o INGRESO A JUDICIAL
 begin
	 UPDATE cartera_clientes_cpbt_doc
	 Set CCB_ESTIDJ = @ccb_estid,
					 ccb_estcpbt = @ccb_estcpbt,
					 ccb_fecultgest = getdate()
	   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
 end
 else
 begin
	 UPDATE cartera_clientes_cpbt_doc
	 Set ccb_estid = @ccb_estid,
					 ccb_estcpbt = @ccb_estcpbt,
					 ccb_fecultgest = getdate()
	   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
			 ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
 end
  
end