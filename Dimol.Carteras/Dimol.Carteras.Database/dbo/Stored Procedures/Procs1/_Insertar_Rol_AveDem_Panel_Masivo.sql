CREATE PROCEDURE [dbo].[_Insertar_Rol_AveDem_Panel_Masivo] (@codemp integer, @rolid integer, @panelid integer) as

    INSERT INTO rol_avedem  
         ( rad_codemp,   
           rad_rolid,   
           rad_fecdem,   
           rad_cuodem,   
           rad_mondem,   
           rad_monucoudem,   
           rad_fecpcoudem,   
           rad_fecucoudem,   
           rad_intdem,   
           rad_fecave,   
           rad_cuoave,   
           rad_monave,   
           rad_monucouave,   
           rad_fecpcouave,   
           rad_fecucouave,   
           rad_intave,
           rad_monpcuodem,
		   rad_monpcouave)  
	select @codemp, @rolid, FECDEM, CUODEM, MONDEM, 
	MONUCOUDEM, FECPCOUDEM, FECUCOUDEM, INTDEM, FECAVE, 
	CUOAVE, MONAVE, MONUCOUAVE, FECPCOUAVE, FECUCOUAVE, 
	INTAVE, MONPCUODEM, MONPCOUAVE
	from PANEL_DEMANDA_MASIVA 
	where ID_PANEL_MASIVO = @panelid
