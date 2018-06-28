

Create Procedure Delete_Pais(@id integer) as
   DELETE FROM pais  
   WHERE pais.pai_paiid = @id
