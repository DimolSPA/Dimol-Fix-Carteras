CREATE PROCEDURE [dbo].[_Listar_MaximaConvencional_Grilla_Count]
(
@codemp int,
@idioma int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(mxc_mxcid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT maxima_convencional.mxc_mxcid,
				maxima_convencional.MXC_CODEMP,
				maxima_convencional.MXC_TPCID,
				maxima_convencional.MXC_TIPO,
				maxima_convencional.MXC_APLICA,
				maxima_convencional.MXC_CODMON,
				maxima_convencional.MXC_DESDE,
				maxima_convencional.MXC_HASTA,
				maxima_convencional.MXC_VALOR,
				tipos_cpbtdoc_idiomas.tci_nombre
                FROM maxima_convencional,   
                tipos_cpbtdoc_idiomas
                WHERE  maxima_convencional.mxc_codemp = tipos_cpbtdoc_idiomas.tci_codemp  and  
                maxima_convencional.mxc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid  and  
                maxima_convencional.mxc_codemp = '+ CONVERT(VARCHAR,@codemp) +'
                and tipos_cpbtdoc_idiomas.tci_idid = '+ CONVERT(VARCHAR,@idioma) +'
  
   '
   
   set @query = @query +')as tabla ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
