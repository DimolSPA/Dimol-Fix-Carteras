

Create Procedure Delete_Cartera_Clientes_Retiros_EntregaEsp(@cre_codemp integer, @cre_pclid numeric (15), @cre_ctcid numeric (15),
																			    @cre_fecha datetime) as
  DELETE FROM cartera_clientes_retiros_entrega  
   WHERE cartera_clientes_retiros_entrega.cre_codemp = @cre_codemp  AND  
          cartera_clientes_retiros_entrega.cre_pclid = @cre_pclid  AND  
          cartera_clientes_retiros_entrega.cre_ctcid = @cre_ctcid  AND  
          datepart(year,cartera_clientes_retiros_entrega.cre_fecha) = datepart(year,@cre_fecha) AND
          datepart(month,cartera_clientes_retiros_entrega.cre_fecha) = datepart(month,@cre_fecha) AND
          datepart(day,cartera_clientes_retiros_entrega.cre_fecha) = datepart(day,@cre_fecha)
