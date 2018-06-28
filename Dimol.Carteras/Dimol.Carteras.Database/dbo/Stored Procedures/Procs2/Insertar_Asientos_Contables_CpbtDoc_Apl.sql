

Create Procedure Insertar_Asientos_Contables_CpbtDoc_Apl(@ada_codemp integer, @ada_anio integer, @ada_tipo char(1),
																			@ada_numero numeric(15), @ada_item integer, @ada_sucid integer,
																			@ada_tpcid integer, @ada_numcpbt numeric(15), @ada_aniodoc smallint,
																			@ada_numdoc numeric(15), @ada_anioapl smallint, @ada_numapl numeric(15)) as
  INSERT INTO asientos_contables_cpbtdoc_apl  
         ( ada_codemp,   
           ada_anio,   
           ada_tipo,   
           ada_numero,   
           ada_item,   
           ada_sucid,   
           ada_tpcid,   
           ada_numcpbt,   
           ada_aniodoc,   
           ada_numdoc,   
           ada_anioapl,   
           ada_numapl )  
  VALUES ( @ada_codemp,   
           @ada_anio,   
           @ada_tipo,   
           @ada_numero,   
           @ada_item,   
           @ada_sucid,   
           @ada_tpcid,   
           @ada_numcpbt,   
           @ada_aniodoc,   
           @ada_numdoc,   
           @ada_anioapl,   
           @ada_numapl )
