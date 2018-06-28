-- =============================================
-- Author:		Fmunoz
-- Create date: 2014-09-25
-- Description:	Funcion trae etiqueta para base de datos
-- =============================================
create FUNCTION [dbo].[_Formato_Numero] 
(@num decimal(28,2)
)
RETURNS varchar(30)
AS
BEGIN
	-- Declare the return variable here
declare 
@numChar varchar(30) = '',
@largo int,
@decimal varchar(2),
@numero varchar(30),
@nuevoNum varchar(30)='',
@cnt INT = -1;
	-- Add the T-SQL statements to compute the return value here
if @num != 0
begin
set @numChar = cast(@num as varchar(30) )
set @largo = len(@numChar)
set @decimal =  substring(@numChar,@largo-1,2)
set @numero = substring(@numChar,1,@largo-3)
set @largo = len(@numero)

WHILE @cnt < @largo
BEGIN
	SET @cnt = @cnt + 3;
	set @nuevoNum = substring(@numero,@largo-@cnt,3) +'.'+ @nuevoNum
END;
if substring(@nuevoNum,1,1) = '.'
begin
	set @nuevoNum = substring(@nuevoNum,2,len(@nuevoNum)-1)
end
set @nuevoNum = substring(@nuevoNum,1,len(@nuevoNum)-1)+','+@decimal
end
else 
set @nuevoNum =  '0,00'

	-- Return the result of the function
	RETURN @nuevoNum

END
