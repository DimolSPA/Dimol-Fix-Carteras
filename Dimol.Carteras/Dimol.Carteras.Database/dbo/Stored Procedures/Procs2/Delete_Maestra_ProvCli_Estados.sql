

Create Procedure Delete_Maestra_ProvCli_Estados(@mpe_codemp integer, @mpe_pclid integer, @mpe_estid integer,   @mpe_estado varchar(200)) as
   DELETE FROM maestra_provcli_estados  
   WHERE ( maestra_provcli_estados.mpe_codemp = @mpe_codemp ) AND  
         ( maestra_provcli_estados.mpe_pclid = @mpe_pclid ) AND  
         ( maestra_provcli_estados.mpe_estid = @mpe_estid ) AND  
         ( maestra_provcli_estados.mpe_estado = @mpe_estado )
