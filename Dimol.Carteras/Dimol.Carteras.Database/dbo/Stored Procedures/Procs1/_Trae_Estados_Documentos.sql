CREATE Procedure [dbo].[_Trae_Estados_Documentos]      
		@idioma int  
      
       
as          
     
      
BEGIN        
      SELECT  RIGHT(etiquetas.etq_codigo,1) as Id,    
		etiquetas_idiomas.eti_descripcion        
    FROM etiquetas,         
         etiquetas_idiomas        
   WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and        
         ( etiquetas_idiomas.eti_idid = @idioma )  and    
         ( etiquetas.etq_codigo IN 
         ('EstDD1','EstDD2','EstDD3','EstDD4','EstDD5','EstDD6'))    

END
