CREATE procedure [dbo].[SII_Listar_Datos_Contribuyente] (@ctcid int) as
  select CAST(RUT AS VARCHAR) + '-' + DV RUT, 
  NOMBRE_RAZON_SOCIAL, 
  FECHA_CONSULTA, 
  INICIO_ACTIVIDADES, 
  FECHA_INICIO_ACTVIDADES, 
  IMPUESTO_MONEDA_EXTRANJERA, 
  MENOR_PRO_PYME, 
  EMISION, 
  OBSERVACION, 
  substring(REGISTRADO, 1, 1) as REGISTRADO
  from sii..CABECERA with (nolock)
  WHERE CTCID = @ctcid
