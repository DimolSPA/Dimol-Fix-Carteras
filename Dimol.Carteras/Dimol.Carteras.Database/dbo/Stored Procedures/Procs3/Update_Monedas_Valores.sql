

Create  Procedure Update_Monedas_Valores(@mnv_codemp integer, @mnv_codmon integer, @mnv_fecha datetime, @mnv_valor decimal (10,2)) as  
  UPDATE monedas_valores  
     SET mnv_valor = @mnv_valor  
   WHERE ( monedas_valores.mnv_codemp = @mnv_codemp ) AND  
         ( monedas_valores.mnv_codmon = @mnv_codmon ) AND  
         ( monedas_valores.mnv_fecha = @mnv_fecha )
