

Create Procedure Update_EnteJud_Rol(@ejr_codemp integer, @ejr_etjid integer, @ejr_rolid integer) as  
  UPDATE entejud_rol  
     SET ejr_codemp = @ejr_codemp,   
         ejr_etjid = @ejr_etjid,   
         ejr_rolid = @ejr_rolid  
   WHERE ( entejud_rol.ejr_codemp = @ejr_codemp ) AND  
         ( entejud_rol.ejr_etjid = @ejr_etjid ) AND  
         ( entejud_rol.ejr_rolid = @ejr_rolid )
