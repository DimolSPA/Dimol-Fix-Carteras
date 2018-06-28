

Create Procedure Delete_Importacion_CpbtDoc(@ipc_codemp integer, @ipc_impid integer, @ipc_sucid integer, 
															@ipc_tpcid integer, @ipc_numero numeric (15)) as
  DELETE FROM importacion_cpbtdoc  
   WHERE ( importacion_cpbtdoc.ipc_codemp = @ipc_codemp ) AND  
         ( importacion_cpbtdoc.ipc_impid = @ipc_impid ) AND  
         ( importacion_cpbtdoc.ipc_sucid = @ipc_sucid ) AND  
         ( importacion_cpbtdoc.ipc_tpcid = @ipc_tpcid ) AND  
         ( importacion_cpbtdoc.ipc_numero = @ipc_numero )
