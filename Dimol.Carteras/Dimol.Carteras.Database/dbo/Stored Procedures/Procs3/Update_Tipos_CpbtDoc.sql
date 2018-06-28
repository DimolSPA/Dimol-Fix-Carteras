

Create Procedure Update_Tipos_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer, @tpc_clbid integer, 
													 @tpc_nombre varchar(80), @tpc_talonario char(1), 
                                                     @tpc_ultnum integer, @tpc_lineas integer,
                                                     @tpc_codigo varchar(5), @tpc_tipdig smallint) as
   UPDATE tipos_cpbtdoc  
     SET tpc_clbid = @tpc_clbid,   
         tpc_nombre = @tpc_nombre,   
         tpc_talonario = @tpc_talonario,   
         tpc_ultnum = @tpc_ultnum,   
         tpc_lineas = @tpc_lineas,
         tpc_codigo = @tpc_codigo,
         tpc_tipdig = @tpc_tipdig 
   WHERE ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid = @tpc_tpcid )
