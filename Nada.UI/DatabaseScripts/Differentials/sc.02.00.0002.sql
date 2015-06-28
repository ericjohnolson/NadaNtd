-- Add indicator num rounds of pc... story # 198;
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, '4190984d-f272-4359-8414-6e7ef06fc4bc', 5, 250, 26, NOW(), 0, 0, 0, 0, 0, 0, 15, 2, 54);

-- Story #199 Critical decision dropdowns
DELETE FROM IndicatorDropdownValues WHERE ID IN (616, 617);
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (566, 3, 1, 'TASCutoffStopMDA', 'TASCutoffStopMDA', 0, 26, NOW, 26, NOW());
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (566, 3, 2, 'TASCutoffContinueMDA', 'TASCutoffContinueMDA', 0, 26, NOW, 26, NOW());
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (566, 3, 3, 'TASCutoffContinueSurveillance', 'TASCutoffContinueSurveillance', 0, 26, NOW, 26, NOW());

-- Story #191
INSERT INTO SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (5, 'eab663f6-1eb8-4efc-85da-2844ee720020', 5, 610, 26, NOW(), 0, 0, 0, 0, 0, 1, 10, 49, 53);
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (594, 3, 1, 'LFSurSurveySiteCommunity', 'Community', 0, 26, NOW, 26, NOW());
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (594, 3, 2, 'LFSurSurveySiteSchool', 'School', 0, 26, NOW, 26, NOW());

-- Story #190
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (106, 3, 13, 'FTS', 'FTS', 0, 26, NOW, 26, NOW());
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (279, 3, 14, 'FTS', 'FTS', 0, 26, NOW, 26, NOW());

-- Story #189
UPDATE IndicatorDropdownValues SET SortOrder = 100 WHERE ID = 327;
UPDATE IndicatorDropdownValues SET SortOrder = 300 WHERE ID = 328;
INSERT INTO IndicatorDropdownValues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) VALUES (245, 3, 200, 'FTS', 'FTS', 0, 26, NOW, 26, NOW());

-- Story #188
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, 'd807913f-b3a1-4948-a2b3-54eb0800a3bc', 5, 2190, 26, NOW(), 0, 0, -1, 0, 0, 0, 15, 49, 53);

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