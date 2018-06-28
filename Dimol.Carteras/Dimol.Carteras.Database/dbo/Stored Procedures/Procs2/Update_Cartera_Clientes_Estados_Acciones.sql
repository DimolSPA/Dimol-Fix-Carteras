

Create Procedure Update_Cartera_Clientes_Estados_Acciones(@cea_codemp integer, @cea_pclid numeric (15), @cea_ctcid numeric (15), 
                                                                                                      @cea_fecha datetime,  @cea_accid integer, @cea_sucid integer, 
                                                                                                      @cea_gesid integer, @cea_contacto char (1), @cea_ipred varchar (30), @cea_ipmaquina varchar (30), 
                                                                                                      @cea_comentario text, @cea_estado char (1), @cea_usrid integer) as  
  UPDATE cartera_clientes_estados_acciones  
     SET cea_sucid = @cea_sucid,   
         cea_gesid = @cea_gesid,   
         cea_contacto = @cea_contacto,   
         cea_ipred = @cea_ipred,   
         cea_ipmaquina = @cea_ipmaquina,   
         cea_comentario = @cea_comentario,   
         cea_estado = @cea_estado,   
         cea_usrid = @cea_usrid  
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = @cea_codemp ) AND  
         ( cartera_clientes_estados_acciones.cea_pclid = @cea_pclid ) AND  
         ( cartera_clientes_estados_acciones.cea_ctcid = @cea_ctcid ) AND  
         ( cartera_clientes_estados_acciones.cea_fecha = @cea_fecha ) AND  
         ( cartera_clientes_estados_acciones.cea_accid = @cea_accid )
