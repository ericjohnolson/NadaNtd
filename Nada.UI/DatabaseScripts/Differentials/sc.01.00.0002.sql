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