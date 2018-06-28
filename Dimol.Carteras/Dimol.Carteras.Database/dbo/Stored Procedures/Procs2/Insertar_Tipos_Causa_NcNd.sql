

Create Procedure Insertar_Tipos_Causa_NcNd(@tnt_codemp integer, @tnt_tntid integer, @tnt_nombre varchar(80), @tnt_codigo varchar(5)) as
  INSERT INTO tipos_causa_ncnd  
         ( tnt_codemp,   
           tnt_tntid,   
           tnt_nombre,   
           tnt_codigo )  
  VALUES ( @tnt_codemp,   
           @tnt_tntid,   
           @tnt_nombre,   
           @tnt_codigo )
