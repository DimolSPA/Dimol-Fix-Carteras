

CREATE Procedure [dbo].[Trae_Etiquetas](@codigo varchar(20), @idioma integer) as
  SELECT etiquetas_idiomas.eti_descripcion  
    FROM etiquetas with (nolock) ,   
         etiquetas_idiomas  with (nolock)  
   WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and  
         ( ( etiquetas.etq_codigo = @codigo ) AND  
         ( etiquetas_idiomas.eti_idid = @idioma )   
         )
