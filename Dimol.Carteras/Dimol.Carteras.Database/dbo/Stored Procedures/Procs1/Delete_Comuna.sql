

Create Procedure Delete_Comuna(@com_comid integer) as
  DELETE FROM comuna  
   WHERE comuna.com_comid = @com_comid
