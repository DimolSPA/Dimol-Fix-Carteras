

Create Procedure Find_Importacion(@imp_codemp integer, @imp_impid integer) as
  SELECT count(importacion.imp_impid) 
    FROM importacion  
   WHERE ( importacion.imp_codemp = @imp_codemp ) AND  
         ( importacion.imp_impid = @imp_impid )
