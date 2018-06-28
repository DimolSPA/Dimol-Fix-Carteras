

Create Procedure Update_Rol_AveDem(@rad_codemp integer, @rad_rolid integer, @rad_fecdem datetime, @rad_cuodem smallint, 
												   @rad_mondem numeric(15,2), @rad_monucoudem numeric(15,2), @rad_fecpcoudem datetime,
												   @rad_fecucoudem datetime, @rad_intdem numeric(5,3), 
												   @rad_fecave datetime, @rad_cuoave smallint, 
												   @rad_monave numeric(15,2), @rad_monucouave numeric(15,2), @rad_fecpcouave datetime,
												   @rad_fecucouave datetime, @rad_intave numeric(5,3), @rad_monpcuodem numeric(15,2), @rad_monpcouave numeric(15,2)) as
  
  UPDATE rol_avedem  
     SET rad_fecdem = @rad_fecdem,   
         rad_cuodem = @rad_cuodem,   
         rad_mondem = @rad_mondem,   
         rad_monucoudem = @rad_monucoudem,   
         rad_fecpcoudem = @rad_fecpcoudem,   
         rad_fecucoudem = @rad_fecucoudem,   
         rad_intdem = @rad_intdem,   
         rad_fecave = @rad_fecave,   
         rad_cuoave = @rad_cuoave,   
         rad_monave = @rad_monave,   
         rad_monucouave = @rad_monucouave,   
         rad_fecpcouave = @rad_fecpcouave,   
         rad_fecucouave = @rad_fecucouave,   
         rad_intave = @rad_intave,
         rad_monpcuodem  = @rad_monpcuodem, 
         rad_monpcouave = @rad_monpcouave
   WHERE ( rol_avedem.rad_codemp = @rad_codemp ) AND  
         ( rol_avedem.rad_rolid = @rad_rolid )
