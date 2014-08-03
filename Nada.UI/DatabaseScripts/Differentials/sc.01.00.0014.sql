
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE RedistrictEvents ADD COLUMN RedistrictDate DateTime;
COMMIT TRANSACTION;
BEGIN TRANSACTION;

INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0014'
       ,'sc.01.00.0014.sql'
       ,Now());