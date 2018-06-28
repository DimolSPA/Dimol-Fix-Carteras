﻿-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_SuperCategorias] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select sci_spcid as ID, SCI_NOMBRE as Nombre
	from supercategorias_idioma 
	where (sci_codemp = @codemp) and (sci_idiid = @idioma) and
           sci_spcid in (select sci_spcid 
		   from supercategorias 
		   where spc_codemp = @codemp and spc_utilizacion in(3,1)) 
         


END
