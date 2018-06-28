CREATE PROCEDURE [dbo].[_Listar_Panel_Avenimiento_Nuevos_Documentos](
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@ccbid int,
@numeroDocumento varchar(20), --/006-042'
@fechaCuota datetime, 
@numCuotas int,
@montoCuota decimal(15,2))
AS
BEGIN
;WITH meses AS
(
   SELECT 1 AS MES
   UNION ALL
   SELECT t.MES + 1 FROM meses t
   WHERE t.MES < @numCuotas
) 

select 'Cuota ' + CONVERT(VARCHAR,MES) NumCuota, MES-1 Inicio, 
	@numeroDocumento + '/' + RIGHT('000'+CAST(MES AS VARCHAR(3)),3) + '-' + RIGHT('000'+CAST(@numCuotas AS VARCHAR(3)),3)  As Numero,
	CCB_TPCID Tpcid, CCB_TIPCART TipCart, dateadd(MONTH, MES-1, @fechaCuota) as FechaDocumento, ccb_estid Estid, ccb_estcpbt Estcpbt,
	ccb_codmon CodMon, CCB_TIPCAMBIO TipCambio, @montoCuota Monto, @montoCuota Saldo, CCB_GASTJUD GastJud, CCB_GASTOTRO GastOtro, CCB_BCOID BcoId,
	CCB_RUTGIR RutGir, CCB_NOMGIR NomGir, CCB_MTCID MtcId, ccb_codid CodId, CCB_NUMESP NumEsp, CCB_NUMAGRUPA NumAgrupa, CCB_CCTID Cctid,
	CCB_SBCID SBCID, CCB_DOCORI DocOri, ccb_docant DocAnt, TERCEROID, CCB_IDCUENTA IdCuenta, CCB_DESCCUENTA DescCuenta
 from meses
 CROSS JOIN (select 11 as ccb_tpcid, ccb_tipcart, ccb_fecdoc, ccb_fecvenc, 45 as ccb_estid, 'V' as ccb_estcpbt, 
			ccb_codmon, ccb_tipcambio, 1 as ccb_monto, '2' as ccb_saldo,ccb_gastjud, ccb_gastotro,ccb_bcoid, ccb_rutgir,ccb_nomgir,
			ccb_mtcid, ccb_codid, ccb_numesp, ccb_numagrupa, ccb_cctid, ccb_sbcid, 'N' as CCB_DOCORI, 'S' as ccb_docant, 
			isnull(TERCEROID, 0) TERCEROID, CCB_IDCUENTA, CCB_DESCCUENTA 
			from
			CARTERA_CLIENTES_CPBT_DOC
			where CCB_CODEMP = @codemp
			and CCB_PCLID = @pclid
			and CCB_CTCID = @ctcid
			and CCB_CCBID = @ccbid
			and CCB_NUMERO = @numeroDocumento) Documento
OPTION (MAXRECURSION 0)
END
