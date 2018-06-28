

Create Procedure Update_Perfiles(@prf_codemp integer, @prf_prfid integer, @prf_nombre varchar (200), @prf_administrador char (1)) as  
  UPDATE perfiles  
     SET prf_nombre = @prf_nombre,   
         prf_administrador = @prf_administrador  
   WHERE ( perfiles.prf_codemp = @prf_codemp ) AND  
         ( perfiles.prf_prfid = @prf_prfid )
