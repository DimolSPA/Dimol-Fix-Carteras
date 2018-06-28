
CREATE PROCEDURE [dbo].[_Insertar_Panel_Demanda_CursoDemanda](
@codemp int,
@panelId int,
@cursoDemanda varchar(2),
@motivo varchar(200),
@user int)
 AS   
BEGIN
declare
	@cursoDemandaId int = 0,
	@rolId int = 0;
	
	UPDATE PANEL_DEMANDA
	SET CURSODEMANDA = @cursoDemanda
	WHERE PANEL_ID = @panelId
	and CODEMP = @codemp 

	SET @cursoDemandaId = (SELECT IsNull(Max(ID)+1, 1)
									FROM PANEL_DEMANDA_CURSODEMANDA_HISTORIAL)
	INSERT INTO PANEL_DEMANDA_CURSODEMANDA_HISTORIAL(PANEL_ID, ID, CURSODEMANDA,MOTIVO, USRID_REGISTRO)
	VALUES(@panelId, @cursoDemandaId, @cursoDemanda, @motivo, @user)

	
	if (@cursoDemanda = 'NO')
	begin
		
		-- DesAsignar Documentos
		set @rolId =  (select rolid from PANEL_DEMANDA_DETALLE where PANEL_ID = @panelId)
		delete rol_documentos where RDC_ROLID = @rolId
		--Bloquear Rol
		exec dbo._Bloquear_Rol @codemp, @rolId, 'S', @user
		-- Borrar Fecha Ingreso Tribunal, Rol adjudicado, y rolid
		update PANEL_DEMANDA_DETALLE
		set FEC_INGRESO_TRIBUNAL = NULL, ROL_ADJUDICADO = NULL, ROLID = NULL
		where PANEL_ID = @panelId
				
		-- Actualizar estatus de la demanda
		update PANEL_DEMANDA
		set PROCESADA = 'N'
		where PANEL_ID = @panelId
		and CODEMP = @codemp 
		
	end
	if (@cursoDemanda = 'SI')
	begin
		-- Actualizar estatus de la demanda
		update PANEL_DEMANDA
		set PROCESADA = 'S'
		where PANEL_ID = @panelId
		and CODEMP = @codemp 
	end
	
END
