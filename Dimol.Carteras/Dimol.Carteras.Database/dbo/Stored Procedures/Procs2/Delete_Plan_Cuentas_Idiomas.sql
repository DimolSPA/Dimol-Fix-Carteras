

Create Procedure Delete_Plan_Cuentas_Idiomas(@pci_codemp integer, @pci_pctid integer, @pci_idid integer) as 
  DELETE FROM plan_cuentas_idiomas  
   WHERE ( plan_cuentas_idiomas.pci_codemp = @pci_codemp ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid )
