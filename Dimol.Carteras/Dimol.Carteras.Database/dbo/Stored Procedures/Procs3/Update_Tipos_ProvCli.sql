

Create Procedure Update_Tipos_ProvCli(@tpc_codemp integer, @tpc_tpcid integer, @tpc_nombre varchar (40), @tpc_agrupa char (1)) as  
  UPDATE tipos_provcli  
     SET tpc_nombre = @tpc_nombre,   
         tpc_agrupa = @tpc_agrupa  
   WHERE ( tipos_provcli.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_provcli.tpc_tpcid = @tpc_tpcid )
