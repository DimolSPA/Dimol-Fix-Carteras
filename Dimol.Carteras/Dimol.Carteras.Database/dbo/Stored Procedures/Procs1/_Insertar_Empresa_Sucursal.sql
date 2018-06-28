Create Procedure [dbo].[_Insertar_Empresa_Sucursal](
	@esu_codemp integer, 
	@esu_nombre varchar(100), 
	@esu_comid integer, 
	@esu_direccion varchar(400),  
	@esu_telefono varchar(80), 
	@esu_fax varchar(80), 
	@esu_mail varchar(80), 
	@esu_css varchar(80), 
	@esu_matriz char(1)) 
as  

declare @id int                                
set @id = (select IsNull(Max(esu_sucid )+1, 1) from empresa_sucursal      
   where esu_codemp = @esu_codemp)                            

  INSERT INTO empresa_sucursal    
         ( esu_codemp,     
           esu_sucid,     
           esu_nombre,     
           esu_comid,     
           esu_direccion,     
           esu_telefono,     
           esu_fax,     
           esu_mail,     
           esu_css,     
           esu_matriz )    
  VALUES ( @esu_codemp,     
           @id,     
           @esu_nombre,     
           @esu_comid,     
           @esu_direccion,     
           @esu_telefono,     
           @esu_fax,     
           @esu_mail,     
           @esu_css,     
           @esu_matriz )
