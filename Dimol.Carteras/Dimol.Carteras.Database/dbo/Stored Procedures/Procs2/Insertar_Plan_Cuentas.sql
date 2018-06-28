

Create Procedure Insertar_Plan_Cuentas(@pct_codemp integer, @pct_pctid integer,@pct_codigo varchar(20),  @pct_nombre varchar(1000), @pct_ctpid integer, @pct_ccs char(1), @pct_activos char(1), @pct_estado char(1) ) as
  INSERT INTO plan_cuentas  
         ( pct_codemp,   
           pct_pctid,   
           pct_codigo,   
           pct_nombre,   
           pct_ctpid,   
           pct_ccs,   
           pct_activos,
           pct_estado )  
  VALUES ( @pct_codemp,   
           @pct_pctid,   
           @pct_codigo,   
           @pct_nombre,   
           @pct_ctpid,   
           @pct_ccs,   
           @pct_activos,
           @pct_estado )
