-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Salida_Telefonos] 
(
	@codemp as integer,
	@pclid as int,
	@fecha_inicio as datetime,
	@fecha_termino as datetime
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
		select convert(varchar,d.CTC_NUMERO)+';'+
				isnull(dt.NUMERO,'')+';'+
				isnull(dt.CODIGO_AREA,case LEN(dt.NUMERO) 
			when  9 then
			case SUBSTRING(dt.NUMERO,1,1) 
				when 2 then 2
				when 9 then 9
				else SUBSTRING(dt.NUMERO,1,2)
			end
			when 8 then
			case SUBSTRING(dt.NUMERO,1,1) 
				when 2 then 2
				else 9
			end
			else 0
		end )+';'+
		case dt.tipo
			when 'C' then 'PARTICULAR'
			when 'M' then 'CELULAR'
			when 'O' then 'OTRO'
			when 'F' then 'COMERCIAL'
			when null then ''
		end+';'+
		isnull(dt.ANEXO,'')
		from DEUDORES_TELEFONOS_SITREL DT, DEUDORES d
		where d.ctc_codemp = @codemp
		and dt.ORIGEN = 'G'
		and fecha > @fecha_inicio and fecha < @fecha_termino
		and dt.CTCID = d.CTC_CTCID
		and dt.CODEMP = d.CTC_CODEMP
		and (select COUNT(CCB_CCBID) from CARTERA_CLIENTES_CPBT_DOC where CCB_CODEMP = @codemp and CCB_PCLID = @pclid and CCB_CTCID = d.CTC_CTCID) > 0
	
          


END
