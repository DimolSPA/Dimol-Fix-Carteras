

Create Procedure Delete_Entes_Judicial(@etj_codemp integer, @etj_etjid integer) as
  DELETE FROM entes_judicial  
   WHERE ( entes_judicial.etj_codemp = @etj_codemp ) AND  
         ( entes_judicial.etj_etjid = @etj_etjid )
