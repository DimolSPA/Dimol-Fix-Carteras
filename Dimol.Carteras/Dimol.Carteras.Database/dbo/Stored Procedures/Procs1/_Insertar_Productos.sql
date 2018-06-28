CREATE Procedure [dbo].[_Insertar_Productos](@pdt_codemp integer, @pdt_codfisico varchar (20), @pdt_nombre varchar (200),
											 @pdt_tipo smallint, @pdt_mostrar char (1), @pdt_pctid integer, @pdt_insid numeric (15),
											@pdt_cc smallint, @pdt_exento char (1), 
											@pdt_impesp char (1),  @pdt_stock char (1), @pdt_perecible char (1), 
											@pdt_gastjud char (1), @pdt_impdeu char (1), @pdt_impcli char (1), 
                                            @pdt_catid integer, @pdt_codbarra varchar(20), @pdt_SpcID integer,
											@pst_unmlgt integer, @pst_alto decimal (15,2),
											@pst_ancho decimal (15,2), @pst_largo decimal (15,2), 
											@pst_cubicaje decimal (15,2), @pst_unmpeso integer,
											@pst_peso decimal (15,2), @pst_unmstock integer, @pst_orden_bodega integer,
											@pst_orden_otro integer, @pst_armado char (1), @pst_tiparm smallint, 
											@pst_closeout char (1),
											@pst_stock_minimo decimal(15,2), @pst_stock_maximo decimal(15,2),
                                            @pst_pack smallint, @pst_packint smallint,
											@pdi_iptid smallint,
											@pdm_codmon integer, @pdm_precio decimal (25,2)) as
											declare @pdt_prodid int

set @pdt_prodid = (select IsNull(Max(pdt_prodid)+1, 1) from productos where pdt_codemp = @pdt_codemp)

  INSERT INTO productos  
         ( pdt_codemp,   
           pdt_prodid,   
           pdt_codfisico,   
           pdt_nombre,   
           pdt_estado,   
           pdt_tipo,   
           pdt_mostrar,   
           pdt_fecing,   
           pdt_fecfin,   
           pdt_pctid,   
           pdt_insid,   
           pdt_costo,   
           pdt_costoprom,   
           pdt_cc,   
           pdt_exento,   
           pdt_impesp,   
           pdt_stock,   
           pdt_perecible,   
           pdt_gastjud,   
           pdt_impdeu,   
           pdt_impcli,
           pdt_catid,
           pdt_codbarra,
           pdt_SpcID )  
  VALUES ( @pdt_codemp,   
           @pdt_prodid,   
           @pdt_codfisico,   
           @pdt_nombre,   
           'U',   
           @pdt_tipo,   
           @pdt_mostrar,   
           getdate(),   
           null,   
           @pdt_pctid,   
           @pdt_insid,   
           0,   
           0,   
           @pdt_cc,   
           @pdt_exento,   
           @pdt_impesp,   
           @pdt_stock,   
           @pdt_perecible,   
           @pdt_gastjud,   
           @pdt_impdeu,   
           @pdt_impcli,
           @pdt_catid,
           @pdt_codbarra,
           @pdt_SpcID )

if(@pdt_tipo = 1)
BEGIN
	INSERT INTO productos_stock  
         ( pst_codemp,   
           pst_prodid,   
           pst_unmlgt,   
           pst_alto,   
           pst_ancho,   
           pst_largo,   
           pst_cubicaje,   
           pst_unmpeso,   
           pst_peso,   
           pst_unmstock,   
           pst_stock_cierre_anio,   
           pst_stock_minimo,   
           pst_stock_merma,   
           pst_stock_transito,   
           pst_stock_reservado,   
           pst_stock_total,   
           pst_orden_bodega,   
           pst_orden_otro,   
           pst_armado,   
           pst_tiparm,   
           pst_closeout,
           pst_stock_maximo,
           pst_pack,
           pst_packint )  
  VALUES ( @pdt_codemp,   
           @pdt_prodid,   
           @pst_unmlgt,   
           @pst_alto,   
           @pst_ancho,   
           @pst_largo,   
           @pst_cubicaje,   
           @pst_unmpeso,   
           @pst_peso,   
           @pst_unmstock,   
           0,   
           @pst_stock_minimo,   
           0,   
           0,   
           0,   
           0,   
           @pst_orden_bodega,   
           @pst_orden_otro,   
           @pst_armado,   
           @pst_tiparm,   
           @pst_closeout,
           @pst_stock_maximo,
           @pst_pack,
           @pst_packint )
END

if(@pdi_iptid != null)
BEGIN
INSERT INTO productos_impuestos  
         ( pdi_codemp,   
           pdi_prodid,   
           pdi_iptid )  
  VALUES ( @pdt_codemp,   
           @pdt_prodid,   
           @pdi_iptid )
END

if(@pdm_codmon != null)
BEGIN
INSERT INTO productos_moneda  
         ( pdm_codemp,   
           pdm_prodid,   
           pdm_codmon,   
           pdm_precio )  
  VALUES ( @pdt_codemp,   
           @pdt_prodid,   
           @pdm_codmon,   
           @pdm_precio )
END
