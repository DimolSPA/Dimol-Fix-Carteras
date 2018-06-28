CREATE PROCEDURE [dbo].[_Insertar_Cartera_Clientes_Cpbt_Doc_Extension] (@codemp integer, @pclid numeric(15), @ctcid numeric(15),
@ccbid integer, @NUM_RESOLUCION varchar(20)
           ,@RUT_REPRESENTANTE_1 varchar(20)
           ,@NOM_REPRESENTANTE_1 varchar(100)
           ,@RUT_REPRESENTANTE_2 varchar(20)
           ,@NOM_REPRESENTANTE_2 varchar(100)
           ,@RUT_REPRESENTANTE_3 varchar(20)
           ,@NOM_REPRESENTANTE_3 varchar(100)
		   ,@FEC_RESOLUCION datetime)
AS
declare @existe int =0;


set @existe = (SELECT COUNT(CCB_CCBID)
  FROM [CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO]
  where CCB_CODEMP = @codemp
  and CCB_PCLID = @pclid
  and CCB_CTCID = @ctcid
  and CCB_CCBID = @ccbid)   
  
      if @existe = 0 
		begin 
INSERT INTO [dbo].[CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO]
           ([CCB_CODEMP]
           ,[CCB_PCLID]
           ,[CCB_CTCID]
           ,[CCB_CCBID]
           ,[NUM_RESOLUCION]
           ,[RUT_REPRESENTANTE_1]
           ,[NOM_REPRESENTANTE_1]
           ,[RUT_REPRESENTANTE_2]
           ,[NOM_REPRESENTANTE_2]
           ,[RUT_REPRESENTANTE_3]
           ,[NOM_REPRESENTANTE_3]
		   ,FEC_RESOLUCION
		   ,FEC_CARGA)
     VALUES
           (@CODEMP
           ,@PCLID
           ,@CTCID
           ,@CCBID
           ,@NUM_RESOLUCION
           ,@RUT_REPRESENTANTE_1
           ,@NOM_REPRESENTANTE_1
           ,@RUT_REPRESENTANTE_2
           ,@NOM_REPRESENTANTE_2
           ,@RUT_REPRESENTANTE_3
           ,@NOM_REPRESENTANTE_3
		   ,@FEC_RESOLUCION
		   ,getdate())



		end
		else
		begin
		update [dbo].[CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO] 
		set [NUM_RESOLUCION] = @NUM_RESOLUCION
           ,[RUT_REPRESENTANTE_1] = @RUT_REPRESENTANTE_1
           ,[NOM_REPRESENTANTE_1] = @NOM_REPRESENTANTE_1
           ,[RUT_REPRESENTANTE_2] = @RUT_REPRESENTANTE_2
           ,[NOM_REPRESENTANTE_2] = @NOM_REPRESENTANTE_2
           ,[RUT_REPRESENTANTE_3] = @RUT_REPRESENTANTE_3
           ,[NOM_REPRESENTANTE_3] = @NOM_REPRESENTANTE_3
		   ,FEC_RESOLUCION = @FEC_RESOLUCION
		   ,FEC_CARGA = getdate()
     where [CCB_CODEMP] = @CODEMP
           and [CCB_PCLID] = @PCLID
           and [CCB_CTCID] = @CTCID
           and [CCB_CCBID] = @CCBID



		end
