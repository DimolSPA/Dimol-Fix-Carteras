

Create Procedure UltNum_Ejercicios(@ejc_codemp integer) as
        SELECT IsNull(Max(ejc_ejcid)+1, 1) 
    FROM ejercicios  
   WHERE ( ejercicios.ejc_codemp = @ejc_codemp )
