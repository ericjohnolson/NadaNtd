-- New low priority indicators NOT YET RAN, waiting for JENNIFER RESPONSE. Also need to test!;

INSERT INTO InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, 
CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (6, 'PcIntvModeDistribution',4,5050, 26, NOW(), 0, 0, 0, 0,0,-1,2,54);

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,2,1,'PcIntvModeDistributionCommunity','PcIntvModeDistributionCommunity',10,26,Now(),26,Now() from InterventionIndicators where displayname = 'PcIntvModeDistribution';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,2,2,'PcIntvModeDistributionSchool','PcIntvModeDistributionSchool',20,26,Now(),26,Now() from InterventionIndicators where displayname = 'PcIntvModeDistribution';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,2,3,'PcIntvModeDistributionFixed','PcIntvModeDistributionFixed',30,26,Now(),26,Now() from InterventionIndicators where displayname = 'PcIntvModeDistribution';

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvModeDistribution'; 

INSERT INTO InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, 
CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (1, 'PcIntvBatchRefNum',4,21050, 26, NOW(), 0, 0, 0, 0,0,0,2,54);

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvBatchRefNum'; 

INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (13, 2, 'OnchoSurNumRefused',5,250, 26, NOW(), 0, 0, 0, 0,0,0,2,57);

INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (11, 5, 'SchSentinenlIsPartOfCohort',5,250, 26, NOW(), 0, 0, 0, 0,0,0,2,51);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Yes','Yes',10,26,Now(),26,Now() from InterventionIndicators where displayname = 'SchSentinenlIsPartOfCohort';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'No','No',20,26,Now(),26,Now() from InterventionIndicators where displayname = 'SchSentinenlIsPartOfCohort';

INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (12, 5, 'SthSentinelIsPartOfCohort',5,250, 26, NOW(), 0, 0, 0, 0,0,0,2,51);
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Yes','Yes',10,26,Now(),26,Now() from InterventionIndicators where displayname = 'SthSentinelIsPartOfCohort';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'No','No',20,26,Now(),26,Now() from InterventionIndicators where displayname = 'SthSentinelIsPartOfCohort';

INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (11, 13, 'SchSentinelPrevalenceEggsInUrine',5,250, 26, NOW(), 0, 0, 0, 0,-1,0,2,54);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (17, 13, 'SchMappingPrevalenceEggsInUrine',5,250, 26, NOW(), 0, 0, 0, 0,-1,0,2,54);
