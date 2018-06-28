-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Agrupa_Accion] 
(
	@agrupa int,
	@idid int
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @agrupa_accion varchar(50)

	-- Add the T-SQL statements to compute the return value here
	set @agrupa_accion = (SELECT etiquetas_idiomas.eti_descripcion
    FROM etiquetas,   
         etiquetas_idiomas  
   WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and  
         ( ( etiquetas.etq_codigo = 'AgrAc'+convert(char,@agrupa) ) AND  
         ( etiquetas_idiomas.eti_idid = @idid ))   
         )

	-- Return the result of the function
	RETURN @agrupa_accion

END

