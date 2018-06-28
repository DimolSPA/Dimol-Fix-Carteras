-- =============================================
-- Author:		Fmunoz
-- Create date: 2014-09-25
-- Description:	Funcion trae etiqueta para base de datos
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Error] 
(@codigo varchar(20),@idioma int
)
RETURNS varchar(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar varchar(100)

	-- Add the T-SQL statements to compute the return value here
	set @ResultVar=(SELECT errores_idiomas.eri_descripcion  
    FROM errores,   
         errores_idiomas  
   WHERE errores_idiomas.eri_errid = errores.err_errid  and  
          errores.err_codigo = @codigo AND  
          errores_idiomas.eri_idid = @idioma )

	-- Return the result of the function
	RETURN @ResultVar

END

