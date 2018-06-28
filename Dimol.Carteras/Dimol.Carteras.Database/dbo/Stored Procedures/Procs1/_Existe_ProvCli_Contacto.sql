
CREATE Procedure [dbo].[_Existe_ProvCli_Contacto](@psc_pclid numeric (15)) as
select 1 
from provcli_sucursal_contacto
where psc_pclid = @psc_pclid
