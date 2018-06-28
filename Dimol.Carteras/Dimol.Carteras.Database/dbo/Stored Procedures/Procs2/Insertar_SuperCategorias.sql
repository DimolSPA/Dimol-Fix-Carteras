

Create Procedure Insertar_SuperCategorias(@spc_codemp integer, @spc_spcid integer, @spc_nombre varchar(50), @spc_orden smallint, @spc_utilizacion smallint) as
  INSERT INTO supercategorias  
         ( spc_codemp,   
           spc_spcid,   
           spc_nombre,   
           spc_orden,   
           spc_utilizacion )  
  VALUES ( @spc_codemp,   
           @spc_spcid,   
           @spc_nombre,   
           @spc_orden,   
           @spc_utilizacion )
