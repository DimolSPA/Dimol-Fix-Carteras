CREATE Procedure [dbo].[_Guardar_Deudor](@ctc_codemp integer, 
										@ctc_ctcid integer, 
										@ctc_rut varchar (20), 
										@ctc_numero numeric (12),
										@ctc_digito char (1), 
										@ctc_nombre varchar (400), 
										@ctc_apepat varchar (100), 
										@ctc_apemat varchar (100),
                                        @ctc_nomfant varchar (600), 
                                        @ctc_comid integer, 
                                        @ctc_direccion varchar (800), 
                                        @ctc_partemp char (1),
                                        @ctc_socid varchar(12), 
                                        @ctc_quiebra char(1), 
                                        @ctc_nacext char(1),
                                        @ctc_estdir int,
										@ctc_solicita_quiebra char(1) ) as 
                                                          

declare @ctcid int = (select isnull(CTC_CTCID,0) ctcid from DEUDORES where CTC_RUT = @ctc_rut)

if @ctcid <> 0
begin
 UPDATE deudores  
     SET ctc_rut = @ctc_rut,   
         ctc_numero = @ctc_numero,   
         ctc_digito = @ctc_digito,   
         ctc_nombre = @ctc_nombre,   
         ctc_apepat = @ctc_apepat,   
         ctc_apemat = @ctc_apemat,   
         ctc_nomfant = @ctc_nomfant,   
         ctc_comid = @ctc_comid,   
         ctc_direccion = @ctc_direccion,   
         ctc_partemp = @ctc_partemp,   
         ctc_socid = @ctc_socid,
         ctc_estdir = @ctc_estdir,
         ctc_quiebra = @ctc_quiebra,
         ctc_nacext = @ctc_nacext,
		 ctc_solicita_quiebra =  @ctc_solicita_quiebra 
   WHERE ( deudores.ctc_codemp = @ctc_codemp ) AND  
         ( deudores.ctc_ctcid = @ctc_ctcid )
         
         SELECT @ctcid as ctcid
end
else
begin 

set @ctcid = (  SELECT IsNull(Max(ctc_ctcid)+1, 1)     FROM deudores   WHERE ( deudores.ctc_codemp = @ctc_codemp ))

 INSERT INTO deudores  
         ( ctc_codemp,   
           ctc_ctcid,   
           ctc_rut,   
           ctc_numero,   
           ctc_digito,   
           ctc_nombre,   
           ctc_apepat,   
           ctc_apemat,   
           ctc_nomfant,   
           ctc_comid,   
           ctc_direccion,   
           ctc_partemp,   
           ctc_fecing,   
           ctc_socid, 
           ctc_estdir,
           ctc_quiebra,
           ctc_nacext,
		   ctc_solicita_quiebra )  
  VALUES ( @ctc_codemp,   
           @ctcid,   
           @ctc_rut,   
           @ctc_numero,   
           @ctc_digito,   
           @ctc_nombre,   
           @ctc_apepat,   
           @ctc_apemat,   
           @ctc_nomfant,   
           @ctc_comid,   
           @ctc_direccion,   
           @ctc_partemp,   
           getdate(),  
           @ctc_socid,
           1,
           @ctc_quiebra,
           @ctc_nacext,
		   @ctc_solicita_quiebra )
           
SELECT @ctcid as ctcid
end

                                                          
 
