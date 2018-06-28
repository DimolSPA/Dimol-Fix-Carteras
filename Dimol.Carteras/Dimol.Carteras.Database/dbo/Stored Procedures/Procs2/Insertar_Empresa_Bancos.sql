

create Procedure Insertar_Empresa_Bancos(@esb_codemp integer, @esb_bcoid integer, @esb_sucid integer, 
                                                                         @esb_ctacte varchar (40), @esb_comid integer, @esb_direccion varchar (200), 
                                                                         @esb_agente varchar (200), @esb_telefono varchar (80), @esb_email varchar (100), @esb_pctid integer) as 
  INSERT INTO empresa_bancos  
         ( esb_codemp,   
           esb_bcoid,   
           esb_sucid,   
           esb_ctacte,   
           esb_comid,   
           esb_direccion,   
           esb_agente,   
           esb_telefono,   
           esb_email,   
           esb_pctid )  
  VALUES ( @esb_codemp,   
           @esb_bcoid,   
           @esb_sucid,   
           @esb_ctacte,   
           @esb_comid,   
           @esb_direccion,   
           @esb_agente,   
           @esb_telefono,   
           @esb_email,   
           @esb_pctid )
