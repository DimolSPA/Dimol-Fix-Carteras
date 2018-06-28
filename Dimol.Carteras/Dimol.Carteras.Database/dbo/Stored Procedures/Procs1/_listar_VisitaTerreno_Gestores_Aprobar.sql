CREATE PROCEDURE [dbo].[_listar_VisitaTerreno_Gestores_Aprobar](
@codemp int, @sucid int
)
AS
BEGIN    
	select CONVERT(VARCHAR,ges.GES_GESID) + '|' + ges.GES_IMEI 
		+ '|' + isnull(carges.CARTERA_ID, '') + '|' + isnull(carges.CARTERA_NOMBRE,''),
		ges.GES_NOMBRE + ' - ' + isnull(ges.GES_TELEFONO_TERRENO,'')
	from GESTOR ges 
	left join VISITA_TERRENO_CARTERA_GESTOR carges
	on ges.GES_GESID = carges.GESID
	where ges.GES_CODEMP = @codemp
	and ges.GES_SUCID = @sucid
	and ges.GES_ESTADO = 'A'
	and ges.GES_VISITA_TERRENO = 'S'
END
