CREATE Procedure [dbo].[_Insertar_Tipos_Traslado](@codemp integer, @nombre varchar(80), @codigo varchar(5), @idioma integer) as
declare @ttlid int

set @ttlid = (select IsNull(Max(ttl_ttlid)+1, 1) from TIPOS_TRASLADO where ttl_codemp = @codemp)

  INSERT INTO TIPOS_TRASLADO  
         ( ttl_codemp,   
           ttl_ttlid,   
           ttl_nombre,   
           ttl_codigo )  
  VALUES ( @codemp,
		   @ttlid,
           @nombre,   
           @codigo )
           
  INSERT INTO TIPOS_TRASLADO_IDIOMAS  
         ( tli_codemp,   
           tli_ttlid,   
           tli_idid,   
           tli_nombre )  
  VALUES ( @codemp,
		   @ttlid,
           @idioma,   
           @nombre )
