

Create Procedure Delete_ProvCli_CtaCte(@pct_codemp integer, @pct_tpcid integer, @pct_pclid numeric (15)) as  
   DELETE FROM provcli_ctacte  
   WHERE ( provcli_ctacte.pct_codemp = @pct_codemp ) AND  
         ( provcli_ctacte.pct_tpcid = @pct_tpcid ) AND  
         ( provcli_ctacte.pct_pclid = @pct_pclid )
