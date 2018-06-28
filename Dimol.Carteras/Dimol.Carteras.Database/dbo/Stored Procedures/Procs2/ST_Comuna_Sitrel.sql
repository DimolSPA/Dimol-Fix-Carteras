-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Comuna_Sitrel]
(
@CODEMP int 
,@PCLID int
,@CODIGO varchar(20)
)
AS
	SELECT TOP 1 C.COM_COMID comid FROM SITREL_COMUNA ST, COMUNA C 
	WHERE UPPER(ST.NOMBRE) = UPPER(C.COM_NOMBRE)
	AND ST.CODIGO = @CODIGO
	AND ST.PCLID = @PCLID
	and st.CODEMP = @CODEMP

