-- =============================================
-- Author:		FMO
-- Create date: 23-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Clasificacion_Insumo] (@codemp int, @insid int)
AS
BEGIN
	SET NOCOUNT ON;
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [INS_CODEMP]
      ,[INS_INSID]
      ,[INS_CODIGO]
      ,[INS_NOMBRE]
      ,[INS_ESTADO]
      ,[INS_STOCK_TOTAL]
      ,[INS_STOCK_RESERVADO]
      ,[INS_STOCK_TRANSITO]
      ,[INS_STOCK_MERMA]
      ,[INS_STOCK_MINIMO]
      ,[INS_STOCK_MAXIMO]
      ,[INS_STOCK_CIERRE_ANIO]
      ,[INS_ORDEN_BODEGA]
      ,[INS_ORDEN_OTRO]
      ,[INS_FECINGRESO]
      ,[INS_FECFIN]
      ,[INS_ARANCEL]
      ,[INS_PORCARAN]
      ,[INS_EXENTO]
      ,[INS_TIPO]
      ,[INS_TERMINADO]
      ,[INS_COSTO]
      ,[INS_COSTO_PROM]
      ,[INS_TIPING]
      ,[INS_TIPID]
      ,[INS_CATID]
      ,[INS_CUBICAJE]
      ,[INS_UNMLGT]
      ,[INS_ALTO]
      ,[INS_ANCHO]
      ,[INS_LARGO]
      ,[INS_PERECIBLE]
      ,[INS_FECVENC]
      ,[INS_GASTJUD]
      ,[INS_IMPDEU]
      ,[INS_IMPCLI]
      ,[INS_PCTID]
      ,[INS_PACK]
      ,[INS_PACKINT]
      ,[INS_SPCID]
  FROM [INSUMOS]
  WHERE INS_CODEMP = @codemp
  AND INS_INSID = @insid		
END
