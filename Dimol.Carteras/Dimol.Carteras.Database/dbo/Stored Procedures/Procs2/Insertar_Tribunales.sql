

Create Procedure Insertar_Tribunales(@trb_codemp integer, @trb_trbid integer, @trb_rut varchar (20), @trb_nombre varchar (800), @trb_ttbid integer,
											@trb_comid integer, @trb_direccion varchar (120), @trb_telefono1 varchar (80), @trb_telefono2 varchar (80),
											@trb_fax varchar (80), @trb_email varchar (100), @trb_bcoid integer, @trb_ctacte varchar (20)) as  
  INSERT INTO tribunales  
         ( trb_codemp,   
           trb_trbid,   
           trb_rut,   
           trb_nombre,   
           trb_ttbid,   
           trb_comid,   
           trb_direccion,   
           trb_telefono1,   
           trb_telefono2,   
           trb_fax,   
           trb_email,   
           trb_bcoid,   
           trb_ctacte )  
  VALUES ( @trb_codemp,   
           @trb_trbid,   
           @trb_rut,   
           @trb_nombre,   
           @trb_ttbid,   
           @trb_comid,   
           @trb_direccion,   
           @trb_telefono1,   
           @trb_telefono2,   
           @trb_fax,   
           @trb_email,   
           @trb_bcoid,   
           @trb_ctacte )
