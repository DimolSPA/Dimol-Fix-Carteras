-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Job_Correo_Roles_Externos] 

AS
BEGIN


DECLARE @bodyMsg nvarchar(max)
DECLARE @subject nvarchar(max)
DECLARE @tableHTML nvarchar(max)
DECLARE @recipients  nvarchar(maX)
DECLARE @CANTIDAD INT
DECLARE @intervalo int = -1

if datename(dw,getdate()) = 'Monday' begin set @intervalo = -3 end

SET @subject = 'Causas externas actualizadas'
SET @recipients='famunoz@gmail.com;miguel.herrera@dimol.cl; jorge.olave@dimol.cl; pedro.salas@dimol.cl'

SET @CANTIDAD = (select  COUNT(distinct ID_CAUSA) 
				from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_litigante
				where FECHA > (DATEADD(DD, @intervalo, GETDATE())))

SET @tableHTML = 

N'<H3><font color="Red">Causas externas actualizadas: '+ CONVERT(VARCHAR,@CANTIDAD)+'</H3>' 
--SELECT @tableHTML 
set @tableHTML = @tableHTML + '<p>Este mensaje ha sido generado automaticamente. Favor no contestar.</p>'

EXEC msdb.dbo.sp_send_dbmail 
@profile_name='Dimol.Status', 
@recipients= @recipients,
@subject = @subject,
@body = @tableHTML,
@body_format = 'HTML' ;


END

