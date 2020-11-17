--if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sp_ScriptTTAppData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
--drop procedure dbo.sp_ScriptTTAppData
--GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nicholas Bolduc
-- Description:	Permet de générer les INSERT statements pour créer les données des objets TTApp
-- MARK IT WITH : EXEC sys.sp_MS_marksystemobject sp_ScriptTTAppData
-- =============================================
ALTER PROCEDURE sp_ScriptTTAppData
AS
BEGIN
SET NOCOUNT ON;

DECLARE @TTL_3PL_AppName VARCHAR(15) = 'LogirackPublic'
DECLARE @TTL_App_Name VARCHAR(30)
SELECT @TTL_App_Name = TTL_App_Name FROM TTLogIn WHERE TTL_App_Principale = 1

IF OBJECT_ID(N'tempdb.dbo.#TTAppObjects') IS NULL
	CREATE TABLE #TTAppObjects (LineID INT IDENTITY(1,1), InsertStatement VARCHAR(5000) NOT NULL)
ELSE
	TRUNCATE TABLE #TTAppObjects

INSERT INTO #TTAppObjects
SELECT 'IF (SELECT db_name()) NOT IN (''TMS_Dev'', ''WMS_Dev'', ''3PL_Dev'', ''SGSP5_Alim_Dev'', ''Projet_Dev'', ''ERP_DEV'')'
UNION ALL
SELECT 'BEGIN'

INSERT INTO #TTAppObjects
SELECT 'EXEC usp_EmptyTTApp;'


--*** TTAppCaption ***
INSERT INTO #TTAppObjects
SELECT 'ALTER INDEX ' + ind.Name + ' ON dbo.TTAppCaption DISABLE;' 
FROM sys.indexes ind INNER JOIN Sys.tables t ON ind.object_id = t.object_id WHERE t.Name = 'TTAppCaption' AND ind.is_primary_key = 0 AND t.is_ms_shipped = 0 AND ind.Name IS NOT NULL
UNION ALL
SELECT 'ALTER TABLE dbo.[TTAPPCaption] NOCHECK CONSTRAINT ' + obj.name
FROM sys.foreign_key_columns fkc
	INNER JOIN sys.objects obj ON obj.object_id = fkc.constraint_object_id
	INNER JOIN sys.tables tab1 ON tab1.object_id = fkc.parent_object_id
WHERE tab1.name = 'TTAppCaption'

INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTAppCaption', @from='FROM TTAppCaption ORDER BY TTAC_Cle ASC'

UPDATE #TTAppObjects SET InsertStatement = REPLACE(InsertStatement, 'INSERT INTO [TTAppCaption] (','INSERT INTO [TTAppCaption] WITH (TABLOCKX)(')

INSERT INTO #TTAppObjects
SELECT 'ALTER INDEX ' + ind.Name + ' ON dbo.TTAppCaption REBUILD;' 
FROM sys.indexes ind INNER JOIN Sys.tables t ON ind.object_id = t.object_id WHERE t.Name = 'TTAppCaption' AND ind.is_primary_key = 0 AND t.is_ms_shipped = 0 AND ind.Name IS NOT NULL
UNION ALL
SELECT 'ALTER TABLE dbo.[TTAPPCaption] CHECK CONSTRAINT ' + obj.name
FROM sys.foreign_key_columns fkc
	INNER JOIN sys.objects obj ON obj.object_id = fkc.constraint_object_id
	INNER JOIN sys.tables tab1 ON tab1.object_id = fkc.parent_object_id
WHERE tab1.name = 'TTAppCaption'


--*** TTCABMenu ***
IF @TTL_App_Name <> @TTL_3PL_AppName 
BEGIN
	INSERT INTO #TTAppObjects
	SELECT 'SET IDENTITY_INSERT TTCABMenu ON'
END

INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTCabMenu', @from='FROM TTCabMenu'

IF @TTL_App_Name <> @TTL_3PL_AppName 
BEGIN
	INSERT INTO #TTAppObjects
	SELECT 'SET IDENTITY_INSERT TTCABMenu OFF'
END


--*** TTCABOperation ***
IF @TTL_App_Name <> @TTL_3PL_AppName 
BEGIN
	INSERT INTO #TTAppObjects
	SELECT 'SET IDENTITY_INSERT TTCABOperation ON'
END

INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTCABOperation', @from='FROM TTCABOperation'

IF @TTL_App_Name <> @TTL_3PL_AppName 
BEGIN
	INSERT INTO #TTAppObjects
	SELECT 'SET IDENTITY_INSERT TTCABOperation OFF'
END


