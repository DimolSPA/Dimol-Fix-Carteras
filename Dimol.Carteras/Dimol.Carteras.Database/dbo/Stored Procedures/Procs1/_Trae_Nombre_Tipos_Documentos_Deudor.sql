-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Nombre_Tipos_Documentos_Deudor] 
(
	@codemp as integer,
	@idioma as integer,
	@id as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT tipos_cpbtdoc_idiomas.tci_nombre
    FROM clasificacion_cpbtdoc,   
         tipos_cpbtdoc,   
         tipos_cpbtdoc_idiomas  
    WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  
          tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  
          tipos_cpbtdoc_idiomas.tci_codemp = tipos_cpbtdoc.tpc_codemp  and  
          tipos_cpbtdoc_idiomas.tci_tpcid = tipos_cpbtdoc.tpc_tpcid  and  
           clasificacion_cpbtdoc.clb_codemp = @codemp  AND  
          clasificacion_cpbtdoc.clb_cartcli = 'S'  AND  
          clasificacion_cpbtdoc.clb_selapl = 'N'  AND  
          clasificacion_cpbtdoc.clb_aplica = 'N'  AND  
          clasificacion_cpbtdoc.clb_findeuda = 'N'    AND
          tci_idid = @idioma AND
		  tci_tpcid = @id
         


END
