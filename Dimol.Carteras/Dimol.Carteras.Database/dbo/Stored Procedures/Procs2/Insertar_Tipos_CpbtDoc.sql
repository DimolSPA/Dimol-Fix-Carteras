

Create Procedure Insertar_Tipos_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer, @tpc_clbid integer, 
													 @tpc_nombre varchar(80), @tpc_talonario char(1), 
                                                     @tpc_ultnum integer, @tpc_lineas integer,
                                                     @tpc_codigo varchar(5), @tpc_tipdig smallint) as
  INSERT INTO tipos_cpbtdoc  
         ( tpc_codemp,   
           tpc_tpcid,   
           tpc_clbid,   
           tpc_nombre,   
           tpc_talonario,   
           tpc_ultnum,   
           tpc_lineas,
           tpc_codigo,
           tpc_tipdig )  
  VALUES ( @tpc_codemp,   
           @tpc_tpcid,   
           @tpc_clbid,   
           @tpc_nombre,   
           @tpc_talonario,   
           @tpc_ultnum,   
           @tpc_lineas,
           @tpc_codigo,
           @tpc_tipdig )
