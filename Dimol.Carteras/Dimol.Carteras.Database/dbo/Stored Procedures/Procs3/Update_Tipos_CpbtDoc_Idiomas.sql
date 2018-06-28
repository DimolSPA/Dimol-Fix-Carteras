

Create Procedure Update_Tipos_CpbtDoc_Idiomas(@tci_codemp integer, @tci_tpcid integer, @tci_idid integer, @tci_nombre varchar(100)) as
   UPDATE tipos_cpbtdoc_idiomas  
     SET tci_nombre = @tci_nombre  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = @tci_codemp ) AND  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = @tci_tpcid ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid )
