-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 03-09-2014
-- Description:	Procedimiento para listar tipos traslado para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposTraslado_Grilla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(Codigo) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	  SELECT distinct [TTL_CODEMP] Codemp        
	  ,[TTL_NOMBRE] Nombre  
	  ,[TTL_CODIGO] Codigo
	  ,[TTL_TTLID] Id  
	  FROM [dbo].[TIPOS_TRASLADO] tip    
	  WHERE [TTL_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+'
   '
   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
 exec(@query)	
	

END
