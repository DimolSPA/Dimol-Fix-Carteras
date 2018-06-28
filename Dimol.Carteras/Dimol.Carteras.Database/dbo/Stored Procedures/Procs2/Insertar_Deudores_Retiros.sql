﻿

create procedure Insertar_Deudores_Retiros(@ddr_codemp integer, @ddr_ctcid integer, @ddr_ddrid integer, 
                                           @ddr_nombre varchar(250), @ddr_contacto varchar(350),   										
                                           @ddr_comid integer, @ddr_direccion varchar(400), @ddr_hordes datetime,
                                           @ddr_horhas datetime, @ddr_bcoid integer, @ddr_sucursal varchar(300), 
                                           @ddr_comentario text, @ddr_horcall01 datetime, @ddr_horcall02 datetime) as
INSERT INTO deudores_retiros
            ( ddr_codemp,                
              ddr_ctcid,
              ddr_ddrid,
              ddr_nombre,
              ddr_contacto,             
              ddr_comid,
              ddr_direccion,
              ddr_hordes,
              ddr_horhas,
              ddr_bcoid,
              ddr_sucursal,
              ddr_comentario,
              ddr_horcall01,
              ddr_horcall02 )      
VALUES ( @ddr_codemp,          
         @ddr_ctcid,
         @ddr_ddrid,
         @ddr_nombre,
         @ddr_contacto,
         @ddr_comid,            
         @ddr_direccion,
         @ddr_hordes,
         @ddr_horhas,
         @ddr_bcoid,
         @ddr_sucursal,            
         @ddr_comentario,
         @ddr_horcall01,
         @ddr_horcall02 )
