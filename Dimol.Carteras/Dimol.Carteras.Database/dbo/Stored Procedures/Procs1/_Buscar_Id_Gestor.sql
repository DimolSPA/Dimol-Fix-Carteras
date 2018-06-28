-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Buscar_Id_Gestor] (@codemp int, @sucid int, @ctcid int, @pclid int)
AS
BEGIN
	SET NOCOUNT ON;
	select gsc_gesid from gestor_cartera 
	where gsc_codemp = @codemp
    and gsc_sucid =@sucid
    and gsc_ctcid =@ctcid
    and gsc_pclid=@pclid
    
END
