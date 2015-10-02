-- Update IVM + PZQ + ALB intv key;
UPDATE InterventionTypes SET InterventionTypeName = 'IntvIvmPzqAlb'
	WHERE InterventionTypeName = 'IVM+PZQ+ALB Intervention';

INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('02'
       ,'00'
       ,'0002'
       ,'sc.02.00.0002.sql'
       ,Now());