

Create Procedure Delete_EnteJud_Rol(@ejr_codemp integer, @ejr_etjid integer, @ejr_rolid integer) as
  DELETE FROM entejud_rol  
   WHERE ( entejud_rol.ejr_codemp = @ejr_codemp ) AND  
         ( entejud_rol.ejr_etjid = @ejr_etjid ) AND  
         ( entejud_rol.ejr_rolid = @ejr_rolid )
