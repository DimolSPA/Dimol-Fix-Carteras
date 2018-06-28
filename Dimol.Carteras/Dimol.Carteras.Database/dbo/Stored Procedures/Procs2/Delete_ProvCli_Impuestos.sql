

Create Procedure Delete_ProvCli_Impuestos(@pci_codemp integer, @pci_pclid numeric (15), @pci_iptid smallint) as  
  DELETE FROM provcli_impuestos  
   WHERE ( provcli_impuestos.pci_codemp = @pci_codemp ) AND  
         ( provcli_impuestos.pci_pclid = @pci_pclid ) AND  
         ( provcli_impuestos.pci_iptid = @pci_iptid )
