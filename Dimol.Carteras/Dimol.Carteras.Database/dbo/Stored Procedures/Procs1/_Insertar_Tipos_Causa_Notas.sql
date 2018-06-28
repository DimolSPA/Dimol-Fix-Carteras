CREATE Procedure [dbo].[_Insertar_Tipos_Causa_Notas](@codemp integer, @nombre varchar(80), @codigo varchar(5), @idioma integer) as
declare @tntid int

set @tntid = (select IsNull(Max(tnt_tntid)+1, 1) from TIPOS_CAUSA_NCND where tnt_codemp = @codemp)

  INSERT INTO TIPOS_CAUSA_NCND  
         ( tnt_codemp,   
           tnt_tntid,   
           tnt_nombre,   
           tnt_codigo )  
  VALUES ( @codemp,
		   @tntid,
           @nombre,   
           @codigo )
           
  INSERT INTO TIPOS_CAUSA_NCND_IDIOMAS  
         ( tni_codemp,   
           tni_tntid,   
           tni_idid,   
           tni_nombre )  
  VALUES ( @codemp,
		   @tntid,
           @idioma,   
           @nombre )
