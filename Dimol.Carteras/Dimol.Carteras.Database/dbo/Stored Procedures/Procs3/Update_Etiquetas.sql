

Create  Procedure Update_Etiquetas(@etq_etqid integer, @etq_codigo varchar (8), @etq_descripcion varchar (100)) as  
  UPDATE etiquetas  
     SET etq_codigo = @etq_codigo,   
         etq_descripcion = @etq_descripcion  
   WHERE etiquetas.etq_etqid = @etq_etqid
