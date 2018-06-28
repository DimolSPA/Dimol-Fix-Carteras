

Create Procedure UltNum_Entes_Judicial(@etj_codemp integer) as
  SELECT IsNull(Max(etj_etjid)+1, 1)
    FROM entes_judicial  
   WHERE ( entes_judicial.etj_codemp = @etj_codemp )
