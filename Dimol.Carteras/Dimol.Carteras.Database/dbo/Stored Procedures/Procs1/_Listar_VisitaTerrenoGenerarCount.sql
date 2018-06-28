CREATE PROCEDURE [dbo].[_Listar_VisitaTerrenoGenerarCount]
(
@gesid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS

BEGIN

SET NOCOUNT ON;
declare @query varchar(7000);

set @query = 'select count(SOLICITUD_ID) count from
  (select *,ROW_NUMBER() OVER (ORDER BY SOLICITUD_ID asc) as row from    
  ('
set @query = @query + '
	SELECT VST.SOLICITUD_ID SOLICITUD_ID, DD.CTC_RUT RUT, DD.CTC_NOMFANT DEUDOR, VST.DIRECCION DIRECCION, 
		VST.COMUNA COMUNA, CIU.CIU_NOMBRE CIUDAD, VST.LATITUD LATITUD, VST.LONGITUD LONGITUD, VST.DEUDA DEUDA,
		VSTG.GESTOR, VSTG.GESID, VSTG.TELEFONO_IMEI 
	FROM VISITA_TERRENO_SOLICITUD_GESTOR VSTG
	JOIN VISITA_TERRENO_SOLICITUD VST 
	ON VSTG.SOLICITUD_ID = VST.SOLICITUD_ID 
	LEFT JOIN DEUDORES DD
	ON VST.CTCID = DD.CTC_CTCID
	JOIN COMUNA COM
	ON VST.IDCOMUNA = COM.COM_COMID
	JOIN CIUDAD CIU
	ON COM.COM_CIUID = CIU.CIU_CIUID
	WHERE VSTG.GESID = ' + CONVERT(VARCHAR, @gesid) +
	'AND VST.ID_ESTATUS = 2'
set @query = @query +') as tabla  ) as t
  where  row >= 1' 

if @where is not null
begin
set @query = @query + @where;
end

 exec(@query)	

END
