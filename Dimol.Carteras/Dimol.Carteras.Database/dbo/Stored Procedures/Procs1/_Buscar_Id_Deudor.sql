-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 11-04-2014
-- Description:	TRae cantidad de compromisos de pago por deudor
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Id_Deudor](
	@rut as varchar(20),
	@codemp as int
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
select CTC_CTCID ctcid from DEUDORES where CTC_RUT = @rut and CTC_CODEMP =@codemp

END
