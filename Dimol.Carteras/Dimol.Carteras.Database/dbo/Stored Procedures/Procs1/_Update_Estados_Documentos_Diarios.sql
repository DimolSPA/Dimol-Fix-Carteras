CREATE Procedure [dbo].[_Update_Estados_Documentos_Diarios](
			@edc_codemp integer, 
			@edc_edcid integer, 
			@edc_tipmov char (1),
			@edc_nombre varchar (20), 
			@edc_estado smallint,
			@idid int) 

as   
  UPDATE estados_documentos_diarios    
     SET edc_tipmov = @edc_tipmov,     
         edc_nombre = @edc_nombre,     
         edc_estado = @edc_estado    
   WHERE ( estados_documentos_diarios.edc_codemp = @edc_codemp ) AND    
         ( estados_documentos_diarios.edc_edcid = @edc_edcid )  
         
         
  UPDATE estados_documentos_diarios_idiomas    
     SET edi_nombre = @edc_nombre    
   WHERE ( estados_documentos_diarios_idiomas.edi_codemp = @edc_codemp ) AND    
         ( estados_documentos_diarios_idiomas.edi_edcid = @edc_edcid ) AND    
         ( estados_documentos_diarios_idiomas.edi_idiid = @idid )
