

CREATE PROCEDURE [dbo].[_Buscar_Deudor_Cpbt_Mail_Grilla]
(
@codemp int ,
@idioma int ,
@pclid int ,
@ctcid int ,

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
  
set @query = @query + 'SELECT pcl_rut RutCliente, pcl_nomfant NombreCliente, ccb_pclid Pclid, ccb_ctcid Ctcid, ccb_ccbid Ccbid, deudores.ctc_rut Rut,
		case ccb_codmon when 1 then ''PESOS'' when 2 then ''UF'' when 3 then ''DOLAR'' end as ccb_codmon,
       deudores.ctc_nomfant NombreFantasia, tci.tci_nombre TipoDocumento, cccd.ccb_numero Numero, cccd.ccb_numesp Negocio, cccd.ccb_fecdoc FechaDocumento,  
             cccd.ccb_fecvenc FechaVencimiento,  
             CASE cccd.ccb_estcpbt  
			WHEN ''V'' THEN ( SELECT etiquetas_idiomas.eti_descripcion FROM etiquetas, etiquetas_idiomas  
							WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and ( ( etiquetas.etq_codigo = ''EstCart1'' ) AND  
         ( etiquetas_idiomas.eti_idid = ' + CONVERT(VARCHAR,@idioma) +' )))
			WHEN ''F'' THEN ( SELECT etiquetas_idiomas.eti_descripcion FROM etiquetas, etiquetas_idiomas  
							WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and ( ( etiquetas.etq_codigo = ''EstCart2'' ) AND  
         ( etiquetas_idiomas.eti_idid = ' + CONVERT(VARCHAR,@idioma) +')))
			WHEN ''X'' THEN ( SELECT etiquetas_idiomas.eti_descripcion FROM etiquetas, etiquetas_idiomas  
							WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and ( ( etiquetas.etq_codigo = ''EstCart3'' ) AND  
         ( etiquetas_idiomas.eti_idid = ' + CONVERT(VARCHAR,@idioma) +' )))
			WHEN ''J'' THEN ( SELECT etiquetas_idiomas.eti_descripcion FROM etiquetas, etiquetas_idiomas  
							WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and ( ( etiquetas.etq_codigo = ''EstCart4'' ) AND  
         ( etiquetas_idiomas.eti_idid =' + CONVERT(VARCHAR,@idioma) +' )))
			ELSE ''''
			END as EstadoCpbt, eci.eci_nombre Estado, ccb_monto Monto, ccb_saldo Saldo, pcc.pcc_nombre Carga 
             FROM cartera_clientes cc,  
             deudores,  
              
             tipos_cpbtdoc_idiomas tci,  
             estados_cartera_idiomas eci, provcli,
			 PROVCLI_CODIGO_CARGA pcc RIGHT OUTER JOIN  cartera_clientes_cpbt_doc cccd
			 ON pcc.pcc_codemp = cccd.ccb_codemp and 
			 pcc.pcc_pclid = cccd.ccb_pclid and 
			 pcc.pcc_codid = cccd.ccb_codid
             WHERE  deudores.ctc_codemp = cc.ctc_codemp  and 
             deudores.ctc_ctcid = cc.ctc_ctcid  and 
              provcli.pcl_codemp = cc.ctc_codemp  and 
             provcli.pcl_pclid = cc.ctc_pclid  and 
             cccd.ccb_codemp = cc.ctc_codemp  and 
             cccd.ccb_pclid = cc.ctc_pclid  and 
             cccd.ccb_ctcid = cc.ctc_ctcid  and 
             cccd.ccb_codemp = tci.tci_codemp  and 
             cccd.ccb_tpcid = tci.tci_tpcid  and 
             cccd.ccb_codemp = eci.eci_codemp  and 
			 cccd.ccb_estcpbt = ''V'' and 
             cccd.ccb_estid = eci.eci_estid
             and ccb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
             and eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
             and tci_idid = ' + CONVERT(VARCHAR,@idioma) 
	-- Cliente
if @pclid is not null 
	begin
		set @query = @query + ' and ccb_pclid= ' +  CONVERT(VARCHAR,@pclid)
	end

	-- Asegurado
if @ctcid is not null 
	begin
		set @query = @query + ' and ccb_ctcid= ' +  CONVERT(VARCHAR,@ctcid)
	end
	
  
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	

END
