CREATE Procedure [dbo].[_Existe_ProvCli_ContratosClientes](@ctc_pclid numeric (15)) as
select 1 
from contratos_clientes
where ctc_pclid = @ctc_pclid
