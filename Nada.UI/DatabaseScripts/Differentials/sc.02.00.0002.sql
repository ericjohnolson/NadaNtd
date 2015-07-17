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