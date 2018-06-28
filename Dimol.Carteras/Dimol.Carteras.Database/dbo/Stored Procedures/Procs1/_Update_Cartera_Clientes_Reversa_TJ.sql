CREATE Procedure [dbo].[_Update_Cartera_Clientes_Reversa_TJ](@codemp integer, @pclid numeric (15), @ctcid numeric (15), @ccbid integer, @estid integer, @estcpbt char(1), @saldo decimal(15,2) ) as  
  UPDATE cartera_clientes_cpbt_doc
 Set  CCB_ESTIDJ = @estid,
                 ccb_estcpbt = @estcpbt,
                 ccb_fecultgest = getdate(),
                 ccb_calchon = 'S',
                 ccb_saldo = @saldo
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccbid )
