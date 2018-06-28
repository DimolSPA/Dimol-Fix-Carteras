-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Telefonos_Contacto_Deudor_Grilla]
(
@codemp int,
@ctcid integer, 
@telefono varchar(20),
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
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'SELECT vdtc.tci_nombre TipoContacto,  
              vdtc.ddc_nombre NombreContacto,  
             vdtc.dct_numero Numero,  
             vdtc.dct_tipo TipoTelefono,  
             (SELECT ei.eti_descripcion  
				FROM etiquetas e,   
				etiquetas_idiomas  ei 
				WHERE ( ei.eti_etiid = e.etq_etqid ) and  
					( ( e.ETQ_DESCRIPCION like vdtc.dct_estado+''%'') 
					AND e.ETQ_CODIGO like ''EstTel%'' and				 
					( ei.eti_idid =' + CONVERT(VARCHAR,@idioma) +'))) as DescEstado,
             dct_estado IdEstado, 
             tci_ticid ticid, 
             ddc_ddcid ddcid, 
             ddc_estdir EstadoDireccion, 
             isnull(ddc_comid,0) Comuna, 
             ddc_ctcid ctcid,
             isnull(ciu_codarea,0) CodigoArea,
             ddc_direccion Direccion,
             isnull((select COM_CIUID from COMUNA where COM_COMID = ddc_comid),0) as Ciudad,
			 isnull((select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = ddc_comid)),0) as Region,
			 isnull((select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = ddc_comid))),0) as Pais
             FROM view_deudores_telefonos_contactos vdtc
             WHERE  vdtc.dct_codemp =' + CONVERT(VARCHAR,@codemp)+ '
             and vdtc.dct_ctcid =' + CONVERT(VARCHAR,@ctcid )+ '  
             and vdtc.ddc_estado =''A'''
if @telefono is not null
begin
set @query = @query + ' and   vdtc.dct_numero = '+ @telefono
end

set @query = @query +' union select '''', '''', d.numero, d.tipo, '''', ''A'', 0, 0, 1,0, d.ctcid, d.codigo_area, '''', 0,0,0
						from deudores_telefonos_sitrel d
						where d.codemp =' + CONVERT(VARCHAR,@codemp)+ '
						and d.ctcid = ' + CONVERT(VARCHAR,@ctcid )

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
