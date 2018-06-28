

Create Procedure Delete_Gestor(@ges_codemp integer, @ges_sucid integer, @ges_gesid integer) as
  DELETE FROM gestor  
   WHERE ( gestor.ges_codemp = @ges_codemp ) AND  
         ( gestor.ges_sucid = @ges_sucid ) AND  
         ( gestor.ges_gesid = @ges_gesid )
