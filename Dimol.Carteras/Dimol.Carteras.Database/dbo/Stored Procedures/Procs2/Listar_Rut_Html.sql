CREATE PROCEDURE [dbo].[Listar_Rut_Html]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
declare @indice int

  SELECT @indice= IsNull(Max(cbc_numero)+1, 1)
    FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = 1 ) AND  
         ( cabacera_comprobantes.cbc_sucid = 1 ) AND  
         ( cabacera_comprobantes.cbc_tpcid = 31 ) 	
	
select [rut]
      ,[dv]
      ,[nombre]
      ,[html]
      ,[CTC_CTCID] ctcid ,(ROW_NUMBER() OVER (ORDER BY [rut])) + @indice numero, 
      (select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 559 and CCB_CTCID = CTC_CTCID)) ADHERENTE
  FROM [Iluvatar].[dbo].[AA_CASTIGO_MASIVO_MUTUAL_LEY]
  ORDER BY CONVERT(INT,(select SBC_RUT from SUBCARTERAS s where s.SBC_CODEMP = 1 AND SBC_SBCID =(select top 1  CCB_SBCID from .CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = 1 and CCB_PCLID = 559 and CCB_CTCID = CTC_CTCID)))
  

--select top 2 m.rut, m.dv, m.[nom deu] nombre, c.html
--  FROM [10.0.1.12\MSSQLSERVER14].[SII].[dbo].[Mutual_Ley] m 
--  inner join [10.0.1.12\MSSQLSERVER14].[SII].[dbo].RUT_CAPTCHA c
--on m.rut = c.RUT
--and m.dv = c.DV COLLATE database_default


end
