-- HAVEN'T INSERTED, waiting for Jennifer's response;

update diseasedistributionindicators set sortorder = 100 where DiseaseId = 13 and displayname = 'DDTraDiseaseDistributionPcInterventions';
update diseasedistributionindicators set sortorder = 110 where DiseaseId = 13 and displayname = 'DDTraDiseaseDistributionTrichiasisSurge';
update diseasedistributionindicators set sortorder = 140 where DiseaseId = 13 and displayname = 'DDTraPopulationAtRisk';
update diseasedistributionindicators set sortorder = 210 where DiseaseId = 13 and displayname = 'DDTraPopulationLivingInAreasDistrict';
update diseasedistributionindicators set sortorder = 230 where DiseaseId = 13 and displayname = 'DDTraPopulationLivingInAreasSubDistrict';
update diseasedistributionindicators set sortorder = 200 where DiseaseId = 13 and displayname = 'DDTraYearDeterminedThatAchievedCriteriaF';
update diseasedistributionindicators set sortorder = 160, datatypeid=4 where DiseaseId = 13 and displayname = 'DDTraYearOfPlannedTrachomaImpactSurvey';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 5, 'DDTraLevelRecWho', 3, 120, 26, NOW(), 0, 0, 0, 0, 0, -1, 2, 58);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'District','District',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraLevelRecWho';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'SubDistrict','SubDistrict',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraLevelRecWho';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 5, 'DDTraLevelCurrent', 3, 120, 26, NOW(), 0, 0, 0, 0, 0, -1, 2, 58);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'District','District',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraLevelCurrent';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'SubDistrict','SubDistrict',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraLevelCurrent';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 5, 'DDTraAllTf', 3, 130, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 58);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'Yes','Yes',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAllTf';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'No','No',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAllTf';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 5, 'DDTraAllTt',3, 170, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 58);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'Yes','Yes',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAllTt';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'No','No',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAllTt';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 5, 'DDTraAchievedCriteria', 3, 180, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 58);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'Yes','Yes',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAchievedCriteria';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'No','No',30,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAchievedCriteria';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,3,'NA','NA',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDTraAchievedCriteria';

insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 7, 'DDTraYearAchievedUiga', 2, 220, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 58);
insert into DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (13, 2, 'DDTraTrichiasisBacklog', 1, 150, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 57);

-- Trachoma Mapping Changes;

update SurveyIndicators set sortorder = 60 where surveytypeid = 20 and displayname = 'TraMapSurEndDateOfSurvey';
update SurveyIndicators set sortorder = 190 where surveytypeid = 20 and displayname = 'Partners';
update SurveyIndicators set sortorder = 50 where surveytypeid = 20 and displayname = 'TraMapSurStartDateOfSurvey';
update SurveyIndicators set sortorder = 30, IsRequired=0 where surveytypeid = 20 and displayname = 'SubDistrictName';
update SurveyIndicators set sortorder = 20, IsRequired=0 where surveytypeid = 20 and displayname = 'SuperDistrict';
update SurveyIndicators set sortorder = 40 where surveytypeid = 20 and displayname = 'TraMapSurSurveyType';
update SurveyIndicators set sortorder = 10, IsRequired=-1, CanAddValues=0 where surveytypeid = 20 and displayname = 'TraMapSurSurveyPower';
delete from indicatordropdownvalues where  EntityType = 3 AND IndicatorId = DLookup("ID", "SurveyIndicators", "DisplayName ='TraMapSurSurveyPower'");
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'SuperDistrict','SuperDistrict',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurSurveyPower';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'District','District',1,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurSurveyPower';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'SubDistrict','SubDistrict',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurSurveyPower';
update SurveyIndicators set sortorder = 180 where surveytypeid = 20 and displayname = 'TraMapSurLevelOfAntibioticTreatmentRequi';
delete from indicatordropdownvalues where  EntityType = 3 AND IndicatorId = DLookup("ID", "SurveyIndicators", "DisplayName ='TraMapSurLevelOfAntibioticTreatmentRequi'");
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraMapSurDistLevelMda','TraMapSurDistLevelMda',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurLevelOfAntibioticTreatmentRequi';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraMapSurSubDistMda','TraMapSurSubDistMda',1,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurLevelOfAntibioticTreatmentRequi';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraMapSurTargetedMda','TraMapSurTargetedMda',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurLevelOfAntibioticTreatmentRequi';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,4,'TraMapSurNoAReq','TraMapSurNoAReq',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurLevelOfAntibioticTreatmentRequi';

insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurNoClusters', 5, 70, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSur1to9yo', 5, 80, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSur15yo', 5, 90, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurPrevalenceActive', 5, 100, 26, NOW(), 0, 0, -1, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurPrevTt', 5, 120, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurSurgery01pop', 5, 150, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurSurgeryNumTt', 5, 155, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 2, 'TraMapSurFandE', 5, 170, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);

insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 5, 'TraMapSurActiveTrachomaIndicator', 5, 110, 26, NOW(), 0, 0, -1, 0, 0, 0, 2, 1);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraMapSurTfAlone','TraMapSurTfAlone',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraMapSurTiAlone','TraMapSurTiAlone',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraMapSurTfTi','TraMapSurTfTi',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';

insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (20, 5, 'TraMapSurTtInd', 5, 130, 26, NOW(), 0, 0, 0, 0, 0, -1, 2, 1);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraMapSurMaleFemale15yo','TraMapSurMaleFemale15yo',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraMapSurFemale15yo','TraMapSurFemale15yo',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraMapSurMaleFemale40yo','TraMapSurMaleFemale40yo',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,4,'TraMapSurFemale40yo','TraMapSurFemale40yo',4,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';


-- Tra Impact;

update SurveyIndicators set sortorder =  130 where surveytypeid = 14 and displayname = 'TraSurDateOfTheFirstRoundOfDistributionO';
update SurveyIndicators set sortorder =  50 where surveytypeid = 14 and displayname = 'TraSurEndDateOfSurvey';
update SurveyIndicators set sortorder =  180 where surveytypeid = 14 and displayname = 'Partners';
update SurveyIndicators set sortorder = 170 where surveytypeid = 14 and displayname = 'TraSurLevelOfAntibioticTreatmentRequired';
delete from indicatordropdownvalues where  EntityType = 3 AND IndicatorId = DLookup("ID", "SurveyIndicators", "DisplayName ='TraSurLevelOfAntibioticTreatmentRequired'");
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraSurDistLevel','TraSurDistLevel',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurLevelOfAntibioticTreatmentRequired';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraSurSubDistLevel','TraSurSubDistLevel',1,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurLevelOfAntibioticTreatmentRequired';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraSurTargetedMda','TraSurTargetedMda',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurLevelOfAntibioticTreatmentRequired';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,4,'TraSurPostMda','TraSurPostMda',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurLevelOfAntibioticTreatmentRequired';

update SurveyIndicators set sortorder = 40, IsRequired=0 where surveytypeid = 14 and displayname = 'TraSurStartDateOfSurvey';
update SurveyIndicators set sortorder = 20, IsRequired=0 where surveytypeid = 14 and displayname = 'SubDistrictName';
update SurveyIndicators set sortorder = 30 where surveytypeid = 14 and displayname = 'TraSurSurveyType';
update SurveyIndicators set sortorder = 10, IsRequired=-1, CanAddValues=0 where surveytypeid = 14 and displayname = 'TraSurSurveyPower';
delete from indicatordropdownvalues where  EntityType = 3 AND IndicatorId = DLookup("ID", "SurveyIndicators", "DisplayName ='TraSurSurveyPower'");
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'District','District',1,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurSurveyPower';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'SubDistrict','SubDistrict',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraSurSurveyPower';


insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurNoClusters', 5, 60, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSur1to9yo', 5, 70, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSur15yo', 5, 80, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurPrevalenceActive', 5, 90, 26, NOW(), 0, 0, -1, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurPrevTt', 5, 110, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurSurgery01pop', 5, 140, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurSurgeryNumTt', 5, 150, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);
insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 2, 'TraMapSurFandE', 5, 160, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 1);

insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 5, 'TraMapSurActiveTrachomaIndicator', 5, 100, 26, NOW(), 0, 0, -1, 0, 0, 0, 2, 1);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraMapSurTfAlone','TraMapSurTfAlone',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraMapSurTiAlone','TraMapSurTiAlone',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraMapSurTfTi','TraMapSurTfTi',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurActiveTrachomaIndicator';

insert into SurveyIndicators (surveytypeid, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, 
IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (14, 5, 'TraMapSurTtInd', 5, 120, 26, NOW(), 0, 0, 0, 0, 0, -1, 2, 1);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'TraMapSurMaleFemale15yo','TraMapSurMaleFemale15yo',0,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'TraMapSurFemale15yo','TraMapSurFemale15yo',2,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,3,'TraMapSurMaleFemale40yo','TraMapSurMaleFemale40yo',3,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,4,'TraMapSurFemale40yo','TraMapSurFemale40yo',4,26,Now(),26,Now() from SurveyIndicators where displayname = 'TraMapSurTtInd';


-- Intv Zith + Teo;

Delete from IndicatorDropdownValues where indicatorid = 285 and entitytype = 2;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
values (285,2,1,'PcIntvTraDrugZith','PcIntvTraDrugZith',1,26,Now(),26,Now());
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
values (285,2,2,'PcIntvTraDrugTeo','PcIntvTraDrugTeo',2,26,Now(),26,Now());
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
values (285,2,3,'PcIntvTraDrugPos','PcIntvTraDrugPos',3,26,Now(),26,Now());

-- Intv Ts;

insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsNumKits', 1, 10, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 57);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsNumKits'; 

insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsNumOperated', 1, 20, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 57);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsNumOperated'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsNumFemales', 1, 30, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 57);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsNumFemales'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsNumMales', 1, 40, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 57);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsNumMales'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsNumTt', 1, 50, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 57);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsNumTt'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsTtToZithro', 2, 60, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 56);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsTtToZithro'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsTtToTeo', 2, 70, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 56);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsTtToTeo'; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (2, 'PcIntvTsPercentWithPostOpTt', 2, 90, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 56);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsPercentWithPostOpTt'; 

insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) 
values (5, 'PcIntvTsEval', 3, 80, 26, NOW(), 0, 0, 0, 0, 0, 0, 2, 58);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 24, ID FROM interventionindicators where displayname = 'PcIntvTsEval'; 
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,2,1,'Yes','Yes',0,26,Now(),26,Now() from interventionindicators where displayname = 'PcIntvTsEval';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,2,2,'No','No',10,26,Now(),26,Now() from interventionindicators where displayname = 'PcIntvTsEval';


