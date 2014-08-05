INSERT INTO DiseaseDistributionIndicators (DiseaseId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, 
IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (4, 5, 'DDPTS',3,113, 26, NOW(), 0, 0, 0, 0,0,0,2,58);

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,1,'Yes','Yes',10,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDPTS';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,1,2,'No','No',20,26,Now(),26,Now() from DiseaseDistributionIndicators where displayname = 'DDPTS';

INSERT INTO InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, 
CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (2, 'IntvFemaleSac',1,19100, 26, NOW(), 0, 0, 0, 0,0,0,0,57);

INSERT INTO InterventionIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, 
CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (2, 'IntvMaleSac',1,19200, 26, NOW(), 0, 0, 0, 0,0,0,0,57);

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'IntvFemaleSac'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'IntvMaleSac'; 

INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (15, 2, 'TasNumNoTreatment',5,250, 26, NOW(), 0, 0, 0, 0,0,0,2,54);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (15, 2, 'TasActualNonResponse',5,2825, 26, NOW(), 0, 0, 0, 0,0,0,2,54);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (17, 6, 'SchMappingTypeOfSite',4,55, 26, NOW(), 0, 0, 0, 0,0,-1,2,54);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (11, 4, 'SchSentinelDatePc',5,25, 26, NOW(), 0, 0, 0, 0,0,0,49,53);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (11, 6, 'SchSentinelTypeSite',4,45, 26, NOW(), 0, 0, 0, 0,0,-1,2,54);

UPDATE SurveyIndicators SET SortOrder = SortOrder * 10 WHERE SurveyTypeId = 18;
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (18, 6, 'SthMappingTypeSite',4,55, 26, NOW(), 0, 0, 0, 0,0,-1,2,54);

UPDATE SurveyIndicators SET SortOrder = SortOrder * 10 WHERE SurveyTypeId = 12;
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (12, 4, 'SthSpotCheckDatePc',5,15, 26, NOW(), 0, 0, 0, 0,0,0,49,53);
INSERT INTO SurveyIndicators (SurveyTypeId, DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, RedistrictRuleId, MergeRuleId) VALUES (12, 6, 'SthSpotCheckTypeSite',4,45, 26, NOW(), 0, 0, 0, 0,0,-1,2,54);


insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Community','Community',10,26,Now(),26,Now() from SurveyIndicators where displayname = 'SchMappingTypeOfSite';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'School','School',20,26,Now(),26,Now() from SurveyIndicators where displayname = 'SchMappingTypeOfSite';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Community','Community',10,26,Now(),26,Now() from SurveyIndicators where displayname = 'SchSentinelTypeSite';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'School','School',20,26,Now(),26,Now() from SurveyIndicators where displayname = 'SchSentinelTypeSite';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Community','Community',10,26,Now(),26,Now() from SurveyIndicators where displayname = 'SthMappingTypeSite';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'School','School',20,26,Now(),26,Now() from SurveyIndicators where displayname = 'SthMappingTypeSite';
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,1,'Community','Community',10,26,Now(),26,Now() from SurveyIndicators where displayname = 'SthSpotCheckTypeSite';

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,3,2,'School','School',20,26,Now(),26,Now() from SurveyIndicators where displayname = 'SthSpotCheckTypeSite';


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0013'
       ,'sc.01.00.0013.sql'
       ,Now());