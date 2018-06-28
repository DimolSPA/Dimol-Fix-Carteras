CREATE PROCEDURE [dbo].[_Trae_Monedas]
(        
  @codemp int                     
)        
       
as          

BEGIN    

  SELECT '-1' as Id,UPPER(ETQ_DESCRIPCION) as Nombre
  FROM ETIQUETAS
  WHERE ETQ_CODIGO='Selec'
  
  UNION 

  SELECT MON_CODMON, MON_NOMBRE  
  FROM MONEDAS 
  WHERE MON_CODEMP = @codemp 

END
