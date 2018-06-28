CREATE procedure [dbo].[_Trae_Ruta_Estampes_Deudor] (@codemp int, @pclid numeric(15), @ctcid numeric(15)) as 
 select 'Estampes/' + CAST(DDE_PCLID AS VARCHAR) + '/'+ CAST(DDE_CTCID AS VARCHAR) + '/'+ CAST(DDE_ROLID AS VARCHAR) + '/' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT ARCHIVO 
 from DEUDORES_ESTAMPES 
 where DDE_CODEMP = @codemp 
 AND DDE_PCLID = @pclid 
 AND DDE_CTCID = @ctcid
