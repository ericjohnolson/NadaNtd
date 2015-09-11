-- Add indicator num rounds of pc... story # 198;
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId)
	values (2, '4190984d-f272-4359-8414-6e7ef06fc4bc', 5, 250, 26, NOW(), 0, 0, 0, 0, 0, 0, 15, 2, 54);

-- Story #199 Critical decision dropdowns
DELETE FROM IndicatorDropdownValues
	WHERE DropdownValue IN ('TASCutoffFail', 'TASCutoffPass'); --ID IN (616, 617);
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 1, 'TASCutoffStopMDA', 'TASCutoffStopMDA', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'TASCriticalCutoff';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 2, 'TASCutoffContinueMDA', 'TASCutoffContinueMDA', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'TASCriticalCutoff';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 3, 'TASCutoffContinueSurveillance', 'TASCutoffContinueSurveillance', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'TASCriticalCutoff';

-- Story #191
INSERT INTO SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId)
	values (5, 'eab663f6-1eb8-4efc-85da-2844ee720020', 5, 610, 26, NOW(), 0, 0, 0, 0, 0, 1, 10, 49, 53);
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 1, 'LFSurSurveySiteCommunity', 'Community', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'eab663f6-1eb8-4efc-85da-2844ee720020';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 2, 'LFSurSurveySiteSchool', 'School', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'eab663f6-1eb8-4efc-85da-2844ee720020';

-- Story #190
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 13, 'FTS', 'FTS', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'LFSurTestType';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 14, 'FTS', 'FTS', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'LFMapSurTestType';

-- Story #189
UPDATE IndicatorDropdownValues SET SortOrder = 100
	WHERE DropdownValue = 'ICT' AND IndicatorId = (SELECT ID FROM SurveyIndicators WHERE DisplayName = 'TASDiagnosticTest');
UPDATE IndicatorDropdownValues SET SortOrder = 300
	WHERE DropdownValue = 'BrugiaRapid' AND IndicatorId = (SELECT ID FROM SurveyIndicators WHERE DisplayName = 'TASDiagnosticTest');
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 200, 'FTS', 'FTS', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'TASDiagnosticTest';

-- Story #188
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId)
	values (2, 'd807913f-b3a1-4948-a2b3-54eb0800a3bc', 5, 2190, 26, NOW(), 0, 0, -1, 0, 0, 0, 15, 49, 53);

-- Story #200
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 200, 'SurIntegrateLf', 'SurIntegrateLf', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'STHSurTestType';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 300, 'SurNotIntegrateLf', 'SurNotIntegrateLf', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'STHSurTestType';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 200, 'SurIntegrateLf', 'SurIntegrateLf', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'STHMapSurSurTestType';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 3, 300, 'SurNotIntegrateLf', 'SurNotIntegrateLf', 0, 26, NOW, 26, NOW() FROM SurveyIndicators WHERE DisplayName = 'STHMapSurSurTestType';

-- Story #217
DELETE FROM InterventionTypes_to_Diseases WHERE InterventionTypeId = (SELECT ID FROM InterventionTypes WHERE InterventionTypeName = 'LeishIntervention');
DELETE FROM InterventionTypes_to_Indicators WHERE InterventionTypeId = (SELECT ID FROM InterventionTypes WHERE InterventionTypeName = 'LeishIntervention');
DELETE FROM InterventionIndicators WHERE InterventionTypeId = (SELECT ID FROM InterventionTypes WHERE InterventionTypeName = 'LeishIntervention');
DELETE FROM Interventions WHERE InterventionTypeId = (SELECT ID FROM InterventionTypes WHERE InterventionTypeName = 'LeishIntervention');
DELETE FROM InterventionTypes WHERE InterventionTypeName = 'LeishIntervention';

-- Story #216 SQL from Nick story 216
UPDATE ProcessIndicators set AggTypeId=5, RedistrictRuleId=1, MergeRuleId=1, IsRequired=0, SortOrder=25 where displayname = 'LeishTrainPercentageOfHealthFacilitiesHa' and ProcessTypeId=4;

-- Story #214, warning multi-nested queries, but trying to be safe and hard-code as little as possible
DELETE FROM SurveyIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM SurveyIndicators
		WHERE DisplayName = 'NumPopSurveyedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey')
);
DELETE FROM SurveyIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM SurveyIndicators
		WHERE DisplayName = 'NumPopScreenedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey')
);
DELETE FROM SurveyIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM SurveyIndicators
		WHERE DisplayName = 'NumCasesDiagnosedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey')
);

DELETE FROM SurveyIndicators
	WHERE DisplayName = 'NumPopSurveyedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey');
DELETE FROM SurveyIndicators
	WHERE DisplayName = 'NumPopScreenedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey');
DELETE FROM SurveyIndicators
	WHERE DisplayName = 'NumCasesDiagnosedCm' AND SurveyTypeId = (SELECT ID FROM SurveyTypes WHERE SurveyTypeName = 'SurLeishSurvey');

