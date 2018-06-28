-- =============================================
-- Author:		Fmunoz
-- Create date: 2014-09-25
-- Description:	Funcion trae etiqueta para base de datos
-- =============================================
CREATE FUNCTION [dbo].[_Formato_Rut_DF] 
(@rut varchar(20)
)
RETURNS varchar(20)
AS
BEGIN
	-- Declare the return variable here
declare 
@largo int,
@dv varchar(1),
@numero varchar(8),
@nuevo_rut varchar(20)='',
@cnt INT = 0;
	-- Add the T-SQL statements to compute the return value here
set @largo = len(@rut)
set @dv =  substring(@rut,@largo,1)
set @numero = substring(@rut,1,@largo-1)
WHILE @cnt < @largo
BEGIN
	SET @cnt = @cnt + 3;
	set @nuevo_rut = substring(@numero,@largo-@cnt,3) +'.'+ @nuevo_rut
END;
set @nuevo_rut = substring(@nuevo_rut,1,len(@nuevo_rut)-1)+'-'+@dv

	-- Return the result of the function
	if @largo = 8
		set  @nuevo_rut = '0' +@nuevo_rut
	RETURN @nuevo_rut

END
