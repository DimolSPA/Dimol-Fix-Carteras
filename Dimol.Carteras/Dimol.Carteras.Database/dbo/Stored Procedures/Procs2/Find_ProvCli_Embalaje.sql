

Create Procedure Find_ProvCli_Embalaje(@pce_codemp integer, @pce_pclid integer) as
  SELECT count(provcli_embalaje.pce_pclid)  
    FROM provcli_embalaje  
   WHERE ( provcli_embalaje.pce_codemp = @pce_codemp ) AND  
         ( provcli_embalaje.pce_pclid = @pce_pclid )
