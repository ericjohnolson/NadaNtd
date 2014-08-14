update interventionindicators set mergeruleid = 58 where displayname = 'PcIntvNumberOfTreatmentRoundsPlannedForTheYear';
update processindicators set mergeruleid = 53 where displayname = 'PCTrainTrainingCategory';
update processindicators set mergeruleid = 53 where displayname = 'SCMDrug';
update processindicators set mergeruleid = 53 where displayname = 'SCMUnit';



INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0015'
       ,'sc.01.00.0015.sql'
       ,Now());