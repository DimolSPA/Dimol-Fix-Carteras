Create Procedure [dbo].[_Insertar_Materia_Estados](
		@mej_codemp integer, 
		@mej_esjid integer, 
		@mej_estid smallint) 
as    

IF NOT EXISTS (SELECT 1 FROM materia_estados
			WHERE mej_codemp = @mej_codemp     
            AND mej_esjid = @mej_esjid     
            AND mej_estid = @mej_estid) 
BEGIN
	 INSERT INTO materia_estados    
         ( mej_codemp,     
           mej_esjid,     
           mej_estid )    
     VALUES ( @mej_codemp,     
           @mej_esjid,     
           @mej_estid )  

END
