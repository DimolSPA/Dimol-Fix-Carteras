

Create Procedure Update_Cartera_Clientes_Estados_Historial(@ceh_codemp integer, @ceh_pclid numeric (15), @ceh_ctcid numeric (15), @ceh_ccbid integer, 
                                                                                                     @ceh_fecha datetime, @ceh_estid smallint, @ceh_sucid integer, @ceh_gesid integer, 
                                                                                                     @ceh_ipred varchar (30), @ceh_ipmaquina varchar (30), @ceh_comentario text, @ceh_monto decimal (15,2),
                                                                                                     @ceh_saldo decimal (15,2), @ceh_usrid integer) as  
  UPDATE cartera_clientes_estados_historial  
     SET ceh_sucid = @ceh_sucid,   
         ceh_gesid = @ceh_gesid,   
         ceh_ipred = @ceh_ipred,   
         ceh_ipmaquina = @ceh_ipmaquina,   
         ceh_comentario = @ceh_comentario,   
         ceh_monto = @ceh_monto,   
         ceh_saldo = @ceh_saldo,   
         ceh_usrid = @ceh_usrid  
   WHERE ( cartera_clientes_estados_historial.ceh_codemp = @ceh_codemp ) AND  
         ( cartera_clientes_estados_historial.ceh_pclid = @ceh_pclid ) AND  
         ( cartera_clientes_estados_historial.ceh_ctcid = @ceh_ctcid ) AND  
         ( cartera_clientes_estados_historial.ceh_ccbid = @ceh_ccbid ) AND  
         ( cartera_clientes_estados_historial.ceh_fecha = @ceh_fecha ) AND  
         ( cartera_clientes_estados_historial.ceh_estid = @ceh_estid )
