

Create Procedure Update_Tipos_Causa_NcNd(@tnt_codemp integer, @tnt_tntid integer, @tnt_nombre varchar(80), @tnt_codigo varchar(5)) as
  UPDATE tipos_causa_ncnd  
     SET tnt_nombre = @tnt_nombre,   
         tnt_codigo = @tnt_codigo 
   WHERE ( tipos_causa_ncnd.tnt_codemp = @tnt_codemp ) AND  
         ( tipos_causa_ncnd.tnt_tntid = @tnt_tntid )
