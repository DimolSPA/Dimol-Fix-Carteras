

Create Procedure Insertar_Cartera_Clientes_Campana(@ccc_codemp integer, @ccc_sucid integer, @ccc_cccid integer, @ccc_nombre varchar (150), @ccc_prioridad smallint,
																	@ccc_fecini datetime, @ccc_fecfin datetime,  @ccc_descripcion text,
   																	@ccc_usrid integer, @ccc_gesid integer, @ccc_supgest char(1), @ccc_predictivo char(1)) as
  INSERT INTO cartera_clientes_campana  
         ( ccc_codemp,   
           ccc_sucid,   
           ccc_cccid,   
           ccc_nombre,   
           ccc_prioridad,   
           ccc_fecini,   
           ccc_fecfin,   
           ccc_fecfinreal,   
           ccc_estado,   
           ccc_descripcion,   
           ccc_usrid,   
           ccc_gesid,
           ccc_supgest,
           ccc_predictivo )  
  VALUES ( @ccc_codemp,   
           @ccc_sucid,   
           @ccc_cccid,   
           @ccc_nombre,   
           @ccc_prioridad,   
           @ccc_fecini,   
           @ccc_fecfin,   
           null,   
           'I',   
           @ccc_descripcion,   
           @ccc_usrid,   
           @ccc_gesid,
           @ccc_supgest,
           @ccc_predictivo )
