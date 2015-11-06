--Story 334;
ALTER TABLE Surveys ADD SiteTypeId INTEGER;
UPDATE Surveys SET SiteTypeId = 1 WHERE SiteType IN ('Sentinel', 'Sentinelle', 'sentinela');
UPDATE Surveys SET SiteTypeId = 2 WHERE SiteType IN ('Spot Check', 'Vérification ponctuelle', 'Spot check', 'verificar no local');

INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('02'
       ,'00'
       ,'0004'
       ,'sc.02.00.0004.sql'
       ,Now());
