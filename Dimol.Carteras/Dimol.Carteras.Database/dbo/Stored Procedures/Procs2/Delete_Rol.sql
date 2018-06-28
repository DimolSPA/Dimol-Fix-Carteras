

Create Procedure Delete_Rol(@rol_codemp integer, @rol_rolid integer) as

  DELETE FROM rol_informes  
   WHERE ( rol_informes.rif_codemp = @rol_codemp ) AND  
         ( rol_informes.rif_rolid = @rol_rolid )   


  DELETE FROM rol_estados  
   WHERE ( rol_estados.rle_codemp = @rol_codemp ) AND  
         ( rol_estados.rle_rolid = @rol_rolid )   
     

  DELETE FROM rol_documentos  
   WHERE ( rol_documentos.rdc_codemp = @rol_codemp ) AND  
         ( rol_documentos.rdc_rolid = @rol_rolid ) 

  DELETE FROM rol_avedem  
   WHERE ( rad_codemp = @rol_codemp ) AND  
         ( rad_rolid = @rol_rolid ) 


  DELETE FROM rol_demandados  
   WHERE ( rol_demandados.rld_codemp = @rol_codemp ) AND  
         ( rol_demandados.rld_rolid = @rol_rolid ) 


  DELETE FROM rol  
   WHERE ( rol.rol_codemp = @rol_codemp ) AND  
         ( rol.rol_rolid = @rol_rolid )
