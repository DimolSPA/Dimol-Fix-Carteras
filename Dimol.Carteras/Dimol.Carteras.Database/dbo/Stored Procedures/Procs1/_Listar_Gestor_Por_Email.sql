CREATE PROCEDURE [dbo].[_Listar_Gestor_Por_Email]
(
@codemp int,
@codsuc int,
@email varchar(100)
)
AS
BEGIN
	SET NOCOUNT ON;
	
select GES_GESID, GES_NOMBRE 
from GESTOR with (nolock)
where GES_CODEMP = @codemp 
and GES_SUCID = @codsuc 
and GES_EMAIL = @email 
and GES_ESTADO = 'A' 
order by GES_NOMBRE 

end
