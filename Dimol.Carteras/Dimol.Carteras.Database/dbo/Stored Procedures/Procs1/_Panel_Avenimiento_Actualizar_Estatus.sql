CREATE PROCEDURE [dbo].[_Panel_Avenimiento_Actualizar_Estatus]
(
@codemp int,
@rolid int,
@pclid int,
@ctcid int,
@nuevo_estado varchar(1)
)
AS
BEGIN
UPDATE PANEL_TRASPASOS_AVENIMIENTO
		 Set	ESTATUS = @nuevo_estado
		   WHERE ( PANEL_TRASPASOS_AVENIMIENTO.CODEMP = @codemp ) AND  
				 ( PANEL_TRASPASOS_AVENIMIENTO.ROLID = @rolid ) AND  
				 ( PANEL_TRASPASOS_AVENIMIENTO.PCLID = @pclid ) AND  
				 ( PANEL_TRASPASOS_AVENIMIENTO.CTCID = @ctcid )
END
