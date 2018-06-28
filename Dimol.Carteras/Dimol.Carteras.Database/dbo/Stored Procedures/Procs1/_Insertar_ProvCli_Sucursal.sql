﻿create Procedure [dbo].[_Insertar_ProvCli_Sucursal](@pcs_codemp integer, @pcs_pclid numeric (15), @pcs_nombre varchar (150),
                                                                        @pcs_comid integer, @pcs_direccion varchar (500), @pcs_telefono varchar (100), @pcs_fax varchar (100), 
                                                                        @pcs_mail varchar (100), @pcs_casamatriz char (1), @pcs_bcoid integer, @pcs_tipcta char (1), 
                                                                        @pcs_numcta varchar (20), @pcs_codigo varchar(10)) as
declare @pcs_pcsid int

set @pcs_pcsid = (select IsNull(Max(pcs_pcsid)+1, 1) from provcli_sucursal where pcs_codemp = @pcs_codemp)

  INSERT INTO provcli_sucursal  
         ( pcs_codemp,   
           pcs_pclid,   
           pcs_pcsid,   
           pcs_nombre,   
           pcs_comid,   
           pcs_direccion,   
           pcs_telefono,   
           pcs_fax,   
           pcs_mail,   
           pcs_casamatriz,   
           pcs_bcoid,   
           pcs_tipcta,   
           pcs_numcta,
           pcs_codigo )  
  VALUES ( @pcs_codemp,   
           @pcs_pclid,   
           @pcs_pcsid,   
           @pcs_nombre,   
           @pcs_comid,   
           @pcs_direccion,   
           @pcs_telefono,   
           @pcs_fax,   
           @pcs_mail,   
           @pcs_casamatriz,   
           @pcs_bcoid,   
           @pcs_tipcta,   
           @pcs_numcta,
           @pcs_codigo )