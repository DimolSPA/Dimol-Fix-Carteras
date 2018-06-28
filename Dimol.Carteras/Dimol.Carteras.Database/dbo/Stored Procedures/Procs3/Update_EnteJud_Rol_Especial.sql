

Create Procedure Update_EnteJud_Rol_Especial(@ejr_codemp integer, @ejr_etjid integer, @ejr_etjidN integer) as  
  UPDATE entejud_rol  
     SET ejr_codemp = @ejr_codemp,   
         ejr_etjid = @ejr_etjidN
   WHERE ( entejud_rol.ejr_codemp = @ejr_codemp ) AND  
         ( entejud_rol.ejr_etjid = @ejr_etjid )
