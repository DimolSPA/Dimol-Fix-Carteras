

Create Procedure Insert_Empleado_Asistencia_Dia_Horas(@codemp integer, @codsuc integer, @empleado integer, @dia datetime, @item integer, @tipo integer) as
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
  VALUES ( @codemp,   
           @codsuc,   
           @empleado,   
           @dia,   
           @item,   
           @tipo,   
           getdate(),   
           getdate(),   
           'N',   
           'N' )
