-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Relacion_Valida]
(
@codemp int ,
@pclid int ,
@accion varchar(20),
@contacto varchar(20),
@respuesta varchar(20)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT COUNT([CODEMP]) as valida
  FROM [SITREL_RELACION]
  WHERE [CODEMP]= @codemp
      and [PCLID] = @pclid
      and [ACCION] = @accion
      and [CONTACTO] = @contacto
      and [RESPUESTA] = @respuesta

  

END
