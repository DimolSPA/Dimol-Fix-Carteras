-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Estados_Historial] 
(@codemp int,
@grupo int ,
@idioma int ,
@tipo varchar(1),
@estadoXDoc varchar(1),
@perfil int)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);


set @query = 'SELECT estados_cartera.ect_estid ID,   
 estados_cartera_idiomas.eci_nombre Nombre
FROM estados_cartera,   
estados_cartera_idiomas
WHERE  estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp  and  
estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid  and  
estados_cartera.ect_codemp =  ' + CONVERT(VARCHAR,@codemp) +'   AND  
estados_cartera.ect_agrupa > 1  AND  
estados_cartera.ect_agrupa =  ' + CONVERT(VARCHAR,@grupo) +'   AND  
estados_cartera_idiomas.eci_idid =  ' + CONVERT(VARCHAR,@idioma) +'   AND  '

If @tipo = 'V' begin    -- originalmente era @tipo
	set @query = @query + ' estados_cartera.ect_prejud in (''P'',''A'')  AND ' 
end 
Else
begin
	set @query = @query + ' estados_cartera.ect_prejud in (''J'',''A'')  AND ' 
End 

If @estadoXDoc = 'V' begin
	set @query = @query + ' estados_cartera.ect_utiliza = ''D'' '
	end
Else
begin
	set @query = @query + ' estados_cartera.ect_utiliza = ''R'' '
End
set @query = @query + ' and ect_estid in (SELECT pfe_estid   FROM perfiles_estados    WHERE  pfe_codemp = ' + CONVERT(VARCHAR,@codemp) +'  AND      pfe_prfid = ' + CONVERT(VARCHAR,@perfil) +'  ) order by eci_nombre'


				

 --select @query
 exec(@query)	
	

end

