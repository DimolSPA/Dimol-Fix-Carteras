CREATE PROCEDURE [dbo].[_Listar_PanelQuiebra_Grilla]
(
@codemp int,
@idioma int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select PQ.QUIEBRA_ID QuiebraId,
	PQ.ROLID,
	PQ.SBCID,
	PQ.ROLNUMERO,
	pcli.pcl_nomfant Cliente,
	(select ctc_nomfant from deudores where ctc_ctcid =  r.rol_ctcid) Deudor,
	(select SBC_NOMBRE from SUBCARTERAS with(nolock) where SBC_CODEMP = CODEMP and SBC_SBCID = SBCID) Asegurado,
	PQ.CUANTIA,
	PQ.FEC_REGISTRO FechaIngresoQuiebra,
	(select mji_nombre from materia_judicial_idiomas with (nolock)
	 where mji_codemp =  '+ CONVERT(VARCHAR,@codemp) + ' and mji_idid = '+ CONVERT(VARCHAR,@idioma) + ' and mji_esjid = r.rol_esjid) Materia,
	PQD.SOLICITANTE,
	PQD.MTO_COSTAS_PERSONALES MtoCostasPersonales,
    PQD.FEC_COSTAS_PERSONALES FecCostasPersonales,
    PQD.FEC_INGRESO_SOLICITUD FecIngresoSolicitud,
    PQD.FEC_NOTIFICACION_SOLICITUD FecNotificacionSolicitud,
    PQD.FEC_AUDIENCIA_INICIAL FecAudienciaInicial,
    PQD.FEC_AUDIENCIA_PRUEBA FecAudienciaPrueba,
    PQD.FEC_AUDIENCIA_FALLO FecAudienciaFallo,
    PQD.FEC_RESOLUCION_LIQUIDACION FecResolucionLiquidacion,
    PQD.FEC_RESOLUCION_LIQUIDACION_BC FecResolucionLiquidacionBC,
    PQD.FEC_RESOLUCION_REORGANIZACION_BC FecResolucionReorganizacionBC,
    PQD.FEC_VERIFICACION FecVerificacion,
    PQD.FEC_ACREDITACION_PODER FecAcreditacionPoder,
    PQD.FEC_JUNTA_CONSTITUTIVA FecJuntaConstitutiva,
    PQD.FEC_JUNTA_DELIBERATIVA FecJuntaDeliberativa,
    case PQD.STATUS_ACUERDO
	when ''A'' then ''APROBADO''
	when ''R'' then ''RECHAZADO''
	else PQD.STATUS_ACUERDO
	end StatusAcuerdo,
    PQD.FEC_ACUERDO FecAcuerdo,
    PQD.FEC_VERIFICADO_ACREDITADO FecVerificadoAcreditado,
    PQD.FEC_NOMINA_CREDITO_VERIFICADO FecNomCreditoVerificado,
    PQD.FEC_IMPUGNACION FecImpugnacion,
    PQD.FEC_NOMINA_CREDITO_RECONOCIDO FecNomCreditoReconocido,
    PQD.FEC_SOLICITUD_ANTECEDENTE FecSolicitudAntecedente,
    PQD.FEC_RECEPCION_ANTECEDENTE FecRecepcionAntecedente,
    PQD.FEC_ENVIO_ANTECEDENTE FecEnvioAntecedente,
    PQD.FEC_EMISION_ND FecEmisionND,
    PQD.MTO_EMISION_ND MtoEmision,
    PQD.FEC_REPARTOS FecRepartos,
    PQD.MTO_REPARTOS MtoRepartos,
    PQD.FEC_DEVOLUCION FecDevolucion,
    PQD.PGO_COSTAS_PERSONALES PgoCostasPersonales,
    PQD.FEC_PGO_COSTAS_PERSONALES FecPgoCostasPersonales,
    PQD.FEC_APROBACION_CTAFINAL FecAprobacionCtaFinal,
    PQD.FEC_CERTIFICADO_INCOBRABLE FecCertificadoIncobrable
from PANEL_QUIEBRA PQ with (nolock)
LEFT JOIN PANEL_QUIEBRA_DETALLE PQD with (nolock)
ON PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
join rol r with (nolock)
on PQ.ROLID = r.ROL_ROLID
and PQ.CODEMP = r.ROL_CODEMP
join PROVCLI pcli with (nolock)
on r.ROL_PCLID = pcli.PCL_PCLID
and r.ROL_CODEMP = pcli.PCL_CODEMP
where PQ.CODEMP = '+ CONVERT(VARCHAR,@codemp) + ''
set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
