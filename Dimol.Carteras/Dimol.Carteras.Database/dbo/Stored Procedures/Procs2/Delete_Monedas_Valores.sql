

Create Procedure Delete_Monedas_Valores(@mnv_codemp integer, @mnv_codmon integer, @mnv_fecha datetime) as 
  DELETE FROM monedas_valores  
   WHERE ( monedas_valores.mnv_codemp = @mnv_codemp ) AND  
         ( monedas_valores.mnv_codmon = @mnv_codmon ) AND  
         ( monedas_valores.mnv_fecha = @mnv_fecha )
