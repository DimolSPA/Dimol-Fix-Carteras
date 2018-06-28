-- =============================================
-- Author:		Fmunoz
-- Create date: 2014-09-25
-- Description:	Funcion trae etiqueta para base de datos
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Etiqueta] 
(@codigo varchar(8),@idioma int
)
RETURNS varchar(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar varchar(100)

	-- Add the T-SQL statements to compute the return value here
	set @ResultVar=(SELECT etiquetas_idiomas.eti_descripcion 
    FROM etiquetas,   
         etiquetas_idiomas  
   WHERE etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid 
   and  etiquetas.etq_codigo = @codigo 
   AND   etiquetas_idiomas.eti_idid = @idioma )

	-- Return the result of the function
	RETURN @ResultVar

END