-- New process type: Trichiasis Surgeon Training;

INSERT INTO ProcessTypes (TypeName, UpdatedById, UpdatedAt, CreatedById, CreatedAt) values ('TriTraining', 26, NOW(), 26, NOW());
INSERT INTO ProcessTypes_to_diseases (ProcessTypeId, DiseaseId) Select ID, 13 from processtypes where typename = 'TriTraining';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 4, 'DateReported', 5, 0, 26, NOW(), 0, 0, -1, 0, 0,  2, 55 from ProcessTypes where typename = 'TriTraining';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 5, 'TriTrainingTrainerName', 4, 10, 26, NOW(), 0, 0, -1, 0, -1,  2, 54 from ProcessTypes where typename = 'TriTraining';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 5, 'TriTrainingSurgeonName', 4, 40, 26, NOW(), 0, 0, 0, 0, -1,  2, 54 from ProcessTypes where typename = 'TriTraining';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed,  CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 6, 'TriTrainingType', 4, 20, 26, NOW(), 0, 0, 0, 0,  -1, 2, 54 from ProcessTypes where typename = 'TriTraining';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'TriTrainingNewSurgeon','TriTrainingNewSurgeon',0,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingType';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,2,'TriTrainingRefresher','TriTrainingRefresher',10,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingType';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,3,'TriTrainingCertification','TriTrainingCertification',20,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingType';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 6, 'TriTrainingMethod', 4, 30, 26, NOW(), 0, 0, 0, 0, -1, 2, 54 from ProcessTypes where typename = 'TriTraining';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'TriTrainingBltr','TriTrainingBltr',0,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingMethod';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,2,'TriTrainingTrabut','TriTrainingTrabut',10,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingMethod';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 5, 'TriTrainingQualifications', 4, 50, 26, NOW(), 0, 0, 0, 0,  -1, 2, 54 from ProcessTypes where typename = 'TriTraining';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'TriTrainingEyeCareWorker','TriTrainingEyeCareWorker',0,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingQualifications';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,2,'TriTrainingHealthWorker','TriTrainingHealthWorker',10,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingQualifications';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 5, 'TriTrainingWasCertified', 3, 60, 26, NOW(), 0, 0, -1, 0, 0,  2, 58 from ProcessTypes where typename = 'TriTraining';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'Yes','Yes',0,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingWasCertified';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,2,'No','No',10,26,Now(),26,Now() from processindicators where displayname = 'TriTrainingWasCertified';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 15, 'Notes', 4, 500, 26, NOW(), 0, 0, 0, 0, 0,  1, 1 from ProcessTypes where typename = 'TriTraining';

-- New process type: TT AIOs;

INSERT INTO ProcessTypes (TypeName, UpdatedById, UpdatedAt, CreatedById, CreatedAt) values ('TtAios', 26, NOW(), 26, NOW());
INSERT INTO ProcessTypes_to_diseases (ProcessTypeId, DiseaseId) Select ID, 13 from processtypes where typename = 'TtAios';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 4, 'DateReported', 5, 0, 26, NOW(), 0, 0, -1, 0, 0,  2, 55 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 15, 'Notes', 4, 500, 26, NOW(), 0, 0, 0, 0, 0,  1, 1 from ProcessTypes where typename = 'TtAios';

insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2014', 1, 10, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2015', 1, 20, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2016', 1, 30, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2017', 1, 40, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2018', 1, 50, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2019', 1, 60, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 2, 'TtAios2020', 1, 70, 26, NOW(), 0, 0, 0, 0, 0,  0, 57 from ProcessTypes where typename = 'TtAios';

-- SCM;

update processindicators set sortorder = sortorder * 10 where processtypeid = 8;
update processindicators set DataTypeId = 13 where displayname = 'SCMRemaining';
update processindicators set sortorder = 115 where displayname = 'SCMExpiredDrugs';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'ScmDrpPara','ScmDrpPara',0,26,Now(),26,Now() from processindicators where displayname = 'SCMDrug';
 
insert into processindicators (ProcessTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, CanAddValues, RedistrictRuleId, MergeRuleId) 
Select ID, 5, 'ScmTypeOfInventory', 4, 25, 26, NOW(), 0, 0, -1, 0, -1,  2, 54 from ProcessTypes where typename = 'SCM';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,1,'ScmPhysicalCount','ScmPhysicalCount',0,26,Now(),26,Now() from processindicators where displayname = 'ScmTypeOfInventory';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,4,2,'ScmTheoreticalCount','ScmTheoreticalCount',10,26,Now(),26,Now() from processindicators where displayname = 'ScmTypeOfInventory';
