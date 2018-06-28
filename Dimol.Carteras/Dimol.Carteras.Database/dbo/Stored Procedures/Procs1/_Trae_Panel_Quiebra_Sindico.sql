CREATE PROCEDURE [dbo].[_Trae_Panel_Quiebra_Sindico](@rolId int)
AS
BEGIN
	select top 1 SINDICO, VEEDOR, INTERVENTOR  from PANEL_QUIEBRA_SINDICO
		where ROLID  = @rolId
		
END
