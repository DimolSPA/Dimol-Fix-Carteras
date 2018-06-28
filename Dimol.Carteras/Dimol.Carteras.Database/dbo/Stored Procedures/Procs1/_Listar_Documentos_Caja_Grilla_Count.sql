-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar subcarteras para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Documentos_Caja_Grilla_Count]
(
@codemp int ,
@idioma int ,
@ctcid int ,
@pclid int ,
@tipo_movimiento varchar(1),
@tipo_documento int,
@emplid int ,
@num_cta varchar(20),
@monto_desde decimal(16,4),
@monto_hasta decimal(16,4),
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
  
set @query = '  select count(anio) count from
  (select *,ROW_NUMBER() OVER (ORDER BY NumeroDocumento asc) as row from    
  (	' 
  
set @query = @query + 'select ddi_anio Anio , 
ddi_numdoc NumeroDocumento, 
case ddi_tipmov
when ''I'' then  (select dbo._Trae_Etiqueta(''TipAsi2'', '+ convert(varchar,@idioma) +') )
when ''E'' then (select dbo._Trae_Etiqueta(''TipAsi3'', '+convert(varchar,@idioma)+') ) end as TipoMovimiento,
tci_nombre TipoDocumento, 
ddi_monto Monto, 
pcl_nomfant NombreCliente,
ctc_nomfant NombreDeudor, 
ddi_numcta NumeroCuenta, 
case ddi_empleado
when ''S'' then epl_nombre + '' '' + epl_apepat end as Empleado,
edi_nombre Estado
 from view_documentos_diarios
where ddi_codemp= '+convert(varchar,@codemp) +'
and tci_idid= '+convert(varchar,@idioma)+'
and edi_idiid= '+convert(varchar,@idioma)


If @pclid != '' begin
	set @query = @query + ' and ddi_pclid= '+ convert(varchar,@pclid)
            End

If @ctcid != '' begin
    set @query = @query + ' and ddi_ctcid= '+ convert(varchar,@ctcid)
            End


If @tipo_movimiento != '' begin
    set @query = @query + ' and ddi_tipmov=  ''' + @tipo_movimiento +''''
            End 

If @tipo_documento <> 0 begin
    set @query = @query + ' and ddi_tpcid= '+ convert(varchar,@tipo_documento)
            End 

If @emplid <> 0 begin
    set @query = @query + ' and ddi_emplid= '+convert(varchar,@emplid)
            End 

If  @num_cta != ''  begin
    set @query = @query + ' and ddi_numcta like ''%' + @num_cta + '%'''
            End 

If @monto_desde > 0 begin
    set @query = @query + ' and ddi_monto >= '+ convert(varchar,@monto_desde)
            End 

If @monto_hasta > 0 begin
    set @query = @query + ' and ddi_monto <= '+ convert(varchar,@monto_hasta)
            End 

  
set @query = @query + ') as tabla  ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
