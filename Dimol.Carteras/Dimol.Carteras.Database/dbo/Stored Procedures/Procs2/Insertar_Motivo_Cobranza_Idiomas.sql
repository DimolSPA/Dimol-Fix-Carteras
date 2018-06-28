

Create Procedure Insertar_Motivo_Cobranza_Idiomas(@mci_codemp integer, @mci_mtcid integer, @mci_idid integer, @mci_nombre varchar (120)) as 
  INSERT INTO motivo_cobranza_idiomas  
         ( mci_codemp,   
           mci_mtcid,   
           mci_idid,   
           mci_nombre )  
  VALUES ( @mci_codemp,   
           @mci_mtcid,   
           @mci_idid,   
           @mci_nombre )
