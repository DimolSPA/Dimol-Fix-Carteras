

Create Procedure Update_Entes_Judicial(@etj_codemp integer, @etj_etjid integer, @etj_pclid numeric (15), @etj_emplid integer,
													@etj_sindico char (1), @etj_abogado char (1), @etj_procurador char (1), @etj_receptor char (1)) as  
  UPDATE entes_judicial  
     SET  etj_pclid = @etj_pclid,   
         etj_emplid = @etj_emplid,   
         etj_sindico = @etj_sindico,   
         etj_abogado = @etj_abogado,   
         etj_procurador = @etj_procurador,   
         etj_receptor = @etj_receptor  
   WHERE ( entes_judicial.etj_codemp = @etj_codemp ) AND  
         ( entes_judicial.etj_etjid = @etj_etjid )
