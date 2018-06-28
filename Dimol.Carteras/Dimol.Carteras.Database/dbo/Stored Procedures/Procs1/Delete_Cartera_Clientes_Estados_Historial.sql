

Create Procedure Delete_Cartera_Clientes_Estados_Historial(@ceh_codemp integer, @ceh_pclid numeric (15), @ceh_ctcid numeric (15),
                                                                                                    @ceh_ccbid integer, @ceh_fecha datetime, @ceh_estid smallint) as  
  DELETE FROM cartera_clientes_estados_historial  
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = @ceh_codemp ) AND  
         ( cartera_clientes_estados_historial.ceh_pclid = @ceh_pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @ceh_ctcid ) AND  
         ( cartera_clientes_estados_historial.ceh_ccbid = @ceh_ccbid ) AND  
         ( cartera_clientes_estados_historial.ceh_fecha = @ceh_fecha ) AND  
         ( cartera_clientes_estados_historial.ceh_estid = @ceh_estid )
