

Create Procedure Update_Empresa_Sucursal(@esu_codemp integer, @esu_sucid integer, @esu_nombre varchar (100), @esu_comid integer, @esu_direccion varchar (400),
                                                                          @esu_telefono varchar (80), @esu_fax varchar (80), @esu_mail varchar (80), @esu_css varchar (80), @esu_matriz char (1)) as
  UPDATE empresa_sucursal  
     SET esu_nombre = @esu_nombre,   
         esu_comid = @esu_comid,   
         esu_direccion = @esu_direccion,   
         esu_telefono = @esu_telefono,   
         esu_fax = @esu_fax,   
         esu_mail = @esu_mail,   
         esu_css = @esu_css,   
         esu_matriz = @esu_matriz  
   WHERE ( empresa_sucursal.esu_codemp = @esu_codemp ) AND  
         ( empresa_sucursal.esu_sucid = @esu_sucid )
