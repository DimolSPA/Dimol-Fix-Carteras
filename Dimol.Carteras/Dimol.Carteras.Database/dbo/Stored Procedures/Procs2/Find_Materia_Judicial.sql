

Create Procedure Find_Materia_Judicial(@esj_codemp integer, @esj_esjid integer) as
  SELECT count(materia_judicial.esj_esjid)  
    FROM materia_judicial  
   WHERE materia_judicial.esj_codemp = @esj_codemp   and
                 materia_judicial.esj_esjid = @esj_esjid
