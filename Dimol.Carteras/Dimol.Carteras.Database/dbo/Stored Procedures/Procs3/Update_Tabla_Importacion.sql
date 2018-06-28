

Create Procedure Update_Tabla_Importacion(@tbi_nombre varchar(400), @tbi_procesada char(1)) as
  UPDATE tabla_importacion  
     SET tbi_procesada = @tbi_procesada,   
         tbi_fecproc = getdate()  
   WHERE tabla_importacion.tbi_nombre = @tbi_nombre
