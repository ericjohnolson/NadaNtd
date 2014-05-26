insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvTraEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumIndividualsTreated'"), 2);

CREATE TABLE TaskForceCountries(
   [ID] AUTOINCREMENT,
   TaskForceId INTEGER,
   Name Text,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

INSERT INTO TaskForceCountries (TaskForceId, Name) values (1, 'Burkina Faso');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (2, 'Ghana');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (3, 'Mali');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (4, 'Niger');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (5, 'Uganda');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (8, 'Haiti');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (9, 'Sierra Leone');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (10, 'South Sudan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (11, 'Bangladesh');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (12, 'Nepal');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (13, 'Cameroon');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (14, 'Togo');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (15, 'Viet Nam');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (16, 'Guinea');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (17, 'Tanzania');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (18, 'Guinea Bissau');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (19, 'Kenya');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (20, 'Mauritania');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (21, 'Senegal');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (22, 'Sudan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (23, 'Nigeria');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (24, 'Ethiopia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (25, 'The Gambia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (26, 'Eritrea');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (27, 'Morocco');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (28, 'Burundi');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (29, 'Zambia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (30, 'CAR');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (31, 'Egypt');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (32, 'Malawi');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (33, 'Algeria');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (34, 'Myanmar');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (35, 'Oman');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (36, 'Cape Verde');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (37, 'Chad');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (38, 'Mexi co');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (39, 'India');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (40, 'Mozambique');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (41, 'Pakistan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (42, 'Cambodia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (43, 'Solomon Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (44, 'Djibouti');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (45, 'Brazil');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (46, 'Yemen');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (47, 'Australia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (48, 'China');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (49, 'Afghanistan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (50, 'Benin');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (51, "Cote d' Ivoire");
INSERT INTO TaskForceCountries (TaskForceId, Name) values (52, 'Fiji');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (53, 'Kiribati');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (54, 'Nauru');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (56, 'Vanuatu');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (57, 'Rwanda');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (58, 'Dominican Republic');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (59, 'Guyana');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (60, 'Zanzibar');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (61, 'Comoros');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (62, 'Madagascar');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (63, 'American Samoa');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (64, 'Cook Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (65, 'Fed. States Micronesia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (66, 'French Polnesia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (67, 'Marshall Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (68, 'New Caledonia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (69, 'Niue');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (70, 'Papau New Guinea');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (71, 'Samoa');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (72, 'Tonga');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (73, 'Tuvalu');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (74, 'Wallis Futuna');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (75, 'Laos');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (76, 'Malaysia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (77, 'Philippines');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (78, 'Indonesia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (79, 'Maldives');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (80, 'Sri Lanka');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (81, 'Timor Leste');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (82, 'Thailand');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (83, 'Botswana');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (111, 'Guatemala');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (85, 'Iran');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (86, 'Iraq');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (87, 'Libya');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (88, 'Somalia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (89, 'Zimbabwe');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (90, 'Namibia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (112, 'Angola');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (84, 'Congo');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (113, 'DRC');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (114, 'Equatorial Guinea');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (115, 'Gabon');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (116, 'Liberia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (118, 'Mauritius');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (117, 'Sao Tome Principe');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (91, 'Seychelles');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (93, 'Costa Rica');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (94, 'Surinam');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (95, 'Trinidad and tobago');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (97, 'Brunei Darssalam');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (98, 'Palau');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (99, 'Bolivia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (100, 'Colombia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (110, 'Cuba');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (101, 'El Salvador');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (102, 'Honduras');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (103, 'Lebanon');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (104, 'Nicaragua');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (105, 'North Korea');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (106, 'Portugal');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (108, 'South Africa');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (109, 'Albania');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (119, 'Andorra');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (120, 'Anguilla');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (121, 'Antigua and Barbuda');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (122, 'Argentina');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (123, 'Armenia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (124, 'Aruba');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (125, 'Austria');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (126, 'Azerbaijan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (127, 'Bahamas, The');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (128, 'Bahrain');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (129, 'Barbados');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (130, 'Belarus');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (131, 'Belgium');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (132, 'Belize');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (133, 'Bermuda');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (134, 'Bhutan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (135, 'Bosnia and Herzegovina');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (136, 'Bulgaria');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (137, 'Burma');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (138, 'Canada');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (139, 'Cayman Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (140, 'Chile');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (141, 'Croatia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (142, 'Curacao');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (143, 'Cyprus');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (144, 'Czech Republic');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (145, 'Denmark');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (146, 'Dominica');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (147, 'Ecuador');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (148, 'Estonia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (149, 'Faroe Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (150, 'Finland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (151, 'France');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (152, 'Gaza Strip');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (153, 'Georgia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (154, 'Germany');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (155, 'Gibraltar');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (156, 'Greece');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (157, 'Greenland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (158, 'Grenada');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (159, 'Guam');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (160, 'Guernsey');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (161, 'Hong Kong');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (162, 'Hungary');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (163, 'Iceland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (164, 'Ireland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (165, 'Isle of Man');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (166, 'Israel');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (167, 'Italy');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (168, 'Jamaica');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (169, 'Japan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (170, 'Jersey');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (171, 'Jordan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (172, 'Kazakhstan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (173, 'Korea, South');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (174, 'Kosovo');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (175, 'Kuwait');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (176, 'Kyrgyzstan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (177, 'Latvia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (178, 'Lesotho');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (179, 'Liechtenstein');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (180, 'Lithuania');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (181, 'Luxembourg');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (182, 'Macau');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (183, 'Macedonia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (184, 'Malta');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (185, 'Mayotte');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (186, 'Moldova');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (187, 'Monaco');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (188, 'Mongolia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (189, 'Montenegro');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (190, 'Montserrat');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (191, 'Netherlands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (192, 'New Zealand');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (193, 'Northern Mariana Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (194, 'Norway');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (195, 'Panama');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (196, 'Paraguay');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (197, 'Peru');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (198, 'Poland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (199, 'Puerto Rico');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (200, 'Qatar');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (201, 'Romania');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (202, 'Russia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (203, 'Saint Barthelemy');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (204, 'Saint Helena');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (205, 'Saint Kitts and Nevis');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (206, 'Saint Lucia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (207, 'Saint Martin');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (208, 'Saint Pierre and Miquelon');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (209, 'Saint Vincent and the Grenadines');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (210, 'San Marino');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (211, 'Saudi Arabia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (212, 'Serbia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (213, 'Singapore');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (214, 'Sint Maarten');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (215, 'Slovakia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (216, 'Slovenia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (217, 'Spain');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (218, 'Swaziland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (219, 'Sweden');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (220, 'Switzerland');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (221, 'Syria');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (222, 'Taiwan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (223, 'Tajikistan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (224, 'Tunisia');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (225, 'Turkey');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (226, 'Turkmenistan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (227, 'Turks and Caicos Islands');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (228, 'Ukraine');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (229, 'United Arab Emirates');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (230, 'United Kingdom');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (231, 'United States');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (232, 'Uruguay');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (233, 'Uzbekistan');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (234, 'Venezuela');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (235, 'Virgin Islands, British');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (236, 'Virgin Islands, U.S.');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (237, 'West Bank');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (238, 'Western Sahara');
INSERT INTO TaskForceCountries (TaskForceId, Name) values (999, 'Temporary');


COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE Country ADD COLUMN TaskForceName Text NULL;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ADD COLUMN TaskForceName Text NULL;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ADD COLUMN TaskForceId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE AdminLevels ALTER COLUMN TaskForceId SET DEFAULT 0;
UPDATE AdminLevels set TaskForceId=0;

UPDATE SurveyIndicators Set SortOrder=SortOrder * 100 where SurveyTypeId = 10; 
UPDATE SurveyIndicators Set SortOrder=SortOrder * 100 where SurveyTypeId = 16; 
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFMapSurExaminedLympho',1910, 16, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFMapSurExaminedHydro1',1920, 16, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFSurExaminedLympho',1810, 10, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFSurPosLympho',1820, 10, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFSurExaminedHydro',1830, 10, 2, 1, 0, 0, 0, 0, 26, NOW());
insert into surveyindicators ( DisplayName ,SortOrder, SurveyTypeId ,DataTypeId, AggTypeId, IsRequired,IsCalculated, CanAddValues, IsDisplayed,UpdatedById ,UpdatedAt) values('LFSurPosHydro',1840, 10, 2, 1, 0, 0, 0, 0, 26, NOW());

insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('Year', 1, 4, 7,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('JrfEndemicLf', 2, 4, 5,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('JrfEndemicOncho', 3, 4, 5,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('JrfEndemicSth', 4, 4, 5,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('JrfEndemicSch', 5, 4, 5,-1,26,NOW());

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,1,'JrfEndemic','JrfEndemic',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicLf' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,2,'JrfEndemicNoPc','JrfEndemicNoPc',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicLf' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,3,'JrfEndemicNot','JrfEndemicNot',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicLf' and ExportTypeId = 4;

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,1,'JrfEndemic','JrfEndemic',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicOncho' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,2,'JrfEndemicNoPc','JrfEndemicNoPc',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicOncho' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,3,'JrfEndemicNot','JrfEndemicNot',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicOncho' and ExportTypeId = 4;

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,1,'JrfEndemic','JrfEndemic',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSth' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,2,'JrfEndemicNoPc','JrfEndemicNoPc',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSth' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,3,'JrfEndemicNot','JrfEndemicNot',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSth' and ExportTypeId = 4;

insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,1,'JrfEndemic','JrfEndemic',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSch' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,2,'JrfEndemicNoPc','JrfEndemicNoPc',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSch' and ExportTypeId = 4;
insert into indicatordropdownvalues (IndicatorId,EntityType,SortOrder,DropdownValue,TranslationKey,WeightedValue,UpdatedById,UpdatedAt,CreatedById,CreatedAt) 
Select ID,11,3,'JrfEndemicNot','JrfEndemicNot',0,26,Now(),26,Now() from exportindicators where displayname = 'JrfEndemicSch' and ExportTypeId = 4;

COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicators ADD COLUMN MergeRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE SurveyIndicators ALTER COLUMN MergeRuleId SET DEFAULT 1;
UPDATE SurveyIndicators set MergeRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicators ADD COLUMN MergeRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE DiseaseDistributionIndicators ALTER COLUMN MergeRuleId SET DEFAULT 1;
UPDATE DiseaseDistributionIndicators set MergeRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicators ADD COLUMN MergeRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE InterventionIndicators ALTER COLUMN MergeRuleId SET DEFAULT 1;
UPDATE InterventionIndicators set MergeRuleId=1;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicators ADD COLUMN MergeRuleId INTEGER;
COMMIT TRANSACTION;
BEGIN TRANSACTION;
ALTER TABLE ProcessIndicators ALTER COLUMN MergeRuleId SET DEFAULT 1;
UPDATE ProcessIndicators set MergeRuleId=1;


insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiName', 1, 5, 1,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiTitle', 2, 5, 1,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiProjectName', 3, 5, 1,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiSubPartnerName', 4, 5, 1,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiYearOfWorkbook', 5, 5, 7,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiReportingPeriod', 6, 5, 1,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiTotalDistrictsTreatedWithUsaid', 7, 5, 2,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiDataCompleteness', 9, 5, 2,-1,26,NOW());
insert into exportindicators (DisplayName,SortOrder,ExportTypeId,DataTypeId,IsRequired,UpdatedById,UpdatedAt) VALUES ('RtiTotalDistrictsComplete', 8, 5, 2,-1,26,NOW());



insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvOfTotalFemalesTargetedOncho', 1, 13100, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvOfTotalMalesTargetedOncho', 1, 13200, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvOfTotalFemalesOncho', 1, 20100, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvOfTotalMalesOncho', 1, 20200, 26, NOW(), 0, 0, 0, 0, 0, 0);

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOfTotalFemalesTargetedOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOfTotalMalesTargetedOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOfTotalFemalesOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOfTotalMalesOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOfTotalFemalesTargetedOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOfTotalMalesTargetedOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOfTotalFemalesOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOfTotalMalesOncho'; 


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0004'
       ,'sc.01.00.0010.sql'
       ,Now());