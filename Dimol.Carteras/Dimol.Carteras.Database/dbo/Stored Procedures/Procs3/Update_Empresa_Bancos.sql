

Create Procedure Update_Empresa_Bancos(@esb_codemp integer, @esb_bcoid integer, @esb_sucid integer, @esb_ctacte varchar (40),
                                                                        @esb_comid integer, @esb_direccion varchar (200), @esb_agente varchar (200), @esb_telefono varchar (80),
                                                                        @esb_email varchar (100), @esb_pctid integer) as 
  UPDATE empresa_bancos  
     SET esb_comid = @esb_comid,   
         esb_direccion = @esb_direccion,   
         esb_agente = @esb_agente,   
         esb_telefono = @esb_telefono,   
         esb_email = @esb_email,   
         esb_pctid = @esb_pctid  
   WHERE ( empresa_bancos.esb_codemp = @esb_codemp ) AND  
         ( empresa_bancos.esb_bcoid = @esb_bcoid ) AND  
         ( empresa_bancos.esb_sucid = @esb_sucid ) AND  
         ( empresa_bancos.esb_ctacte = @esb_ctacte )
