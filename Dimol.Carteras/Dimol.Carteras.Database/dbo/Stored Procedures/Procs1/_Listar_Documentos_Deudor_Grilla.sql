-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar subcarteras para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Documentos_Deudor_Grilla]
(
@codemp int ,
@idioma int ,
@ctcid int ,
@pclid int ,
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
  (	' 
  
set @query = @query + 'SELECT dcd_pclid Pclid, 
						dcd_ctcid Ctcid,
						dcd_dcdid Dcdid,   
						tddi.tdi_nombre TipoDocumento,   
						dcd_nombre Archivo,   
						provcli.pcl_nomfant NombreCliente,
						(select CTC_RUT from DEUDORES where CTC_CTCID =  dcd_ctcid) RutDeudor
						FROM {oj deudores_documentos LEFT OUTER JOIN provcli ON deudores_documentos.dcd_codemp = provcli.pcl_codemp 
						AND deudores_documentos.dcd_pclid = provcli.pcl_pclid},   
						tipos_documentos_deudores_idiomas tddi
						WHERE  tddi.tdi_codemp = dcd_codemp  and  
						tddi.tdi_tddid = dcd_tddid  and  
						tddi.tdi_codemp =  '+ convert(varchar,@codemp)+'
						and tddi.tdi_idid =  '+ convert(varchar,@idioma)+'
						and dcd_ctcid = '+ convert(varchar,@ctcid)
						

If @pclid <> '' begin
    set @query = @query + ' and dcd_pclid  = '+ convert(varchar,@pclid)
End
  
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
