

Create Procedure Find_Tipos_CpbtDoc_Talonario(@tct_codemp integer, @tct_tpcid integer, @tct_sucid integer) as
select count(tct_tpcid)
from tipos_cpbtdoc_talonario
where tct_codemp = @tct_codemp and
           tct_tpcid = @tct_tpcid and
           tct_sucid = @tct_sucid
