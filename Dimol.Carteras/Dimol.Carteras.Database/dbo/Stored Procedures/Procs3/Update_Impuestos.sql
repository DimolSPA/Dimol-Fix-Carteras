

Create Procedure Update_Impuestos(@ipt_codemp integer, @ipt_iptid smallint, @ipt_nomcort varchar (10), @ipt_nombre varchar (80),
                                                            @ipt_pctid integer, @ipt_retenido char (1), @ipt_monto decimal (5,2)) as  
  UPDATE impuestos  
     SET ipt_nomcort = @ipt_nomcort,   
         ipt_nombre = @ipt_nombre,   
         ipt_pctid = @ipt_pctid,   
         ipt_retenido = @ipt_retenido,   
         ipt_monto = @ipt_monto  
   WHERE ( impuestos.ipt_codemp = @ipt_codemp ) AND  
         ( impuestos.ipt_iptid = @ipt_iptid )
