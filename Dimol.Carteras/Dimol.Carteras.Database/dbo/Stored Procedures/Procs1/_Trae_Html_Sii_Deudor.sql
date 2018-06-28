create procedure [dbo].[_Trae_Html_Sii_Deudor](@ctcid int) as 
	 select top 1 r.HTML 
	 from sii..RUT_CAPTCHA r, sii..CABECERA c
	 where r.ID_RUT = c.ID_RUT 
	 and c.CTCID = @ctcid
