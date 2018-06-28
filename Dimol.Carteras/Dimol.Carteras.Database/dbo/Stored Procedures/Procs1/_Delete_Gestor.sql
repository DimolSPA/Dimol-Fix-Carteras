CREATE Procedure [dbo].[_Delete_Gestor](
@ges_codemp integer, 
@ges_sucid integer, 
@ges_gesid integer) 
as  

  
 DELETE FROM grupo_cobranza_gestor    
   WHERE ( grupo_cobranza_gestor.gcg_codemp = @ges_codemp ) AND    
         ( grupo_cobranza_gestor.gcg_sucid = @ges_sucid ) AND    
         ( grupo_cobranza_gestor.gcg_gesid = @ges_gesid )  
  
  DELETE FROM gestor    
   WHERE ( gestor.ges_codemp = @ges_codemp ) AND    
         ( gestor.ges_sucid = @ges_sucid ) AND    
         ( gestor.ges_gesid = @ges_gesid )
