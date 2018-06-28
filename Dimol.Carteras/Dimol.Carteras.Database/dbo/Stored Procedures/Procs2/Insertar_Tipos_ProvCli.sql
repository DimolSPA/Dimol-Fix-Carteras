

Create Procedure Insertar_Tipos_ProvCli(@tpc_codemp integer, @tpc_tpcid integer, @tpc_nombre varchar (40), @tpc_agrupa char (1)) as
  INSERT INTO tipos_provcli  
         ( tpc_codemp,   
           tpc_tpcid,   
           tpc_nombre,   
           tpc_agrupa )  
  VALUES ( @tpc_codemp,   
           @tpc_tpcid,   
           @tpc_nombre,   
           @tpc_agrupa )
