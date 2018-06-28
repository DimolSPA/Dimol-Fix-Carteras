
-- =============================================
-- Author:		César León
-- Create date: 27-03-2018
-- Description:	Listado de traspasos hechos para módulo de traspaso judicial previsional
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Traspaso_Hecho_Documentos_Grilla_Previsional]
(
	@codemp int,
	@fecha_desde varchar(20),
	@fecha_hasta varchar(20),
	@where varchar(1000),
	@sidx varchar(255),
	@sord varchar(10),
	@inicio int,
	@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

	declare @query varchar(8000) = ''
	declare @desde datetime, @hasta datetime

	set @desde = CONVERT(datetime, @fecha_desde, 105)
	set @hasta = CONVERT(datetime, @fecha_hasta, 105)

	set @query = 'select * from
	  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
	  ('
	  
	set @query = @query + '
		SELECT Pclid, Ctcid, Cliente, RutDeudor, Deudor, NumResolucion, FecResolucion, SUM(Monto) Monto, SUM(Saldo) Saldo, Estado, EstadoCpbt
		FROM (
			SELECT distinct
				cpbt.ccb_pclid Pclid,
				cpbt.ccb_ctcid Ctcid,
				p.PCL_NOMFANT Cliente,
				d.CTC_RUT RutDeudor,
				d.CTC_NOMFANT Deudor,
				cpbt.ccb_monto Monto, 
				cpbt.ccb_saldo Saldo,
				CASE cpbt.ccb_estcpbt
					when ''J'' then ''JUDICIAL''
					when ''F'' then ''FINALIZADO''
					when ''V'' then ''VIGENTE''
					when ''X'' then ''NULO''
				END Estado,
				cpbt.ccb_estcpbt EstadoCpbt,
				cpbt_ext.NUM_RESOLUCION NumResolucion,
				cpbt_ext.FEC_RESOLUCION FecResolucion

			FROM 
				CARTERA_CLIENTES_ESTADOS_HISTORIAL h WITH (NOLOCK)

				JOIN CARTERA_CLIENTES_CPBT_DOC cpbt WITH (NOLOCK) on 
					h.CEH_CODEMP = cpbt.CCB_CODEMP
					AND h.CEH_PCLID = cpbt. CCB_PCLID
					AND h.CEH_CTCID = cpbt.CCB_CTCID
					AND h.CEH_CCBID = cpbt.CCB_CCBID
					AND h.CEH_ESTID = cpbt.CCB_ESTIDJ
				JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO cpbt_ext WITH (NOLOCK) on 
					cpbt_ext.CCB_CODEMP = cpbt.CCB_CODEMP
					AND cpbt_ext.CCB_PCLID = cpbt. CCB_PCLID
					AND cpbt_ext.CCB_CTCID = cpbt.CCB_CTCID
					AND cpbt_ext.CCB_CCBID = cpbt.CCB_CCBID
				JOIN PROVCLI p WITH (NOLOCK) on 
					cpbt.CCB_CODEMP = p.PCL_CODEMP
					AND cpbt.CCB_PCLID =  p.PCL_PCLID
				JOIN DEUDORES d WITH (NOLOCK) on 
					cpbt.CCB_CODEMP = d.CTC_CODEMP
					AND cpbt.CCB_CTCID = d.CTC_CTCID
				JOIN TIPOS_CPBTDOC_IDIOMAS tci WITH (NOLOCK) on 
					cpbt.CCB_CODEMP = tci.TCI_CODEMP
					AND cpbt.CCB_TPCID = tci.TCI_TPCID

			WHERE h.CEH_CODEMP = ' + CONVERT(char,@codemp) + ' 
				AND h.CEH_ESTID = (SELECT EMC_VALNUM FROM EMPRESA_CONFIGURACION WHERE EMC_CODEMP = ' + CONVERT(char,@codemp) + ' AND EMC_EMCID = 68)
				and h.CEH_FECHA > ''' + CONVERT (char(10),@desde,112) + ''' 
				and h.CEH_FECHA < ''' + CONVERT (char(10),DATEADD(day,1, @hasta),112) + '''
		) a
		GROUP by Pclid, Ctcid, Cliente, RutDeudor, Deudor, NumResolucion, FecResolucion, Estado, EstadoCpbt
	'

	set @query = @query +') as tabla  ) as t
	  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= ' + CONVERT(VARCHAR,@limite)

	if @where is not null
	begin
		set @query = @query + @where;
	end

	exec(@query)	
END
