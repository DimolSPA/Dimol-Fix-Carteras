﻿

Create Procedure Insertar_Rol_AveDem(@rad_codemp integer, @rad_rolid integer, @rad_fecdem datetime, @rad_cuodem smallint, 
												   @rad_mondem numeric(15,2), @rad_monucoudem numeric(15,2), @rad_fecpcoudem datetime,
												   @rad_fecucoudem datetime, @rad_intdem numeric(5,3), 
												   @rad_fecave datetime, @rad_cuoave smallint, 
												   @rad_monave numeric(15,2), @rad_monucouave numeric(15,2), @rad_fecpcouave datetime,
												   @rad_fecucouave datetime, @rad_intave numeric(5,3), @rad_monpcuodem numeric(15,2), @rad_monpcouave numeric(15,2)) as
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
		rad_monpcouave )  
  VALUES ( @rad_codemp,   
           @rad_rolid,   
           @rad_fecdem,   
           @rad_cuodem,   
           @rad_mondem,   
           @rad_monucoudem,   
           @rad_fecpcoudem,   
           @rad_fecucoudem,   
           @rad_intdem,   
           @rad_fecave,   
           @rad_cuoave,   
           @rad_monave,   
           @rad_monucouave,   
           @rad_fecpcouave,   
           @rad_fecucouave,   
           @rad_intave,
           @rad_monpcuodem,
           @rad_monpcouave )
