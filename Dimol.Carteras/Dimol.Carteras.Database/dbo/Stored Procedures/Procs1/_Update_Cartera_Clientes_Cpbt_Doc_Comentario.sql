CREATE PROCEDURE _Update_Cartera_Clientes_Cpbt_Doc_Comentario(
@ccb_codemp int, 
@ccb_pclid int, 
@ccb_ctcid int, 
@ccb_ccbid int, 
@ccb_comentario text) 
as
BEGIN
  UPDATE cartera_clientes_cpbt_doc  
     SET ccb_comentario = @ccb_comentario
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = @ccb_ccbid )
END
