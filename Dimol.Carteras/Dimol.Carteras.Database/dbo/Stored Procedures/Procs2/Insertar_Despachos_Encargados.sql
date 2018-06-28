

Create Procedure Insertar_Despachos_Encargados(@dpe_codemp integer, @dpe_sucid integer, @dpe_dpcid integer, @dpe_rut varchar(20), @dpe_nombre varchar(200), @dpe_emplid integer, @dpe_nivel smallint) as
  INSERT INTO despachos_encargados  
         ( dpe_codemp,   
           dpe_sucid,   
           dpe_dpcid,   
           dpe_rut,   
           dpe_nombre,   
           dpe_emplid,   
           dpe_nivel )  
  VALUES ( @dpe_codemp,   
           @dpe_sucid,   
           @dpe_dpcid,   
           @dpe_rut,   
           @dpe_nombre,   
           @dpe_emplid,   
           @dpe_nivel )
