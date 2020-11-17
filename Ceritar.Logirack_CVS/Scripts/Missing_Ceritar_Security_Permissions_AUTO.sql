DECLARE @securedSchema VARCHAR(50)
SET @securedSchema = CASE WHEN (SELECT TTL_APP_Name FROM TTLogIn WHERE TTL_APP_Principale = 1) = 'LogirackPublic' THEN 'Logirack_Security' ELSE 'App_Table' END

DECLARE CUR CURSOR LOCAL FAST_FORWARD FOR
SELECT  SSU.[name] AS "Schema",
		SSO.[name],
		SSO.[type]
FROM sys.sysobjects as SSO 
	LEFT JOIN sys.database_permissions as SDP ON SSO.id = SDP.major_id  
	INNER JOIN sys.sysusers as SSU ON SSO.uid = SSU.uid 
WHERE sdp.state_desc is null
	AND SSO.[type] not in ('D', 'SQ', 'F', 'K', 'TR', 'R')
	AND SSU.[name] not in ('sys')
	AND sso.name not in ('sysdiagrams')
	AND sso.name not like 'syncobj_0x%'
	AND sso.name not like 'sys%'
	AND sso.name not like 'VTC%'
ORDER BY SSU.[name], sso.type, SSO.[name]

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
	if @type in ('U') /*Table (user-defined)*/
	begin
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  SELECT  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  INSERT  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  UPDATE  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
		SET @StrSQL_3 = @StrSQL_3 + 'GRANT  DELETE  ON [' + @schema + '].[' + @name + '] TO [' + @securedSchema + ']' + CHAR(13)
	end
	  
	FETCH NEXT FROM cur into @schema, @name, @type
END
  
   --PRINT @StrSQL_1 + @StrSQL_2 + @StrSQL_3
  
   EXEC (@StrSQL_1 + @StrSQL_2 + @StrSQL_3)

close cur
deallocate cur  