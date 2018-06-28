CREATE Procedure [dbo].[_Trae_Tipos_Movimientos]        
      
       
as          
           
      
BEGIN        
   SELECT 
	SUBSTRING(ETQ_DESCRIPCION,1,1) AS Id,
	ETQ_DESCRIPCION as Descripcion
	FROM ETIQUETAS
	where ETQ_CODIGO IN
	 ('TipAsi2','TipAsi3','TipAsi4')

END
