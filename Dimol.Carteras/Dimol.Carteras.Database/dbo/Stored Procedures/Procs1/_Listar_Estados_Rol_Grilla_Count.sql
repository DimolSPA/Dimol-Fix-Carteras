-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Estados_Rol_Grilla_Count]
(
@codemp int,
@rolid int,
@idioma int,
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
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(re.rle_rolid) count  
            FROM rol_estados re,  
            materia_judicial_idiomas mji,  
            estados_cartera_idiomas eci, usuarios u
            WHERE  re.rle_codemp = mji.mji_codemp  and 
            re.rle_esjid = mji.mji_esjid  and 
            re.rle_codemp = eci.eci_codemp  and 
            re.rle_estid = eci.eci_estid
            and re.rle_codemp = u.USR_CODEMP
            and re.rle_usrid = u.usr_usrid
            and re.rle_codemp= ' + CONVERT(VARCHAR,@codemp) +'
            and mji_idid= ' + CONVERT(VARCHAR,@idioma) +'
            and eci_idid= ' + CONVERT(VARCHAR,@idioma) +'
            and rle_rolid= ' + CONVERT(VARCHAR,@rolid)
            --ORDER BY re.rle_rolid ASC,   
            --re.rle_fecjud DESC,   
            --re.rle_fecha Asc

set @query = @query +') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
