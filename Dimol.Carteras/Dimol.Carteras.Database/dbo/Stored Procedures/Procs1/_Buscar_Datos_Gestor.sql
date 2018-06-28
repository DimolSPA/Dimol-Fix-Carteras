create procedure [dbo].[_Buscar_Datos_Gestor] (@mail varchar(1000)) AS 
select GES_EMAIL Email, GES_NOMBRE Nombre, GES_TELEFONO Telefono
from GESTOR
WHERE GES_EMAIL = @mail
