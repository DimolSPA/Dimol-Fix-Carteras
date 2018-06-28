-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Acciones_Agrupa] (@codemp int, @accid int)
AS
BEGIN
	SET NOCOUNT ON;
	select acc_agrupa agrupa
	from acciones 
	where acc_codemp =@codemp
	and acc_accid=@accid
	

END

