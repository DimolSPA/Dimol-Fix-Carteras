
CREATE Procedure [dbo].[Insertar_EnteJud_Rol](@ejr_codemp integer, @ejr_etjid integer, @ejr_rolid integer) as  

declare
	@existEnte int = 0
	SET @existEnte = (select count(EJR_ETJID) from entejud_rol where EJR_CODEMP = @ejr_codemp and EJR_ETJID = @ejr_etjid and EJR_ROLID = @ejr_rolid)
	if @existEnte = 0
	begin
	INSERT INTO entejud_rol  
         ( ejr_codemp,   
           ejr_etjid,   
           ejr_rolid )  
	  VALUES ( @ejr_codemp,   
			   @ejr_etjid,   
			   @ejr_rolid )
	end
	else
	begin
		update entejud_rol
		set ejr_etjid = @ejr_etjid
		where EJR_CODEMP = @ejr_codemp 
		and EJR_ETJID = @ejr_etjid 
		and EJR_ROLID = @ejr_rolid
	end
  


