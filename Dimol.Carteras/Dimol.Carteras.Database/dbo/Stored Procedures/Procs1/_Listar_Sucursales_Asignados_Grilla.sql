CREATE PROCEDURE [dbo].[_Listar_Sucursales_Asignados_Grilla]
(
@codemp int,
@idUsuario int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT empresa_sucursal.esu_sucid, empresa_sucursal.esu_nombre,  
            usuarios_sucursal.uss_default as sel
            FROM empresa_sucursal,   
            usuarios_sucursal
            WHERE ( usuarios_sucursal.uss_codemp = empresa_sucursal.esu_codemp ) and  
            ( usuarios_sucursal.uss_sucid = empresa_sucursal.esu_sucid ) and  
            ( ( empresa_sucursal.esu_codemp = ' + CONVERT(VARCHAR,@codemp) +' ) AND  
            ( usuarios_sucursal.uss_usrid = ' + CONVERT(VARCHAR,@idUsuario) +' )            )
      '
   set @query = @query +')as tabla ) as t
  where  row > 0 ' 

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
