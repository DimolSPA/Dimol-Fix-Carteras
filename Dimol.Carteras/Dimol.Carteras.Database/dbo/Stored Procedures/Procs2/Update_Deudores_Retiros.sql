

create procedure Update_Deudores_Retiros (@ddr_codemp integer, @ddr_ctcid integer, @ddr_ddrid integer, 
                                          @ddr_nombre varchar(250), @ddr_contacto varchar(350),   										
                                          @ddr_comid integer, @ddr_direccion varchar(400), @ddr_hordes datetime,
                                          @ddr_horhas datetime, @ddr_bcoid integer,  @ddr_sucursal varchar(300), 
                                          @ddr_comentario text, @ddr_horcall01 datetime, @ddr_horcall02 datetime) as      
UPDATE deudores_retiros 
       SET ddr_nombre = @ddr_nombre,              
       ddr_contacto = @ddr_contacto,
       ddr_comid = @ddr_comid,              
       ddr_direccion = @ddr_direccion,
       ddr_hordes = @ddr_hordes,              
       ddr_horhas = @ddr_horhas,
       ddr_bcoid = @ddr_bcoid,              
       ddr_sucursal = @ddr_sucursal,
       ddr_comentario = @ddr_comentario,
       ddr_horcall01 = @ddr_horcall01,
       ddr_horcall02 = @ddr_horcall02       
 WHERE ( deudores_retiros.ddr_codemp= @ddr_codemp ) AND
       ( deudores_retiros.ddr_ctcid = @ddr_ctcid) AND             
       ( deudores_retiros.ddr_ddrid = @ddr_ddrid )
