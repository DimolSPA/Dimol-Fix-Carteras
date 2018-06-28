CREATE Procedure [dbo].[_Insertar_TipoContacto](@tic_codemp integer, @idioma integer, @tic_nombre varchar(80)) as
declare @ticid int

set @ticid = (select IsNull(Max(tic_ticid)+1, 1) from TIPOS_CONTACTO where tic_codemp = @tic_codemp)

  INSERT INTO TIPOS_CONTACTO  
         ( tic_codemp,   
           tic_ticid,   
           tic_nombre)  
  VALUES ( @tic_codemp,
		   @ticid,
           @tic_nombre)
           
  INSERT INTO TIPOS_CONTACTO_IDIOMAS  
         ( tci_codemp,   
           tci_ticid,   
           tci_idid,   
           tci_nombre )  
  VALUES ( @tic_codemp,
           @ticid,
		   @idioma,
           @tic_nombre )
