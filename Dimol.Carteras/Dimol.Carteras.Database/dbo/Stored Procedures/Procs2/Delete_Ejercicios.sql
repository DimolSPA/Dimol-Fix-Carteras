

Create Procedure Delete_Ejercicios(@ejc_codemp integer, @ejc_ejcid integer) as
  DELETE FROM ejercicios_idiomas  
   WHERE ( ejercicios_idiomas.eji_codemp = @ejc_codemp ) AND  
         ( ejercicios_idiomas.eji_ejcid = @ejc_ejcid )   


  DELETE FROM ejercicios  
   WHERE ( ejercicios.ejc_codemp = @ejc_codemp ) AND  
         ( ejercicios.ejc_ejcid = @ejc_ejcid )
