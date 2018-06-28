-- =============================================
-- Author:		Rodrigo GarridoA
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposCausaGuias_Grilla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(id) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT [TGD_CODEMP] Codemp        
	  ,[TGD_TGDID] id
	  ,[TGD_NOMBRE] nombre        
	  ,[TGD_CODIGO] codigo        
	  ,CONVERT(varchar(5), TGD_CODEMP) + '''' + CONVERT(varchar(5), TGD_TGDID) IdTiposCausaGuias
	  FROM [TIPOS_CAUSA_GUIAS] 
	  WHERE [TGD_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= 0' 

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
