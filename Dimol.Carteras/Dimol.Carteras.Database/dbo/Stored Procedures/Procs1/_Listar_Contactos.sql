-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Listar_Contactos] (@codemp int, @ctcid int)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT dc.DDC_CODEMP Codemp,
						dc.DDC_CTCID Ctcid, 
						dc.ddc_ddcid Ddcid,   
						dc.ddc_nombre Nombre,   
						'' Tipo, 
						ddc_estado Estado,
						CASE ddc_estado
								WHEN 'A' THEN 'Activo'
								WHEN 'D' THEN 'Desactivado'
								ELSE ''
								END as EstadoContacto, 
						(select com_nombre  from COMUNA where COM_COMID = dc.ddc_comid ) as Comuna, 
						dc.ddc_direccion Direccion
						FROM deudores_contactos dc
						WHERE    
						dc.ddc_codemp = @codemp
						and dc.ddc_ctcid = @ctcid 

	

END

