

Create Procedure Delete_Gestor_Restriccion_Nula(@grn_codemp integer, @grn_usrid integer, @grn_sucid integer, @grn_gesid integer) as
   DELETE FROM gestor_restriccion_nula  
   WHERE ( gestor_restriccion_nula.grn_codemp = @grn_codemp ) AND  
         ( gestor_restriccion_nula.grn_usrid = @grn_usrid ) AND  
         ( gestor_restriccion_nula.grn_sucid = @grn_sucid ) AND  
         ( gestor_restriccion_nula.grn_gesid = @grn_gesid )
