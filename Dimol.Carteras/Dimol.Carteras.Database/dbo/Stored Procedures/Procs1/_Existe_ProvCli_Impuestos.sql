CREATE Procedure [dbo].[_Existe_ProvCli_Impuestos](@pci_pclid numeric (15)) as
select 1 
from provcli_impuestos
where pci_pclid = @pci_pclid
