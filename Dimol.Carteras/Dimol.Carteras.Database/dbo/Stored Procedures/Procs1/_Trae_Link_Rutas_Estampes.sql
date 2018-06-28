

 CREATE procedure [dbo].[_Trae_Link_Rutas_Estampes] (@codemp int, @pclid numeric(15), @ctcid numeric(15), @rolid int, @tpcid int, @numero int) as
 select '<a href="\Documentos\Estampes\' + CAST(DDE_PCLID AS VARCHAR) + '\'+ CAST(DDE_CTCID AS VARCHAR) + '\'+ CAST(DDE_ROLID AS VARCHAR) + '\' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT + '" target="_blank">' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT + '</a>' ARCHIVO 
 from DEUDORES_ESTAMPES 
 where DDE_CODEMP = @codemp 
 AND DDE_PCLID = @pclid 
 AND DDE_CTCID = @ctcid 
 AND DDE_ROLID = @rolid 
 AND DDE_TPCID IS NULL 
 AND DDE_NUMERO IS NULL 
 
 UNION 

 select '<a href="\Documentos\Estampes\' + CAST(DDE_PCLID AS VARCHAR) + '\'+ CAST(DDE_CTCID AS VARCHAR) + '\'+ CAST(DDE_ROLID AS VARCHAR) + '\' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT + '" target="_blank">' + DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT + '</a>' ARCHIVO 
 from DEUDORES_ESTAMPES 
 WHERE DDE_CODEMP = @codemp 
 AND DDE_PCLID = @pclid 
 AND DDE_CTCID = @ctcid 
 AND DDE_ROLID = @rolid 
 AND DDE_TPCID = @tpcid 
 AND DDE_NUMERO = @numero 
