

Create Procedure Update_Comuna(@com_ciuid integer, @com_comid integer, @com_nombre varchar(200), @com_codpost varchar(20)) as
  UPDATE comuna  
     SET com_ciuid = @com_ciuid,   
         com_nombre = @com_nombre,   
         com_codpost = @com_codpost  
   WHERE comuna.com_comid = @com_comid
