

Create Procedure Update_Maestra_ProvCli_Estados(@mpe_codemp integer, @mpe_pclid integer, @mpe_estid integer,   @mpe_estado varchar(200), @mpe_estidN integer, 
                                                @mpe_estadoN varchar(200), @mpe_codigo varchar(10)) as
   UPDATE maestra_provcli_estados  
     SET mpe_estid = @mpe_estidN,   
         mpe_estado = @mpe_estadoN,
         mpe_codigo = @mpe_codigo
   WHERE ( maestra_provcli_estados.mpe_codemp = @mpe_codemp ) AND  
         ( maestra_provcli_estados.mpe_pclid = @mpe_pclid ) AND  
         ( maestra_provcli_estados.mpe_estid = @mpe_estid ) AND  
         ( maestra_provcli_estados.mpe_estado = @mpe_estado )
