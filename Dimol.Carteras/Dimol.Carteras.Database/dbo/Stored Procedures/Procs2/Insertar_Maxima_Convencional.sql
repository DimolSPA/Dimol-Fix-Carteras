

Create Procedure Insertar_Maxima_Convencional(@mxc_codemp integer, @mxc_mxcid integer, @mxc_tpcid integer, @mxc_tipo char (1), @mxc_aplica char (1),
												   			@mxc_codmon integer, @mxc_desde decimal (10,2), @mxc_hasta decimal (10,2), @mxc_valor decimal (10,2)) as
  INSERT INTO maxima_convencional  
         ( mxc_codemp,   
           mxc_mxcid,   
           mxc_tpcid,   
           mxc_tipo,   
           mxc_aplica,   
           mxc_codmon,   
           mxc_desde,   
           mxc_hasta,   
           mxc_valor )  
  VALUES ( @mxc_codemp,   
           @mxc_mxcid,   
           @mxc_tpcid,   
           @mxc_tipo,   
           @mxc_aplica,   
           @mxc_codmon,   
           @mxc_desde,   
           @mxc_hasta,   
           @mxc_valor )
