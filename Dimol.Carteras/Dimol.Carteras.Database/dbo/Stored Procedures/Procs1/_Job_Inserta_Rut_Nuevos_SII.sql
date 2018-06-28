-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE _Job_Inserta_Rut_Nuevos_SII

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	insert into sii..RUT_CAPTCHA
select ctc_numero, CTC_DIGITO, 'A',GETDATE(), 'L','A' from Iluvatar..DEUDORES  with (nolock) where CTC_CTCID in 
(select distinct CCB_CTCID from Iluvatar..CARTERA_CLIENTES_CPBT_DOC   with (nolock)where CCB_ESTCPBT in ('V','J'))
and CTC_NUMERO not in (select rut from sii..RUT_CAPTCHA with (nolock))

END
