INSERT INTO [Medicines] (DisplayName) Values ('Database updates are working');

UPDATE SurveyIndicators SET DataTypeId = 1 Where ID = 266;
update indicatordropdownvalues set indicatorid = 139 where id = 421;
update indicatordropdownvalues set indicatorid = 139 where id = 422;
update indicatordropdownvalues set indicatorid = 143 where id = 423;
update indicatordropdownvalues set indicatorid = 143 where id = 424;
update indicatordropdownvalues set indicatorid = 146 where indicatorid = 145 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 158 where indicatorid = 157 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 157 where indicatorid = 156 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 163 where indicatorid = 162 and entitytype = 4;
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
values (164,4,30,'SaeYes','SaeYes',0,26,Now(),26,Now());
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
values (164,4,31,'SaeNo','SaeNo',0,26,Now(),26,Now());
insert into indicatordropdownvalues (IndicatorId, EntityType, SortOrder, DropdownValue, TranslationKey, WeightedValue, UpdatedById, UpdatedAt, CreatedById, CreatedAt) 
values (164,4,32,'SaeUnknown','SaeUnknown',0,26,Now(),26,Now());
update indicatordropdownvalues set indicatorid = 169 where indicatorid = 168 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 184 where indicatorid = 183 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 189 where indicatorid = 188 and entitytype = 4;
update indicatordropdownvalues set indicatorid = 191 where indicatorid = 190 and entitytype = 4;
update surveyindicators set isdisabled=-1 where id in (121, 122, 305, 304);
update indicatordropdownvalues set dropdownvalue = 'ScmDrpAlb', translationkey = 'ScmDrpAlb', sortorder=1 where id = 425;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpAzirtho', translationkey = 'ScmDrpAzirtho', sortorder=2 where id = 426;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpDec', translationkey = 'ScmDrpDec', sortorder=4 where id = 427;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpIvm', translationkey = 'ScmDrpIvm', sortorder=5 where id = 428;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpMdb', translationkey = 'ScmDrpMdb', sortorder=6 where id = 429;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpPzq', translationkey = 'ScmDrpPzq', sortorder=7 where id = 430;
update indicatordropdownvalues set dropdownvalue = 'ScmDrpTeo', translationkey = 'ScmDrpTeo', sortorder=8 where id = 431;
insert into indicatordropdownvalues (	IndicatorId,	EntityType,	SortOrder,	DropdownValue,	TranslationKey,	WeightedValue,	UpdatedById,	UpdatedAt,	CreatedById	,CreatedAt) values
(146, 4, 3, 'ScmDrpAzPos', 'ScmDrpAzPos', 0, 26, Now(), 26, Now());


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0001'
       ,'sc.01.00.0001.sql'
       ,Now());