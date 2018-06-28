﻿CREATE PROCEDURE [dbo].[_Trae_CpbtDoc]
(
@codemp int,
@pclid int,
@ctcid int,
@ccbid int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [CCB_CODEMP] Codemp
      ,[CCB_PCLID] Pclid
      ,[CCB_CTCID] Ctcid
      ,[CCB_CCBID] Ccbid
      ,[CCB_TPCID] TipoDocumento
      ,[CCB_TIPCART] TipoCartera
      ,[CCB_NUMERO] NumeroCpbt
      ,[CCB_FECING] FechaIngreso
      ,[CCB_FECDOC] FechaDocumento
      ,[CCB_FECVENC] FechaVencimiento
      ,[CCB_FECULTGEST] FechaUltimaGestion
      ,[CCB_FECPLAZO] FechaPlazo
      ,[CCB_FECCALCINT] FechaCalculoInteres
      ,[CCB_FECCAST] FechaCastigo
      ,[CCB_ESTID] EstadoCartera
      ,[CCB_ESTCPBT] EstadoCpbt
      ,[CCB_CODMON] CodigoMoneda
      ,[CCB_TIPCAMBIO] TipoCambio
      ,[CCB_ASIGNADO] MontoAsignado
      ,[CCB_MONTO] Monto
      ,[CCB_SALDO] Saldo
      ,[CCB_GASTJUD] GastoJudicial
      ,[CCB_GASTOTRO] GastoOtros
      ,[CCB_INTERESES] Intereses
      ,[CCB_HONORARIOS] Honorarios
      ,[CCB_CALCHON] CalculoHonorarios
      ,[CCB_BCOID] NombreBanco
      ,[CCB_RUTGIR] RutGirador
      ,[CCB_NOMGIR] NombreGirador
      ,[CCB_MTCID] MotivoCobranza
      ,[CCB_COMENTARIO] Comentario
      ,[CCB_RETENT] Retent
      ,[CCB_CODID] CodigoCarga
      ,[CCB_NUMESP] NumeroEspecial
      ,[CCB_NUMAGRUPA] NumeroAgrupa
      ,[CCB_CARTA] Carta
      ,[CCB_COBRABLE] Cobrable
      ,[CCB_CCTID] Contrato
      ,[CCB_SBCID] Subcartera
      ,(select top 1 SBC_RUT + ' - ' + SBC_NOMBRE from SUBCARTERAS where SBC_CODEMP = ccb_codemp and SBC_SBCID = ccb_sbcid) SubcarteraRutNombre 
      ,[CCB_DOCORI] Originales
      ,[CCB_DOCANT] Antecedentes
      ,[CCB_COMPROMISO] Compromiso
	  ,[TERCEROID] tercero
	  ,[CCB_IDCUENTA] IdCuenta
      ,[CCB_DESCCUENTA] DescCuenta
  FROM [CARTERA_CLIENTES_CPBT_DOC]
  WHERE CCB_CODEMP = @codemp
  AND CCB_PCLID = @pclid
  AND CCB_CTCID = @ctcid
  AND CCB_CCBID = @ccbid


END