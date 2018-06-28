

Create Procedure Update_ProvCli_Embalaje(@pce_codemp integer, @pce_pclid numeric (15), @pce_ultnum numeric (15)) as
  UPDATE provcli_embalaje  
     SET pce_codemp = @pce_codemp,   
         pce_pclid = @pce_pclid,   
         pce_ultnum = @pce_ultnum  
   WHERE ( provcli_embalaje.pce_codemp = @pce_codemp ) AND  
         ( provcli_embalaje.pce_pclid = @pce_pclid )
