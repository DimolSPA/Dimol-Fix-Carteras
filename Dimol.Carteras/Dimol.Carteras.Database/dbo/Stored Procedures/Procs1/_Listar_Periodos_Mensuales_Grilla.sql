-- =============================================
-- Author:		Rodrigo GarridoA
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Periodos_Mensuales_Grilla]
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
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT [PCM_CODEMP] Codemp        
	  ,[PCM_ANIO] ano        
	  ,[PCM_MES] mes        
	  ,[PCM_INICIO] inicio
	  ,[PCM_FIN] fin
	  ,[PCM_HABILITADO] habilitado 
	  ,[PCM_FINALIZADO] finalizado    
	  ,CONVERT(varchar(5), PCM_CODEMP) + '''' + CONVERT(varchar(5), PCM_ANIO) + '''' + CONVERT(varchar(5), PCM_MES) IdPeriodoMensual
	  FROM [dbo].[PERIODOS_CONTABLES_MESES]  
	  WHERE [PCM_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
