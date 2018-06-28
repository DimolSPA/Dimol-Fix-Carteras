

Create Procedure Find_Clasificacion_CpbtDoc(@clb_codemp integer, @clb_clbid integer) as
select count(clb_clbid)
from clasificacion_cpbtdoc
where clb_codemp = @clb_codemp and clb_clbid = @clb_clbid
