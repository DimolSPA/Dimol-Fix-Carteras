

create Procedure [dbo].[Update_Cartera_Clientes_Cpbt_Doc_Cod_Carga](@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer, @ccb_estid integer, @ccb_codid integer, @ccb_estcpbt varchar) as  
  UPDATE cartera_clientes_cpbt_doc
 Set ccb_estid = @ccb_estid,
                 ccb_codid = @ccb_codid,
                 ccb_fecultgest = getdate(),
				ccb_estcpbt=@ccb_estcpbt
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
