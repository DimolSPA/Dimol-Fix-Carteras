

Create Procedure Update_Maxima_Convencional(@mxc_codemp integer, @mxc_mxcid integer, @mxc_tpcid integer, @mxc_tipo char (1), @mxc_aplica char (1),
												   			@mxc_codmon integer, @mxc_desde decimal (10,2), @mxc_hasta decimal (10,2), @mxc_valor decimal (10,2)) as  
  UPDATE maxima_convencional  
     SET mxc_tpcid = @mxc_tpcid,   
         mxc_tipo = @mxc_tipo,   
         mxc_aplica = @mxc_aplica,   
         mxc_codmon = @mxc_codmon,   
         mxc_desde = @mxc_desde,   
         mxc_hasta = @mxc_hasta,   
         mxc_valor = @mxc_valor  
   WHERE ( maxima_convencional.mxc_codemp = @mxc_codemp ) AND  
         ( maxima_convencional.mxc_mxcid = @mxc_mxcid )
