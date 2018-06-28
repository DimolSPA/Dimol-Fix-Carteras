

Create Procedure Update_ProvCli(@pcl_codemp integer, @pcl_pclid numeric (15), @pcl_tpcid integer, @pcl_rut varchar (20),
                                                       @pcl_nombre varchar (200), @pcl_apepat varchar (80), @pcl_apemat varchar (80), @pcl_nomfant varchar (800),
                                                       @pcl_girid integer,  @pcl_replegal varchar (400), @pcl_rutlegal varchar (20), @pcl_tipcli char (1), @pcl_web char (1),
                                                       @pcl_comentario text, @pcl_tipcart integer, @pcl_transportista char (1), 
                                                       @pcl_usrid integer, @pcl_estado char(1), @pcl_naviera char(1), @pcl_codigo varchar(10)) as
  UPDATE provcli  
     SET pcl_tpcid = @pcl_tpcid,   
         pcl_rut = @pcl_rut,   
         pcl_nombre = @pcl_nombre,   
         pcl_apepat = @pcl_apepat,   
         pcl_apemat = @pcl_apemat,   
         pcl_nomfant = @pcl_nomfant,   
         pcl_girid = @pcl_girid,   
         pcl_replegal = @pcl_replegal,   
         pcl_rutlegal = @pcl_rutlegal,   
         pcl_tipcli = @pcl_tipcli,   
         pcl_web = @pcl_web,   
         pcl_comentario = @pcl_comentario,   
         pcl_tipcart = @pcl_tipcart,   
         pcl_transportista = @pcl_transportista,   
         pcl_usrid = @pcl_usrid,
         pcl_estado  = @pcl_estado,
         pcl_naviera = @pcl_naviera,
         pcl_codigo = @pcl_codigo
   WHERE ( provcli.pcl_codemp = @pcl_codemp ) AND  
         ( provcli.pcl_pclid = @pcl_pclid )
