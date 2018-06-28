

Create Procedure Update_Cartera_Clientes_Campana(@ccc_codemp integer, @ccc_sucid integer, @ccc_cccid integer, @ccc_nombre varchar (150),
						@ccc_prioridad smallint, @ccc_fecini datetime, @ccc_fecfin datetime,
						@ccc_descripcion text, @ccc_supgest char(1), @ccc_predictivo char(1)) as
  UPDATE cartera_clientes_campana  
     SET ccc_nombre = @ccc_nombre,   
         ccc_fecini = @ccc_fecini,   
         ccc_fecfin = @ccc_fecfin,   
         ccc_descripcion = @ccc_descripcion,
         ccc_supgest = @ccc_supgest,
         ccc_predictivo = @ccc_predictivo
   WHERE ( cartera_clientes_campana.ccc_codemp = @ccc_codemp ) AND  
         ( cartera_clientes_campana.ccc_sucid = @ccc_sucid ) AND  
         ( cartera_clientes_campana.ccc_cccid = @ccc_cccid )
