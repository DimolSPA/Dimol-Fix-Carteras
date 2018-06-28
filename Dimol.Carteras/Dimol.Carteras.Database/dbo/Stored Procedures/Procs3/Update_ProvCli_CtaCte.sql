

Create Procedure Update_ProvCli_CtaCte(@pct_codemp integer, @pct_tpcid integer, @pct_pclid numeric (15), @pct_frpid integer,
                                                                    @pct_credito char (1), @pct_limite_credito decimal (15,2), @pct_credito_consumido decimal (15,2),
                                                                    @pct_estado char (1), @pct_comentarios text) as  
UPDATE provcli_ctacte  
     SET pct_frpid = @pct_frpid,   
         pct_credito = @pct_credito,   
         pct_limite_credito = @pct_limite_credito,   
         pct_credito_consumido = @pct_credito_consumido,   
         pct_estado = @pct_estado,   
         pct_comentarios = @pct_comentarios  
   WHERE ( provcli_ctacte.pct_codemp = @pct_codemp ) AND  
         ( provcli_ctacte.pct_tpcid = @pct_tpcid ) AND  
         ( provcli_ctacte.pct_pclid = @pct_pclid )
