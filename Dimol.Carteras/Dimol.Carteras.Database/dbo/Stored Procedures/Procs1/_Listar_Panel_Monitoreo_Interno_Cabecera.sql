CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Interno_Cabecera]
AS
BEGIN
	SET NOCOUNT ON;
	select  
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 1) CantCausasDiaAnterior,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 2) CantDeudoresDiaAnterior,
		(select Valor from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 3) SaldoDiaAnterior,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 4) CantDeudoresJudicializado,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 5) CantCausasJudicializadas,
		(select Valor from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 6) SaldoJudicializado,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 7) CantDeudoresCausasActivas,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 8) CantCausasActivas,
		(select Valor from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 9) SaldoCausaActiva,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 10) CantDeudoresCausasArchivadas,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 11) CantCausasArchivadas,
		(select Valor from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 12) SaldoCausaArchivada,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 13) CantDeudoresCausasArchivadas7dias,
		(select CAST(Valor as int) from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 14) CantCausaArchivada7Dias,
		(select Valor from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA where id = 15) SaldoCausaArchivada7Dias,
		(select top 1 FEC_REGISTRO from PODER_JUDICIAL_MONITOREO_INTERNO_CABECERA order by FEC_REGISTRO desc) FechaActualizacion

END
