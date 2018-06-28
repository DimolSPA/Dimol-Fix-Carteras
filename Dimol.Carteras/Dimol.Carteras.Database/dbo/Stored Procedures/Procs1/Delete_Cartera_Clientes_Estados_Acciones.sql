

Create Procedure Delete_Cartera_Clientes_Estados_Acciones(@cea_codemp integer, @cea_pclid numeric (15), @cea_ctcid numeric (15), 
                                                                                                     @cea_fecha datetime, @cea_accid integer) as  
  DELETE FROM cartera_clientes_estados_acciones  
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = @cea_codemp ) AND  
         ( cartera_clientes_estados_acciones.cea_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_acciones.cea_ctcid = @cea_ctcid ) AND  
         ( cartera_clientes_estados_acciones.cea_fecha = @cea_fecha ) AND  
         ( cartera_clientes_estados_acciones.cea_accid = @cea_accid )
