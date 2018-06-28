CREATE Procedure [dbo].[_Existe_ProvCli_Sucursal](@pcs_pclid numeric (15)) as
select 1 
from provcli_sucursal
where pcs_pclid = @pcs_pclid
