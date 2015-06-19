-- Add indicator num rounds of pc... story # 198;
insert into SurveyIndicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, SurveyTypeId, RedistrictRuleId, MergeRuleId) values (2, '4190984d-f272-4359-8414-6e7ef06fc4bc', 5, 250, 26, NOW(), 0, 0, 0, 0, 0, 0, 15, 2, 54);


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('02'
       ,'00'
       ,'0001'
       ,'sc.02.00.0002.sql'
       ,Now());