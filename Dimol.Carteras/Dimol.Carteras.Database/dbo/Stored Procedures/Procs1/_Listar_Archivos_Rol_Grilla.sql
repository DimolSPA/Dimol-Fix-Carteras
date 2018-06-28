-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Archivos_Rol_Grilla]
(
@codemp int,
@ctcid int,
@rolid integer, 
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

--declare @codemp int = 1
--declare @pclid integer = 279
--declare @ctcid int = 1194244
--declare @idioma int = 1
declare @ruta varchar(200) = (SELECT cfs_valtxt FROM configuracion_sistema WHERE cfs_cfsid = 13)
declare @rut varchar(20) = (select CTC_RUT from DEUDORES where CTC_CTCID = @ctcid)

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'SELECT ri.rif_item,  
            tii.tfi_nombre,  
            ri.rif_nombre,  
            ri.rif_ubicacion,'
set @query = @query +'''../'+@ruta+'/' + CONVERT(VARCHAR,@codemp)+'/'+@rut+'/' + ''' + rif_nombre as Archivo
            FROM rol_informes ri,  
            tipos_informes_idiomas tii
            WHERE  ri.rif_codemp = tii.tfi_codemp  and 
            ri.rif_tifid = tii.tfi_tifid  and 
            ri.rif_codemp = ' + CONVERT(VARCHAR,@codemp) +'
            and ri.rif_rolid = ' + CONVERT(VARCHAR,@rolid) +'
            and tfi_idid = ' + CONVERT(VARCHAR,@idioma)
            --order by rif_ubicacion, tfi_nombre, rif_nombre

set @query = @query +') as tabla  ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
