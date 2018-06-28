

Create Procedure Trae_Usuario_Provcli(@codemp integer, @usuario integer) as
  SELECT provcli.pcl_pclid,   
         provcli.pcl_tpcid,   
         provcli.pcl_rut,   
         provcli.pcl_nombre,   
         provcli.pcl_apepat,   
         provcli.pcl_apemat,   
         provcli.pcl_nomfant,   
         provcli.pcl_girid,   
         provcli.pcl_fecing,   
         provcli.pcl_estado,   
         provcli.pcl_fecfin,   
         provcli.pcl_replegal,   
         provcli.pcl_rutlegal,   
         provcli.pcl_tipcli,   
         provcli.pcl_web,   
         provcli.pcl_comentario,   
         provcli.pcl_tipcart,   
         provcli.pcl_transportista  
    FROM provcli  
   WHERE ( provcli.pcl_codemp = @codemp ) AND  
         ( provcli.pcl_usrid = @usuario )
