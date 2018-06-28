

Create Procedure Update_Insumos(@ins_codemp integer, @ins_insid numeric(15), @ins_codigo varchar(30), @ins_nombre varchar(300),
											 @ins_arancel char(1),  @ins_porcaran decimal(8,2), @ins_exento char(1), @ins_tipo smallint, @ins_terminado char(1), @ins_tiping char(1), 
											 @ins_tipid integer, @ins_catid integer, @ins_cubicaje decimal(15,2), @ins_unmlgt integer,
											 @ins_alto decimal(15,2), @ins_ancho decimal(15,2), @ins_largo decimal(15,2),
											 @ins_perecible char(1), @ins_fecvenc datetime, @ins_gastjud char(1), @ins_impdeu char(1), 
                                             @ins_impcli char(1), @ins_pctid integer, @ins_pack smallint, 
                                             @ins_packint smallint, @ins_SpcID integer) as

  UPDATE insumos  
     SET ins_codigo = @ins_codigo,   
         ins_nombre = @ins_nombre,   
         ins_arancel = @ins_arancel,   
         ins_porcaran = @ins_porcaran,   
         ins_exento = @ins_exento,   
         ins_tipo = @ins_tipo,   
         ins_terminado = @ins_terminado,   
         ins_tiping = @ins_tiping,   
         ins_tipid = @ins_tipid,   
         ins_catid = @ins_catid,   
         ins_cubicaje = @ins_cubicaje,   
         ins_unmlgt = @ins_unmlgt,   
         ins_alto = @ins_alto,   
         ins_ancho = @ins_ancho,   
         ins_largo = @ins_largo,   
         ins_perecible = @ins_perecible,   
         ins_fecvenc = @ins_fecvenc,   
         ins_gastjud = @ins_gastjud,   
         ins_impdeu = @ins_impdeu,   
         ins_impcli = @ins_impcli,
         ins_pctid = @ins_pctid,
         ins_pack = @ins_pack,
         ins_packint = @ins_packint,
         ins_SpcID = @ins_SpcID  
   WHERE ( insumos.ins_codemp = @ins_codemp ) AND  
         ( insumos.ins_insid = @ins_insid )
