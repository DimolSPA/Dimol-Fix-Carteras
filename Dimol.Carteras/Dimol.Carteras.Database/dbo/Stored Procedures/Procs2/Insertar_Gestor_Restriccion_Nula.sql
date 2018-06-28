

Create Procedure Insertar_Gestor_Restriccion_Nula(@grn_codemp integer, @grn_usrid integer, @grn_sucid integer, @grn_gesid integer, @grn_desde datetime, @grn_hasta datetime) as
  INSERT INTO gestor_restriccion_nula  
         ( grn_codemp,   
           grn_usrid,   
           grn_sucid,   
           grn_gesid,   
           grn_desde,   
           grn_hasta )  
  VALUES ( @grn_codemp,   
           @grn_usrid,   
           @grn_sucid,   
           @grn_gesid,   
           @grn_desde,   
           @grn_hasta )
