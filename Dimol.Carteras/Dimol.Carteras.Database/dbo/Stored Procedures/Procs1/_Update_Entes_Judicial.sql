
CREATE Procedure [dbo].[_Update_Entes_Judicial](
					@etj_pclid numeric (15), 
					@etj_sindico varchar (1), 
					@etj_abogado varchar (1), 
					@etj_procurador varchar (1), 
					@etj_receptor varchar (1)) as  
  
		 UPDATE entes_judicial    
			SET etj_sindico = @etj_sindico,     
				etj_abogado = @etj_abogado,     
				etj_procurador = @etj_procurador,     
				etj_receptor = @etj_receptor    
		WHERE etj_pclid = @etj_pclid
