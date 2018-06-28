

create Procedure UltNum_Tipos_Motivos_Castigos(@tmc_codemp integer) as
select IsNull(Max(tmc_tmcid)+1, 1) 
from tipos_motivos_castigos
where tmc_codemp = tmc_codemp
