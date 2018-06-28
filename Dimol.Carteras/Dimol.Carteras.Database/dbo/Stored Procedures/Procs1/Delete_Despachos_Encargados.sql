

Create Procedure Delete_Despachos_Encargados(@dpe_codemp integer, @dpe_sucid integer, @dpe_dpcid integer, @dpe_rut varchar(20)) as
 
  DELETE FROM despachos_encargados  
   WHERE ( despachos_encargados.dpe_codemp = @dpe_codemp ) AND  
         ( despachos_encargados.dpe_sucid = @dpe_sucid ) AND  
         ( despachos_encargados.dpe_dpcid = @dpe_dpcid ) AND  
         ( despachos_encargados.dpe_rut = @dpe_rut )
