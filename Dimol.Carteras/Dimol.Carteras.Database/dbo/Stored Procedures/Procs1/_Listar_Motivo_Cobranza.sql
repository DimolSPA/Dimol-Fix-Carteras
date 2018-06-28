-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Motivo_Cobranza]
(
@codemp int,
@idioma int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT mci_mtcid Id,    
              mci_nombre Descripcion
             FROM motivo_cobranza_idiomas
             WHERE  mci_codemp = @codemp
             and mci_idid = @idioma
             order by mci_nombre


END
