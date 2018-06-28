-- =============================================
-- Author:		Rodrigo GarridoA
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Periodos_Grilla_Count]
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
  
set @query = '  select count(ano) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT [PEC_CODEMP] Codemp        
	  ,[PEC_ANIO] ano        
	  ,[PEC_HABILITADO] habilitado        
	  ,[PEC_FINALIZADO] finalizado  
	  ,CONVERT(varchar(5), PEC_CODEMP) + '''' + CONVERT(varchar(5), PEC_ANIO) IdPeriodo
	  FROM [dbo].[PERIODOS_CONTABLES]  
	  WHERE [PEC_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= 0 '

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
