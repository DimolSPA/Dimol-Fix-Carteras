

Create Procedure Insertar_Clasificacion_CpbtDoc_Stock(@ccs_codemp integer, @ccs_clbid integer, @ccs_stock smallint, @ccs_saldos char(1), @ccs_reserva char(1), @ccs_transito char(1)) as

  DELETE FROM clasificacion_cpbtdoc_stock  
   WHERE ( clasificacion_cpbtdoc_stock.ccs_codemp = @ccs_codemp ) AND  
         ( clasificacion_cpbtdoc_stock.ccs_clbid = @ccs_clbid )   
           


  INSERT INTO clasificacion_cpbtdoc_stock  
         ( ccs_codemp,   
           ccs_clbid,   
           ccs_stock,   
           ccs_saldos,   
           ccs_reserva,
           ccs_transito  )  
  VALUES ( @ccs_codemp,   
           @ccs_clbid,   
           @ccs_stock,   
           @ccs_saldos,   
           @ccs_reserva,
           @ccs_transito )
