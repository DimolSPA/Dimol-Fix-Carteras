CREATE PROCEDURE [dbo].[_Trae_perfiles](
@codemp int,
@idioma int,
@perfilid int)
AS
begin
	declare @indicaAdm char(1)

	set @indicaAdm = (Select perfiles.prf_administrador 
					FROM perfiles 
					WHERE perfiles.prf_codemp = @codemp 
					and perfiles.prf_prfid = @perfilid)

	SELECT perfiles.prf_prfid,perfiles_idiomas.pfi_nombre 
	FROM perfiles, perfiles_idiomas
	WHERE ( perfiles_idiomas.pfi_codemp = perfiles.prf_codemp ) and 
	( perfiles_idiomas.pfi_prfid = perfiles.prf_prfid ) and  
	 ( ( perfiles.prf_codemp = @codemp ) AND  
	 ( perfiles_idiomas.pfi_idid = @idioma ) ) and prf_administrador in ('N',@indicaAdm)

 end
