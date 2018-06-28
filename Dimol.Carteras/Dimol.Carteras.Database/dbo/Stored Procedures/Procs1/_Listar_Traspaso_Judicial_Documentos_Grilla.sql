﻿CREATE PROCEDURE [dbo].[_Listar_Traspaso_Judicial_Documentos_Grilla]
(
	@codemp int,
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

	DECLARE @query varchar(8000) = '',
	@estid int

	SELECT @estid = EMC_VALNUM FROM EMPRESA_CONFIGURACION WHERE EMC_CODEMP = @codemp AND EMC_EMCID = 66

	SET @query = 'select * from
	  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
	  ('

	SET @query = @query + '
		SELECT
			ccb_pclid Pclid,
			ccb_ctcid Ctcid,
			CC.pcl_nomfant Cliente,
			ctc_rut RutDeudor,
			ctc_rut + '' '' + ctc_nomfant Deudor,
			tci_nombre TipoDocumento,
			ccb_ccbid ccbid,
			ccb_numero Numero,
			ccb_fecing FechaAsignacion,
			ccb_fecvenc FechaVencimiento,
			ccb_monto Monto, 
			ccb_saldo Saldo,
			case ccb_estcpbt
				when ''J'' then ''JUDICIAL''
				when ''F'' then ''FINALIZADO''
				when ''V'' then ''VIGENTE''
				when ''X'' then ''NULO''
			end Estado,
			ccb_estcpbt EstadoCpbt,
			sbc_nombre Asegurado
		  
		FROM cartera_clientes_documentos_cpbt_doc CC
		INNER JOIN idiomas idi on 
			idi.idi_idid = tci_idid AND 
			idi.idi_idid = eci_idid AND 
			idi.idi_idid = mci_idid AND 
			idi.idi_idid = ' + convert(char,@idioma) + '
		INNER JOIN PROVCLI PC on
			PC.PCL_CODEMP = CC.CCB_CODEMP AND
			PC.PCL_PCLID = CC.CCB_PCLID
			
		WHERE 
		ccb_codemp = ' + convert(char,@codemp) + ' AND 
		ccb_estcpbt = ''V'' AND 
		(ESTIJ = ' + convert(char,@estid) + ' OR ECI_ESTID = ' + convert(char,@estid) + ') AND
		PC.PCL_TIPCLI <> ''P'''
	
	
	set @query = @query +') as tabla  ) as t
	  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

	if @where is not null
	begin
		set @query = @query + @where;
	end
	
	exec(@query)	
END