
/****** Object:  StoredProcedure [dbo].[_Trae_Buscar_Facturas]    Script Date: 05-04-2017 17:07:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

   CREATE procedure [dbo].[_Trae_Buscar_Facturas] as 

 select rut, dv, Facturas, cruce, SS, MILANO, Nombre1, Div, Referencia, M, Ano
 from dbo.BuscarFacturas 
--where Referencia in (select ref from ref)
GO

