
CREATE PROCEDURE [dbo].[_Valida_Usuario_PJ] (@usuario varchar(30), @password varchar(100)) 
AS
BEGIN

	select count(usrid) 
	from usuarios_pj with (nolock)
	where login = @usuario and password = @password and activa = 0 

END

