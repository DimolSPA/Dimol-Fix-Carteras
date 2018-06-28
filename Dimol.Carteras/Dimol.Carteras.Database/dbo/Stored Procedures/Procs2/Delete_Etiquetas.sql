

Create Procedure Delete_Etiquetas(@etq_etqid integer) as 
  DELETE FROM etiquetas  
   WHERE etiquetas.etq_etqid = @etq_etqid
