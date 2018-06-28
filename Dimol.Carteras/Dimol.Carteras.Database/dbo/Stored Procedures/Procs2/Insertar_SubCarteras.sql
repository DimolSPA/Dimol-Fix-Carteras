

Create Procedure Insertar_SubCarteras(@sbc_codemp integer, @sbc_sbcid integer, @sbc_rut varchar (20), @sbc_nombre varchar (400),
												@sbc_comid integer, @sbc_direccion varchar (200), @sbc_telefono varchar (80)) as
  INSERT INTO subcarteras  
         ( sbc_codemp,   
           sbc_sbcid,   
           sbc_rut,   
           sbc_nombre,   
           sbc_comid,   
           sbc_direccion,   
           sbc_telefono )  
  VALUES ( @sbc_codemp,   
           @sbc_sbcid,   
           @sbc_rut,   
           @sbc_nombre,   
           @sbc_comid,   
           @sbc_direccion,   
           @sbc_telefono )
