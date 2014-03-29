update processindicators set sortorder = 60 where ID = 66;
insert into processindicators (ProcessTypeId, DisplayName,SortOrder,DataTypeId,CanAddValues,AggTypeId,IsRequired,IsDisabled,IsEditable,IsDisplayed,UpdatedById,UpdatedAt)
values (7,'TrainingDrugPackages',4,6,0,4,-1,0,0,0,26,NOW());
insert into processindicators (ProcessTypeId, DisplayName,SortOrder,DataTypeId,CanAddValues,AggTypeId,IsRequired,IsDisabled,IsEditable,IsDisplayed,UpdatedById,UpdatedAt)
values (7,'TrainingDiseases',3,6,0,4,-1,0,0,0,26,NOW());
insert into processindicators (ProcessTypeId, DisplayName,SortOrder,DataTypeId,CanAddValues,AggTypeId,IsRequired,IsDisabled,IsEditable,IsDisplayed,UpdatedById,UpdatedAt)
values (7,'TrainingNumFemales',55,2,0,1,0,0,0,0,26,NOW());
insert into processindicators (ProcessTypeId, DisplayName,SortOrder,DataTypeId,CanAddValues,AggTypeId,IsRequired,IsDisabled,IsEditable,IsDisplayed,UpdatedById,UpdatedAt)
values (7,'TrainingNumMales',56,2,0,1,0,0,0,0,26,NOW());
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,1,'IntvPkgALB','IntvPkgALB',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,2,'IntvPkgMBD','IntvPkgMBD',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,3,'IntvPkgPZQ','IntvPkgPZQ',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,4,'IntvPkgIVM','IntvPkgIVM',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,5,'IntvPkgIVMALB','IntvPkgIVMALB',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,6,'IntvPkgDECALB','IntvPkgDECALB',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,7,'IntvPkgPZQALB','IntvPkgPZQALB',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,8,'IntvPkgPZQMBD','IntvPkgPZQMBD',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,9,'IntvPkgPZQIVMALB','IntvPkgPZQIVMALB',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,10,'IntvPkgALB2forLF','IntvPkgALB2forLF',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,11,'IntvPkgTEO','IntvPkgTEO',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,12,'IntvPkgZithro','IntvPkgZithro',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,13,'IntvPkgTEOZithro','IntvPkgTEOZithro',0,26,Now(),26,Now() from processindicators where displayname = 'trainingdrugpackages' and processtypeid = 7;

insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,1,'LF','LF',0,26,Now(),26,Now() from processindicators where displayname = 'TrainingDiseases' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,2,'Oncho','Oncho',0,26,Now(),26,Now() from processindicators where displayname = 'TrainingDiseases' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,3,'Schisto','Schisto',0,26,Now(),26,Now() from processindicators where displayname = 'TrainingDiseases' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,4,'STH','STH',0,26,Now(),26,Now() from processindicators where displayname = 'TrainingDiseases' and processtypeid = 7;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
Select ID, 4,5,'Trachoma','Trachoma',0,26,Now(),26,Now() from processindicators where displayname = 'TrainingDiseases' and processtypeid = 7;

update indicatordropdownvalues set dropdownvalue = 'LfEnd0b', TranslationKey = 'LfEnd0b' where id = 37;

insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
values (268,2,20,'Schisto','Schisto',4,26,Now(),26,Now());

CREATE TABLE [EvaluationSites](
   [ID] AUTOINCREMENT,
   [DisplayName] TEXT,
   [IsDeleted] YesNo,
   UpdatedById NUMBER,
   UpdatedAt DATETIME,
   CreatedById NUMBER,
   CreatedAt DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

Update SurveyTypes set HasMultipleLocations = -1 Where ID in (19, 13);

insert into processindicators (SurveyTypeId, DisplayName,SortOrder,DataTypeId,CanAddValues,AggTypeId,IsRequired,IsDisabled,IsEditable,IsDisplayed,UpdatedById,UpdatedAt)
values (7,'TrainingDrugPackages',4,6,0,4,-1,0,0,0,26,NOW());


Insert into indicatordatatypes (DataType, HasValueList) values ('EvaluationSite', -1);

update surveyindicators set sortorder = -10 where id = 430;
update surveyindicators set sortorder = -10 where id = 424;

insert into surveyindicators (DisplayName, SortOrder,SurveyTypeId,DataTypeId,AggTypeId,IsDisabled,IsEditable,IsDisplayed,IsRequired,IsCalculated,CanAddValues,UpdatedById,UpdatedAt)
values ('OnchoMapSiteName', -3,19,1,5,0,0,0,0,0,0,26,NOW());
insert into surveyindicators (DisplayName, SortOrder,SurveyTypeId,DataTypeId,AggTypeId,IsDisabled,IsEditable,IsDisplayed,IsRequired,IsCalculated,CanAddValues,UpdatedById,UpdatedAt)
values ('OnchoMapEvaluationSite', -5,19,16,5,0,0,0,-1,0,-1,26,NOW());

insert into surveyindicators (DisplayName, SortOrder,SurveyTypeId,DataTypeId,AggTypeId,IsDisabled,IsEditable,IsDisplayed,IsRequired,IsCalculated,CanAddValues,UpdatedById,UpdatedAt)
values ('OnchoSurSiteName', -3,13,1,5,0,0,0,0,0,0,26,NOW());
insert into surveyindicators (DisplayName, SortOrder,SurveyTypeId,DataTypeId,AggTypeId,IsDisabled,IsEditable,IsDisplayed,IsRequired,IsCalculated,CanAddValues,UpdatedById,UpdatedAt)
values ('OnchoSurEvaluationSite', -5,13,16,5,0,0,0,-1,0,-1,26,NOW());

update indicatordropdownvalues set indicatorid = 999999 where entitytype = 3 and indicatorid = 239;
update indicatordropdownvalues set indicatorid = 239 where entitytype = 3 and indicatorid = 238;
update indicatordropdownvalues set indicatorid = 238 where entitytype = 3 and indicatorid = 999999;

delete from diseasedistributionindicators where id = 176;
delete from diseasedistributionindicators where id = 179;
delete from interventiontypes_to_indicators where indicatorid = 356 and interventiontypeid = 5;
delete from interventiontypes_to_indicators where indicatorid = 357 and interventiontypeid = 5;
delete from interventiontypes_to_indicators where indicatorid = 358 and interventiontypeid = 5;
delete from interventiontypes_to_indicators where indicatorid = 359 and interventiontypeid = 5;
delete from interventiontypes_to_indicators where indicatorid = 360 and interventiontypeid = 5;
delete from interventiontypes_to_indicators where indicatorid = 332 and interventiontypeid = 8;
delete from interventiontypes_to_indicators where indicatorid = 349 and interventiontypeid =9;
delete from interventiontypes_to_indicators where indicatorid = 350 and interventiontypeid =9;
delete from interventiontypes_to_indicators where indicatorid = 351 and interventiontypeid =9;
delete from interventiontypes_to_indicators where indicatorid = 352 and interventiontypeid =9;
delete from interventiontypes_to_indicators where indicatorid = 353 and interventiontypeid =9;
delete from interventiontypes_to_indicators where indicatorid = 354 and interventiontypeid =9;

insert into surveytypes (surveytypename, diseaseid, updatedbyid, updatedat, createdbyid, createdat) values ('OnchoSurEntomological', 4, 26, NOW(), 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('DateReported',-10, 21, 4, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurProject',0, 21, 5, 1, -1, 0, -1, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurCatchingPoint',10, 21, 1, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurRiverBasin',20, 21, 1, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurRiver',30, 21, 1, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurLat',40, 21, 2, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurLng',50, 21, 2, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurMonth',60, 21, 8, 5, -1, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurNoFlies',70, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurFliesDissected',80, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurParousFlies',90, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurInfectedFlies',100, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurInfectiveFlies',110, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurInfectiveLarvae',120, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurInfectionRate',130, 21, 13, 5, 0, -1, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurParousRate',140, 21, 13, 5, 0, -1, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurInfectivityRate',150, 21, 13, 5, 0, -1, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurParousFliesInfected',94, 21, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('OnchoSurParousFliesDisected',97, 21, 2, 1, 0, 0, 0, 0, 26, NOW());

drop table vectors;
drop table surveys_to_partners;
drop table surveys_to_vectors;
drop table Interventions_to_partners;
drop table Interventions_to_medicines;
drop table Interventions_to_diseases;
drop table SurveyLfMf;

insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID - 3, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurParousFliesInfected' and SurveyTypeId = 21;
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID - 4, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurParousFliesDisected' and SurveyTypeId = 21;
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID + 5, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurParousFlies' and SurveyTypeId = 21;
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID + 7, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurNoFlies' and SurveyTypeId = 21;
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID + 4, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurInfectiveFlies' and SurveyTypeId = 21;
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) Select ID + 8, 3, ID, 3 FROM SurveyIndicators where DisplayName ='OnchoSurNoFlies' and SurveyTypeId = 21;

Drop table InterventionDistributionMethods;

CREATE TABLE [CustomReports](
   [ID] AUTOINCREMENT,
   [DisplayName] TEXT,
   [ReportOptions] TEXT,
   [IsDeleted] YesNo,
   UpdatedById NUMBER,
   UpdatedAt DATETIME,
   CreatedById NUMBER,
   CreatedAt DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);


update surveyindicators set datatypeid = 1 where id = 259;
update surveyindicators set datatypeid = 1 where id = 261;
update surveyindicators set datatypeid = 1 where id = 260;
delete from surveyindicators where displayname = 'TASTasActualTimelineAndResourcesStaff';
delete from surveyindicators where displayname = 'TASTasActualTimelineAndResourcesTeams';

UPDATE SurveyIndicators Set SortOrder=SortOrder * 100 where SurveyTypeId = 15; 
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasEstimatedNonResponseRate',1950, 15, 2, 5, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasGradesWithMajorityOf6yo',3650, 15, 1, 5, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasTotalNumChildrenInGrades',3660, 15, 2, 5, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasTotalNumPrimarySchools',3670, 15, 2, 5, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasTotal6yoInCommunities',3680, 15, 2, 5, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId , IsRequired ,IsCalculated, CanAddValues , IsDisplayed,UpdatedById ,UpdatedAt) values('TasTotalAreasInCommunities',3690, 15, 2, 5, 0, 0, 0, 0, 26, NOW());

update surveyindicators set canaddvalues = 0 where id in (246, 249);
Delete from IndicatorDropdownValues where indicatorid = 249 and entitytype = 3 and ID not in (269, 270, 271);
Delete from IndicatorDropdownValues where indicatorid = 246 and entitytype = 3 and ID not in (329, 330);

ALTER TABLE Country ADD COLUMN ReportingYearStartDate DATETIME;
UPDATE Country SET ReportingYearStartDate = '1/1/2000';


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0002'
       ,'sc.01.00.0002.sql'
       ,Now());