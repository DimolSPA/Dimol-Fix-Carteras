

Create Procedure Insertar_Maestra_ProvCli_Estados(@mpe_codemp integer, @mpe_pclid integer, @mpe_estid integer,   
                                                  @mpe_estado varchar(200), @mpe_codigo varchar(10)) as
  INSERT INTO maestra_provcli_estados
         ( mpe_codemp,   
           mpe_pclid,   
           mpe_estid,   
           mpe_estado,
           mpe_codigo  )  
  VALUES ( @mpe_codemp,   
           @mpe_pclid,   
           @mpe_estid,   
           @mpe_estado,
           @mpe_codigo )
