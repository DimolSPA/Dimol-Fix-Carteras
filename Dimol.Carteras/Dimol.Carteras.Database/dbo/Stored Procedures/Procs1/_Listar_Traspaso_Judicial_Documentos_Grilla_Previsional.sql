
-- =============================================
-- Author:		César León
-- Create date: 27-03-2018
-- Description:	Listado para módulo de traspaso judicial previsional
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Traspaso_Judicial_Documentos_Grilla_Previsional]
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

	SELECT @estid=EMC_VALNUM FROM EMPRESA_CONFIGURACION WHERE EMC_CODEMP = @codemp AND EMC_EMCID = 66

	SET @query = 'select * from
		(select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
		('

	SET @query = @query + '
		SELECT Pclid, Ctcid, Cliente, RutDeudor, Deudor, SUM(Monto) Monto, SUM(Saldo) Saldo, Estado, EstadoCpbt, NumResolucion, FecResolucion
		FROM (
			SELECT
				CC.ccb_pclid Pclid,
				CC.ccb_ctcid Ctcid,
				CC.pcl_nomfant Cliente,
				ctc_rut RutDeudor,
				ctc_nomfant Deudor,
				ccb_monto Monto, 
				ccb_saldo Saldo,
				CASE ccb_estcpbt
					when ''J'' then ''JUDICIAL''
					when ''F'' then ''FINALIZADO''
					when ''V'' then ''VIGENTE''
					when ''X'' then ''NULO''
				END Estado,
				ccb_estcpbt EstadoCpbt,
				CC_EXT.NUM_RESOLUCION NumResolucion,
				CC_EXT.FEC_RESOLUCION FecResolucion

			FROM
				cartera_clientes_documentos_cpbt_doc CC
				JOIN idiomas IDI on CC.ECI_IDID = IDI.IDI_IDID AND CC.MCI_IDID = IDI.IDI_IDID
				INNER JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO CC_EXT on 
							CC_EXT.CCB_CODEMP = CC.CCB_CODEMP AND 
							CC_EXT.CCB_PCLID = CC.CCB_PCLID AND 
							CC_EXT.CCB_CTCID = CC.CCB_CTCID AND 
							CC_EXT.CCB_CCBID = CC.CCB_CCBID
				INNER JOIN PROVCLI PC on
							PC.PCL_CODEMP = CC.CCB_CODEMP AND
							PC.PCL_PCLID = CC.CCB_PCLID
			WHERE 
				CC.TCI_IDID = IDI.IDI_IDID
				AND CC.CCB_CODEMP = ' + convert(char,@codemp) + ' 
				AND CC.CCB_ESTCPBT = ''V'' 
				AND (CC.ESTIJ = ' + convert(char,@estid) + ' OR CC.ECI_ESTID = ' + convert(char,@estid) + ') 
				AND IDI.IDI_IDID = ' + convert(char,@idioma) + ' 
				AND PC.PCL_TIPCLI = ''P''
		) a
		GROUP by Pclid, Ctcid, Cliente, RutDeudor, Deudor, Estado, EstadoCpbt, NumResolucion, FecResolucion'

	SET @query = @query +') as tabla  ) as t
	  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

	IF @where IS NOT NULL
	BEGIN
		SET @query = @query + @where;
	END

	exec(@query)	
END