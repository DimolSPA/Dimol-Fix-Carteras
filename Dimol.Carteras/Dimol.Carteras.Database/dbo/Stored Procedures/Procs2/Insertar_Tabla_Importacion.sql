

Create Procedure Insertar_Tabla_Importacion(@tbi_nombre varchar(400), @tbi_orden integer) as
  INSERT INTO tabla_importacion  
         ( tbi_nombre,   
           tbi_orden,   
           tbi_procesada,   
           tbi_fecproc )  
  VALUES ( @tbi_nombre,   
           @tbi_orden,   
           'N',   
           null )
