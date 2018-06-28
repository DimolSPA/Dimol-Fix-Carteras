

Create Procedure Insertar_ProvCli_Impuestos(@pci_codemp integer, @pci_pclid numeric (15), @pci_iptid smallint) as
delete from provcli_impuestos where pci_codemp= @pci_codemp and pci_pclid = @pci_pclid and pci_iptid = @pci_iptid

  INSERT INTO provcli_impuestos  
         ( pci_codemp,   
           pci_pclid,   
           pci_iptid )  
  VALUES ( @pci_codemp,   
           @pci_pclid,   
           @pci_iptid )
