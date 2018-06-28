
CREATE PROCEDURE [dbo].[_Listar_Estados_Email_Cliente_Grilla]
	 @codemp int,  
	 @pclid int,
     @idid int,                                               
     @inicio int,                        
     @limite int  
AS
BEGIN
	SELECT * FROM (
		SELECT *, ROW_NUMBER()	OVER (
			
			ORDER BY ESTADO ASC
		) AS fila FROM(
				SELECT [dbo].[ESTADOS_CARTERA].[ECT_NOMBRE] as ESTADO, 
					[dbo].[CLIENTES_ESTADOS].[ESTID] as ID
				FROM [dbo].[CLIENTES_ESTADOS]
					LEFT JOIN [dbo].[ESTADOS_CARTERA]
					ON [dbo].[CLIENTES_ESTADOS].ESTID = [dbo].[ESTADOS_CARTERA].[ECT_ESTID],
					[dbo].[ESTADOS_CARTERA_IDIOMAS] WITH (NOLOCK)			
				WHERE [dbo].[ESTADOS_CARTERA_IDIOMAS].[ECI_CODEMP] = [dbo].[ESTADOS_CARTERA].[ECT_CODEMP] AND
					[dbo].[ESTADOS_CARTERA_IDIOMAS].[ECI_ESTID] = [dbo].[ESTADOS_CARTERA].[ECT_ESTID] AND
					[dbo].[ESTADOS_CARTERA].[ECT_CODEMP] = CONVERT(varchar, @codemp) AND
					[dbo].[ESTADOS_CARTERA_IDIOMAS].[ECI_ESTID] = [dbo].[ESTADOS_CARTERA].ECT_ESTID AND
					[dbo].[ESTADOS_CARTERA_IDIOMAS].[ECI_IDID] = CONVERT(varchar, @idid) AND
					[dbo].[CLIENTES_ESTADOS].[CODEMP] = CONVERT(varchar, @codemp) AND
					[dbo].[ESTADOS_CARTERA].[ECT_PREJUD] IN('A', 'P') AND
					[dbo].[CLIENTES_ESTADOS].[PCLID] = CONVERT(varchar, @pclid)
			) AS tabla
		) AS t 
	WHERE fila > CONVERT(varchar, @inicio) AND fila <= CONVERT(varchar, @limite)
END
RETURN 0
