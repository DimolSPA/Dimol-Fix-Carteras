-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Grupo_Cobranza]
(
@codemp int,
@codsuc int
)
AS
BEGIN
	SET NOCOUNT ON;
	
select grc_grcid Id, grc_nombre Nombre from grupos_cobranza 
where grc_codemp =@codemp
and grc_sucid =@codsuc
order by grc_nombre

end