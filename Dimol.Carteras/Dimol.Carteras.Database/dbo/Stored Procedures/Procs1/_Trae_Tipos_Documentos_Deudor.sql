CREATE Procedure [dbo].[_Trae_Tipos_Documentos_Deudor](@codemp integer, @idid integer) as
 SELECT tipos_documentos_deudores_idiomas.tdi_tddid,   
tipos_documentos_deudores_idiomas.tdi_nombre
FROM tipos_documentos_deudores_idiomas
WHERE  tipos_documentos_deudores_idiomas.tdi_codemp = @codemp
and tipos_documentos_deudores_idiomas.tdi_idid = @idid
order by tdi_nombre
