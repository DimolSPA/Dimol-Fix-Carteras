

Create Procedure Insertar_Usuarios_Sucursal(@uss_codemp integer, @uss_usrid integer, @uss_sucid integer, @uss_default char (1)) as  
  INSERT INTO usuarios_sucursal  
         ( uss_codemp,   
           uss_usrid,   
           uss_sucid,   
           uss_default )  
  VALUES ( @uss_codemp,   
           @uss_usrid,   
           @uss_sucid,   
           @uss_default )
