

 Create Procedure Insertar_Impuestos(@ipt_codemp integer, @ipt_iptid smallint, @ipt_nomcort varchar (10), @ipt_nombre varchar (80), 
                                                                              @ipt_pctid integer, @ipt_retenido char (1), @ipt_monto decimal (5,2)) as
  INSERT INTO impuestos  
         ( ipt_codemp,   
           ipt_iptid,   
           ipt_nomcort,   
           ipt_nombre,   
           ipt_pctid,   
           ipt_retenido,   
           ipt_monto )  
  VALUES ( @ipt_codemp,   
           @ipt_iptid,   
           @ipt_nomcort,   
           @ipt_nombre,   
           @ipt_pctid,   
           @ipt_retenido,   
           @ipt_monto )
