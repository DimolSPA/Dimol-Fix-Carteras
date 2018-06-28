

Create Procedure Delete_Motivo_Cobranza_Idiomas(@mci_codemp integer, @mci_mtcid integer, @mci_idid integer) as  
  DELETE FROM motivo_cobranza_idiomas  
   WHERE ( motivo_cobranza_idiomas.mci_codemp = @mci_codemp ) AND  
         ( motivo_cobranza_idiomas.mci_mtcid = @mci_mtcid ) AND  
         ( motivo_cobranza_idiomas.mci_idid = @mci_idid )
