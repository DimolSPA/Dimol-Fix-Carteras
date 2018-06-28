-- =============================================
-- Author:	Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Periodos_Grilla]
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
  (	 SELECT [PEC_CODEMP] Codemp        
 ,[PEC_ANIO] ano        
 ,[PEC_HABILITIADO] habilitado        
 ,[PEC_FINALIZADO] finalizado  
    
 FROM [dbo].[PERIODOS_CONTABLES]    
   ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 --exec(@query)	

END
