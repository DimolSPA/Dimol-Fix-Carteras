

Create Procedure Find_Caja_Compensacion(@cjc_codemp integer, @cjc_cjcid integer) as
  SELECT count(caja_compensacion.cjc_cjcid)  
    FROM caja_compensacion  
   WHERE ( caja_compensacion.cjc_codemp = @cjc_codemp ) AND  
         ( caja_compensacion.cjc_cjcid = @cjc_cjcid )
