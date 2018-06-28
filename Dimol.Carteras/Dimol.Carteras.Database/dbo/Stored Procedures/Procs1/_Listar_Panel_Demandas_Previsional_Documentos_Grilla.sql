
-- =============================================
-- Author:		César León
-- Create date: 20/03/2018
-- Description:	Lista las resoluciones correspondientes a una demanda (Para el panel de demandas previsionales)
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Panel_Demandas_Previsional_Documentos_Grilla]
(
	@codemp int,
	@panelId int,
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
set @query = 'SELECT * FROM
	(SELECT *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row FROM    
	('
  
set @query = @query + '
	SELECT 
		ltRes.PANELID,
		ltRes.PCLID,
		ltRes.CTCID,
		ltRes.NUM_RESOLUCION,
		ltRes.FEC_RESOLUCION,
		SUM(ltRes.MONTO) MONTO,
		SUM(ltRes.SALDO) SALDO,
		ltRes.ESTADO
	FROM
	(
		SELECT 
			PDD.PANEL_ID PANELID
			,CPBT.CCB_PCLID PCLID
			,CPBT.CCB_CTCID CTCID
			,CPBT_EXT.NUM_RESOLUCION
			,CPBT_EXT.FEC_RESOLUCION
			,ccb_monto MONTO
			,ccb_saldo SALDO
			,case ccb_estcpbt
				when ''J'' then ''JUDICIAL''
				when ''F'' then ''FINALIZADO''
				when ''V'' then ''VIGENTE''
				when ''X'' then ''NULO''
			end ESTADO

		FROM PANEL_DEMANDA_PREVISIONAL_DOCUMENTOS PDD with (nolock)
		JOIN CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
			ON PDD.CODEMP = CPBT.CCB_CODEMP
			AND PDD.PCLID = CPBT.CCB_PCLID
			AND PDD.CTCID = CPBT.CCB_CTCID
			AND PDD.CCBID = CPBT.CCB_CCBID
		JOIN CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO CPBT_EXT with (nolock)
			ON CPBT_EXT.CCB_CODEMP = CPBT.CCB_CODEMP
			AND CPBT_EXT.CCB_PCLID = CPBT.CCB_PCLID
			AND CPBT_EXT.CCB_CTCID = CPBT.CCB_CTCID
			AND CPBT_EXT.CCB_CCBID = CPBT.CCB_CCBID
		
		WHERE PANEL_ID = '+ convert(varchar,@panelId) +'
		AND CODEMP = '+ convert(varchar,@codemp) + '
	) ltRes
	GROUP by PANELID, PCLID, CTCID, NUM_RESOLUCION, FEC_RESOLUCION, ESTADO
	'

set @query = @query +') as tabla  ) as t
  WHERE  row > ' + CONVERT(VARCHAR,@inicio) + ' AND row <= ' + CONVERT(VARCHAR,@limite)

if @where is not null
begin
	set @query = @query + @where;
end

exec(@query)	

END