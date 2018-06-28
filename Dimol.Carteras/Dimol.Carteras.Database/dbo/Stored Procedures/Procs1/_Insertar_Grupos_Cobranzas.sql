Create Procedure [dbo].[_Insertar_Grupos_Cobranzas]
(
			@codemp integer, 
			@sucid integer, 
			@nombre varchar (200), 
			@emplid integer) 
as  

declare @id int        
set @id = (select IsNull(Max(GRC_GRCID )+1, 1) from grupos_cobranza     
			where GRC_CODEMP = @codemp 
			and grc_sucid =@sucid)        

   INSERT INTO grupos_cobranza    
         ( grc_codemp,     
           grc_sucid,     
           grc_grcid,     
           grc_nombre,     
           grc_emplid )    
  VALUES ( @codemp,     
           @sucid,     
           @id,     
           @nombre,     
           @emplid )
