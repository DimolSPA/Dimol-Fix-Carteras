

Create Procedure Delete_Importacion(@imp_codemp integer, @imp_impid integer) as

  DELETE FROM importacion_cpbtdoc  
   WHERE ( importacion_cpbtdoc.ipc_codemp = @imp_codemp ) AND  
         ( importacion_cpbtdoc.ipc_impid = @imp_impid )   
      
   DELETE FROM importacion  
   WHERE ( importacion.imp_codemp = @imp_codemp ) AND  
         ( importacion.imp_impid = @imp_impid )
