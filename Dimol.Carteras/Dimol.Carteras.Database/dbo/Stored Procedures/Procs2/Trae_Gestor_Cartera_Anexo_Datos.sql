﻿create Procedure Trae_Gestor_Cartera_Anexo_Datos(@gsa_codemp integer, @gsa_sucid integer, @gsa_ctcid numeric(15), @gsa_gesid2 integer) as    SELECT gestor_cartera_anexo.gsa_gesid,              gestor_cartera_anexo.gsa_porcom,              gestor_cartera_anexo.gsa_porcomgp        FROM gestor_cartera_anexo       WHERE ( gestor_cartera_anexo.gsa_codemp = @gsa_codemp ) AND             ( gestor_cartera_anexo.gsa_sucid = @gsa_sucid ) AND             ( gestor_cartera_anexo.gsa_ctcid = @gsa_ctcid ) AND             ( gestor_cartera_anexo.gsa_gesid2 = @gsa_gesid2 )   