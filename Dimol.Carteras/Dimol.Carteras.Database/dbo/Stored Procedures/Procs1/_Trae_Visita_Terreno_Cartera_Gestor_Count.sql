CREATE PROCEDURE [dbo].[_Trae_Visita_Terreno_Cartera_Gestor_Count](
@gestorId int)

AS
BEGIN
declare @countCartera int = 0
	set @countCartera = (select count(gesid) from VISITA_TERRENO_CARTERA_GESTOR
						where GESID = @gestorId)
	select @countCartera countCartera
	
END
