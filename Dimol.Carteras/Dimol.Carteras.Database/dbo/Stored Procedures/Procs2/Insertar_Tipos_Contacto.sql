

Create Procedure Insertar_Tipos_Contacto(@tic_codemp integer, @tic_ticid integer, @tic_nombre varchar (100)) as
  INSERT INTO tipos_contacto  
         ( tic_codemp,   
           tic_ticid,   
           tic_nombre )  
  VALUES ( @tic_codemp,   
           @tic_ticid,   
           @tic_nombre )
