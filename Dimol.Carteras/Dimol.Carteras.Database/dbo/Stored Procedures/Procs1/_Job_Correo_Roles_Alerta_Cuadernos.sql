-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Job_Correo_Roles_Alerta_Cuadernos] 

AS
BEGIN

DECLARE @bodyMsg nvarchar(max)
DECLARE @subject nvarchar(max)
DECLARE @tableHTML nvarchar(max)
DECLARE @recipients  nvarchar(maX)
DECLARE @cc_recipients  nvarchar(maX)
DECLARE @intervalo int = 0

if datename(dw,getdate()) = 'Monday' begin set @intervalo = -2 end

  declare @fecha date
  
  	set @fecha = ( select top 1 fecha 
  from [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_CUADERNO_ALERTA]
  where fecha < CONVERT(date, DATEADD(DD, @intervalo, CAST(GETDATE() AS DATE)))
  order by fecha desc)


SET @subject = 'Causas con ALERTA DE CUADERNO'
SET @recipients= 'famunoz@gmail.com; miguel.herrera@dimol.cl; jorge.olave@dimol.cl; pedro.salas@dimol.cl; May.gutierrez@dimol.cl'--; Gustavo.castillo@dimol.cl; Sandra.valenzuela@dimol.cl; Eugenio.poli@dimol.cl; Francisco.nunez@dimol.cl; gerencia@dimol.cl; control@dimol.cl; nury.rojas@dimol.cl; ingrid.silva@dimol.cl'
set @cc_recipients = ''--'famunoz@gmail.com; miguel.herrera@dimol.cl; jorge.olave@dimol.cl; pedro.salas@dimol.cl'
SET @tableHTML = 

N'<H3><font color="Red">Nuevos cuadernos por causas.</H3>' +
N'<table id="box-table" style="border-collapse: collapse;" >' +
N'<tr>
<th style="padding: 15px;text-align: center;border: 1px solid black;">CLIENTE</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">RUT DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NOMBRE DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TIPO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NUMERO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">A&Ntilde;O</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TRIBUNAL</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">CUADERNO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">FECHA CUADERNO</th>
</tr>' +
CAST ( ( 
  select 
  'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.pcl_nomfant,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.ctc_rut,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.ctc_nomfant,'', 
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=t.TIPO,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=t.NUMERO,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
      td=t.ANIO ,'',
      'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
     td= REPLACE(   K.TRIBUNAL,'º','' ) ,  '',
     'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
      td= c.DESC_CUADERNO  ,'',
      'padding: 15px;text-align: center;border: 1px solid black;' AS 'td/@style',
      td= isnull(Iluvatar.dbo.[_Trae_Fecha_Cuaderno_PJ](a1.id_causa,a1.id_cuaderno),CONVERT(varchar, a1.fecha, 103) )
  from  [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_CUADERNO_ALERTA] a1
  left join 
  [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_CUADERNO_ALERTA] a2
  on  a1.id_causa = a2.id_causa
  and a1.id_cuaderno = a2.id_cuaderno
  and a2.fecha = @fecha
  inner join [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_CUADERNO c with(nolock) 
  on a1.id_causa = c.id_causa and a1.id_cuaderno = c.id_cuaderno
  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
ON a1.ID_CAUSA = T.ID_CAUSA
inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
ON T.TRIBUNAL = K.ID_TRIBUNAL
inner join [10.0.1.11].[PoderJudicial].[dbo].[ROL_PODER_JUDICIAL] r WITH (NOLOCK)
on RPJ_ID_CAUSA = a1.ID_CAUSA
inner join [Iluvatar].[dbo].VIEW_ROL v WITH (NOLOCK)
on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL
  
  where a1.tipo_cuaderno in (4,7,9,10,12,13,14,17,21,22,23,24,25,34,51,65,93,84,85,86,87)
  and a2.tipo_cuaderno is null
  and a1.fecha =  CONVERT(date, GETDATE())
  
--  select 
--  'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--		td=v.pcl_nomfant,'',
--		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--		td=v.ctc_rut,'',
--		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--		td=v.ctc_nomfant,'', 
--		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--		td=t.TIPO,'',
--		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--		td=t.NUMERO,'',
--		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--      td=t.ANIO ,'',
--      'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--     td= REPLACE(   K.TRIBUNAL,'º','' ) ,  '',
--     'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
--      td= c.DESC_CUADERNO  ,'',
--      'padding: 15px;text-align: center;border: 1px solid black;' AS 'td/@style',
--      td= CONVERT(varchar, Iluvatar.dbo._Trae_Fecha_Cuaderno_PJ (a1.id_causa, a1.id_cuaderno), 103) 
--  from  [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_CUADERNO_ALERTA] a1
--  left join 
--  [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_CUADERNO_ALERTA] a2
--  on  a1.id_causa = a2.id_causa
--  and a1.id_cuaderno = a2.id_cuaderno
--  and a2.fecha = @fecha
--  inner join [10.0.1.11].[poderjudicial].dbo.PODER_JUDICIAL_CUADERNO c with(nolock) 
--  on a1.id_causa = c.id_causa and a1.id_cuaderno = c.id_cuaderno
--  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
--ON a1.ID_CAUSA = T.ID_CAUSA
--inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
--ON T.TRIBUNAL = K.ID_TRIBUNAL
--inner join [10.0.1.11].[PoderJudicial].[dbo].[ROL_PODER_JUDICIAL] r WITH (NOLOCK)
--on RPJ_ID_CAUSA = a1.ID_CAUSA
--inner join [Iluvatar].[dbo].VIEW_ROL v WITH (NOLOCK)
--on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL
--inner join  Iluvatar..ROL_ACTUALIZA_PODERJUDICIAL a
--on a.CODEMP = RPJ_CODEMP and a.ROLID = RPJ_ROLID
--  where a1.tipo_cuaderno in (4,7,9,10,12,13,14,17,21,22,23,24,25,34,51,65,93,84,85,86,87)
--  and a2.tipo_cuaderno is null
--  and a1.fecha =   CONVERT(date, GETDATE())
--  and a.FLAG_PODERJUDICIAL = 'S'
--  order by Iluvatar.dbo._Trae_Fecha_Cuaderno_PJ (a1.id_causa, a1.id_cuaderno) desc
  
  
FOR XML PATH('tr'), TYPE 
) AS NVARCHAR(MAX) ) +
N'</table>'

--SELECT @tableHTML 
set @tableHTML = @tableHTML + '<p>Este mensaje ha sido generado automaticamente. Favor no contestar.</p>'

IF @tableHTML IS NULL
BEGIN
	set @tableHTML = '<p><B>NO HAY CASOS NUEVOS REGISTRADOS EN LAS ULTIMAS 24 HORAS.<B><BR/><BR/><BR/><BR/></p><p>Este mensaje ha sido generado automaticamente. Favor no contestar.</p>'
END

EXEC msdb.dbo.sp_send_dbmail 
@profile_name='Dimol.Status', 
@recipients= @recipients,
@blind_copy_recipients = @cc_recipients,
@subject = @subject,
@body = @tableHTML,
@body_format = 'HTML' ;


END

