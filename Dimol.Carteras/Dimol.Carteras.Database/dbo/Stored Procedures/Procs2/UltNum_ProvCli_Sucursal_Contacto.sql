

Create Procedure UltNum_ProvCli_Sucursal_Contacto(@psc_codemp integer, @psc_pclid integer, @psc_pcsid integer) as
select IsNull(Max(psc_pscid)+1, 1)
from provcli_sucursal_contacto
where psc_codemp = @psc_codemp and
           psc_pclid = @psc_pclid and
           psc_pcsid = @psc_pcsid
