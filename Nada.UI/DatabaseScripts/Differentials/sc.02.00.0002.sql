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