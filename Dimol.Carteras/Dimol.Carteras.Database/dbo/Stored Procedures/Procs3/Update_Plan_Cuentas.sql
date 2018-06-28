

Create Procedure Update_Plan_Cuentas(@pct_codemp integer, @pct_pctid integer, @pct_codigo varchar (20), @pct_nombre varchar (1000),
                                                                  @pct_ctpid integer, @pct_ccs char (1), @pct_activos char (1), @pct_estado char(1)) as  
  UPDATE plan_cuentas  
     SET pct_codigo = @pct_codigo,   
         pct_nombre = @pct_nombre,   
         pct_ctpid = @pct_ctpid,   
         pct_ccs = @pct_ccs,   
         pct_activos = @pct_activos,
         pct_estado = @pct_estado  
   WHERE ( plan_cuentas.pct_codemp = @pct_codemp ) AND  
         ( plan_cuentas.pct_pctid = @pct_pctid )
