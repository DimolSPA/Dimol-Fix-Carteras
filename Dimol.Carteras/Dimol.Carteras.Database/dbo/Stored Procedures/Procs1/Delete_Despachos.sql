

Create Procedure Delete_Despachos(@dpc_codemp integer, @dpc_sucid integer, @dpc_dpcid numeric (15)) as

  DELETE FROM despachos_encargados  
   WHERE ( despachos_encargados.dpe_codemp = @dpc_codemp ) AND  
         ( despachos_encargados.dpe_sucid = @dpc_sucid ) AND  
         ( despachos_encargados.dpe_dpcid = @dpc_dpcid )   

  DELETE FROM despachos_documentos_estados  
   WHERE ( despachos_documentos_estados.dde_codemp = @dpc_codemp ) AND  
         ( despachos_documentos_estados.dde_sucid = @dpc_sucid ) AND  
         ( despachos_documentos_estados.dde_dpcid = @dpc_dpcid ) 


  DELETE FROM despachos_documentos  
   WHERE ( despachos_documentos.dcd_codemp = @dpc_codemp ) AND  
         ( despachos_documentos.dcd_sucid = @dpc_sucid ) AND  
         ( despachos_documentos.dcd_dpcid = @dpc_dpcid )   


  DELETE FROM despachos  
   WHERE ( despachos.dpc_codemp = @dpc_codemp ) AND  
         ( despachos.dpc_sucid = @dpc_sucid ) AND  
         ( despachos.dpc_dpcid = @dpc_dpcid )
