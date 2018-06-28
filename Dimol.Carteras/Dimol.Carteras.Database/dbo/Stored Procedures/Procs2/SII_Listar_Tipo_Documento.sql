CREATE procedure [dbo].[SII_Listar_Tipo_Documento] (@ctcid int) as
  select td.DOCUMENTO, t.ANIO  
  from sii..TIMBRAJE t with(nolock), 
  sii..TIPO_DOCUMENTO td with(nolock), 
  sii..cabecera c with(nolock)  
  where t.TIPO_DOCUMENTO = td.TIPO_DOCUMENTO 
  and c.ctcid = t.ctcid 
  and c.FECHA = t.FECHA
  and t.ctcid = @ctcid
