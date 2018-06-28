

Create Procedure Find_Cartera_Clientes_CpbtDoc_Count(@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_estcpbt char(1), @ccb_tipcart integer) as
  SELECT count(cartera_clientes_documentos_cpbt_doc.ccb_tipcart)
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid  and ccb_estcpbt = @ccb_estcpbt and ccb_tipcart = @ccb_tipcart)
