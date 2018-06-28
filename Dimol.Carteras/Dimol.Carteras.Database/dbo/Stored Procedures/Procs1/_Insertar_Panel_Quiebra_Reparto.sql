

CREATE PROCEDURE [dbo].[_Insertar_Panel_Quiebra_Reparto](
@quiebraId int,
@fecReparto datetime,
@mtoReparto decimal(15,2),
@user int)
	
AS
BEGIN

declare @repartoId int = 0
SET @repartoId = (SELECT IsNull(Max(REPARTO_ID)+1, 1)
							FROM PANEL_QUIEBRA_REPARTOS)

INSERT INTO PANEL_QUIEBRA_REPARTOS(REPARTO_ID, QUIEBRA_ID,FEC_REPARTO,MTO_REPARTO,USRID_REGISTRO)
VALUES(@repartoId,@quiebraId,@fecReparto,@mtoReparto,@user)

UPDATE PANEL_QUIEBRA_DETALLE
SET FEC_REPARTOS = @fecReparto,MTO_REPARTOS= @mtoReparto
WHERE QUIEBRA_ID = @quiebraId

select @quiebraId quiebraId	
END
