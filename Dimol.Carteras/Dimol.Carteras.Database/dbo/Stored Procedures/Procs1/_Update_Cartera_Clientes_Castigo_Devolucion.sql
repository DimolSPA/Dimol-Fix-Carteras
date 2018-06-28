CREATE Procedure [dbo].[_Update_Cartera_Clientes_Castigo_Devolucion](@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_ccbid integer, @ccb_estid integer, @ccb_estcpbt char(1) , @comentario varchar(4000)) as  
 
 
  UPDATE cartera_clientes_cpbt_doc
 Set ccb_estid = @ccb_estid,
                 ccb_estcpbt = @ccb_estcpbt,
                 ccb_fecultgest = getdate(),
                 ccb_comentario = @comentario,
			     CCB_SALDO = 0
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
