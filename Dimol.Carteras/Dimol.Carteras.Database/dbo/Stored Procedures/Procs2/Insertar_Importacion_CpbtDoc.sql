

Create Procedure Insertar_Importacion_CpbtDoc(@ipc_codemp integer, @ipc_impid integer, @ipc_sucid integer, 
															@ipc_tpcid integer, @ipc_numero numeric (15), @ipc_tcoid integer) as
  INSERT INTO importacion_cpbtdoc  
         ( ipc_codemp,   
           ipc_impid,   
           ipc_sucid,   
           ipc_tpcid,   
           ipc_numero,   
           ipc_tcoid )  
  VALUES ( @ipc_codemp,   
           @ipc_impid,   
           @ipc_sucid,   
           @ipc_tpcid,   
           @ipc_numero,   
           @ipc_tcoid )
