

create Procedure Find_Tipos_Causa(@tca_codemp integer, @tca_tcaid integer) as
select count(tca_tcaid)
from tipos_causa
where tca_codemp = @tca_codemp and
           tca_tcaid = @tca_tcaid
