

Create Procedure Delete_Tipos_Contacto(@tic_codemp integer, @tic_ticid integer) as  
  
    DELETE FROM tipos_contacto_idiomas  
   WHERE ( tipos_contacto_idiomas.tci_codemp = @tic_codemp ) AND  
         ( tipos_contacto_idiomas.tci_ticid = @tic_ticid ) 
  
  DELETE FROM tipos_contacto  
   WHERE ( tipos_contacto.tic_codemp = @tic_codemp ) AND  
         ( tipos_contacto.tic_ticid = @tic_ticid )
