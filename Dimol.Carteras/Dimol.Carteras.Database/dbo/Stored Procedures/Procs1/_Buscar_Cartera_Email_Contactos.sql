-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Cartera_Email_Contactos]
(
@codemp int,
@ctcid int,
@todo int,
@contacto int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000)='';
declare @query2 varchar(7000)='';

set @query = 'SELECT ddm_mail,   
 ddm_tipo,   
view_datos_geograficos.ciu_codarea, ctc_nombre, ''N'' as Contacto, ctc_rut, ctc_nomfant
FROM deudores,   
deudores_mail,   
view_datos_geograficos
WHERE  deudores_mail.ddm_codemp = deudores.ctc_codemp  and  
deudores_mail.ddm_ctcid = deudores.ctc_ctcid  and  
deudores.ctc_comid = view_datos_geograficos.com_comid
and  deudores_mail.ddm_codemp =  '+ CONVERT(VARCHAR,@codemp) +'
and deudores_mail.ddm_ctcid = '+ CONVERT(VARCHAR,@ctcid)
If @todo = 0 begin
set @query = @query + ' and ddm_masivo =''S'''
end

If @contacto > 0 begin
    set @query = @query + ' UNION ' 
	set @query = @query + 'SELECT deudores_contactos_mail.dcm_mail,   
    deudores_contactos_mail.dcm_tipo,   
    view_datos_geograficos.ciu_codarea, ddc_nombre, ''S'' as Contacto, ctc_rut, ctc_nomfant
    FROM {oj deudores_contactos LEFT OUTER JOIN view_datos_geograficos ON deudores_contactos.ddc_comid = view_datos_geograficos.com_comid},   
    deudores_contactos_mail, deudores
    WHERE  deudores_contactos_mail.dcm_codemp = deudores_contactos.ddc_codemp  
    and deudores_contactos_mail.dcm_ctcid = deudores_contactos.ddc_ctcid  
    and deudores_contactos_mail.dcm_ddcid = deudores_contactos.ddc_ddcid

    and deudores_contactos_mail.dcm_codemp = deudores.ctc_codemp  
    and deudores_contactos_mail.dcm_ctcid = deudores.ctc_ctcid  

    and  deudores_contactos_mail.dcm_codemp =  '+ CONVERT(VARCHAR,@codemp) +'
    and deudores_contactos_mail.dcm_ctcid =   '+ CONVERT(VARCHAR,@ctcid)

	If @todo = 0 begin
	set @query = @query + ' and dcm_masivo =''S'''
	end
end


--select @query
--select @query2
exec(@query)	
	

END
