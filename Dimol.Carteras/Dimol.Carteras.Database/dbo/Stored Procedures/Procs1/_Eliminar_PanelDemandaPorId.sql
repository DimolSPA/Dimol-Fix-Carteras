CREATE procedure [dbo].[_Eliminar_PanelDemandaPorId] (
	@idPanel int
) as
	DELETE PANEL_DEMANDA_DOCUMENTOS WHERE panel_id = @idPanel

	DELETE PANEL_DEMANDA_DETALLE WHERE panel_id = @idPanel

	DELETE PANEL_DEMANDA_CORRECCION_HISTORIAL WHERE panel_id = @idPanel

	DELETE PANEL_DEMANDA_CURSODEMANDA_HISTORIAL WHERE panel_id = @idPanel

	DELETE PANEL_DEMANDA WHERE panel_id = @idPanel
