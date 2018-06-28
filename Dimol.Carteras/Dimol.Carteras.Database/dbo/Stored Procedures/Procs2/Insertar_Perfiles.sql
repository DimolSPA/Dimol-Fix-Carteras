

Create Procedure Insertar_Perfiles(@prf_codemp integer, @prf_prfid integer, @prf_nombre varchar (200), @prf_administrador char (1)) as  
INSERT INTO perfiles  
         ( prf_codemp,   
           prf_prfid,   
           prf_nombre,   
           prf_administrador )  
  VALUES ( @prf_codemp,   
           @prf_prfid,   
           @prf_nombre,   
           @prf_administrador )