--*** TTCABRight ***
IF @TTL_App_Name <> @TTL_3PL_AppName
BEGIN
	INSERT INTO #TTAppObjects
	EXEC sp_generate_inserts @table_name='TTCABRight', @from='FROM TTCABRight WHERE TTCG_NRI = 1', @ommit_identity = 1	
END
ELSE
BEGIN
	DECLARE @TNEW_TTCR_NRI TABLE(NewNum INT)
	DECLARE @TTCG_NRI INT
	SELECT @TTCG_NRI = TTCG_NRI FROM TTCabGroupe WHERE TTCG_Admin = 1

	DECLARE @TTCG_NRI_Ori INT
	DECLARE @TTCM_NRI INT
	DECLARE @TTCR_Right INT
	DECLARE @TTCR_TS INT
	DECLARE @TmpTTCABRight TABLE ([TTCG_NRI] INT,[TTCM_NRI] INT,[TTCR_Right] INT,[TTCR_TS] INT)
	INSERT INTO @TmpTTCABRight
	SELECT TTCG_NRI,TTCM_NRI,TTCR_Right,TTCR_TS FROM TTCABRight WHERE TTCG_NRI = 1

	DECLARE rsTTCABRight CURSOR LOCAL FORWARD_ONLY READ_ONLY FOR
	SELECT * FROM @TmpTTCABRight

	OPEN rsTTCABRight
			
	FETCH NEXT FROM rsTTCABRight INTO   @TTCG_NRI_Ori, 
										@TTCM_NRI, 
										@TTCR_Right, 
										@TTCR_TS
			
	WHILE @@FETCH_STATUS <> -1
	BEGIN

		DELETE FROM @TNEW_TTCR_NRI
		UPDATE SystemeNRI 
		SET Number_NRI = Number_NRI + 1 
		OUTPUT inserted.Number_NRI 
		INTO @TNEW_TTCR_NRI 
		WHERE Field_NRI = 'TTCR_NRI'

		INSERT INTO #TTAppObjects
		SELECT 'INSERT INTO TTCABRight (TTCR_NRI,TTCR_Right,TTCR_TS,TTCM_NRI,TTCG_NRI) ' +
			   'VALUES (' + CONVERT(VARCHAR, (SELECT NewNum FROM @TNEW_TTCR_NRI)) + ',' +
			   CONVERT(VARCHAR, @TTCR_Right) + ',' +
			   CONVERT(VARCHAR, @TTCR_TS) + ',' +
			   CONVERT(VARCHAR, @TTCM_NRI) + ',' +
			   CONVERT(VARCHAR, CASE WHEN @TTCG_NRI IS NOT NULL THEN @TTCG_NRI ELSE @TTCG_NRI_Ori END) + ')'

		FETCH NEXT FROM rsTTCABRight INTO   @TTCG_NRI_Ori, 
											@TTCM_NRI, 
											@TTCR_Right, 
											@TTCR_TS
	END

	CLOSE rsTTCABRight
	DEALLOCATE rsTTCABRight
END


--*** TTListeGen ***
INSERT INTO #TTAppObjects
SELECT 'SET IDENTITY_INSERT TTListeGen ON'
INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTListeGen', @from='FROM TTListeGen'
INSERT INTO #TTAppObjects
SELECT 'SET IDENTITY_INSERT TTListeGen OFF'


--*** TTListeGenField ***
INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTListeGenField', @from='FROM TTListeGenField'


--*** TTListeGenRight ***
INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTListeGenRight', @from='FROM TTListeGenRight WHERE TTG_NRI=1', @ommit_identity = 1


--*** TTMenu ***
INSERT INTO #TTAppObjects
SELECT 'SET IDENTITY_INSERT TTMenu ON'

INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTMenu', @from='FROM TTMenu'

INSERT INTO #TTAppObjects
SELECT 'SET IDENTITY_INSERT TTMenu OFF'


--*** TTMenuRight ***
INSERT INTO #TTAppObjects
EXEC sp_generate_inserts @table_name='TTMenuRight', @from='FROM TTMenuRight WHERE TTG_NRI=1', @ommit_identity = 1


INSERT INTO #TTAppObjects
SELECT 'INSERT INTO TTScript (TTScr_Version,TTScr_Path,TTScr_Nom,TTScr_DtExecution,TTScr_User) VALUES ((SELECT TTP_Value FROM TTParam WHERE TTP_Name = ''AppVersion''), ''Like others'', ''TTApp.sql'',GETDATE(),(select SUSER_SNAME()))'
UNION ALL
SELECT 'END'

SELECT InsertStatement FROM #TTAppObjects ORDER BY LineID ASC

SET NOCOUNT OFF;
END
GO