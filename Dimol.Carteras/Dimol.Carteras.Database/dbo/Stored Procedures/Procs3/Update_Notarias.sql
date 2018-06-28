

Create Procedure Update_Notarias(@not_codemp integer, @not_notid integer, @not_rut varchar (20), @not_nombre varchar (200),
											@not_nomnot varchar (250), @not_comid integer, @not_direccion varchar (300), @not_telefono1 integer,
											@not_telefono2 integer, @not_fax integer, @not_celular integer, @not_mail varchar (80)) as
  UPDATE notarias  
     SET
         not_rut = @not_rut,   
         not_nombre = @not_nombre,   
         not_nomnot = @not_nomnot,   
         not_comid = @not_comid,   
         not_direccion = @not_direccion,   
         not_telefono1 = @not_telefono1,   
         not_telefono2 = @not_telefono2,   
         not_fax = @not_fax,   
         not_celular = @not_celular,   
         not_mail = @not_mail  
   WHERE ( notarias.not_codemp = @not_codemp ) AND  
         ( notarias.not_notid = @not_notid )
