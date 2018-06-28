CREATE Procedure [dbo].[_Trae_TipoCartera]  
(  
  @idioma integer
  )  
 
as    
     

BEGIN  
   SELECT  ROW_NUMBER() OVER (ORDER BY etiquetas_idiomas.eti_descripcion) as Id,  
  etiquetas_idiomas.eti_descripcion      
    FROM etiquetas,       
         etiquetas_idiomas      
   WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and      
         ( etiquetas_idiomas.eti_idid = @idioma )  and  
         ( etiquetas.etq_codigo IN ('TipCart1','TipCart2','TipCart3'))  
      
           
END
