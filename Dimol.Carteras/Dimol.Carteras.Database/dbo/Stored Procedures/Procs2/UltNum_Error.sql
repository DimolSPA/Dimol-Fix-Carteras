

Create Procedure UltNum_Error as
SELECT IsNull(Max(errores.err_errid)+1, 1) 
    FROM errores
