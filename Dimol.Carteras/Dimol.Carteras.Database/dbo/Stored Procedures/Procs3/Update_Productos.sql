

Create Procedure Update_Productos(@pdt_codemp integer, @pdt_prodid numeric (15), @pdt_codfisico varchar (20), @pdt_nombre varchar (200),
											 @pdt_tipo smallint, @pdt_mostrar char (1),
											@pdt_pctid integer, @pdt_insid integer, 
											@pdt_cc smallint,  @pdt_exento char (1), 
											@pdt_impesp char (1), @pdt_stock char (1), @pdt_perecible char (1), 
											@pdt_gastjud char (1), @pdt_impdeu char (1), 
                                            @pdt_impcli char (1), @pdt_catid integer, @pdt_codbarra varchar(20), @pdt_SpcID integer) as
  UPDATE productos  
     SET pdt_codemp = @pdt_codemp,   
         pdt_prodid = @pdt_prodid,   
         pdt_codfisico = @pdt_codfisico,   
         pdt_nombre = @pdt_nombre,   
         pdt_tipo = @pdt_tipo,   
         pdt_mostrar = @pdt_mostrar,   
         pdt_pctid = @pdt_pctid,   
         pdt_insid = @pdt_insid,   
         pdt_cc = @pdt_cc,   
         pdt_exento = @pdt_exento,   
         pdt_impesp = @pdt_impesp,   
         pdt_stock = @pdt_stock,   
         pdt_perecible = @pdt_perecible,   
         pdt_gastjud = @pdt_gastjud,   
         pdt_impdeu = @pdt_impdeu,   
         pdt_impcli = @pdt_impcli,
         pdt_catid = @pdt_catid,
         pdt_codbarra = @pdt_codbarra,
         pdt_SpcID = @pdt_SpcID  
   WHERE ( productos.pdt_codemp = @pdt_codemp ) AND  
         ( productos.pdt_prodid = @pdt_prodid )
