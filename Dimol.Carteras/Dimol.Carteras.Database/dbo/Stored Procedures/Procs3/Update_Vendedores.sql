

Create Procedure Update_Vendedores(@vde_codemp integer, @vde_sucid integer, @vde_vdeid integer, @vde_nombre varchar (200),
												@vde_telefono integer, @vde_email varchar (100), @vde_comk decimal (8,4), @vde_como decimal (8,4),
												@vde_emplid integer) as
  UPDATE vendedores  
     SET vde_codemp = @vde_codemp,   
         vde_sucid = @vde_sucid,   
         vde_vdeid = @vde_vdeid,   
         vde_nombre = @vde_nombre,   
         vde_telefono = @vde_telefono,   
         vde_email = @vde_email,   
         vde_comk = @vde_comk,   
         vde_como = @vde_como,   
         vde_emplid = @vde_emplid  
   WHERE ( vendedores.vde_codemp = @vde_codemp ) AND  
         ( vendedores.vde_sucid = @vde_sucid ) AND  
         ( vendedores.vde_vdeid = @vde_vdeid )
