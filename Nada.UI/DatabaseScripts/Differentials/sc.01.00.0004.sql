CREATE TABLE [RedistrictEvents](
   [ID] AUTOINCREMENT,
   RedistrictTypeId INTEGER,
   CreatedById Integer,
   CreatedAt DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

CREATE TABLE [RedistrictUnits](
   [ID] AUTOINCREMENT,
   RedistrictEventId Integer,
   AdminLevelUnitId INTEGER,
   RelationshipId INTEGER,
   Percentage DOUBLE,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

CREATE TABLE [RedistrictForms](
   [ID] AUTOINCREMENT,
   RedistrictEventId Integer,
   EntityId INTEGER,
   EntityTypeId INTEGER,
   RelationshipId INTEGER,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);


COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ADD COLUMN RedistrictIdForDaughter INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;

ALTER TABLE AdminLevels ALTER COLUMN RedistrictIdForDaughter SET DEFAULT 0;
UPDATE AdminLevels set RedistrictIdForDaughter=0;

COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ADD COLUMN RedistrictIdForMother INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;

ALTER TABLE AdminLevels ALTER COLUMN RedistrictIdForMother SET DEFAULT 0;
UPDATE AdminLevels set RedistrictIdForMother=0;

COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicators ADD COLUMN RedistrictRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicators ALTER COLUMN RedistrictRuleId SET DEFAULT 1;
UPDATE SurveyIndicators set RedistrictRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicators ADD COLUMN RedistrictRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicators ALTER COLUMN RedistrictRuleId SET DEFAULT 1;
UPDATE DiseaseDistributionIndicators set RedistrictRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicators ADD COLUMN RedistrictRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicators ALTER COLUMN RedistrictRuleId SET DEFAULT 1;
UPDATE InterventionIndicators set RedistrictRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicators ADD COLUMN RedistrictRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicators ALTER COLUMN RedistrictRuleId SET DEFAULT 1;
UPDATE ProcessIndicators set RedistrictRuleId=1;

COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicatorValues ADD COLUMN CalcByRedistrict YesNo;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicatorValues ALTER COLUMN CalcByRedistrict SET DEFAULT 0;
UPDATE SurveyIndicatorValues set CalcByRedistrict=0;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicatorValues ADD COLUMN CalcByRedistrict YesNo;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicatorValues ALTER COLUMN CalcByRedistrict SET DEFAULT 0;
UPDATE InterventionIndicatorValues set CalcByRedistrict=0;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicatorValues ADD COLUMN CalcByRedistrict YesNo;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicatorValues ALTER COLUMN CalcByRedistrict SET DEFAULT 0;
UPDATE DiseaseDistributionIndicatorValues set CalcByRedistrict=0;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicatorValues ADD COLUMN CalcByRedistrict YesNo;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicatorValues ALTER COLUMN CalcByRedistrict SET DEFAULT 0;
UPDATE ProcessIndicatorValues set CalcByRedistrict=0;


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0004'
       ,'sc.01.00.0004.sql'
       ,Now());