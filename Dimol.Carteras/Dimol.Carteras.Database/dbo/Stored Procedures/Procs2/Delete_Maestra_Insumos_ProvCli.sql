

Create Procedure Delete_Maestra_Insumos_ProvCli(@mip_codemp integer, @mip_insid numeric(15), @mip_pclid numeric(15)) as

   DELETE FROM maestra_insumos_provcli  
   WHERE ( maestra_insumos_provcli.mip_codemp = @mip_codemp ) AND  
         ( maestra_insumos_provcli.mip_insid = @mip_insid ) AND  
         ( maestra_insumos_provcli.mip_pclid = @mip_pclid )
