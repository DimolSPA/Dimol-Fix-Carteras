-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Doc_Deudor_Grilla_Count]
(
@codemp int,
@ctcid int,
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

declare @ruta varchar(200) = (SELECT cfs_valtxt FROM configuracion_sistema WHERE cfs_cfsid = 13)
declare @rut varchar(20) = (select CTC_RUT from DEUDORES where CTC_CTCID = @ctcid)

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(tddi.tdi_nombre) count
			 FROM deudores_documentos dd,  
             tipos_documentos_deudores tdd,  
             tipos_documentos_deudores_idiomas tddi
             WHERE  tdd.tdd_codemp = dd.dcd_codemp  and 
             tdd.tdd_tddid = dd.dcd_tddid  and 
             tddi.tdi_codemp = tdd.tdd_codemp  and 
             tddi.tdi_tddid = tdd.tdd_tddid  and 
             dd.dcd_codemp =  ' + CONVERT(VARCHAR,@codemp) +'
             and tdd.tdd_tipo = ''R''
             and tddi.tdi_idid = ' + CONVERT(VARCHAR,@idioma) +'
             and dcd_ctcid = ' + CONVERT(VARCHAR,@ctcid )

set @query = @query +') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
