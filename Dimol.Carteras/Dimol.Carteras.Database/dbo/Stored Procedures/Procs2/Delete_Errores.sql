

Create Procedure Delete_Errores(@err_errid integer) as 
DELETE FROM errores  
   WHERE errores.err_errid = @err_errid
