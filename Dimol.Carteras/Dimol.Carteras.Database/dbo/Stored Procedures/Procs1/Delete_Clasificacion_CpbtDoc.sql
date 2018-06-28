

Create Procedure Delete_Clasificacion_CpbtDoc(@clb_codemp integer, @clb_clbid integer) as

  DELETE FROM clasificacion_cpbtdoc_stock  
   WHERE ( clasificacion_cpbtdoc_stock.ccs_codemp = @clb_codemp ) AND  
         ( clasificacion_cpbtdoc_stock.ccs_clbid = @clb_clbid )   

  DELETE FROM clasificacion_cpbtdoc_contable  
   WHERE ( clasificacion_cpbtdoc_contable.cct_codemp = @clb_codemp ) AND  
         ( clasificacion_cpbtdoc_contable.cct_clbid = @clb_clbid )   

  DELETE FROM clasificacion_cpbtdoc  
   WHERE ( clasificacion_cpbtdoc.clb_codemp = @clb_codemp ) AND  
         ( clasificacion_cpbtdoc.clb_clbid = @clb_clbid )
