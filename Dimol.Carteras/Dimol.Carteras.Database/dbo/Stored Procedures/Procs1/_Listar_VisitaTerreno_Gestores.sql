CREATE PROCEDURE [dbo].[_Listar_VisitaTerreno_Gestores](
@codemp int, @sucid int
)
AS
BEGIN    
	select CONVERT(VARCHAR,ges.GES_GESID) + '|' + isnull(ges.GES_IMEI,'') as id,
		ges.GES_NOMBRE + ' - ' + isnull(ges.GES_TELEFONO_TERRENO,'') as gestor
	from GESTOR ges 
	where ges.GES_CODEMP = @codemp
	and ges.GES_SUCID = @sucid
	and ges.GES_ESTADO = 'A'
	and ges.GES_VISITA_TERRENO = 'S'
END
