

Create Procedure Update_SubCarteras(@sbc_codemp integer, @sbc_sbcid integer, @sbc_rut varchar (20), @sbc_nombre varchar (400),
												@sbc_comid integer, @sbc_direccion varchar (200), @sbc_telefono varchar (80)) as
  UPDATE subcarteras  
     SET sbc_rut = @sbc_rut,   
         sbc_nombre = @sbc_nombre,   
         sbc_comid = @sbc_comid,   
         sbc_direccion = @sbc_direccion,   
         sbc_telefono = @sbc_telefono  
   WHERE ( subcarteras.sbc_codemp = @sbc_codemp ) AND  
         ( subcarteras.sbc_sbcid = @sbc_sbcid )
