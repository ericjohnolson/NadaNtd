
ALTER TABLE DiseaseDistributionIndicators ADD COLUMN NewYearType INTEGER;
ALTER TABLE DiseaseDistributionIndicators ALTER COLUMN NewYearType SET DEFAULT 1;
update diseasedistributionindicators set newyeartype=1;
update diseasedistributionindicators set newyeartype=2 where id in (161, 162, 163, 140, 141, 142, 143, 144, 145, 125, 126, 127, 128, 129, 111, 112, 113, 98, 99, 100, 101);

CREATE TABLE [ExportIndicators](
   [ID] AUTOINCREMENT,
   [DisplayName] TEXT,
   ExportTypeId INTEGER,
   [SortOrder] Integer,
   DataTypeId INTEGER,
   IsRequired YESNO,
   UpdatedById Integer,
   UpdatedAt DATETIME,
   CreatedById Integer,
   CreatedAt DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

CREATE TABLE [ExportIndicatorValues](
   [ID] AUTOINCREMENT,
   IndicatorId INTEGER,
   ExportTypeId Integer,
   DynamicValue TEXT,
   UpdatedById Integer,
   UpdatedAt DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

insert into exportindicators (ExportTypeId,DisplayName,SortOrder,DataTypeId,IsRequired,UpdatedById,UpdatedAt)
values (3,'ExportTasStartYear',1,1,0,26,NOW());
insert into exportindicators (ExportTypeId,DisplayName,SortOrder,DataTypeId,IsRequired,UpdatedById,UpdatedAt)
values (3,'ExportTasDays',2,1,0,26,NOW());
insert into exportindicators (ExportTypeId,DisplayName,SortOrder,DataTypeId,IsRequired,UpdatedById,UpdatedAt)
values (3,'ExportTasCost',3,1,0,26,NOW());
insert into exportindicators (ExportTypeId,DisplayName,SortOrder,DataTypeId,IsRequired,UpdatedById,UpdatedAt)
values (3,'ExportTasSources',4,1,0,26,NOW());
insert into exportindicators (ExportTypeId,DisplayName,SortOrder,DataTypeId,IsRequired,UpdatedById,UpdatedAt)
values (3,'ExportTasIctTests',5,1,0,26,NOW());

UPDATE interventionindicators Set SortOrder=SortOrder * 100; 
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvSurveyedCoverage', 1, 25100, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvUpperConfidence', 1, 25200, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues) 
values (2, 'PcIntvLowerConfidence', 1, 25300, 26, NOW(), 0, 0, 0, 0, 0, 0);
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 21, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 22, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvSurveyedCoverage'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 21, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 22, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvUpperConfidence'; 

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 21, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 22, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvLowerConfidence'; 

insert into indicatordatatypes (DataType) values ('DiseaseMultiselect');
insert into indicatordatatypes (DataType) values ('DrugPackageMultiselect');
update interventionindicators set datatypeid = 17 where displayname = 'PcIntvDiseases';
delete from interventiontypes_to_indicators where indicatorid=253;

ALTER TABLE interventionindicators ADD COLUMN IsMetaData YESNO;
ALTER TABLE InterventionIndicators ADD COLUMN DiseaseId INTEGER;
ALTER TABLE InterventionIndicators ALTER COLUMN DiseaseId SET DEFAULT 0;
update interventionindicators set diseaseid = 0;

insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSthPopReqPc', 5, 10, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSthPsacAtRisk', 5, 20, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSthSacAtRisk', 5, 30, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSthAtRisk', 5, 40, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvSthPsacEpiCoverage', 5, 50, 26, NOW(), 0, 0, 0, 0,-1, 0, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvSthSacEpiCoverage', 5, 60, 26, NOW(), 0, 0, 0, 0,-1, 0, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvSthEpiCoverage', 5, 70, 26, NOW(), 0, 0, 0, 0,-1, 0, 5);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvLfAtRisk', 5, 80, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 3);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvLfPopRecPc', 5, 90, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 3);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvLfEpiCoverage', 5, 100, 26, NOW(), 0, 0, 0, 0,-1, 0, 3);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvOnchoAtRisk', 5, 110, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 4);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvOnchoPopReqPc', 5, 120, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 4);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvOnchoEpiCoverage', 5, 130, 26, NOW(), 0, 0, 0, 0,-1, 0, 4);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvOnchoProgramCov', 5, 140, 26, NOW(), 0, 0, 0, 0,-1, 0, 4);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSchAtRisk', 5, 150, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 12);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSchPopReqPc', 5, 160, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 12);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvSchSacAtRisk', 5, 170, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 12);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvSchSacEpi', 5, 180, 26, NOW(), 0, 0, 0, 0,-1, 0, 12);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvSchEpi', 5, 190, 26, NOW(), 0, 0, 0, 0,-1, 0, 12);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, IsMetaData, DiseaseId) values (13, 'PcIntvTraAtRisk', 5, 200, 26, NOW(), 0, 0, 0, 0,0, 0, -1, 13);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvTraEpi', 5, 210, 26, NOW(), 0, 0, 0, 0,-1, 0, 13);
insert into interventionindicators (DataTypeId, DisplayName, AggTypeId, SortOrder, UpdatedById, UpdatedAt, IsDisabled, IsEditable, IsRequired, IsDisplayed, IsCalculated, CanAddValues, DiseaseId) values (13, 'PcIntvOnchoEpiCoverageOfOncho', 5, 130, 26, NOW(), 0, 0, 0, 0,-1, 0, 4);

insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 16, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage';
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvLfAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvLfPopRecPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 18, ID FROM interventionindicators where displayname = 'PcIntvLfEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvLfAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvLfPopRecPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvLfEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 11, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 17, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvOnchoAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvOnchoPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 12, ID FROM interventionindicators where displayname = 'PcIntvOnchoEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSchAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSchPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSchSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSchSacEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 15, ID FROM interventionindicators where displayname = 'PcIntvSchEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 21, ID FROM interventionindicators where displayname = 'PcIntvTraAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 21, ID FROM interventionindicators where displayname = 'PcIntvTraEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvLfAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvLfPopRecPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvLfEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOnchoAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOnchoPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOnchoEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvOnchoProgramCov'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSchAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSchPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSchSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSchSacEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 20, ID FROM interventionindicators where displayname = 'PcIntvSchEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvLfAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvLfPopRecPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvLfEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOnchoAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOnchoPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOnchoEpiCoverageOfOncho'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 10, ID FROM interventionindicators where displayname = 'PcIntvOnchoProgramCov'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvOnchoAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvOnchoPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvOnchoEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSchAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSchPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSchSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSchSacEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 19, ID FROM interventionindicators where displayname = 'PcIntvSchEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSchAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSchPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSchSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSchSacEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 13, ID FROM interventionindicators where displayname = 'PcIntvSchEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthPsacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthPsacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthSacEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSthEpiCoverage'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSchAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSchPopReqPc'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSchSacAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSchSacEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 14, ID FROM interventionindicators where displayname = 'PcIntvSchEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 22, ID FROM interventionindicators where displayname = 'PcIntvTraAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 22, ID FROM interventionindicators where displayname = 'PcIntvTraEpi'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvTraAtRisk'; 
insert into interventiontypes_to_indicators (InterventionTypeId, IndicatorId) SELECT 23, ID FROM interventionindicators where displayname = 'PcIntvTraEpi'; 

insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvSthPsacEpiCoverage'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvPsacTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvSthSacEpiCoverage'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumSacTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvSthEpiCoverage'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumIndividualsTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvLfEpiCoverage'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumIndividualsTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOnchoEpiCoverageOfOncho'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOfTotalTreatedForOncho'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOnchoEpiCoverage'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumIndividualsTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOnchoProgramCov'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOfTotalTreatedForOncho'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOnchoProgramCov'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvOfTotalTargetedForOncho'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvSchSacEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumSacTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvSchEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumIndividualsTreated'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvTraEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumTreatedTeo'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvTraEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumTreatedZx'"), 2);
insert into indicatorcalculations (IndicatorId,EntityTypeId,RelatedIndicatorId,RelatedEntityTypeId) 
VALUES (DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvTraEpi'"), 2, DLookup("ID", "InterventionIndicators", "DisplayName ='PcIntvNumTreatedZxPos'"), 2);
	

Insert into indicatoraggtype (TypeName) values ('Recent');	


INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0003'
       ,'sc.01.00.0003.sql'
       ,Now());