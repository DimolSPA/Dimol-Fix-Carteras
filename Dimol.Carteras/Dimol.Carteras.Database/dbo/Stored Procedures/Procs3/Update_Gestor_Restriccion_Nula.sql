

Create Procedure Update_Gestor_Restriccion_Nula(@grn_codemp integer, @grn_usrid integer, @grn_sucid integer, @grn_gesid integer, @grn_desde datetime, @grn_hasta datetime) as
   UPDATE gestor_restriccion_nula  
     SET grn_desde = @grn_desde,   
         grn_hasta = @grn_hasta  
   WHERE ( gestor_restriccion_nula.grn_codemp = @grn_codemp ) AND  
         ( gestor_restriccion_nula.grn_usrid = @grn_usrid ) AND  
         ( gestor_restriccion_nula.grn_sucid = @grn_sucid ) AND  
         ( gestor_restriccion_nula.grn_gesid = @grn_gesid )
