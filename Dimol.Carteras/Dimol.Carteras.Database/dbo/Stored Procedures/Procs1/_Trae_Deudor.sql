CREATE PROCEDURE [dbo].[_Trae_Deudor] (@codemp int, @ctcid int)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [CTC_CODEMP]
      ,[CTC_CTCID]
      ,[CTC_RUT]
      ,[CTC_NUMERO]
      ,[CTC_DIGITO]
      ,[CTC_NOMBRE]
      ,[CTC_APEPAT]
      ,[CTC_APEMAT]
      ,[CTC_NOMFANT]
      ,[CTC_COMID]
      ,CO.COM_CIUID
	  ,CI.CIU_REGID 
      ,RE.REG_PAIID
      ,[CTC_DIRECCION]
      ,[CTC_PARTEMP]
      ,[CTC_FECING]
      ,[CTC_SOCID]
      ,(select ctc_nomfant from DEUDORES dd where dd.CTC_CODEMP = d.ctc_codemp and dd.CTC_CTCID = d.ctc_socid) SOCIEDAD
      ,[CTC_ESTDIR]
      ,[CTC_QUIEBRA]
      ,[CTC_NACEXT]
      ,(select case count(ROL_PREQUIEBRA) when 0 then 'N' else 'S' end Prequiebra from ROL where ROL_CODEMP = CTC_CODEMP and ROL_CTCID = ctc_ctcid and ROL_PREQUIEBRA = 'S') as Prequiebra
      ,ISNULL((select CCC_CATEGORIA from CARTERA_CLIENTES_CATEGORIA WHERE CTC_CODEMP = CCC_CODEMP  and CCC_CTCID = ctc_ctcid ),'S')  AS Categoria
	  ,[CTC_SOLICITA_QUIEBRA]
  FROM [DEUDORES] d
  , COMUNA CO, CIUDAD CI , REGION RE
  where d.CTC_CODEMP = @codemp and d.CTC_CTCID = @ctcid
  and CO.COM_COMID = D.CTC_COMID
  AND CO.COM_CIUID = CI.CIU_CIUID
  AND CI.CIU_REGID = RE.REG_REGID

		
END