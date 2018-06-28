

Create Procedure UltNum_Cartera_Clientes_Cpbt_Doc_Imagenes(@cdi_codemp integer, @cdi_pclid integer, @cdi_ctcid integer, @cdi_ccbid integer) as
select IsNull(Max(cdi_cdid)+1, 1) 
from cartera_clientes_cpbt_doc_imagenes
where cdi_codemp = @cdi_codemp and
           cdi_pclid = @cdi_pclid and
           cdi_ctcid = @cdi_ctcid and
           cdi_ccbid = @cdi_ccbid
