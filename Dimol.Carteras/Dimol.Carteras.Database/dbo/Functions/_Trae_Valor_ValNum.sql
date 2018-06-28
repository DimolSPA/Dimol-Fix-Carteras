-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE  FUNCTION [dbo].[_Trae_Valor_ValNum] 
(
	@emc_codemp INT,
	@emc_valor INT
)
RETURNS decimal(15,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @valNum  INT

	-- Add the T-SQL statements to compute the return value here

	
	SELECT @valNum = empresa_configuracion.emc_valnum
    FROM empresa_configuracion   with (nolock)  
   WHERE ( empresa_configuracion.emc_codemp = @emc_codemp ) AND  
         ( empresa_configuracion.emc_emcid = @emc_valor )

	-- Return the result of the function
	RETURN @valNum

END
