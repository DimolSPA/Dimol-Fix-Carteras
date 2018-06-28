-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Deudor_Cpbt_Grilla]
(
@codemp int ,
@idioma int ,
@pclid int ,
@sbcid int ,
@nombre varchar(400) ,
@paterno varchar(100) ,
@materno varchar(100),
@rut varchar(20),
@nom_fant varchar(600),
@tipoDocumento  varchar(20),
@numero varchar(30),
--@telefono  varchar(20),
--@email varchar(300),
--@direccion varchar(800),

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
       deudores.ctc_nomfant NombreFantasia, tci.tci_nombre TipoDocumento, cccd.ccb_numero Numero, cccd.ccb_fecdoc FechaDocumento,  
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
			END as EstadoCpbt, eci.eci_nombre Estado, ccb_monto Monto, ccb_saldo Saldo
             FROM cartera_clientes cc,  
             deudores,  
             cartera_clientes_cpbt_doc cccd,  
             tipos_cpbtdoc_idiomas tci,  
             estados_cartera_idiomas eci, provcli
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
if @sbcid is not null 
	begin
		set @query = @query + ' and ccb_sbcid= ' +  CONVERT(VARCHAR,@sbcid)
	end
	
	-- Nombre
if @nombre is not null
begin
	set @query = @query + ' and ctc_nombre like ''%' + @nombre+ '%''';
end
-- Paterno
if @paterno is not null
begin
set @query = @query + ' and ctc_apepat like ''%'+ @paterno + '%''';
end
-- Materno
if @materno is not null
begin
set @query = @query + ' and ctc_apemat like ''%'+ @materno + '%''';
end
-- RUT
if @rut is not null
begin
set @query = @query + ' and ctc_rut like ''%' + @rut + '%''';
end
-- Nombre fantasia
if @nom_fant is not null
begin
set @query = @query + ' and ctc_nomfant like ''%' + @nom_fant + '%''';
end
-- Tipo Documento
if @tipoDocumento is not null
begin
set @query = @query + ' and ccb_tpcid = ' +CONVERT(VARCHAR,@tipoDocumento)+' ';
end
-- Numero
if @numero is not null
begin
set @query =  @query + ' and ccb_numero = ''' +  CONVERT(VARCHAR,@numero) + '''';
end


-- Telefono
/*if @telefono is not null
begin
set @query = @query + ' and ccb_ctcid in (select ddt_ctcid from deudores_telefonos where ddt_codemp = ' + CONVERT(VARCHAR,@codemp) +'
						 and ddt_numero like ''%' + CONVERT(VARCHAR,@telefono) + '%''
						 UNION
						 select dct_ctcid from deudores_contactos_telefonos where dct_codemp =  ' + CONVERT(VARCHAR,@codemp) +'
						 and dct_numero like ''%' + CONVERT(VARCHAR,@telefono) + '%'' ) '
						
end
-- eMail
if @email is not null
begin
set @query = @query + ' and ccb_ctcid in ("
                 select ddm_ctcid from deudores_mail where ddm_codemp = ' + CONVERT(VARCHAR,@codemp) +'
                 and ddm_mail like ''%' + lower(@email) + '%''
                 UNION
                 select dcm_ctcid from deudores_contactos_mail where dcm_codemp = ' + CONVERT(VARCHAR,@codemp) +'
                 and dcm_mail like ''%' + lower(@email) + '%'' ) '
end
-- Direccion
if @direccion is not null
begin
set @query = @query + ' and ctc_direccion like ''%' +@direccion + '%''';
end
*/
  
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	

END
