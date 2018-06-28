

CREATE Procedure [dbo].[Insertar_Cartera_Clientes_Estados_Historial_Especial](@ceh_codemp integer, @ceh_pclid numeric (15), @ceh_ctcid numeric (15), @ceh_ccbid integer, 
                                                                                                    @ceh_fecha datetime, @ceh_estid smallint, @ceh_sucid integer, @ceh_gesid integer, 
                                                                                                    @ceh_ipred varchar (30), @ceh_ipmaquina varchar (30), @ceh_comentario text, @ceh_monto decimal (15,2),
                                                                                                    @ceh_saldo decimal (15,2), @ceh_usrid integer) as  
--declare  @fecha datetime
 
 
--BEGIN TRY  
--     set @fecha = CONVERT(datetime, @ceh_fecha, 103) 
--     if DATEPART(MONTH, GETDATE()) = DATEPART(MONTH,@fecha )
--     begin
--		 set @fecha = CONVERT(datetime, @ceh_fecha, 103) 
--     end 
--     else 
--     begin 
--		 set @fecha = CONVERT(datetime, @ceh_fecha, 101) 
--     end
       
--END TRY  
--BEGIN CATCH  
--     set @fecha = CONVERT(datetime, @ceh_fecha, 101) 
--END CATCH 

  INSERT INTO cartera_clientes_estados_historial  
         ( ceh_codemp,   
           ceh_pclid,   
           ceh_ctcid,   
           ceh_ccbid,   
           ceh_fecha,   
           ceh_estid,   
           ceh_sucid,   
           ceh_gesid,   
           ceh_ipred,   
           ceh_ipmaquina,   
           ceh_comentario,   
           ceh_monto,   
           ceh_saldo,   
           ceh_usrid )  
  VALUES ( @ceh_codemp,   
           @ceh_pclid,   
           @ceh_ctcid,   
           @ceh_ccbid,   
           @ceh_fecha,   
           @ceh_estid,   
           @ceh_sucid,   
           @ceh_gesid,   
           @ceh_ipred,   
           @ceh_ipmaquina,   
           @ceh_comentario,   
           @ceh_monto,   
           @ceh_saldo,   
           @ceh_usrid )
--end try
--begin catch
--set @fecha = CONVERT(datetime, @ceh_fecha, 100) 
--	  INSERT INTO cartera_clientes_estados_historial  
--         ( ceh_codemp,   
--           ceh_pclid,   
--           ceh_ctcid,   
--           ceh_ccbid,   
--           ceh_fecha,   
--           ceh_estid,   
--           ceh_sucid,   
--           ceh_gesid,   
--           ceh_ipred,   
--           ceh_ipmaquina,   
--           ceh_comentario,   
--           ceh_monto,   
--           ceh_saldo,   
--           ceh_usrid )  
--  VALUES ( @ceh_codemp,   
--           @ceh_pclid,   
--           @ceh_ctcid,   
--           @ceh_ccbid,   
--           @ceh_fecha,   
--           @ceh_estid,   
--           @ceh_sucid,   
--           @ceh_gesid,   
--           @ceh_ipred,   
--           @ceh_ipmaquina,   
--           @ceh_comentario,   
--           @ceh_monto,   
--           @ceh_saldo,   
--           @ceh_usrid )
--end catch