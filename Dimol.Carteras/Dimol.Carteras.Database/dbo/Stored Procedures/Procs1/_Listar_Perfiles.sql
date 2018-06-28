-- =============================================
-- Author:		RG
-- Create date: 20150108
-- Description:	TRae Lista Perfiles
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Perfiles] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT perfiles.prf_prfid as ID, perfiles_idiomas.pfi_nombre as Nombre
	FROM perfiles,perfiles_idiomas
    wHERE  perfiles_idiomas.pfi_codemp = perfiles.prf_codemp  and  
	perfiles_idiomas.pfi_prfid = perfiles.prf_prfid
	and prf_codemp = @codemp
    order by pfi_nombre
         


END
