

Create Procedure Find_Monedas_Valores(@mnv_codemp integer, @mnv_codmon integer, @mnv_fecha datetime) as
  SELECT count(mnv_codemp)  
    FROM monedas_valores  
   WHERE ( monedas_valores.mnv_codemp = @mnv_codemp ) AND  
         ( monedas_valores.mnv_codmon = @mnv_codmon ) AND  
         ( datepart(year,monedas_valores.mnv_fecha) = datepart(year, @mnv_fecha) ) AND  
         ( datepart(month,monedas_valores.mnv_fecha) = datepart(month, @mnv_fecha) ) AND  
         ( datepart(day,monedas_valores.mnv_fecha) = datepart(day, @mnv_fecha) )
