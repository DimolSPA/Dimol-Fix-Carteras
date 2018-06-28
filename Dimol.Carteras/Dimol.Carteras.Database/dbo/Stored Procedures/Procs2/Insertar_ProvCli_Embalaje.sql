

Create Procedure Insertar_ProvCli_Embalaje(@pce_codemp integer, @pce_pclid numeric (15), @pce_ultnum numeric (15)) as
  INSERT INTO provcli_embalaje  
         ( pce_codemp,   
           pce_pclid,   
           pce_ultnum )  
  VALUES ( @pce_codemp,   
           @pce_pclid,   
           @pce_ultnum )
