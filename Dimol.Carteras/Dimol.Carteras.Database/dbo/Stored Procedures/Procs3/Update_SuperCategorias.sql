

Create Procedure Update_SuperCategorias(@spc_codemp integer, @spc_spcid integer, @spc_nombre varchar(50), @spc_orden smallint, @spc_utilizacion smallint) as
  UPDATE supercategorias  
     SET spc_nombre = @spc_nombre,   
         spc_orden = @spc_orden,   
         spc_utilizacion = @spc_utilizacion  
   WHERE ( supercategorias.spc_codemp = @spc_codemp ) AND  
         ( supercategorias.spc_spcid = @spc_spcid )
