

 Create Procedure Insertar_Empleado_Asistencia_Horas(@eah_codemp integer, @eah_sucid integer, @eah_emplid integer, @eah_dia datetime, @eah_item smallint,
                                                                                            @eah_tipoid integer, @eah_entrada datetime, @eah_salida datetime, @eah_authora char (1), @eah_pagsueldo char (1)) as
  INSERT INTO empleado_asistencia_horas  
         ( eah_codemp,   
           eah_sucid,   
           eah_emplid,   
           eah_dia,   
           eah_item,   
           eah_tipoid,   
           eah_entrada,   
           eah_salida,   
           eah_authora,   
           eah_pagsueldo )  
  VALUES ( @eah_codemp,   
           @eah_sucid,   
           @eah_emplid,   
           @eah_dia,   
           @eah_item,   
           @eah_tipoid,   
           @eah_entrada,   
           @eah_salida,   
           @eah_authora,   
           @eah_pagsueldo )