-- Story #214 sql from Nick
UPDATE SurveyIndicators set AggTypeId=5, RedistrictRuleId=49, MergeRuleId=53, IsRequired=0, SortOrder=100000 where displayname = 'Notes' and SurveyTypeId=7;
UPDATE SurveyIndicators set AggTypeId=5, RedistrictRuleId=49, MergeRuleId=53, IsRequired=-1, SortOrder=1 where displayname = 'DateReported' and SurveyTypeId=7;
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, 'e4ece583-91ce-4f0a-baf7-de45831c1135', 5, 2, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, '5f5b7326-b505-4321-90d8-ea69d6464801', 5, 3, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, '08bde675-17ca-4ed4-8dea-af284e15ba3d', 5, 4, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, 'a47f1af8-7399-4915-a541-ded4c2c9d739', 5, 5, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, 'd61a5efa-4b2c-4f72-bf56-edab9025b6f2', 5, 6, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, '0d21bea5-9f29-4973-aa51-d3503bb284ac', 5, 7, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (13, '6aff65b1-ca6f-4bd8-9982-4f0527dd8a99', 5, 8, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (13, '9dd22c4f-8130-4fbc-8251-607f65d3a7b2', 5, 9, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (13, '9ed458a6-2495-4ffb-adc0-1dfd5a2b6397', 5, 10, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (13, '71bec938-836c-467e-8e58-5fab966b71ea', 5, 11, 26, NOW(), 0, 0, 0, 0, 0, 0, 7, 49, 53);

-- Story #214 calculations
UPDATE SurveyIndicators SET IsCalculated = 1
	WHERE DisplayName IN ('6aff65b1-ca6f-4bd8-9982-4f0527dd8a99', '9dd22c4f-8130-4fbc-8251-607f65d3a7b2', '9ed458a6-2495-4ffb-adc0-1dfd5a2b6397', '71bec938-836c-467e-8e58-5fab966b71ea');
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='6aff65b1-ca6f-4bd8-9982-4f0527dd8a99'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='5f5b7326-b505-4321-90d8-ea69d6464801'"), 3);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='6aff65b1-ca6f-4bd8-9982-4f0527dd8a99'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='e4ece583-91ce-4f0a-baf7-de45831c1135'"), 3);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='9dd22c4f-8130-4fbc-8251-607f65d3a7b2'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='08bde675-17ca-4ed4-8dea-af284e15ba3d'"), 3);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='9dd22c4f-8130-4fbc-8251-607f65d3a7b2'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='5f5b7326-b505-4321-90d8-ea69d6464801'"), 3);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='9ed458a6-2495-4ffb-adc0-1dfd5a2b6397'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='d61a5efa-4b2c-4f72-bf56-edab9025b6f2'"), 3);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='9ed458a6-2495-4ffb-adc0-1dfd5a2b6397'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='a47f1af8-7399-4915-a541-ded4c2c9d739'"), 3);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='71bec938-836c-467e-8e58-5fab966b71ea'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='0d21bea5-9f29-4973-aa51-d3503bb284ac'"), 3);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "SurveyIndicators", "DisplayName ='71bec938-836c-467e-8e58-5fab966b71ea'"), 3, DLookup("ID", "SurveyIndicators", "DisplayName ='d61a5efa-4b2c-4f72-bf56-edab9025b6f2'"), 3);

