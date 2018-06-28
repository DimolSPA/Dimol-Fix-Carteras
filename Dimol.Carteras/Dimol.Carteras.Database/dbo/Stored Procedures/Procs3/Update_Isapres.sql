

Create Procedure Update_Isapres(@isp_codemp integer, @isp_ispid integer, @isp_rut varchar (20),
                                                        @isp_nombre varchar (120), @isp_pctid integer) as  
  UPDATE isapres  
     SET isp_rut = @isp_rut,   
         isp_nombre = @isp_nombre,   
         isp_pctid = @isp_pctid  
   WHERE ( isapres.isp_codemp = @isp_codemp ) AND  
         ( isapres.isp_ispid = @isp_ispid )
