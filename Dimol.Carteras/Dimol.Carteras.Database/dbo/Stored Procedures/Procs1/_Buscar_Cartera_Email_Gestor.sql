-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Cartera_Email_Gestor]
(
@codemp int,
@ctcid int,
@codsuc int,
@pclid int
)
AS
BEGIN
	SET NOCOUNT ON;



SELECT DISTINCT gestor.ges_nombre Nombre,   
gestor.ges_telefono Telefono,   
gestor.ges_email Email
FROM gestor,   
gestor_cartera
WHERE  gestor_cartera.gsc_codemp = gestor.ges_codemp  and  
gestor_cartera.gsc_sucid = gestor.ges_sucid  and  
gestor_cartera.gsc_gesid = gestor.ges_gesid  and  
gestor.ges_codemp = @codemp
and gestor.ges_sucid =  @codsuc
and gestor_cartera.gsc_ctcid = @ctcid
and gsc_pclid =@pclid


	

END
