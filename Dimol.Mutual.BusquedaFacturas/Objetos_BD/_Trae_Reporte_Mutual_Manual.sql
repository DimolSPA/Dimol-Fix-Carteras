
/****** Object:  StoredProcedure [dbo].[_Trae_Reporte_Mutual_Manual]    Script Date: 05-04-2017 17:06:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[_Trae_Reporte_Mutual_Manual] as
SELECT clientes.rut, 
clientes.dv, 
clientes.Nombre, 
base_mutual.Numero, --base_miguel.Numero, 
base_mutual.FECHA_DOC, --base_miguel.FECHA_DOC, 
base_mutual.prestacion,  --base_miguel.prestacion,  
base_mutual.agencia, --base_miguel.agencia, 
base_mutual.Saldo --base_miguel.Saldo 
FROM base_mutual --base_miguel 
INNER JOIN clientes 
ON base_mutual.rut = clientes.rut --base_miguel.Rut_Num = clientes.rut 


GO

