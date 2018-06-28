

Create Procedure Find_Ejercicios(@ejc_codemp integer, @ejc_ejcid integer) as
        SELECT count(ejercicios.ejc_ejcid)  
    FROM ejercicios  
   WHERE ( ejercicios.ejc_codemp = @ejc_codemp ) AND  
         ( ejercicios.ejc_ejcid = @ejc_ejcid )
