

Create Procedure Insertar_Estados_Empleado_Idiomas(@eei_codemp integer, @eei_eemid integer, @eei_idid integer, @eei_nombre varchar(60)) as
  INSERT INTO estados_empleado_idiomas  
         ( eei_codemp,   
           eei_eemid,   
           eei_idid,   
           eei_nombre )  
  VALUES ( @eei_codemp,   
           @eei_eemid,   
           @eei_idid,   
           @eei_nombre )
