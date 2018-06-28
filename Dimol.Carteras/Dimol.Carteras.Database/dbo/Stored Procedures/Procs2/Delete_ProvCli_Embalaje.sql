

Create Procedure Delete_ProvCli_Embalaje(@pce_codemp integer, @pce_pclid numeric (15)) as
  DELETE FROM provcli_embalaje  
   WHERE ( provcli_embalaje.pce_codemp = @pce_codemp ) AND  
         ( provcli_embalaje.pce_pclid = @pce_pclid )
