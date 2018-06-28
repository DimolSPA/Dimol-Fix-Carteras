-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_GestorAnexo] 
(
	@codemp as integer,
	@codsuc as integer,
	@gesid as integer, 
	@ccb_ctcid as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	Select gestor_cartera_anexo.gsa_gesid2, gsa_gesid, gsa_porcom, gsa_porcomgp
    FROM gestor_cartera_anexo
    WHERE  gestor_cartera_anexo.gsa_codemp = @codemp
    and gestor_cartera_anexo.gsa_sucid = @codsuc
    and gestor_cartera_anexo.gsa_gesid = @gesid
    and gestor_cartera_anexo.gsa_ctcid = @ccb_ctcid
		   
END
