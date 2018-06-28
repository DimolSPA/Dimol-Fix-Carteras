CREATE procedure [dbo].[_Update_Cartera_Clientes_Cpbt_Doc_GastJud] (@codemp int, @pclid int, @ctcid int, @ccbid int, @gastjud decimal) as 

	UPDATE cartera_clientes_cpbt_doc  
	set ccb_gastjud = ccb_gastjud + @gastjud
	where (cartera_clientes_cpbt_doc.CCB_CODEMP = @codemp) and 
		  (cartera_clientes_cpbt_doc.CCB_PCLID = @pclid) and 
		  (cartera_clientes_cpbt_doc.CCB_CTCID = @ctcid) and 
		  (cartera_clientes_cpbt_doc.CCB_CCBID = @ccbid)
