-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Email_Contacto_Deudor_Grilla]
(
@codemp int,
@ctcid integer, 
@email varchar(200),
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

set @query = @query +'SELECT vdnc.ddc_ctcid Ctcid,  
             vdnc.tci_nombre TipoContacto,  
             vdnc.ddc_nombre NombreContacto,  
             vdnc.dcm_mail mail, 
             dcm_masivo Masivo, 
            (SELECT ei.eti_descripcion  
				FROM etiquetas e,   
				etiquetas_idiomas  ei 
				WHERE ( ei.eti_etiid = e.etq_etqid ) and  
					( ( e.ETQ_DESCRIPCION like dcm_tipo+''%'') 
					AND e.ETQ_CODIGO like ''TipMail%'' and				 
					( ei.eti_idid = ' + CONVERT(VARCHAR,@idioma) +'))) as DescTipo,
             dcm_tipo TipoEmail,  
             vdnc.ddc_estado IdEstado,  
             vdnc.ddc_ddcid Ddcid,  
             vdnc.ddc_estdir EstadoDireccion,  
             isnull(vdnc.ddc_comid,0) Comuna,
             tci_ticid ticid,
             ddc_direccion Direccion, 
             isnull((select COM_CIUID from COMUNA where COM_COMID = ddc_comid),0) as Ciudad,
			 isnull((select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = ddc_comid)),0) as Region,
			 isnull((select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = ddc_comid))),0) as Pais
             FROM view_deudores_mail_contactos vdnc
             WHERE  vdnc.ddc_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
             and vdnc.ddc_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ '
			and vdnc.ddc_estado =''A'''

if @email is not null
begin
set @query = @query + ' and   vdnc.dcm_mail = = '+ @email
end

set @query = @query +' union select d.ctcid,'''', '''', d.mail, ''N'', '''','''',  ''A'', 0, 1,0,  0, '''', 0,0,0
						from deudores_mail_sitrel d
						where d.codemp = ' + CONVERT(VARCHAR,@codemp)+ '
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
