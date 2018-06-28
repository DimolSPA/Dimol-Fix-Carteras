-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Job_Correo_Roles_Internos] 

AS
BEGIN


DECLARE @bodyMsg nvarchar(max)
DECLARE @subject nvarchar(max)
DECLARE @tableHTML nvarchar(max)
DECLARE @recipients  nvarchar(maX)
DECLARE @CANTIDAD INT
DECLARE @cc_recipients  nvarchar(maX)
DECLARE @intervalo int = -1

if datename(dw,getdate()) = 'Monday' begin set @intervalo = -3 end

SET @subject = 'Causas internas actualizadas'
SET @recipients= 'May.gutierrez@dimol.cl; Gustavo.castillo@dimol.cl; Sandra.valenzuela@dimol.cl; Eugenio.poli@dimol.cl; Francisco.nunez@dimol.cl; gerencia@dimol.cl; control@dimol.cl; nury.rojas@dimol.cl; ingrid.silva@dimol.cl; elizabeth.becerra@dimol.cl'
set @cc_recipients = 'famunoz@gmail.com; miguel.herrera@dimol.cl; jorge.olave@dimol.cl; pedro.salas@dimol.cl'

SET @CANTIDAD = (select COUNT(DISTINCT H.ID_CAUSA)
from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
where h.FECHA_HISTORIAL > (DATEADD(DD, @intervalo, GETDATE())))

SET @tableHTML = 

N'<H3><font color="Red">Causas internas actualizadas: '+ CONVERT(VARCHAR,@CANTIDAD)+'</H3>' +
N'<table id="box-table" style="border-collapse: collapse;" >' +
N'<tr>
<th style="padding: 15px;text-align: center;border: 1px solid black;">CLIENTE</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">RUT DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NOMBRE DEUDOR</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TIPO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">NUMERO</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">A&Ntilde;O</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">TRIBUNAL</th>
<th style="padding: 15px;text-align: center;border: 1px solid black;">GESTIONES</th>
</tr>' +
CAST ( ( 
SELECT 'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.pcl_nomfant,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.ctc_rut,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=v.ctc_nomfant,'', 
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=r.TIPO,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
		td=r.NUMERO,'',
		'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
      td=r.ANIO ,'',
      'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
     td= REPLACE(   K.TRIBUNAL,'º','' ) ,  '',
     'padding: 15px;text-align: left;border: 1px solid black;' AS 'td/@style',
      td= count(h.id_causa) 
from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_historial  h
inner join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL] R WITH (NOLOCK)
on R.ID_CAUSA = h.ID_CAUSA
inner join  [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL K WITH (NOLOCK)
ON r.TRIBUNAL = K.ID_TRIBUNAL
inner join [Iluvatar].[dbo].VIEW_ROL v
on v.rol_numero = CONVERT(varchar, r.NUMERO)+'-'+CONVERT(varchar, r.anio) and v.trb_nombre = k.TRIBUNAL
where h.FECHA_HISTORIAL > (DATEADD(DD, @intervalo, GETDATE()))
group by r.TIPO, 
		r.NUMERO,
		r.ANIO,
		k.TRIBUNAL,
		v.pcl_nomfant,
		v.ctc_rut,
		v.ctc_nomfant
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

