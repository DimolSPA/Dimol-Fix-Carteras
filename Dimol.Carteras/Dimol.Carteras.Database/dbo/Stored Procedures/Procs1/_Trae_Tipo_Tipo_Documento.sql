-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Tipo_Tipo_Documento](@codemp int, @tipo int)
AS
BEGIN
	SET NOCOUNT ON;
	 Select  tipos_documentos_deudores.tdd_tipo Tipo
		FROM tipos_documentos_deudores
		WHERE  tipos_documentos_deudores.tdd_codemp =@codemp
		and tipos_documentos_deudores.tdd_tddid =@tipo
END
