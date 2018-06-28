

Create Procedure Update_Bodegas(@bod_codemp integer, @bod_bodid integer, @bod_nombre varchar (200),
											@bod_avanzado char (1), @bod_cubicaje decimal (15,2), @bod_excluyente char (1),
											@bod_largo decimal(15,2), @bod_ancho decimal(15,2), @bod_alto decimal(15,2), @bod_unmid integer) as  
   UPDATE bodegas  
     SET   bod_nombre = @bod_nombre,   
         bod_avanzado = @bod_avanzado,   
         bod_cubicaje = @bod_cubicaje,   
         bod_excluyente = @bod_excluyente,
         bod_largo = @bod_largo,  
         bod_ancho = @bod_ancho,
         bod_alto = @bod_alto,
         bod_unmid = @bod_unmid
   WHERE ( bodegas.bod_codemp = @bod_codemp ) AND  
         ( bodegas.bod_bodid = @bod_bodid )
