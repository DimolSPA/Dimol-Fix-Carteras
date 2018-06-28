CREATE Procedure [_Insertar_SubCarteras](@codemp integer, @rut varchar (20), @nombre varchar (400),
												@comid integer, @direccion varchar (200), @telefono varchar (80)) as
declare @sbcid int =(select IsNull(Max(sbc_sbcid)+1, 1) from subcarteras where sbc_codemp = @codemp)							
												
  INSERT INTO subcarteras  
         ( sbc_codemp,   
           sbc_sbcid,   
           sbc_rut,   
           sbc_nombre,   
           sbc_comid,   
           sbc_direccion,   
           sbc_telefono )  
  VALUES ( @codemp,   
           @sbcid,   
           @rut,   
           @nombre,   
           @comid,   
           @direccion,   
           @telefono )
           
select @sbcid as sbcid
