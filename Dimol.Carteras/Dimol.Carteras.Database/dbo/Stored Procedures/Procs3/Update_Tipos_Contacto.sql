

Create Procedure Update_Tipos_Contacto(@tic_codemp integer, @tic_ticid integer, @tic_nombre varchar (100)) as  
  UPDATE tipos_contacto  
     SET tic_nombre = @tic_nombre  
   WHERE ( tipos_contacto.tic_codemp = @tic_codemp ) AND  
         ( tipos_contacto.tic_ticid = @tic_ticid )
