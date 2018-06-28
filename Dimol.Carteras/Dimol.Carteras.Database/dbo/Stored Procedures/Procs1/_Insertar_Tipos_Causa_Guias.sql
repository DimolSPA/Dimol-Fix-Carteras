CREATE Procedure [dbo].[_Insertar_Tipos_Causa_Guias](@codemp integer, @nombre varchar(80), @codigo varchar(5), @idioma integer) as
declare @tgdid int

set @tgdid = (select IsNull(Max(tgd_tgdid)+1, 1) from tipos_causa_guias where tgd_codemp = @codemp)

  INSERT INTO tipos_causa_guias  
         ( tgd_codemp,   
           tgd_tgdid,   
           tgd_nombre,   
           tgd_codigo )  
  VALUES ( @codemp,
		   @tgdid,
           @nombre,   
           @codigo )
           
  INSERT INTO tipos_causa_guias_idiomas  
         ( tgi_codemp,   
           tgi_tgdid,   
           tgi_idid,   
           tgi_nombre )  
  VALUES ( @codemp,
		   @tgdid,
           @idioma,   
           @nombre )
