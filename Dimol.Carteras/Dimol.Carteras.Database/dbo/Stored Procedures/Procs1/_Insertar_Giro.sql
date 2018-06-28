CREATE Procedure [dbo].[_Insertar_Giro](@gir_codemp integer, @idioma integer, @gir_nombre varchar(80)) as
declare @girid int

set @girid = (select IsNull(Max(gir_girid)+1, 1) from GIROS where gir_codemp = @gir_codemp)

  INSERT INTO GIROS  
         ( gir_codemp,   
           gir_girid,   
           gir_nombre)  
  VALUES ( @gir_codemp,
		   @girid,
           @gir_nombre)
           
  INSERT INTO GIROS_IDIOMAS  
         ( gii_codemp,   
           gii_girid,   
           gii_idid,   
           gii_nombre )  
  VALUES ( @gir_codemp,
           @girid,
		   @idioma,
           @gir_nombre )
