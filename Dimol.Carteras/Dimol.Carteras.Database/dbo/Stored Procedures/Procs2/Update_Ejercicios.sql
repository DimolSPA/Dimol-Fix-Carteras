

Create Procedure Update_Ejercicios(@ejc_codemp integer, @ejc_ejcid integer, @ejc_tejid integer, @ejc_nombre varchar(200), @ejc_tiempo integer, @ejc_repeticion integer) as
   UPDATE ejercicios  
     SET ejc_tejid = @ejc_tejid,   
         ejc_nombre = @ejc_nombre,   
         ejc_tiempo = @ejc_tiempo,   
         ejc_repeticion = @ejc_repeticion  
   WHERE ( ejercicios.ejc_codemp = @ejc_codemp ) AND  
         ( ejercicios.ejc_ejcid = @ejc_ejcid )
