﻿-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 03-09-2014
-- Description:	Procedimiento para listar tipos causanotas para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposCausaNotas_Grilla]
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
  (' set @query = @query + 'SELECT [TNT_CODEMP] Codemp        
	  ,[TNT_TNTID] id
	  ,[TNT_NOMBRE] nombre        
	  ,[TNT_CODIGO] codigo 
	  FROM [TIPOS_CAUSA_NCND] 
	  WHERE [TNT_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+'
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
