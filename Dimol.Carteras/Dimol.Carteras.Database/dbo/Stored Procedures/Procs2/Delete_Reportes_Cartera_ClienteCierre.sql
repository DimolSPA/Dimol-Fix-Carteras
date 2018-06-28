

Create Procedure Delete_Reportes_Cartera_ClienteCierre(@rcc_codemp integer, @rcc_fecha datetime) as
  DELETE FROM reportes_cartera_cliente_cierre  
   WHERE ( reportes_cartera_cliente_cierre.rcc_codemp = @rcc_codemp ) AND  
         ( datepart(year,reportes_cartera_cliente_cierre.rcc_fecha) = datepart(year,@rcc_fecha) ) AND  
         ( datepart(month,reportes_cartera_cliente_cierre.rcc_fecha) = datepart(month,@rcc_fecha) ) AND  
         ( datepart(day,reportes_cartera_cliente_cierre.rcc_fecha) = datepart(day,@rcc_fecha) )
