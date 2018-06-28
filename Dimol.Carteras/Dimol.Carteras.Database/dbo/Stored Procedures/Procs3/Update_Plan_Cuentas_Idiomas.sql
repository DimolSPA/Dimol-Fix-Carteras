

Create Procedure Update_Plan_Cuentas_Idiomas(@pci_codemp integer, @pci_pctid integer, @pci_idid integer, @pci_nombre varchar (1500)) as  
  UPDATE plan_cuentas_idiomas  
     SET pci_nombre = @pci_nombre  
   WHERE ( plan_cuentas_idiomas.pci_codemp = @pci_codemp ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid )
