
update processindicators set isrequired = 0 where displayname = 'SAEResultoftest';
update processindicators set isrequired = 0 where displayname = 'SAEUnits';
update processindicators set isrequired = 0 where displayname = 'SAENormalRangeLow';
update processindicators set isrequired = 0 where displayname = 'SAENormalRangeHigh';
update processindicators set isrequired = 0 where displayname = 'SAEComments';
update processindicators set isrequired = 0 where displayname = 'SAEExtenuatingorcomplicatingcircumstances';

update surveyindicators set isrequired = 1 where displayname = 'TASActualSampleSizePositive';
update surveyindicators set isrequired = 1 where displayname = 'TASCriticalCutoffValue';

update surveyindicators set datatypeid = 7 where displayname = 'SCHSurDateOfTheFirstRoundOfPcYear';
update surveyindicators set datatypeid = 7 where displayname = 'STHSurDateOfTheFirstRoundOfPcYear';

INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0016'
       ,'sc.01.00.0016.sql'
       ,Now());