CREATE Procedure [dbo].[_Insertar_Entes_Judicial](
					@etj_codemp integer, 
					@etj_pclid numeric (15), 
					@etj_emplid integer,  
					@etj_sindico varchar (1), 
					@etj_abogado varchar (1), 
					@etj_procurador varchar (1), 
					@etj_receptor varchar (1)) 
as    
declare @id int                
set @id = (select IsNull(Max(etj_etjid )+1, 1) from entes_judicial             
    where etj_codemp = @etj_codemp)        

  INSERT INTO entes_judicial    
         ( etj_codemp,     
           etj_etjid,     
           etj_pclid,     
           etj_emplid,     
           etj_sindico,     
           etj_abogado,     
           etj_procurador,     
           etj_receptor )    
  VALUES ( @etj_codemp,     
           @id,     
           @etj_pclid,     
           @etj_emplid,     
           @etj_sindico,     
           @etj_abogado,     
           @etj_procurador,     
           @etj_receptor )
