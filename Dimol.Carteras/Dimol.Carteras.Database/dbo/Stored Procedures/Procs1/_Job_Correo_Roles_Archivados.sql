-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Job_Correo_Roles_Archivados] 

AS
BEGIN

INSERT INTO [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO

SELECT R.ID_CAUSA
		,CAST(GETDATE() AS DATE)
      ,R.[ESTADO_ADM] 
  FROM [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
  where R.estado_adm = 'Archivada'
  AND R.ID_CAUSA IN (SELECT [RPJ_ID_CAUSA] FROM [PoderJudicial].[dbo].[ROL_PODER_JUDICIAL])

DECLARE @bodyMsg nvarchar(max)
DECLARE @subject nvarchar(max)
DECLARE @tableHTML nvarchar(max)
DECLARE @recipients  nvarchar(maX)
DECLARE @cc_recipients  nvarchar(maX)
DECLARE @intervalo int = -1

if datename(dw,getdate()) = 'Monday' begin set @intervalo = -3 end

SET @subject = 'Causas con estado administrativo ARCHIVADA'
SET @recipients= 'May.gutierrez@dimol.cl; Gustavo.castillo@dimol.cl; Sandra.valenzuela@dimol.cl; Eugenio.poli@dimol.cl; Francisco.nunez@dimol.cl; gerencia@dimol.cl; control@dimol.cl; nury.rojas@dimol.cl; ingrid.silva@dimol.cl; elizabeth.becerra@dimol.cl'
set @cc_recipients = 'famunoz@gmail.com; miguel.herrera@dimol.cl; jorge.olave@dimol.cl; pedro.salas@dimol.cl'
SET @tableHTML = 

N'<H3><font color="Red">Nuevas causas archivadas.</H3>' +
N'<table id="box-table" style="border-collapse: collapse;" >' +
N'<tr>
<th style="padding: 15px;text-align: center;border: 1px solid black;">CLIENTE</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">RUT DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NOMBRE DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TIPO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NUMERO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">A&Ntilde;O</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TRIBUNAL</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">ESTADO</th>
</tr>' +
CAST ( ( 
SELECT 'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
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
      td= R.[ESTADO_ADM] 
  FROM [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO  R WITH (NOLOCK)
  inner join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL T WITH (NOLOCK)
  ON R.ID_CAUSA = T.ID_CAUSA
  inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
  ON T.TRIBUNAL = K.ID_TRIBUNAL
  inner join [Iluvatar].[dbo].VIEW_ROL v
  on v.rol_numero = CONVERT(varchar, t.NUMERO)+'-'+CONVERT(varchar, t.anio) and v.trb_nombre = k.TRIBUNAL
  where R.FECHA_ENVIO = CAST(GETDATE() AS DATE)
  AND R.ID_CAUSA NOT IN (SELECT ID_CAUSA FROM [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL_ARCHIVADO TT WITH (NOLOCK)  
                         WHERE TT.FECHA_ENVIO = (DATEADD(DD, @intervalo, CAST(GETDATE() AS DATE))))

  order by ANIO, k.TRIBUNAL, NUMERO
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

