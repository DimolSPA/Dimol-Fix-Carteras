CREATE Procedure [dbo].[_Insertar_SuperCategoria](@spc_codemp integer, @idioma integer, @spc_nombre varchar(80), @spc_orden integer, @spc_utilizacion varchar(8)) as
declare @spcid int

set @spcid = (select IsNull(Max(spc_spcid)+1, 1) from SUPERCATEGORIAS where spc_codemp = @spc_codemp)

  INSERT INTO SUPERCATEGORIAS  
         ( spc_codemp,   
           spc_spcid,   
           spc_nombre,   
           spc_orden,
		   spc_utilizacion )  
  VALUES ( @spc_codemp,
		   @spcid,
           @spc_nombre,   
           @spc_orden,
		   @spc_utilizacion )
           
  INSERT INTO SUPERCATEGORIAS_IDIOMA  
         ( sci_codemp,   
           sci_spcid,   
           sci_idiid,   
           sci_nombre )  
  VALUES ( @spc_codemp,
		   @spcid,
           @idioma,   
           @spc_nombre )
