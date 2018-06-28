create Procedure [dbo].[_Editar_Deudor_Parcial](@ctc_codemp integer, 
										@ctc_ctcid integer, 
										@ctc_nombre varchar (400), 
										@ctc_apepat varchar (100), 
										@ctc_apemat varchar (100),
                                        @ctc_nomfant varchar (600), 
                                        @ctc_comid integer, 
                                        @ctc_direccion varchar (800), 
                                        @ctc_estdir int ) as 
                                                          

begin
 UPDATE deudores  
     SET ctc_nombre = @ctc_nombre,   
         ctc_apepat = @ctc_apepat,   
         ctc_apemat = @ctc_apemat,   
         ctc_nomfant = @ctc_nomfant,   
         ctc_comid = @ctc_comid,   
         ctc_direccion = @ctc_direccion,     
         ctc_estdir = @ctc_estdir 
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )
         
         SELECT @ctc_ctcid as ctcid
end


                                                          
 
