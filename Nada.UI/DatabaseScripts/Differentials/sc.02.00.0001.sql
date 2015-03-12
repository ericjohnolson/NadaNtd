
-- Indicator Management System - New Role;
INSERT INTO aspnet_Roles (ApplicationId, RoleName, Description) VALUES (3, 'RoleDbAdmin', 'Can use indicator management system');

-- Sort admin units;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ADD COLUMN SortOrder INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ALTER COLUMN SortOrder SET DEFAULT 0;
UPDATE AdminLevels set SortOrder=0;


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('02'
       ,'00'
       ,'0001'
       ,'sc.02.00.0001.sql'
       ,Now());