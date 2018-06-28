

Create Procedure Find_ProvCli_Sucursal_Contacto(@psc_codemp integer, @psc_pclid integer, @psc_pcsid integer, @psc_pscid integer) as
select count(psc_pscid)
from provcli_sucursal_contacto
where psc_codemp = @psc_codemp and
           psc_pclid = @psc_pclid and
           psc_pcsid = @psc_pcsid and
           psc_pscid =  @psc_pscid
