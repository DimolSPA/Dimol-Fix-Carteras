

Create Procedure Update_Motivo_Cobranza_Idiomas(@mci_codemp integer, @mci_mtcid integer, @mci_idid integer, @mci_nombre varchar (120)) as  
  UPDATE motivo_cobranza_idiomas  
     SET mci_nombre = @mci_nombre  
   WHERE ( motivo_cobranza_idiomas.mci_codemp = @mci_codemp ) AND  
         ( motivo_cobranza_idiomas.mci_mtcid = @mci_mtcid ) AND  
         ( motivo_cobranza_idiomas.mci_idid = @mci_idid )
