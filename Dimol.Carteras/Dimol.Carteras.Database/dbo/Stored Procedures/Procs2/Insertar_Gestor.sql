﻿

Create Procedure Insertar_Gestor(@ges_codemp integer, @ges_sucid integer, @ges_gesid integer,@ges_nombre varchar (500),
										@ges_telefono integer, @ges_email varchar (100), @ges_tipcart smallint, @ges_comki decimal (8,4),
										@ges_comhon decimal (8,4), @ges_emplid integer, @ges_remoto char(1),
                                        @ges_comjki decimal(8,4), @ges_comjhon decimal(8,4)) as
  INSERT INTO gestor  
         ( ges_codemp,   
           ges_sucid,   
           ges_gesid,   
           ges_nombre,   
           ges_telefono,   
           ges_email,   
           ges_tipcart,   
           ges_comki,   
           ges_comhon,   
           ges_emplid,
           ges_remoto,
           ges_estado,
           ges_comjki,
           ges_comjhon  )  
  VALUES ( @ges_codemp,   
           @ges_sucid,   
           @ges_gesid,   
           @ges_nombre,   
           @ges_telefono,   
           @ges_email,   
           @ges_tipcart,   
           @ges_comki,   
           @ges_comhon,   
           @ges_emplid,
           @ges_remoto,
           'A',
           @ges_comjki,
           @ges_comjhon  )