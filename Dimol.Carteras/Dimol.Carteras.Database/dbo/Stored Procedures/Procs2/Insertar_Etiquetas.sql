

  Create Procedure Insertar_Etiquetas(@etq_etqid integer, @etq_codigo varchar(8), @etq_descripcion varchar(100)) as
  INSERT INTO etiquetas  
         ( etq_etqid,   
           etq_codigo,   
           etq_descripcion )  
  VALUES ( @etq_etqid,   
           @etq_codigo,   
           @etq_descripcion )
