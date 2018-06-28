

Create Procedure Insertar_Plan_Cuentas_Idiomas(@pci_codemp integer, @pci_pctid integer,@pci_idid integer,  @pci_nombre varchar(1500)) as
    INSERT INTO plan_cuentas_idiomas  
         ( pci_codemp,   
           pci_pctid,   
           pci_idid,   
           pci_nombre )  
  VALUES ( @pci_codemp,   
           @pci_pctid,   
           @pci_idid,   
           @pci_nombre )
