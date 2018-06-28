CREATE PROCEDURE [dbo].[_Listar_Estados_Cliente_Perfil] 
(@codemp int,
@grupo int ,
@idioma int ,
@estadoXDoc varchar(1),
@perfil int,
@pclid int)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);


set @query = 'SELECT ec.ect_estid ID,   
		eci.eci_nombre Nombre
FROM estados_cartera ec with(nolock)
	JOIN estados_cartera_idiomas eci with(nolock)
ON ec.ect_codemp = eci.eci_codemp
AND ec.ect_estid = eci.eci_estid
JOIN perfiles_estados per with(nolock)
ON ec.ect_estid = per.pfe_estid
AND ec.ect_codemp = per.pfe_codemp
JOIN clientes_estados cle with(nolock)
ON per.pfe_estid = cle.estid
AND per.pfe_codemp = cle.codemp
WHERE  ec.ect_codemp =  ' + CONVERT(VARCHAR,@codemp) + 
' AND ec.ect_agrupa > 1  
AND ec.ect_agrupa =  ' + CONVERT(VARCHAR,@grupo) + 
' AND eci.eci_idid =  ' + CONVERT(VARCHAR,@idioma) + 
' AND '
If @estadoXDoc = 'V' begin    
	set @query = @query + 'ec.ect_prejud in (''P'',''A'')' 
end 
Else
begin
	set @query = @query + 'ec.ect_prejud in (''J'',''A'')' 
End 

--If @estadoXDoc = 'V' begin
--	set @query = @query + ' AND ec.ect_utiliza = ''D'' '
--	end
--Else
--begin

--	set @query = @query + ' AND ec.ect_utiliza = ''R'' '
--End
set @query = @query + ' AND per.pfe_prfid = ' + CONVERT(VARCHAR,@perfil) + '' +
' AND cle.pclid = ' + CONVERT(VARCHAR,@pclid) + ' ' +
'order by eci_nombre'

 --elect @query
 exec(@query)	
	

end