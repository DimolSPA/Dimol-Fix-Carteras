Create Procedure [dbo].[_Insertar_Estados_Documentos_Diarios](
		@edc_codemp integer, 
		@edc_tipmov char (1),
		@edc_nombre varchar (20), 
		@edc_estado smallint,
		@idiid int
		) 

as  

declare @id int                                
set @id = (select IsNull(Max(edc_edcid )+1, 1) from estados_documentos_diarios      
   where edc_codemp = @edc_codemp)                            

    INSERT INTO estados_documentos_diarios    
         ( edc_codemp,     
           edc_edcid,     
           edc_tipmov,     
           edc_nombre,     
           edc_estado )    
  VALUES ( @edc_codemp,     
           @id,     
           @edc_tipmov,     
           @edc_nombre,     
           @edc_estado )  
           
   INSERT INTO estados_documentos_diarios_idiomas    
         ( edi_codemp,     
           edi_edcid,     
           edi_idiid,     
           edi_nombre )    
  VALUES ( @edc_codemp,     
           @id,     
           @idiid,     
           @edc_nombre )
