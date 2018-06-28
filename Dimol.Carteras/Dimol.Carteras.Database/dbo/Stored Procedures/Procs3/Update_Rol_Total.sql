

Create Procedure Update_Rol_Total(@rol_codemp integer, @rol_rolid integer, @rol_total decimal(15,2)) as
  UPDATE rol  
     SET rol_total = @rol_total  
   WHERE ( rol.rol_codemp = @rol_codemp ) AND  
         ( rol.rol_rolid = @rol_rolid )