-- Story #211 Leish Annual Intervention
INSERT INTO InterventionTypes (InterventionTypeName, DiseaseType, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
	('LeishAnnualIntervention', 'CM', 26, NOW(), 26, NOW());
INSERT INTO InterventionTypes_to_Diseases (InterventionTypeId, DiseaseId) VALUES
	(DLookup("ID", "InterventionTypes", "InterventionTypeName ='LeishAnnualIntervention'"), DLookup("ID", "Diseases", "DisplayName ='Leishmaniasis'"));

-- Story #211 Leish Annual Intervention indicators
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (4, 'DateReported', 3, 0, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 55, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'DateReported' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvTotalPopulationForThe2NdAdministrativeLevel', 1, 1, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvTotalPopulationForThe2NdAdministrativeLevel' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfVLRelapseCases', 1, 2, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfVLRelapseCases' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfImportedVLCases', 1, 3, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfImportedVLCases' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfCLRelapseCases', 1, 7, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfCLRelapseCases' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfImportedCLCases', 1, 8, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfImportedCLCases' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForVL', 6, 4, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForVL' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForCL', 6, 9, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvMonthsElapsedBetweenOnsetOfSymptomsAndDiagnosisMedianForCL' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months', 1, 5, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months', 1, 6, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months', 1, 10, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months', 1, 11, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (1, 'LeishAnnIntvTypeOfInsecticideUsedForIndoorResidualSpryaing', 4, 12, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 54, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvTypeOfInsecticideUsedForIndoorResidualSpryaing' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishAnnIntvNumberOfInsecticideTreatedBednetsItnsDistributed', 1, 13, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvNumberOfInsecticideTreatedBednetsItnsDistributed' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp', 1, 14, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp', 1, 15, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp', 1, 16, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp' AND InterventionTypeId = 26;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp', 1, 17, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 26);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp' AND InterventionTypeId = 26;

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 26, ID FROM interventionindicators where displayname = 'Notes';

-- Story #211 Leish Annual Interv calculations
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfNewVLCasesCuredOutOfNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfNewCLCasesCuredOutOfNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfVLRelapseCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfVLRelapseCasesOutOfTotalNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfCLRelapseCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvOfCLRelapseCasesOutOfTotalNewCasesFollowedUp'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months'"), 2);

-- Story #213 Leish Monthly Interv
INSERT INTO InterventionTypes (InterventionTypeName, DiseaseType, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES
	('LeishMonthlyIntervention', 'CM', 26, NOW(), 26, NOW());
INSERT INTO InterventionTypes_to_Diseases (InterventionTypeId, DiseaseId) VALUES
	(DLookup("ID", "InterventionTypes", "InterventionTypeName ='LeishMonthlyIntervention'"), DLookup("ID", "Diseases", "DisplayName ='Leishmaniasis'"));

-- Story #213 Leish Monthly Interv indicators
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntCasesActivelyFound', 1, 0, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntCasesActivelyFound' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfLabConfirmedCases', 1, 0, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfLabConfirmedCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvDetectionRatePer100000', 1, 0, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvDetectionRatePer100000' AND InterventionTypeId = 27;
--insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (15, 'LeishMontIntvNotes', 4, 100000, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 54, 27);
--insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNotes' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCasesFoundActively', 1, 2, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCasesFoundActively' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCasesTreatedInReferenceCentres', 1, 13200, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCasesTreatedInReferenceCentres' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases', 1, 17, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases', 1, 5, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfLabConfirmedCLCases', 1, 31, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfLabConfirmedCLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (4, 'DateReported', 3, 0, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 55, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'DateReported' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvTotalNumberOfCasesTreated', 1, 1, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 0, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvTotalNumberOfCasesTreated' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical', 1, 3, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL', 1, 4, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests', 1, 6, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT', 1, 7, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology', 1, 8, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVLCasesDiagnosedByDirectExamParasitology', 1, 9, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVLCasesDiagnosedByDirectExamParasitology' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedActivelyForVL', 1, 10, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedActivelyForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedPassivelyForVL', 1, 11, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVisceralLeishmaniasisHIVCoInfection', 1, 12, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVisceralLeishmaniasisHIVCoInfection' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLFemaleCases', 1, 13, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLFemaleCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years', 1, 14, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLCasesInChildren5To14Years', 1, 15, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years', 1, 16, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical', 1, 18, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology', 1, 19, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLCasesDiagnosedByDirectExamParasitology', 1, 20, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLCasesDiagnosedByDirectExamParasitology' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedActivelyForCL', 1, 21, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedActivelyForCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedPassivelyForCL', 1, 22, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLFemaleCases', 1, 23, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLFemaleCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years', 1, 24, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLCasesInChildren5To14Years', 1, 25, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years', 1, 26, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm', 1, 27, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions', 1, 28, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars', 1, 29, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment', 1, 30, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed', 1, 32, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL', 1, 33, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL', 1, 34, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewPKDLFemaleCases', 1, 35, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewPKDLFemaleCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years', 1, 36, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years', 1, 37, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years', 1, 38, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewPKDLCasesHIVCoInfection', 1, 39, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewPKDLCasesHIVCoInfection' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed', 1, 40, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewMCLFemaleCases', 1, 41, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewMCLFemaleCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years', 1, 42, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewMCLCasesInChildren5To14Years', 1, 43, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewMCLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years', 1, 44, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLCasesTreated', 1, 45, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLCasesTreated' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewVLCasesWithInitialCure', 1, 46, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewVLCasesWithInitialCure' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfFailureCasesVL', 1, 47, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfFailureCasesVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfDeathsForNewVLCases', 1, 48, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfDeathsForNewVLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfVLCasesWithSeriousAdverseEvents', 1, 49, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfVLCasesWithSeriousAdverseEvents' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLCasesTreated', 1, 50, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLCasesTreated' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfNewCLCasesWithInitialCure', 1, 51, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfNewCLCasesWithInitialCure' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfFailureCasesCL', 1, 52, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfFailureCasesCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfDeathsForNewCLCases', 1, 53, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfDeathsForNewCLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfCLCasesWithSeriousAdverseEvents', 1, 54, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfCLCasesWithSeriousAdverseEvents' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheBeginningOfTheMonth', 1, 55, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheBeginningOfTheMonth' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (2, 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheEndOfTheMonth', 1, 56, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 2, 57, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheEndOfTheMonth' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases', 1, 57, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases', 1, 58, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfVLCasesDiagnosedByRDT', 1, 59, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfVLCasesDiagnosedByRDT' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfPostitiveRDT', 1, 60, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfPostitiveRDT' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases', 1, 61, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases', 1, 62, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples', 1, 63, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples', 1, 64, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL', 1, 65, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL', 1, 66, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL', 1, 69, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL', 1, 70, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntCaseFatalityRateForVL', 1, 73, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntCaseFatalityRateForVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntCaseFatalityRateForCL', 1, 74, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntCaseFatalityRateForCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm', 1, 75, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions', 1, 76, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars', 1, 77, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment', 1, 78, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvVLIncidenceRate10000PeopleYear', 1, 79, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvVLIncidenceRate10000PeopleYear' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvCLIncidenceRate10000PeopleYear', 1, 80, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvCLIncidenceRate10000PeopleYear' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntFemaleVL', 1, 81, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntFemaleVL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntFemaleCL', 1, 82, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntFemaleCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntFemalePKDL', 1, 83, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntFemalePKDL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntFemaleMCL', 1, 84, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntFemaleMCL' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years', 1, 85, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years', 1, 86, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years', 1, 87, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years', 1, 88, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years', 1, 89, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years', 1, 90, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years', 1, 91, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years', 1, 92, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years', 1, 93, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years', 1, 94, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years', 1, 95, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years', 1, 96, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years' AND InterventionTypeId = 27;
insert into InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, RedistrictRuleId, MergeRuleId, InterventionTypeId) values (13, 'LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits', 1, 97, 26, NOW(), 0, 0, 0, 0, -1, 0, 0, 1, 1, 27);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits' AND InterventionTypeId = 27;

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 27, ID FROM interventionindicators where displayname = 'Notes';

-- Story 213 Leish Monthly interv calcs
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvDetectionRatePer100000'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvDetectionRatePer100000'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfLabConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedCLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfLabConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfLabConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfLabConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfLabConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCasesFoundActively'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForVL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForVL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForCL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForCL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCasesActivelyFound'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVisceralLeishmaniasisHIVCoInfection'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLHIVCoInfectedCasesOfTheTotalNewVLCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesHIVCoInfection'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfPKDLHIVCoInfectedCasesOfTheTotalNewPKDLCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLCasesDiagnosedByRDT'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLCasesDiagnosedByRDT'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfPostitiveRDT'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfPostitiveRDT'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfVLParasitologicallyConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedCLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLParasitologicallyConfirmedCases'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfParasitologicallyConfirmedVLSamples'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfLabConfirmedCLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfParasitologicallyConfirmedCLSamples'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesWithInitialCure'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesWithInitialCure'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfInitialCuredCasesOutOfTotalNewCasesTreatedForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfFailureCasesVL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfFailureCasesCL'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfFailureCasesOutOfTotalNewCasesTreatedForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCaseFatalityRateForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfDeathsForNewVLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCaseFatalityRateForVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCaseFatalityRateForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfDeathsForNewCLCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntCaseFatalityRateForCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithGrtrThnOrEqlTo4Lesions'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsWithLesionsOnFaceEars'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfCLPatientsUndergoingSystemicTreatment'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesTreated'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvVLIncidenceRate10000PeopleYear'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvCLIncidenceRate10000PeopleYear'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLFemaleCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleVL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLFemaleCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemalePKDL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewPKDLFemaleCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemalePKDL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleMCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewMCLFemaleCases'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntFemaleMCL'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesInChildren5To14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewVLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesInChildren5To14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewCLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewPKDLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInChildrenLssThn5Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInChildren5To14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInChildren5To14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvPrcntOfNewMCLCasesInAdultsGrtrThn14Years'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed'"), 2);

INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheBeginningOfTheMonth'"), 2);
INSERT INTO IndicatorCalculations (IndicatorId, EntityTypeId, RelatedIndicatorId, RelatedEntityTypeId) VALUES
	(DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvMonthlyConsumptionRate1StLineTreatmentUnits'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName = 'LeishMontIntvNumberOfUnitsVialsFor1StLineTreatmentAtTheEndOfTheMonth'"), 2);

-- Story 223
ALTER TABLE CountryDemography ADD GrossDomesticProduct NUMBER;
ALTER TABLE CountryDemography ADD CountryIncomeStatus TEXT(64);
ALTER TABLE CountryDemography ADD LifeExpectBirthFemale NUMBER;
ALTER TABLE CountryDemography ADD LifeExpectBirthMale NUMBER;

-- Story 215 Remove previous indicators - warning multi-nested queries, but trying to be safe and hard-code as little as possible
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'EndemicityStatus' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'TotalNumNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'TotalNumVlCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'TotalNumClCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'TotalNumChildNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'TotalNumFemaleNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'PercentNewChildren' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);
DELETE FROM DiseaseDistributionIndicatorValues
	WHERE IndicatorId = (
		SELECT ID FROM DiseaseDistributionIndicators
		WHERE DisplayName = 'PercentNewFemales' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
);

DELETE FROM IndicatorCalculations WHERE
	EntityTypeId = 1 AND
	(
		IndicatorId IN (
			SELECT ID FROM DiseaseDistributionIndicators WHERE DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
			AND DisplayName IN ('EndemicityStatus', 'TotalNumNewCases', 'TotalNumVlCases', 'TotalNumClCases', 'TotalNumChildNewCases',
			'TotalNumFemaleNewCases', 'PercentNewChildren', 'PercentNewFemales'
			)
		)
		OR RelatedIndicatorId IN (
			SELECT ID FROM DiseaseDistributionIndicators WHERE DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis')
			AND DisplayName IN ('EndemicityStatus', 'TotalNumNewCases', 'TotalNumVlCases', 'TotalNumClCases', 'TotalNumChildNewCases',
			'TotalNumFemaleNewCases', 'PercentNewChildren', 'PercentNewFemales'
			)
		)
	);

DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'EndemicityStatus' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'TotalNumNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'TotalNumVlCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'TotalNumClCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'TotalNumChildNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'TotalNumFemaleNewCases' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'PercentNewChildren' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');
DELETE FROM DiseaseDistributionIndicators
	WHERE DisplayName = 'PercentNewFemales' AND DiseaseId = (SELECT ID FROM Diseases WHERE DisplayName = 'Leishmaniasis');

-- Story 215 Add new indicators
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistEndemicityStatusVL', 3, 1, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 2, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistEndemicityStatusCL', 3, 2, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 2, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistEndemicityStatusPKDL', 3, 3, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 9, 2, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistEndemicityStatusMCL', 3, 4, 26, NOW(), 0, 0, 0, 0, 0, 0, 0, 9, 2, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistWasThereAnyVLOutbreakThisYear', 3, 5, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 49, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (5, 'LeishDiseaseDistWasThereAnyCLOutbreakThisYear', 3, 6, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 49, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (2, 'LeishDiseaseDistNumberOfNewVLFociThisYearAreasReportingCasesForTheFirstTime', 1, 9, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 49, 58);
insert into DiseaseDistributionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId, RedistrictRuleId, MergeRuleId) values (2, 'LeishDiseaseDistNumberOfNewCLFociThisYearAreasReportingCasesForTheFirstTime', 1, 10, 26, NOW(), 0, 0, -1, 0, 0, 0, 0, 9, 49, 58);

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Endemic', 'Endemic', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusVL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'NotEndemic', 'NotEndemic', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusVL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'UnknownEndemicity', 'UnknownEndemicity', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusVL';

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Endemic', 'Endemic', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusCL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'NotEndemic', 'NotEndemic', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusCL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'UnknownEndemicity', 'UnknownEndemicity', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusCL';

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Endemic', 'Endemic', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusPKDL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'NotEndemic', 'NotEndemic', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusPKDL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'UnknownEndemicity', 'UnknownEndemicity', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusPKDL';

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Endemic', 'Endemic', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusMCL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'NotEndemic', 'NotEndemic', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusMCL';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'UnknownEndemicity', 'UnknownEndemicity', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistEndemicityStatusMCL';

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Yes', 'Yes', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyVLOutbreakThisYear';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'No', 'No', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyVLOutbreakThisYear';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'Unknown', 'Unknown', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyVLOutbreakThisYear';

INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 1, 'Yes', 'Yes', 20, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyCLOutbreakThisYear';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 2, 'No', 'No', 0, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyCLOutbreakThisYear';
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt)
	SELECT ID, 1, 3, 'Unknown', 'Unknown', 10, 26, NOW, 26, NOW() FROM DiseaseDistributionIndicators WHERE DisplayName = 'LeishDiseaseDistWasThereAnyCLOutbreakThisYear';

-- Story 130
ALTER TABLE SentinelSites ADD IsDeleted YesNo;

-- Story 100
ALTER TABLE AdminLevelDemography ALTER COLUMN TotalPopulation INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN AdultPopulation INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN Pop0Month INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN PopPsac INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN PopSac INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN Pop5yo INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN PopAdult INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN PopFemale INTEGER;
ALTER TABLE AdminLevelDemography ALTER COLUMN PopMale INTEGER;

INSERT INTO IndicatorDataTypes (DataType) VALUES ('Integer');

UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDLFHighRiskAdultsAtRisk' and DiseaseId = 3;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDLFPopulationAtRisk' and DiseaseId = 3;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDLFPopulationLivingInTheDistrictsThatAc' and DiseaseId = 3;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDLFPopulationRequiringPc' and DiseaseId = 3;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDOnchoPopulationAtRisk' and DiseaseId = 4;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDOnchoPopulationLivingInTheDistrictsTha' and DiseaseId = 4;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDOnchoPopulationRequiringPc' and DiseaseId = 4;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDOnchoTotalNumEndemicVillagesCommuniti' and DiseaseId = 4;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDOnchoTotalNumOfVillagesCommunitiesInT' and DiseaseId = 4;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSchistoHighRiskAdultsAtRisk' and DiseaseId = 12;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSchistoPopulationAtRisk' and DiseaseId = 12;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSchistoPopulationLivingInTheDistrictsT' and DiseaseId = 12;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSchistoPopulationRequiringPc' and DiseaseId = 12;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSchistoSacAtRisk' and DiseaseId = 12;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHHighRiskAdultsAtRisk' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHPopulationAtRisk' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHPopulationLivingInTheDistrictsThatA' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHPopulationRequiringPc' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHPsacAtRisk' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDSTHSacAtRisk' and DiseaseId = 5;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDTraPopulationAtRisk' and DiseaseId = 13;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDTraPopulationLivingInAreasDistrict' and DiseaseId = 13;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'DDTraPopulationLivingInAreasSubDistrict' and DiseaseId = 13;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumChildNewCases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumFemaleNewCases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumNewCases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCasesConfirmedPcr' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCat1Cases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCat2Cases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCat3Cases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalUlcerativeCases' and DiseaseId = 10;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalHatConfirmedLab' and DiseaseId = 8;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumHat' and DiseaseId = 8;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumHatChild' and DiseaseId = 8;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumHatFemale' and DiseaseId = 8;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumHat2' and DiseaseId = 8;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'MbCasesRegisteredMdtBeginning' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'PrevalenceBeginningYear' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'MbCasesRegisteredMdtEnd' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumChildNewCases' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumFemaleNewCases' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumMbCases' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumNewCases' and DiseaseId = 7;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'CasesFromRural' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCasesConfirmedLab' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'PreSchoolCases6' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'SchoolCases14' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCasesContacted' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumChildNewCases' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalCasesTargeted' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumFemaleNewCases' and DiseaseId = 11;
UPDATE DiseaseDistributionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumNewCases' and DiseaseId = 11;

UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumPsacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvPsacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumPsacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvPsacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumPsacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvPsacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalFemalesTargetedOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalFemalesOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalMalesTargetedOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalMalesOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalTargetedForOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalTreatedForOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalFemalesTargetedOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalFemalesOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalMalesTargetedOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalMalesOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalTargetedForOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvOfTotalTreatedForOncho';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LFMMDPNumHydroceleCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LFMMDPNumHydroceleCasesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LFMMDPNumLymphoedemaPatients';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LFMMDPNumLymphoedemaPatientsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumPsacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvPsacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumAdultsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvFemaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'IntvMaleSac';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSacTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvTsNumFemales';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvTsNumMales';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvTsNumTt';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvTsNumOperated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleFemalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleIndividualsTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumEligibleMalesTargeted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumFemalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumIndividualsTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumMalesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumTreatedTeo';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumTreatedZxPos';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumTreatedZx';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PcIntvNumSeriousAdverseEventsReported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'AdultsAtEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'ChildrenAtEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVolunteersBu';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumFacilitiesProvidingBu';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesReadmitted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumAffectedBuSelfCare';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumBuRehab';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumBuSurgery';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumRelapsesCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'OtherWithdrawl';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PatientsCuredWoDisability';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PatientsCompletedTreatment';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PatientsFullyScared';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumCasesCat3';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'TotalNumCasesWoDisability';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'CasesRegisteredYear';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'CasesTreatedBu';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumPatientsComplementedBu';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'TotalTreatedWithPrevention';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'TotalWithdrawalTreatment';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVillagesSafeWater';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesLost';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesContained';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumClinical';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumEndemicAbate';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumEndemicVillages';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumEndemicVillageWater';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumEndemicVillagesReporting';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumIdsrReporting';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumIdsr';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumIndigenous';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumLab';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumRumorsInvestigated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumRumors';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVas';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'VasReporting';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVillageWithImported';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTGambTRhod';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesCured';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumProspection';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTreatedRef';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTreatedNect';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesSaes';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumClinicalCasesHat';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumDeaths';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumLabCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTGamb';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTRhod';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTreatmentFailures';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfCLRelapseCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfImportedCLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfImportedVLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfNewCLCasesCuredAfterFollowUpOfAtLeast6Months';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfNewCLCasesFollowedUpAtLeast6Months';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfNewVLCasesCuredAfterFollowUpOfAtLeast6Months';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfNewVLCasesFollowedUpAtLeast6Months';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvNumberOfVLRelapseCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishAnnIntvTotalPopulationForThe2NdAdministrativeLevel';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCasesDiagnosedClinicallyForVL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCasesFoundActively';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCasesTreatedInReferenceCentres';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLCasesDiagnosedByDirectExamParasitology';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLCasesTestedByDirectExamParasitology';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLCasesWithSeriousAdverseEvents';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLPatientsUndergoingSystemicTreatment';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLPatientsWithGrtrThnOrEqlTo4Lesions';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLPatientsWithLesionsGrtrThnOrEqlTo4Cm';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfCLPatientsWithLesionsOnFaceOrEars';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfClinicalCutaneousLeishmaniasisCLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfDeathsForNewCLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfDeathsForNewVLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfFailureCasesCL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfFailureCasesVL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfLabConfirmedVisceralLeishmaniasisVLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfLabConfirmedCLCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLCasesInAdultsGrtrThn14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLCasesInChildrenLssThn5Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLCasesInChildren5To14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLCasesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLCasesWithInitialCure';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewCLFemaleCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInAdultsGrtrThn14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInChildrenLssThn5Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewMCLCasesInChildren5To14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewMCLFemaleCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInAdultsGrtrThn14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInChildrenLssThn5Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesInChildren5To14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewPKDLCasesHIVCoInfection';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewPKDLFemaleCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLCasesInAdultsGrtrThn14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLCasesInChildrenLssThn5Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLCasesInChildren5To14Years';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLCasesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLCasesWithInitialCure';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfNewVLFemaleCases';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForCL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForPKDL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedActivelyForVL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForCL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForPKDL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfPeopleScreenedPassivelyForVL';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfVLCasesDiagnosedByAPositiveRapidDiagnosticTestsRDT';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfVLCasesDiagnosedByDirectExamParasitology';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfVLCasesTestedByDirectExamParasitology';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfVLCasesWithSeriousAdverseEvents';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvNumberOfVLSuspectsTestedWithRapidDiagnosticTests';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvTotalNumberOfCasesTreated';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvTotalNumberOfNewCLCasesDiagnosedLabAndClinical';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvTotalNumberOfNewMCLCasesDiagnosed';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvTotalNumberOfNewPKDLCasesDiagnosed';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'LeishMontIntvTotalNumberOfNewVLCasesDiagnosedLabAndClinical';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'MbAdultsEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'MbChildEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumFirstLine';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumPatientsFootwear';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumPatientsType1';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumPatientsType2';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumLeprosySelfCare';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumLeprosyRehab';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumLeprosySurgery';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumRelapses';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'CasesReadmitted';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PatientsCuredMb';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PatientsCuredPb';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PbAdultEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'PbChildEnd';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumGrade2';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'TotalWithdrawal';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesCuredYaws';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesCuredYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumFullyHealed';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCasesTreatedYaws';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumTreatedAzithro';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVolunteersYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumContactsTreatedYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumFacilitiesProvidingAntibiotics';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumCentersTreatingYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumIneffectiveYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumSaesYw';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumVillagesWithVolunteers';
UPDATE InterventionIndicators set DataTypeId = 19 where DisplayName = 'NumPersonsBenza';

UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'TrainingNumFemales' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'PCTrainNumIndividualsTargeted' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'PCTrainNumIndividualsTrainedFemales' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'PCTrainNumIndividualsTrainedMales' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'PCTrainNumIndividualsTrainedTotal' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'TrainingNumMales' and ProcessTypeId = 7;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'BUTrainNumberOfParticipantsToTrainingSes' and ProcessTypeId = 5;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'GWTrainNumberOfParticipantsToTrainingSessions' and ProcessTypeId = 1;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'HATTrainNumberOfParticipantsToHatTrainin' and ProcessTypeId = 3;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'LeishTrainNumberOfParticipantsToTraining' and ProcessTypeId = 4;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'LeprosyTrainNumberOfParticipantsToTraini' and ProcessTypeId = 2;
UPDATE ProcessIndicators set DataTypeId = 19 where DisplayName = 'YawsTrainNumberOfParticipantsToTrainingS' and ProcessTypeId = 6;

UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfFemalesExamined' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfFemalesPositive' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurExaminedHydro1' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurExaminedLympho' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfIndividualsExamined' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfCasesOfHydrocele' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfCasesOfLymphoedema' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfIndividualsPositive' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfIndividualsWithNonRespon' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfMalesExamined' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurNumberOfMalesPositive' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFMapSurTargetSampleSize' and SurveyTypeId = 16;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurExaminedHydro' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurExaminedLympho' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurNumberOfIndividualsExamined' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurPosHydro' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurPosLympho' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurNumberOfIndividualsPositive' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurNumberOfIndividualsWithNon' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'LFSurTargetSampleSize' and SurveyTypeId = 10;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASActualNumberOfSchoolsOrEasSurveyed' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASActualSampleSizeNegative' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASActualSampleSizePositive' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TasTotal6yoInCommunities' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TasTotalAreasInCommunities' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TasTotalNumChildrenInGrades' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TasTotalNumPrimarySchools' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TasNumNoTreatment' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASTargetNumberOfSchoolsOrEas' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASTargetSampleSize' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TASTotalEuPopulation' and SurveyTypeId = 15;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurIfTestTypeIsNpNumDep' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurIfTestTypeIsNpNumWri' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurIfTestTypeIsNpNumNod' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurNumberOfIndividualsExamined' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurNumberOfIndividualsPositive' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurNumRefused' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurRegistrationPopulation' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurReportedPopulation' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurTargetSampleSize' and SurveyTypeId = 13;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurNoFlies' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurFliesDissected' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurInfectedFlies' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurInfectiveFlies' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurInfectiveLarvae' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurParousFlies' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurParousFliesDisected' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoSurParousFliesInfected' and SurveyTypeId = 21;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurIfTestTypeIsNpNumDep' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurIfTestTypeIsNpNumWri' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurIfTestTypeIsNpNumNod' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurNumberOfIndividualsExamined' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurNumberOfIndividualsPositive' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurRegistrationPopulation' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurReportedPopulation' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'OnchoMapSurTargetSampleSize' and SurveyTypeId = 19;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurProportionOfHeavyIntensityIntes' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurProportionOfHeavyIntensityUrina' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumberOfIndividualsExaminedForI' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumberOfIndividualsExaminedForU' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumberOfIndividualsPositiveForH' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumberOfIndividualsPositiveForI' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumPosSchParasite' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurNumberOfIndividualsWithNonRespo' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurProportionOfModerateIntensityIn' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurProportionOfModerateIntensityUr' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHMapSurTargetSampleSize' and SurveyTypeId = 17;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurProportionOfHeavyIntensityIntestin' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurProportionOfHeavyIntensityUrinaryS' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumberOfIndividualsExaminedForInte' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumberOfIndividualsExaminedForUrin' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumberOfIndividualsPositiveForHaem' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumberOfIndividualsPositiveForInte' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumPosSchParasite' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurNumberOfIndividualsWithNonResponse' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurProportionOfModerateIntensityIntes' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurProportionOfModerateIntensityUrina' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'SCHSurTargetSampleSize' and SurveyTypeId = 11;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfHeavyIntensityOfInAS' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfHeavyIntensityOfInfecti' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfHeavyIntensityOfInHook' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsExaminedAscaris' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsExaminedHookwor' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsExaminedOverall' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsExaminedTrichur' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsPositiveAscaris' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsPositiveHookwor' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsPositiveOverall' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsPositiveTrichur' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurNumberOfIndividualsWithNonResponse' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfModerateIntensityOfInAS' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfModerateIntensityOfInfe' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurProportionOfModerateIntensityOfInHook' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHMapSurSurTargetSampleSize' and SurveyTypeId = 18;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfHeavyIntensityOfAsc' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfHeavyIntensityOfTri' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfHeavyIntensityHook' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsExaminedAscaris' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsExaminedHookwor' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsExaminedOverall' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsExaminedTrichur' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsPositiveAscaris' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsPositiveHookwor' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsPositiveOverall' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsPositiveTrichur' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurNumberOfIndividualsWithNonResponse' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfModerateIntensityAsc' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfModerateIntensityTri' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurProportionOfModerateIntensityHook' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'STHSurTargetSampleSize' and SurveyTypeId = 12;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSur15yo' and SurveyTypeId = 14;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSur1to9yo' and SurveyTypeId = 14;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSurNoClusters' and SurveyTypeId = 14;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSurSurgeryNumTt' and SurveyTypeId = 14;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSur15yo' and SurveyTypeId = 20;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSur1to9yo' and SurveyTypeId = 20;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSurNoClusters' and SurveyTypeId = 20;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TraMapSurSurgeryNumTt' and SurveyTypeId = 20;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesDiagnosedCm' and SurveyTypeId = 8;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesPcrCm' and SurveyTypeId = 8;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopScreenedCm' and SurveyTypeId = 8;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopSurveyedCm' and SurveyTypeId = 8;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesFoundCm' and SurveyTypeId = 4;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopScreenedCm' and SurveyTypeId = 4;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopSurveyedCm' and SurveyTypeId = 4;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesDiagnosedCm' and SurveyTypeId = 6;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopScreenedCm' and SurveyTypeId = 6;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopSurveyedCm' and SurveyTypeId = 6;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = '0d21bea5-9f29-4973-aa51-d3503bb284ac' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'd61a5efa-4b2c-4f72-bf56-edab9025b6f2' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'a47f1af8-7399-4915-a541-ded4c2c9d739' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = '08bde675-17ca-4ed4-8dea-af284e15ba3d' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = '5f5b7326-b505-4321-90d8-ea69d6464801' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'e4ece583-91ce-4f0a-baf7-de45831c1135' and SurveyTypeId = 7;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesLeprosyCm' and SurveyTypeId = 5;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesDiagnosedCm' and SurveyTypeId = 5;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopScreenedCm' and SurveyTypeId = 5;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumPopSurveyedCm' and SurveyTypeId = 5;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesContactsTreatedCm' and SurveyTypeId = 9;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumCasesDiagnosedCm' and SurveyTypeId = 9;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumSchoolsScreenedCm' and SurveyTypeId = 9;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'NumVillagesScreenedCm' and SurveyTypeId = 9;
UPDATE SurveyIndicators set DataTypeId = 19 where DisplayName = 'TotalNumPeopleScreenedCm' and SurveyTypeId = 9;


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