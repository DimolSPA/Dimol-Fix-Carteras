

Create Procedure Find_Tipos_Motivos_Castigos(@tmc_codemp integer, @tmc_tmcid integer) as
select count(tmc_tmcid) from tipos_motivos_castigos
where tmc_codemp = @tmc_codemp and
          tmc_tmcid = @tmc_tmcid
