Create Procedure [dbo].[_Insertar_Notarias](
			@not_codemp integer, 
			@not_rut varchar (20), 
			@not_nombre varchar (200),  
			@not_nomnot varchar (250), 
			@not_comid integer, 
			@not_direccion varchar (300), 
			@not_telefono1 integer,  
			@not_telefono2 integer, 
			@not_fax integer, 
			@not_celular integer, 
			@not_mail varchar (80)) 
as  

declare @id int              
set @id = (select IsNull(Max(not_notid )+1, 1) from notarias           
    where not_codemp = @not_codemp)      
  INSERT INTO notarias    
         ( not_codemp,     
           not_notid,     
           not_rut,     
           not_nombre,     
           not_nomnot,     
           not_comid,     
           not_direccion,     
           not_telefono1,     
           not_telefono2,     
           not_fax,     
           not_celular,     
           not_mail )    
  VALUES ( @not_codemp,     
           @id,     
           @not_rut,     
           @not_nombre,     
           @not_nomnot,     
           @not_comid,     
           @not_direccion,     
           @not_telefono1,     
           @not_telefono2,     
           @not_fax,     
           @not_celular,     
           @not_mail )
