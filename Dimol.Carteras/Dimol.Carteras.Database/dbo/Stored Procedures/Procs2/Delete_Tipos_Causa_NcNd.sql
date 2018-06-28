

Create Procedure Delete_Tipos_Causa_NcNd(@tnt_codemp integer, @tnt_tntid integer) as


  DELETE FROM tipos_causa_ncnd_idiomas  
   WHERE ( tipos_causa_ncnd_idiomas.tni_codemp = @tnt_codemp ) AND  
         ( tipos_causa_ncnd_idiomas.tni_tntid = @tnt_tntid )   
        

  DELETE FROM tipos_causa_ncnd  
   WHERE ( tipos_causa_ncnd.tnt_codemp = @tnt_codemp ) AND  
         ( tipos_causa_ncnd.tnt_tntid = @tnt_tntid )
