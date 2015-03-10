
-- Indicator Management System - New Role;
INSERT INTO aspnet_Roles (ApplicationId, RoleName, Description) VALUES (3, 'RoleDbAdmin', 'Can use indicator management system');



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