

Create Procedure Insertar_Clasificacion_CpbtDoc_Contable(@cct_codemp integer, @cct_clbid integer, @cct_debhab smallint,
																		    @cct_libcomven char(1), @cct_honorarios char(1), @cct_pctid integer, @cct_pctid2 integer) as  

  DELETE FROM clasificacion_cpbtdoc_contable  
   WHERE ( clasificacion_cpbtdoc_contable.cct_codemp = @cct_codemp ) AND  
         ( clasificacion_cpbtdoc_contable.cct_clbid = @cct_clbid )   


  INSERT INTO clasificacion_cpbtdoc_contable  
         ( cct_codemp,   
           cct_clbid,   
           cct_debhab,   
           cct_libcomven,   
           cct_honorarios,   
           cct_pctid,   
           cct_pctid2 )  
  VALUES ( @cct_codemp,   
           @cct_clbid,   
           @cct_debhab,   
           @cct_libcomven,   
           @cct_honorarios,   
           @cct_pctid,   
           @cct_pctid2 )
