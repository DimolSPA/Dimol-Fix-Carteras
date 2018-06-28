

Create Procedure Update_Maestra_Insumos_ProvCli(@mip_codemp integer, @mip_insid numeric(15), @mip_pclid numeric(15), @mip_codigo varchar(30), @mip_nombre varchar(300),
																    @mip_codbarra varchar(30), @mip_cubicaje decimal(15,2), @mip_pack smallint, @mip_unmlgt integer, @mip_alto decimal(15,2),
																    @mip_ancho decimal(15,2), @mip_largo decimal(15,2), @mip_unmid integer, @mip_peso decimal(15,2), @mip_codmon integer, 
																    @mip_precio decimal(14,2)) as

  UPDATE maestra_insumos_provcli  
     SET mip_codigo = @mip_codigo,   
         mip_nombre = @mip_nombre,   
         mip_codbarra = @mip_codbarra,   
         mip_cubicaje = @mip_cubicaje,   
         mip_pack = @mip_pack,   
         mip_unmlgt = @mip_unmlgt,   
         mip_alto = @mip_alto,   
         mip_ancho = @mip_ancho,   
         mip_largo = @mip_largo,   
         mip_unmid = @mip_unmid,   
         mip_peso = @mip_peso,   
         mip_codmon = @mip_codmon,   
         mip_precio = @mip_precio  
   WHERE ( maestra_insumos_provcli.mip_codemp = @mip_codemp ) AND  
         ( maestra_insumos_provcli.mip_insid = @mip_insid ) AND  
         ( maestra_insumos_provcli.mip_pclid = @mip_pclid )
