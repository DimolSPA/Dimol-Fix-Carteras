
CREATE PROCEDURE [dbo].[_Trae_Deudor_Quiebra](
@codemp int,
@rut varchar(20))
	
AS
BEGIN
	
	select ROLNUMERO, TRIBUNALID, t.TRB_NOMBRE Tribunal, TIPOCAUSAID, MATERIAJODICIALID 
	from DEUDOR_QUIEBRA  dq
	join TRIBUNALES t with(nolock)
	on dq.CODEMP = t.TRB_CODEMP
	and dq.TRIBUNALID = t.TRB_TRBID
	where RUT = @rut
	
	
	
END
