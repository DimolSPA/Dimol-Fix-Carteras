CREATE Procedure [dbo].[_Delete_Cartera_Clientes_Estados_Acciones](
@codemp integer, 
@pclid integer, 
@ctcid integer,
@accid integer) 
as  

;WITH CTE AS
(
SELECT TOP 1 *
FROM [dbo].[CARTERA_CLIENTES_ESTADOS_ACCIONES]
WHERE [CEA_CODEMP] = @codemp
AND [CEA_PCLID] = @pclid
AND [CEA_CTCID]=@ctcid
AND [CEA_ACCID] = @accid
ORDER BY cea_fecha DESC
)
DELETE FROM CTE