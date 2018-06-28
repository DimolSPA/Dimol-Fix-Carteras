

Create Procedure Update_Despachos(@dpc_codemp integer, @dpc_sucid integer, @dpc_dpcid numeric(15), @dpc_transpro char(1), @dpc_tptid integer, @dpc_traid integer,
											     @dpc_pclid numeric(15), @dpc_nombre varchar(200), @dpc_numero varchar(20), @dpc_desctrans varchar(500), @dpc_patente2 varchar(20),
												@dpc_nrosello1 varchar(20), @dpc_nrosello2 varchar(20), @dpc_desde datetime, @dpc_hasta datetime, @dpc_nrocita integer, 
                                                @dpc_pcliddesp numeric(15), @pcd_pcdid integer, @dpc_impid integer) as
   UPDATE despachos  
     SET dpc_transpro = @dpc_transpro,   
         dpc_tptid = @dpc_tptid,   
         dpc_traid = @dpc_traid,   
         dpc_pclid = @dpc_pclid,   
         dpc_nombre = @dpc_nombre,   
         dpc_numero = @dpc_numero,   
         dpc_desctrans = @dpc_desctrans,   
         dpc_patente2 = @dpc_patente2,   
         dpc_nrosello1 = @dpc_nrosello1,   
         dpc_nrosello2 = @dpc_nrosello2,   
         dpc_desde = @dpc_desde,   
         dpc_hasta = @dpc_hasta,   
         dpc_nrocita = @dpc_nrocita,   
         dpc_pcliddesp = @dpc_pcliddesp,   
         pcd_pcdid = @pcd_pcdid,
         dpc_impid = @dpc_impid  
   WHERE ( despachos.dpc_codemp = @dpc_codemp ) AND  
         ( despachos.dpc_sucid = @dpc_sucid ) AND  
         ( despachos.dpc_dpcid = @dpc_dpcid )
