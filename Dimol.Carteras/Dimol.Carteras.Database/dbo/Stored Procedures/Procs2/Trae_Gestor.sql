

Create Procedure Trae_Gestor(@ges_codemp integer, @ges_sucid integer, @ges_emplid integer) as
  SELECT gestor.ges_gesid  
    FROM gestor  
   WHERE ( gestor.ges_codemp = @ges_codemp ) AND  
         ( gestor.ges_sucid = @ges_sucid ) AND  
         ( gestor.ges_emplid = @ges_emplid )
