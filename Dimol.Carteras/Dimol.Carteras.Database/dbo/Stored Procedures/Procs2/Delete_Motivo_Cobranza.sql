

Create Procedure Delete_Motivo_Cobranza(@mtc_codemp integer, @mtc_mtcid integer) as  

  DELETE FROM motivo_cobranza_idiomas  
   WHERE ( motivo_cobranza_idiomas.mci_codemp = @mtc_codemp ) AND  
         ( motivo_cobranza_idiomas.mci_mtcid = @mtc_mtcid ) 

  DELETE FROM motivo_cobranza  
   WHERE ( motivo_cobranza.mtc_codemp = @mtc_codemp ) AND  
         ( motivo_cobranza.mtc_mtcid = @mtc_mtcid )
