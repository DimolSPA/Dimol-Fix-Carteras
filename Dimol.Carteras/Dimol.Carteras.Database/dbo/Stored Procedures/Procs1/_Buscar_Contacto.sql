-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Contacto] (@codemp int, @ctcid int, @ticid int, @nombre varchar(200))
AS
BEGIN
	SET NOCOUNT ON;
	Select  ddc_ddcid as ddcid
             FROM deudores_contactos
            WHERE  ddc_codemp = @codemp
            and ddc_ctcid = @ctcid
            and ddc_ticid =  @ticid--CInt(func.Configuracion_Emp_Num(codemp, 19)).ToString
            and UPPER(LTRIM(rtrim( ddc_nombre))) = UPPER(LTRIM(rtrim(@nombre)))
			order by ddc_nombre
END
