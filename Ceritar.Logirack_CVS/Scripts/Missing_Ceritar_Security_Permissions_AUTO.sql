DECLARE @securedSchema VARCHAR(50)
SET @securedSchema = CASE WHEN (SELECT TTL_APP_Name FROM TTLogIn WHERE TTL_APP_Principale = 1) = 'LogirackPublic' THEN 'Logirack_Security' ELSE 'App_Table' END

DECLARE CUR CURSOR LOCAL FAST_FORWARD FOR
SELECT DISTINCT SU.[name] AS "Schema",
				SO.[name],
				SO.[type]--,SDP.*,DP.*
FROM sys.sysobjects as SO 
	INNER JOIN sys.sysusers as SU ON SO.uid = SU.uid 
	--LEFT JOIN sys.database_permissions as SDP 
	--	INNER JOIN sys.database_principals DP ON DP.principal_id = SDP.grantee_principal_id
	--ON SO.id = SDP.major_id 
WHERE SO.[type] not in ('D', 'SQ', 'F', 'K', 'TR', 'R', 'C')
	AND SU.name = 'dbo'
	AND SO.name not in ('sysdiagrams')
	AND SO.name not like 'syncobj_0x%'
	AND SO.name not like 'sys%'
	AND SO.name not like 'VTC%'
	AND SO.name not like 'dt_%'
	--AND So.name = 'Alo'
	AND NOT EXISTS (SELECT 1 FROM sys.database_permissions as SDP 
						INNER JOIN sys.database_principals DP ON DP.principal_id = SDP.grantee_principal_id
					WHERE SO.id = SDP.major_id 
					  AND DP.name in ('Logirack_Security','App_Table'))
ORDER BY SU.[name], SO.type, SO.[name]

OPEN cur

DECLARE @schema VARCHAR(50)
DECLARE @name VARCHAR(250)
DECLARE @type VARCHAR(15)
DECLARE @StrSQL_1 VARCHAR(MAX) = ''
DECLARE @StrSQL_2 VARCHAR(MAX) = ''
DECLARE @StrSQL_3 VARCHAR(MAX) = ''

FETCH NEXT FROM cur into @schema, @name, @type

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Fonctions, stored proc
	if @type in ('P', 'FN') /*SQL Stored Procedure*/ /*SQL scalar function*/
	  begin
		SET @StrSQL_1 = @StrSQL_1 + 'GRANT  EXECUTE  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
	  end
	  
	-- Views
	if @type in ('V', 'IF', 'TF') /*View*/ /*Inline function*/ /*SQL table-valued-function*/
	  begin
		SET @StrSQL_2 = @StrSQL_2 + 'GRANT  SELECT  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
	  end
	  
	-- Tables
	if @type in ('U', 'SN') /*Table (user-defined)*/
	begin
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  SELECT  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  INSERT  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  UPDATE  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  DELETE  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
	end
	  
	FETCH NEXT FROM cur into @schema, @name, @type
END
  
   PRINT @StrSQL_1 + @StrSQL_2 + @StrSQL_3
   /*
	DECLARE @FULL_SQL VARCHAR(max)= @StrSQL_1 + @StrSQL_2 + @StrSQL_3
	EXEC TMS_RDL.dbo.usp_TTLongPrint @FULL_SQL
   */
  
   --EXEC (@StrSQL_1 + @StrSQL_2 + @StrSQL_3)

close cur
deallocate cur  