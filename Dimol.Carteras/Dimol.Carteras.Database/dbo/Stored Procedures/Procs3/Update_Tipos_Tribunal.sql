

Create Procedure Update_Tipos_Tribunal(@ttb_codemp integer, @ttb_ttbid integer, @ttb_nombre varchar (50)) as  
  UPDATE tipos_tribunal  
     SET ttb_codemp = @ttb_codemp,   
         ttb_ttbid = @ttb_ttbid,   
         ttb_nombre = @ttb_nombre  
   WHERE ( tipos_tribunal.ttb_codemp = @ttb_codemp ) AND  
         ( tipos_tribunal.ttb_ttbid = @ttb_ttbid )
