-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar subcarteras para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Subcartera_Grilla_Count]
(
@codemp int ,
@nombre varchar(400) ,
@rut varchar(20),
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
--declare @provcli_consulta int = (Select PCC_PCLID_VER FROM PROVCLI_CONSULTA  where pcc_usrid = @usrid)

  
set @query = '  select count from
  (select *,ROW_NUMBER() OVER (ORDER BY count) as row from    
  (	' 
  
set @query = @query + 'select count(sbc_sbcid) as ''count'' 
from subcarteras 
where sbc_codemp =' + convert(varchar,@codemp)

If @nombre <> '' begin
    set @query = @query + ' and sbc_nombre like ''%'+ @nombre+ '%'''
End


If @rut <> '' begin
	set @query = @query + ' and sbc_rut like ''%' + @rut + '%'''
End

  
set @query = @query + ') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
