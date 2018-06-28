

Create Procedure UltNum_Materia_Judicial(@esj_codemp integer) as
  SELECT IsNull(Max(esj_esjid)+1, 1)
    FROM materia_judicial  
   WHERE materia_judicial.esj_codemp = @esj_codemp
