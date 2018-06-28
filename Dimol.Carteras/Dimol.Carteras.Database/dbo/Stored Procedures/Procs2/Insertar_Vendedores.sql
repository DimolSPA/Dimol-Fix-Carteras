

Create Procedure Insertar_Vendedores(@vde_codemp integer, @vde_sucid integer, @vde_vdeid integer, @vde_nombre varchar (200),
												@vde_telefono integer, @vde_email varchar (100), @vde_comk decimal (8,4), @vde_como decimal (8,4),
												@vde_emplid integer) as
  INSERT INTO vendedores  
         ( vde_codemp,   
           vde_sucid,   
           vde_vdeid,   
           vde_nombre,   
           vde_telefono,   
           vde_email,   
           vde_comk,   
           vde_como,   
           vde_emplid )  
  VALUES ( @vde_codemp,   
           @vde_sucid,   
           @vde_vdeid,   
           @vde_nombre,   
           @vde_telefono,   
           @vde_email,   
           @vde_comk,   
           @vde_como,   
           @vde_emplid )
