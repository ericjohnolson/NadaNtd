
CREATE TABLE [AdminLevelDemography] (
	[ID] AUTOINCREMENT,
	[AdminLevelId] LONG,
	[DateDemographyData] DATETIME,
	[YearCensus] LONG,
	[YearProjections] LONG,
	[GrowthRate] DOUBLE,
	[PercentRural] DOUBLE DEFAULT 0,
	[TotalPopulation] DOUBLE,
	[AdultPopulation] DOUBLE,
	[Pop0Month] DOUBLE,
	[PopPsac] DOUBLE,
	[PopSac] DOUBLE,
	[Pop5yo] DOUBLE,
	[PopAdult] DOUBLE,
	[PopFemale] DOUBLE,
	[PopMale] DOUBLE,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[Notes] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [AdminLevelDemography] ALLOW ZERO LENGTH [Notes];
ALTER TABLE [AdminLevelDemography] FORMAT [IsDeleted] SET "True/False";
CREATE TABLE [AdminLevels] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[AdminLevelTypeId] LONG,
	[ParentId] LONG,
	[EcoZoneId] LONG,
	[UrbanOrRural] TEXT(255) WITH COMPRESSION,
	[LatWho] DOUBLE,
	[LngWho] DOUBLE,
	[LatOther] DOUBLE,
	[LngOther] DOUBLE,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [AdminLevels] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [AdminLevels] ALLOW ZERO LENGTH [UrbanOrRural];
ALTER TABLE [AdminLevels] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [AdminLevelTypes] (
	[ID] AUTOINCREMENT,
	[AdminLevel] LONG,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDistrict] BIT NOT NULL DEFAULT 0,
	[IsAggregatingLevel] BIT NOT NULL DEFAULT 0,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [AdminLevelTypes] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [AdminLevelTypes] FORMAT [IsDistrict] SET "True/False";
ALTER TABLE [AdminLevelTypes] FORMAT [IsAggregatingLevel] SET "True/False";
ALTER TABLE [AdminLevelTypes] FORMAT [IsDeleted] SET "True/False";
CREATE TABLE [aspnet_Applications] (
	[ApplicationName] TEXT(255) WITH COMPRESSION NOT NULL,
	[ApplicationId] AUTOINCREMENT,
	[Description] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ApplicationName]),
	CONSTRAINT [ApplicationId] UNIQUE ([ApplicationId]),
	CONSTRAINT [ApplicationName] UNIQUE ([ApplicationName])
);
ALTER TABLE [aspnet_Applications] DENY ZERO LENGTH [ApplicationName];
ALTER TABLE [aspnet_Applications] ALLOW ZERO LENGTH [Description];
CREATE TABLE [aspnet_Membership] (
	[UserId] LONG NOT NULL DEFAULT 0,
	[Password] TEXT(128) WITH COMPRESSION NOT NULL,
	[PasswordFormat] LONG NOT NULL DEFAULT 1,
	[MobilePIN] TEXT(16) WITH COMPRESSION,
	[Email] TEXT(128) WITH COMPRESSION,
	[PasswordQuestion] TEXT(255) WITH COMPRESSION,
	[PasswordAnswer] TEXT(128) WITH COMPRESSION,
	[IsApproved] BIT NOT NULL DEFAULT Yes,
	[CreateDate] DATETIME NOT NULL DEFAULT Now(),
	[LastLoginDate] DATETIME NOT NULL DEFAULT Now(),
	[LastPasswordChangedDate] DATETIME NOT NULL DEFAULT Now(),
	[Comment] MEMO WITH COMPRESSION,
	[PasswordSalt] TEXT(128) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([UserId]),
	CONSTRAINT [UserId] UNIQUE ([UserId])
);
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [Password];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [MobilePIN];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [Email];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [PasswordQuestion];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [PasswordAnswer];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [Comment];
ALTER TABLE [aspnet_Membership] ALLOW ZERO LENGTH [PasswordSalt];
ALTER TABLE [aspnet_Membership] FORMAT [IsApproved] SET "Yes/No";
CREATE TABLE [aspnet_PagePersonalizationAllUsers] (
	[PathId] LONG DEFAULT 0,
	[PageSettings] MEMO WITH COMPRESSION NOT NULL,
	[LastUpdatedDate] DATETIME NOT NULL,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([PathId]),
	CONSTRAINT [UrlId] UNIQUE ([PathId])
);
ALTER TABLE [aspnet_PagePersonalizationAllUsers] ALLOW ZERO LENGTH [PageSettings];
CREATE TABLE [aspnet_PagePersonalizationPerUser] (
	[Id] AUTOINCREMENT,
	[PathId] LONG DEFAULT 0,
	[UserId] LONG NOT NULL DEFAULT 0,
	[PageSettings] MEMO WITH COMPRESSION NOT NULL,
	[LastUpdatedDate] DATETIME NOT NULL,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([Id])
);
ALTER TABLE [aspnet_PagePersonalizationPerUser] ALLOW ZERO LENGTH [PageSettings];
CREATE TABLE [aspnet_Paths] (
	[ApplicationId] LONG DEFAULT 0,
	[PathId] AUTOINCREMENT,
	[Path] TEXT(255) WITH COMPRESSION NOT NULL,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([PathId])
);
ALTER TABLE [aspnet_Paths] DENY ZERO LENGTH [Path];
CREATE TABLE [aspnet_Profile] (
	[UserId] LONG NOT NULL DEFAULT 0,
	[PropertyNames] MEMO WITH COMPRESSION NOT NULL,
	[PropertyValuesString] MEMO WITH COMPRESSION NOT NULL,
	[LastUpdatedDate] DATETIME NOT NULL DEFAULT Now(),
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([UserId]),
	CONSTRAINT [UserId] UNIQUE ([UserId])
);
ALTER TABLE [aspnet_Profile] ALLOW ZERO LENGTH [PropertyNames];
ALTER TABLE [aspnet_Profile] ALLOW ZERO LENGTH [PropertyValuesString];
CREATE TABLE [aspnet_Roles] (
	[ApplicationId] LONG NOT NULL DEFAULT 0,
	[RoleId] AUTOINCREMENT,
	[RoleName] TEXT(255) WITH COMPRESSION NOT NULL,
	[Description] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([RoleId])
);
ALTER TABLE [aspnet_Roles] DENY ZERO LENGTH [RoleName];
ALTER TABLE [aspnet_Roles] ALLOW ZERO LENGTH [Description];
CREATE TABLE [aspnet_SiteCounters] (
	[Id] AUTOINCREMENT,
	[Application] MEMO WITH COMPRESSION,
	[PageUrl] MEMO WITH COMPRESSION,
	[CounterGroup] MEMO WITH COMPRESSION,
	[CounterName] MEMO WITH COMPRESSION,
	[CounterEvent] MEMO WITH COMPRESSION,
	[NavigateUrl] MEMO WITH COMPRESSION,
	[StartTime] DATETIME,
	[EndTime] DATETIME,
	[Total] LONG NOT NULL DEFAULT 0,
	CONSTRAINT [Id] UNIQUE ([Id])
);
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [Application];
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [PageUrl];
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [CounterGroup];
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [CounterName];
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [CounterEvent];
ALTER TABLE [aspnet_SiteCounters] ALLOW ZERO LENGTH [NavigateUrl];
CREATE TABLE [aspnet_Users] (
	[ApplicationId] LONG NOT NULL DEFAULT 0,
	[UserId] AUTOINCREMENT,
	[UserName] TEXT(255) WITH COMPRESSION NOT NULL,
	[MobileAlias] TEXT(16) WITH COMPRESSION,
	[IsAnonymous] BIT NOT NULL DEFAULT No,
	[LastActivityDate] DATETIME NOT NULL DEFAULT Now(),
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([UserId]),
	CONSTRAINT [UserId] UNIQUE ([UserId])
);
ALTER TABLE [aspnet_Users] DENY ZERO LENGTH [UserName];
ALTER TABLE [aspnet_Users] ALLOW ZERO LENGTH [MobileAlias];
ALTER TABLE [aspnet_Users] FORMAT [IsAnonymous] SET "Yes/No";
CREATE TABLE [aspnet_UsersInRoles] (
	[UserId] LONG NOT NULL DEFAULT 0,
	[RoleId] LONG NOT NULL DEFAULT 0
);
CREATE TABLE [Country] (
	[ID] LONG,
	[AdminLevelId] LONG,
	[MonthYearStarts] LONG,
	[HasReviewedDiseases] BIT NOT NULL,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
CREATE TABLE [CountryDemography] (
	[AdminLevelDemographyId] LONG,
	[AgeRangePsac] TEXT(255) WITH COMPRESSION,
	[DateReporting] DATETIME,
	[AgeRangeSac] TEXT(255) WITH COMPRESSION,
	[YearCensus] LONG,
	[Percent6mos] DOUBLE,
	[YearProjections] LONG,
	[GrowthRate] DOUBLE,
	[PercentPsac] DOUBLE,
	[FemalePercent] DOUBLE,
	[PercentSac] DOUBLE,
	[MalePercent] DOUBLE,
	[Percent5yo] DOUBLE,
	[AdultsPercent] DOUBLE,
	[PercentFemale] DOUBLE,
	[PercentMale] DOUBLE,
	[PercentAdult] DOUBLE,
	CONSTRAINT [CountryId] UNIQUE ([AdminLevelDemographyId])
);
ALTER TABLE [CountryDemography] ALLOW ZERO LENGTH [AgeRangePsac];
ALTER TABLE [CountryDemography] ALLOW ZERO LENGTH [AgeRangeSac];
CREATE TABLE [DiseaseDistributionIndicators] (
	[ID] AUTOINCREMENT,
	[DiseaseId] LONG,
	[DataTypeId] LONG,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[AggTypeId] LONG DEFAULT 1,
	[IsDisabled] BIT NOT NULL DEFAULT 0,
	[IsEditable] BIT NOT NULL DEFAULT 0,
	[IsRequired] BIT NOT NULL DEFAULT 0,
	[IsDisplayed] BIT NOT NULL DEFAULT 1,
	[IsCalculated] BIT NOT NULL DEFAULT 0,
	[IsMetaData] BIT NOT NULL DEFAULT 0,
	[CanAddValues] BIT NOT NULL DEFAULT 0,
	[SortOrder] LONG DEFAULT 0,
	[UpdatedById] LONG DEFAULT 26,
	[UpdatedAt] DATETIME DEFAULT '1/1/2013',
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [DiseaseDistributionIndicators] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsDisabled] SET "Yes/No";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsEditable] SET "Yes/No";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsRequired] SET "Yes/No";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsDisplayed] SET "Yes/No";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsCalculated] SET "True/False";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [IsMetaData] SET "True/False";
ALTER TABLE [DiseaseDistributionIndicators] FORMAT [CanAddValues] SET "True/False";
CREATE TABLE [DiseaseDistributionIndicatorValues] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[DiseaseDistributionId] LONG,
	[DynamicValue] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [DiseaseDistributionIndicatorValues] ALLOW ZERO LENGTH [DynamicValue];
ALTER TABLE [DiseaseDistributionIndicatorValues] ALLOW ZERO LENGTH [UpdatedAt];
CREATE TABLE [DiseaseDistributions] (
	[ID] AUTOINCREMENT,
	[DiseaseId] LONG,
	[AdminLevelId] LONG,
	[DateReported] DATETIME,
	[Notes] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [DiseaseDistributions] ALLOW ZERO LENGTH [Notes];
ALTER TABLE [DiseaseDistributions] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [Diseases] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[DiseaseType] TEXT(255) WITH COMPRESSION,
	[UserDefinedName] TEXT(255) WITH COMPRESSION,
	[IsSelected] BIT NOT NULL DEFAULT 1,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[IsHidden] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Diseases] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [Diseases] ALLOW ZERO LENGTH [DiseaseType];
ALTER TABLE [Diseases] ALLOW ZERO LENGTH [UserDefinedName];
ALTER TABLE [Diseases] FORMAT [IsSelected] SET "True/False";
ALTER TABLE [Diseases] FORMAT [IsDeleted] SET "Yes/No";
ALTER TABLE [Diseases] FORMAT [IsHidden] SET "Yes/No";
CREATE TABLE [EcologicalZones] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [EcologicalZones] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [EcologicalZones] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [EvalSubDistricts] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [EvalSubDistricts] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [EvalSubDistricts] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [EvaluationUnits] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [EvaluationUnits] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [EvaluationUnits] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [ExportCmJrfQuestions] (
	[ID] AUTOINCREMENT,
	[YearReporting] LONG,
	[CmHaveMasterPlan] BIT NOT NULL DEFAULT 0,
	[CmYearsMasterPlan] TEXT(255) WITH COMPRESSION,
	[CmBuget] LONG,
	[CmPercentFunded] LONG,
	[CmHaveAnnualOpPlan] BIT NOT NULL DEFAULT 0,
	[CmDiseaseSpecOrNtdIntegrated] TEXT(255) WITH COMPRESSION,
	[CmBuHasPlan] BIT NOT NULL DEFAULT 0,
	[CmGwHasPlan] BIT NOT NULL DEFAULT 0,
	[CmHatHasPlan] BIT NOT NULL DEFAULT 0,
	[CmLeishHasPlan] BIT NOT NULL DEFAULT 0,
	[CmLeprosyHasPlan] BIT NOT NULL DEFAULT 0,
	[CmYawsHasPlan] BIT NOT NULL DEFAULT 0,
	[CmAnySupplyFunds] BIT NOT NULL DEFAULT 0,
	[CmHasStorage] BIT NOT NULL DEFAULT 0,
	[CmStorageNtdOrCombined] TEXT(255) WITH COMPRESSION,
	[CmStorageSponsor1] TEXT(255) WITH COMPRESSION,
	[CmStorageSponsor2] TEXT(255) WITH COMPRESSION,
	[CmStorageSponsor3] TEXT(255) WITH COMPRESSION,
	[CmStorageSponsor4] TEXT(255) WITH COMPRESSION,
	[CmHasTaskForce] BIT NOT NULL DEFAULT 0,
	[CmHasMoh] BIT NOT NULL DEFAULT 0,
	[CmHasMosw] BIT NOT NULL DEFAULT 0,
	[CmHasMot] BIT NOT NULL DEFAULT 0,
	[CmHasMoe] BIT NOT NULL DEFAULT 0,
	[CmHasMoc] BIT NOT NULL DEFAULT 0,
	[CmHasUni] BIT NOT NULL DEFAULT 0,
	[CmHasNgo] BIT NOT NULL DEFAULT 0,
	[CmHasAnnualForum] BIT NOT NULL DEFAULT 0,
	[CmForumHasRegions] BIT NOT NULL DEFAULT 0,
	[CmForumHasTaskForce] BIT NOT NULL DEFAULT 0,
	[CmHasNtdReviewMeetings] BIT NOT NULL DEFAULT 0,
	[CmHasDiseaseSpecMeetings] BIT NOT NULL DEFAULT 0,
	[CmHasGwMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasLeprosyMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasHatMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasLeishMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasBuMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasYawsMeeting] BIT NOT NULL DEFAULT 0,
	[CmHasWeeklyMech] BIT NOT NULL DEFAULT 0,
	[CmHasMonthlyMech] BIT NOT NULL DEFAULT 0,
	[CmHasQuarterlyMech] BIT NOT NULL DEFAULT 0,
	[CmHasSemesterMech] BIT NOT NULL DEFAULT 0,
	[CmOtherMechs] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmYearsMasterPlan];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmDiseaseSpecOrNtdIntegrated];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmStorageNtdOrCombined];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmStorageSponsor1];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmStorageSponsor2];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmStorageSponsor3];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmStorageSponsor4];
ALTER TABLE [ExportCmJrfQuestions] ALLOW ZERO LENGTH [CmOtherMechs];
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHaveMasterPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHaveAnnualOpPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmBuHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmGwHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHatHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmLeishHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmLeprosyHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmYawsHasPlan] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmAnySupplyFunds] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasStorage] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasTaskForce] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMoh] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMosw] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMot] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMoe] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMoc] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasUni] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasNgo] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasAnnualForum] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmForumHasRegions] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmForumHasTaskForce] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasNtdReviewMeetings] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasDiseaseSpecMeetings] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasGwMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasLeprosyMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasHatMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasLeishMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasBuMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasYawsMeeting] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasWeeklyMech] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasMonthlyMech] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasQuarterlyMech] SET "True/False";
ALTER TABLE [ExportCmJrfQuestions] FORMAT [CmHasSemesterMech] SET "True/False";
CREATE TABLE [ExportContacts] (
	[ID] AUTOINCREMENT,
	[CmContactName] TEXT(255) WITH COMPRESSION,
	[CmContactPost] TEXT(255) WITH COMPRESSION,
	[CmContactTele] TEXT(255) WITH COMPRESSION,
	[CmContactEmail] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ExportContacts] ALLOW ZERO LENGTH [CmContactName];
ALTER TABLE [ExportContacts] ALLOW ZERO LENGTH [CmContactPost];
ALTER TABLE [ExportContacts] ALLOW ZERO LENGTH [CmContactTele];
ALTER TABLE [ExportContacts] ALLOW ZERO LENGTH [CmContactEmail];
CREATE TABLE [ExportJrfQuestions] (
	[ID] AUTOINCREMENT,
	[JrfYearReporting] LONG,
	[JrfEndemicLf] TEXT(255) WITH COMPRESSION,
	[JrfEndemicOncho] TEXT(255) WITH COMPRESSION,
	[JrfEndemicSth] TEXT(255) WITH COMPRESSION,
	[JrfEndemicSch] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ExportJrfQuestions] ALLOW ZERO LENGTH [JrfEndemicLf];
ALTER TABLE [ExportJrfQuestions] ALLOW ZERO LENGTH [JrfEndemicOncho];
ALTER TABLE [ExportJrfQuestions] ALLOW ZERO LENGTH [JrfEndemicSth];
ALTER TABLE [ExportJrfQuestions] ALLOW ZERO LENGTH [JrfEndemicSch];
CREATE TABLE [IndicatorAggType] (
	[ID] AUTOINCREMENT,
	[TypeName] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [IndicatorAggType] ALLOW ZERO LENGTH [TypeName];
CREATE TABLE [IndicatorCalculations] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[EntityTypeId] LONG,
	[RelatedIndicatorId] LONG,
	[RelatedEntityTypeId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
CREATE TABLE [IndicatorDataTypes] (
	[ID] AUTOINCREMENT,
	[DataType] TEXT(255) WITH COMPRESSION,
	[HasValueList] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [IndicatorDataTypes] ALLOW ZERO LENGTH [DataType];
ALTER TABLE [IndicatorDataTypes] FORMAT [HasValueList] SET "True/False";
CREATE TABLE [IndicatorDropdownValues] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[EntityType] LONG,
	[SortOrder] LONG,
	[DropdownValue] TEXT(255) WITH COMPRESSION,
	[TranslationKey] TEXT(255) WITH COMPRESSION,
	[WeightedValue] LONG,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [IndicatorDropdownValues] ALLOW ZERO LENGTH [DropdownValue];
ALTER TABLE [IndicatorDropdownValues] ALLOW ZERO LENGTH [TranslationKey];
CREATE TABLE [InterventionDistributionMethods] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [InterventionDistributionMethods] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [InterventionDistributionMethods] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [InterventionIndicators] (
	[ID] AUTOINCREMENT,
	[InterventionTypeId] LONG,
	[DataTypeId] LONG,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[AggTypeId] LONG DEFAULT 1,
	[IsDisabled] BIT NOT NULL DEFAULT 0,
	[IsEditable] BIT NOT NULL DEFAULT 0,
	[IsRequired] BIT NOT NULL DEFAULT 0,
	[IsDisplayed] BIT NOT NULL DEFAULT 1,
	[IsCalculated] BIT NOT NULL DEFAULT 0,
	[CanAddValues] BIT NOT NULL DEFAULT 0,
	[SortOrder] LONG DEFAULT 0,
	[UpdatedById] LONG DEFAULT 26,
	[UpdatedAt] DATETIME DEFAULT '1/1/2013',
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [InterventionIndicators] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [InterventionIndicators] FORMAT [IsDisabled] SET "Yes/No";
ALTER TABLE [InterventionIndicators] FORMAT [IsEditable] SET "Yes/No";
ALTER TABLE [InterventionIndicators] FORMAT [IsRequired] SET "Yes/No";
ALTER TABLE [InterventionIndicators] FORMAT [IsDisplayed] SET "Yes/No";
ALTER TABLE [InterventionIndicators] FORMAT [IsCalculated] SET "True/False";
ALTER TABLE [InterventionIndicators] FORMAT [CanAddValues] SET "True/False";
CREATE TABLE [InterventionIndicatorValues] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[InterventionId] LONG,
	[DynamicValue] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [InterventionIndicatorValues] ALLOW ZERO LENGTH [DynamicValue];
CREATE TABLE [Interventions] (
	[ID] AUTOINCREMENT,
	[InterventionTypeId] LONG,
	[AdminLevelId] LONG,
	[StartDate] DATETIME,
	[EndDate] DATETIME,
	[DateReported] DATETIME,
	[PcIntvRoundNumber] LONG,
	[Notes] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Interventions] ALLOW ZERO LENGTH [Notes];
ALTER TABLE [Interventions] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [Interventions_to_Diseases] (
	[InterventionId] LONG,
	[DiseaseId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([InterventionId], [DiseaseId])
);
CREATE TABLE [Interventions_to_Medicines] (
	[InterventionId] LONG,
	[MedicineId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([InterventionId], [MedicineId])
);
CREATE TABLE [Interventions_to_Partners] (
	[InterventionId] LONG,
	[PartnerId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([InterventionId], [PartnerId])
);
CREATE TABLE [InterventionTypes] (
	[ID] AUTOINCREMENT,
	[InterventionTypeName] TEXT(255) WITH COMPRESSION,
	[DiseaseType] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [InterventionTypes] ALLOW ZERO LENGTH [InterventionTypeName];
ALTER TABLE [InterventionTypes] ALLOW ZERO LENGTH [DiseaseType];
CREATE TABLE [InterventionTypes_to_Diseases] (
	[InterventionTypeId] LONG,
	[DiseaseId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([InterventionTypeId], [DiseaseId])
);
CREATE TABLE [InterventionTypes_to_Indicators] (
	[InterventionTypeId] LONG,
	[IndicatorId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([InterventionTypeId], [IndicatorId])
);
CREATE TABLE [Languages] (
	[IsoCode] TEXT(8) WITH COMPRESSION,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([IsoCode])
);
ALTER TABLE [Languages] ALLOW ZERO LENGTH [IsoCode];
ALTER TABLE [Languages] ALLOW ZERO LENGTH [DisplayName];
CREATE TABLE [Medicines] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Medicines] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [Medicines] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [Partners] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Partners] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [Partners] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [Processes] (
	[ID] AUTOINCREMENT,
	[ProcessTypeId] LONG,
	[AdminLevelId] LONG,
	[DateReported] DATETIME,
	[ScmDrug] TEXT(255) WITH COMPRESSION,
	[PCTrainTrainingCategory] TEXT(255) WITH COMPRESSION,
	[Notes] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Processes] ALLOW ZERO LENGTH [ScmDrug];
ALTER TABLE [Processes] ALLOW ZERO LENGTH [PCTrainTrainingCategory];
ALTER TABLE [Processes] ALLOW ZERO LENGTH [Notes];
ALTER TABLE [Processes] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [ProcessIndicators] (
	[ID] AUTOINCREMENT,
	[ProcessTypeId] LONG,
	[DataTypeId] LONG,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[AggTypeId] LONG DEFAULT 1,
	[IsDisabled] BIT NOT NULL DEFAULT 0,
	[IsEditable] BIT NOT NULL DEFAULT 0,
	[IsRequired] BIT NOT NULL DEFAULT 0,
	[IsDisplayed] BIT NOT NULL DEFAULT 1,
	[CanAddValues] BIT NOT NULL DEFAULT 0,
	[SortOrder] LONG DEFAULT 0,
	[UpdatedById] LONG DEFAULT 26,
	[UpdatedAt] DATETIME DEFAULT '1/1/2013',
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ProcessIndicators] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [ProcessIndicators] FORMAT [IsDisabled] SET "Yes/No";
ALTER TABLE [ProcessIndicators] FORMAT [IsEditable] SET "Yes/No";
ALTER TABLE [ProcessIndicators] FORMAT [IsRequired] SET "Yes/No";
ALTER TABLE [ProcessIndicators] FORMAT [IsDisplayed] SET "Yes/No";
ALTER TABLE [ProcessIndicators] FORMAT [CanAddValues] SET "True/False";
CREATE TABLE [ProcessIndicatorValues] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[ProcessId] LONG,
	[DynamicValue] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ProcessIndicatorValues] ALLOW ZERO LENGTH [DynamicValue];
CREATE TABLE [ProcessTypes] (
	[ID] AUTOINCREMENT,
	[TypeName] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [ProcessTypes] ALLOW ZERO LENGTH [TypeName];
CREATE TABLE [ProcessTypes_to_Diseases] (
	[ProcessTypeId] LONG,
	[DiseaseId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ProcessTypeId], [DiseaseId])
);
CREATE TABLE [SentinelSites] (
	[ID] AUTOINCREMENT,
	[SiteName] TEXT(255) WITH COMPRESSION,
	[Lat] DOUBLE,
	[Lng] DOUBLE,
	[Notes] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [SentinelSites] ALLOW ZERO LENGTH [SiteName];
ALTER TABLE [SentinelSites] ALLOW ZERO LENGTH [Notes];
CREATE TABLE [SentinelSites_to_AdminLevels] (
	[SentinelSiteId] LONG,
	[AdminLevelId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([SentinelSiteId], [AdminLevelId])
);
CREATE TABLE [SurveyIndicators] (
	[ID] AUTOINCREMENT,
	[SurveyTypeId] LONG,
	[DataTypeId] LONG,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[AggTypeId] LONG DEFAULT 1,
	[IsDisabled] BIT NOT NULL DEFAULT 0,
	[IsEditable] BIT NOT NULL DEFAULT 0,
	[IsDisplayed] BIT NOT NULL DEFAULT 1,
	[IsRequired] BIT NOT NULL DEFAULT 0,
	[IsCalculated] BIT NOT NULL DEFAULT 0,
	[CanAddValues] BIT NOT NULL DEFAULT 0,
	[SortOrder] LONG DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [SurveyIndicators] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [SurveyIndicators] FORMAT [IsDisabled] SET "Yes/No";
ALTER TABLE [SurveyIndicators] FORMAT [IsEditable] SET "Yes/No";
ALTER TABLE [SurveyIndicators] FORMAT [IsDisplayed] SET "Yes/No";
ALTER TABLE [SurveyIndicators] FORMAT [IsRequired] SET "Yes/No";
ALTER TABLE [SurveyIndicators] FORMAT [IsCalculated] SET "True/False";
ALTER TABLE [SurveyIndicators] FORMAT [CanAddValues] SET "True/False";
CREATE TABLE [SurveyIndicatorValues] (
	[ID] AUTOINCREMENT,
	[IndicatorId] LONG,
	[SurveyId] LONG,
	[DynamicValue] TEXT(255) WITH COMPRESSION,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [SurveyIndicatorValues] ALLOW ZERO LENGTH [DynamicValue];
CREATE TABLE [SurveyLfMf] (
	[ID] AUTOINCREMENT,
	[SurveyId] LONG,
	[TimingType] TEXT(255) WITH COMPRESSION DEFAULT '',
	[TestType] TEXT(255) WITH COMPRESSION,
	[SiteType] TEXT(255) WITH COMPRESSION DEFAULT '',
	[SpotCheckName] TEXT(255) WITH COMPRESSION DEFAULT '',
	[SpotCheckLat] LONG,
	[SpotCheckLng] LONG,
	[SentinelSiteId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [SurveyLfMf] ALLOW ZERO LENGTH [TimingType];
ALTER TABLE [SurveyLfMf] ALLOW ZERO LENGTH [TestType];
ALTER TABLE [SurveyLfMf] ALLOW ZERO LENGTH [SiteType];
ALTER TABLE [SurveyLfMf] ALLOW ZERO LENGTH [SpotCheckName];
CREATE TABLE [Surveys] (
	[ID] AUTOINCREMENT,
	[SurveyTypeId] LONG,
	[StartDate] DATETIME,
	[EndDate] DATETIME,
	[SiteType] TEXT(255) WITH COMPRESSION DEFAULT '',
	[SpotCheckName] TEXT(255) WITH COMPRESSION DEFAULT '',
	[SpotCheckLat] DOUBLE,
	[SpotCheckLng] DOUBLE,
	[SentinelSiteId] LONG,
	[DateReported] DATETIME,
	[Notes] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Surveys] ALLOW ZERO LENGTH [SiteType];
ALTER TABLE [Surveys] ALLOW ZERO LENGTH [SpotCheckName];
ALTER TABLE [Surveys] ALLOW ZERO LENGTH [Notes];
ALTER TABLE [Surveys] FORMAT [IsDeleted] SET "Yes/No";
CREATE TABLE [Surveys_to_AdminLevels] (
	[SurveyId] LONG,
	[AdminLevelId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([SurveyId], [AdminLevelId])
);
CREATE TABLE [Surveys_to_Partners] (
	[SurveyId] LONG,
	[PartnerId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([SurveyId], [PartnerId])
);
CREATE TABLE [Surveys_to_Vectors] (
	[SurveyId] LONG,
	[VectorId] LONG,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([SurveyId], [VectorId])
);
CREATE TABLE [SurveyTypes] (
	[ID] AUTOINCREMENT,
	[SurveyTypeName] TEXT(255) WITH COMPRESSION,
	[DiseaseId] LONG,
	[HasMultipleLocations] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [SurveyTypes] ALLOW ZERO LENGTH [SurveyTypeName];
ALTER TABLE [SurveyTypes] FORMAT [HasMultipleLocations] SET "True/False";
CREATE TABLE [Vectors] (
	[ID] AUTOINCREMENT,
	[DisplayName] TEXT(255) WITH COMPRESSION,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[UpdatedById] LONG,
	[UpdatedAt] DATETIME,
	[CreatedById] LONG,
	[CreatedAt] DATETIME,
	CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);
ALTER TABLE [Vectors] ALLOW ZERO LENGTH [DisplayName];
ALTER TABLE [Vectors] FORMAT [IsDeleted] SET "Yes/No";
CREATE INDEX [CountryId]
	ON [AdminLevelDemography] ([AdminLevelId]);
CREATE INDEX [CreatedById]
	ON [AdminLevelDemography] ([CreatedById]);
CREATE INDEX [AdminLevelTypeId]
	ON [AdminLevels] ([AdminLevelTypeId]);
CREATE INDEX [CreatedById]
	ON [AdminLevels] ([CreatedById]);
CREATE INDEX [EcoZoneId]
	ON [AdminLevels] ([EcoZoneId]);
CREATE INDEX [LatWho]
	ON [AdminLevels] ([LatWho]);
CREATE INDEX [ParentId]
	ON [AdminLevels] ([ParentId]);
CREATE INDEX [CreatedById]
	ON [AdminLevelTypes] ([CreatedById]);
CREATE INDEX [Id]
	ON [aspnet_PagePersonalizationPerUser] ([Id]);
CREATE INDEX [UrlId]
	ON [aspnet_PagePersonalizationPerUser] ([PathId]);
CREATE INDEX [UserId]
	ON [aspnet_PagePersonalizationPerUser] ([UserId]);
CREATE INDEX [ApplicationId]
	ON [aspnet_Paths] ([ApplicationId]);
CREATE INDEX [Url]
	ON [aspnet_Paths] ([Path]);
CREATE INDEX [UrlId]
	ON [aspnet_Paths] ([PathId]);
CREATE INDEX [ApplicationId]
	ON [aspnet_Roles] ([ApplicationId]);
CREATE INDEX [RoleId]
	ON [aspnet_Roles] ([RoleId]);
CREATE INDEX [RoleName]
	ON [aspnet_Roles] ([RoleName]);
CREATE INDEX [ApplicationId]
	ON [aspnet_Users] ([ApplicationId]);
CREATE INDEX [UserName]
	ON [aspnet_Users] ([UserName]);
CREATE INDEX [RoleId]
	ON [aspnet_UsersInRoles] ([RoleId]);
CREATE INDEX [UserId]
	ON [aspnet_UsersInRoles] ([UserId]);
CREATE INDEX [AggTypeId]
	ON [DiseaseDistributionIndicators] ([AggTypeId]);
CREATE INDEX [DataTypeId]
	ON [DiseaseDistributionIndicators] ([DataTypeId]);
CREATE INDEX [DiseaseId]
	ON [DiseaseDistributionIndicators] ([DiseaseId]);
CREATE INDEX [UpdatedById]
	ON [DiseaseDistributionIndicators] ([UpdatedById]);
CREATE INDEX [DiseaseDistributionId]
	ON [DiseaseDistributionIndicatorValues] ([DiseaseDistributionId]);
CREATE INDEX [IndicatorId]
	ON [DiseaseDistributionIndicatorValues] ([IndicatorId]);
CREATE INDEX [UpdatedById]
	ON [DiseaseDistributionIndicatorValues] ([UpdatedById]);
CREATE INDEX [AdminLevelId]
	ON [DiseaseDistributions] ([AdminLevelId]);
CREATE INDEX [CreatedById]
	ON [DiseaseDistributions] ([CreatedById]);
CREATE INDEX [DiseaseId]
	ON [DiseaseDistributions] ([DiseaseId]);
CREATE INDEX [UpdatedById]
	ON [DiseaseDistributions] ([UpdatedById]);
CREATE INDEX [Code]
	ON [Diseases] ([DisplayName]);
CREATE INDEX [CreatedById]
	ON [Diseases] ([CreatedById]);
CREATE INDEX [CreatedById]
	ON [EcologicalZones] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [EcologicalZones] ([UpdatedById]);
CREATE INDEX [CreatedById]
	ON [EvalSubDistricts] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [EvalSubDistricts] ([UpdatedById]);
CREATE INDEX [CreatedById]
	ON [EvaluationUnits] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [EvaluationUnits] ([UpdatedById]);
CREATE INDEX [CalculatedIndicatorId]
	ON [IndicatorCalculations] ([IndicatorId]);
CREATE INDEX [EntityTypeId]
	ON [IndicatorCalculations] ([EntityTypeId]);
CREATE INDEX [RelatedEntityTypeId]
	ON [IndicatorCalculations] ([RelatedEntityTypeId]);
CREATE INDEX [RelatedIndicatorId]
	ON [IndicatorCalculations] ([RelatedIndicatorId]);
CREATE INDEX [IndicatorId]
	ON [IndicatorDropdownValues] ([IndicatorId]);
CREATE INDEX [TranslationKey]
	ON [IndicatorDropdownValues] ([TranslationKey]);
CREATE INDEX [UpdatedById]
	ON [IndicatorDropdownValues] ([UpdatedById]);
CREATE INDEX [UpdatedById1]
	ON [IndicatorDropdownValues] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [InterventionDistributionMethods] ([UpdatedById]);
CREATE INDEX [AggTypeId]
	ON [InterventionIndicators] ([AggTypeId]);
CREATE INDEX [DataTypeId]
	ON [InterventionIndicators] ([DataTypeId]);
CREATE INDEX [InterventionTypeId]
	ON [InterventionIndicators] ([InterventionTypeId]);
CREATE INDEX [UpdatedById]
	ON [InterventionIndicators] ([UpdatedById]);
CREATE INDEX [IndicatorId]
	ON [InterventionIndicatorValues] ([IndicatorId]);
CREATE INDEX [InterventionId]
	ON [InterventionIndicatorValues] ([InterventionId]);
CREATE INDEX [UpdatedById]
	ON [InterventionIndicatorValues] ([UpdatedById]);
CREATE INDEX [AdminLevelId]
	ON [Interventions] ([AdminLevelId]);
CREATE INDEX [CreatedById]
	ON [Interventions] ([CreatedById]);
CREATE INDEX [InterventionTypeId]
	ON [Interventions] ([InterventionTypeId]);
CREATE INDEX [UpdatedById]
	ON [Interventions] ([UpdatedById]);
CREATE INDEX [CreatedById]
	ON [InterventionTypes] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [InterventionTypes] ([UpdatedById]);
CREATE INDEX [DiseaseId]
	ON [InterventionTypes_to_Diseases] ([DiseaseId]);
CREATE INDEX [IsoCode]
	ON [Languages] ([IsoCode]);
CREATE INDEX [CreatedById]
	ON [Medicines] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [Medicines] ([UpdatedById]);
CREATE INDEX [CreatedById]
	ON [Partners] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [Partners] ([UpdatedById]);
CREATE INDEX [AdminLevelId]
	ON [Processes] ([AdminLevelId]);
CREATE INDEX [CreatedById]
	ON [Processes] ([CreatedById]);
CREATE INDEX [InterventionTypeId]
	ON [Processes] ([ProcessTypeId]);
CREATE INDEX [UpdatedById]
	ON [Processes] ([UpdatedById]);
CREATE INDEX [AggTypeId]
	ON [ProcessIndicators] ([AggTypeId]);
CREATE INDEX [DataTypeId]
	ON [ProcessIndicators] ([DataTypeId]);
CREATE INDEX [InterventionTypeId]
	ON [ProcessIndicators] ([ProcessTypeId]);
CREATE INDEX [UpdatedById]
	ON [ProcessIndicators] ([UpdatedById]);
CREATE INDEX [IndicatorId]
	ON [ProcessIndicatorValues] ([IndicatorId]);
CREATE INDEX [InterventionId]
	ON [ProcessIndicatorValues] ([ProcessId]);
CREATE INDEX [UpdatedById]
	ON [ProcessIndicatorValues] ([UpdatedById]);
CREATE INDEX [CreatedById]
	ON [ProcessTypes] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [ProcessTypes] ([UpdatedById]);
CREATE INDEX [DiseaseId]
	ON [ProcessTypes_to_Diseases] ([DiseaseId]);
CREATE INDEX [UpdatedById]
	ON [SentinelSites] ([UpdatedById]);
CREATE INDEX [UpdatedById1]
	ON [SentinelSites] ([CreatedById]);
CREATE INDEX [AdminLevelId]
	ON [SentinelSites_to_AdminLevels] ([AdminLevelId]);
CREATE INDEX [AggTypeId]
	ON [SurveyIndicators] ([AggTypeId]);
CREATE INDEX [DataTypeId]
	ON [SurveyIndicators] ([DataTypeId]);
CREATE INDEX [SurveyId]
	ON [SurveyIndicators] ([SurveyTypeId]);
CREATE INDEX [IndicatorId]
	ON [SurveyIndicatorValues] ([IndicatorId]);
CREATE INDEX [SurveyId]
	ON [SurveyIndicatorValues] ([SurveyId]);
CREATE INDEX [SentinelSiteId]
	ON [SurveyLfMf] ([SentinelSiteId]);
CREATE INDEX [SurveyId]
	ON [SurveyLfMf] ([SurveyId]);
CREATE INDEX [SurveyTypeId]
	ON [SurveyLfMf] ([TimingType]);
CREATE INDEX [CreatedById]
	ON [Surveys] ([CreatedById]);
CREATE INDEX [SentinelSiteId]
	ON [Surveys] ([SentinelSiteId]);
CREATE INDEX [SurveyTypeId]
	ON [Surveys] ([SurveyTypeId]);
CREATE INDEX [UpdatedById]
	ON [Surveys] ([UpdatedById]);
CREATE INDEX [DiseaseId]
	ON [Surveys_to_AdminLevels] ([AdminLevelId]);
CREATE INDEX [CreatedById]
	ON [SurveyTypes] ([CreatedById]);
CREATE INDEX [DiseaseId]
	ON [SurveyTypes] ([DiseaseId]);
CREATE INDEX [CreatedById]
	ON [Vectors] ([CreatedById]);
CREATE INDEX [UpdatedById]
	ON [Vectors] ([UpdatedById]);
ALTER TABLE [aspnet_Membership]
	ADD CONSTRAINT [aspnet_Usersaspnet_Membership]
	FOREIGN KEY UNIQUE ([UserId]) REFERENCES
		[aspnet_Users] ([UserId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_PagePersonalizationAllUsers]
	ADD CONSTRAINT [aspnet_Urlsaspnet_UrlPersonalizationAllUsersData]
	FOREIGN KEY UNIQUE ([PathId]) REFERENCES
		[aspnet_Paths] ([PathId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_PagePersonalizationPerUser]
	ADD CONSTRAINT [aspnet_Urlsaspnet_UrlPersonalizationPerUserData]
	FOREIGN KEY ([PathId]) REFERENCES
		[aspnet_Paths] ([PathId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_PagePersonalizationPerUser]
	ADD CONSTRAINT [aspnet_Usersaspnet_UrlPersonalizationPerUserData]
	FOREIGN KEY ([UserId]) REFERENCES
		[aspnet_Users] ([UserId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_Paths]
	ADD CONSTRAINT [aspnet_Applicationsaspnet_Urls]
	FOREIGN KEY ([ApplicationId]) REFERENCES
		[aspnet_Applications] ([ApplicationId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_Profile]
	ADD CONSTRAINT [aspnet_Usersaspnet_Personalization]
	FOREIGN KEY UNIQUE ([UserId]) REFERENCES
		[aspnet_Users] ([UserId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_Roles]
	ADD CONSTRAINT [aspnet_Applicationsaspnet_Roles]
	FOREIGN KEY ([ApplicationId]) REFERENCES
		[aspnet_Applications] ([ApplicationId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_Users]
	ADD CONSTRAINT [aspnet_Applicationsaspnet_Users]
	FOREIGN KEY ([ApplicationId]) REFERENCES
		[aspnet_Applications] ([ApplicationId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_UsersInRoles]
	ADD CONSTRAINT [aspnet_Rolesaspnet_UsersInRoles]
	FOREIGN KEY ([RoleId]) REFERENCES
		[aspnet_Roles] ([RoleId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
ALTER TABLE [aspnet_UsersInRoles]
	ADD CONSTRAINT [aspnet_Usersaspnet_UsersInRoles]
	FOREIGN KEY ([UserId]) REFERENCES
		[aspnet_Users] ([UserId])
	ON UPDATE CASCADE
	ON DELETE CASCADE;
CREATE VIEW [vw_aspnet_Applications] AS SELECT [aspnet_Applications].[ApplicationName], [aspnet_Applications].[ApplicationId], [aspnet_Applications].[Description]
FROM aspnet_Applications;

CREATE VIEW [vw_aspnet_MembershipUsers] AS SELECT aspnet_Membership.UserId, aspnet_Membership.PasswordFormat, aspnet_Membership.MobilePIN, aspnet_Membership.Email, aspnet_Membership.PasswordQuestion, aspnet_Membership.PasswordAnswer, aspnet_Membership.IsApproved, aspnet_Membership.CreateDate, aspnet_Membership.LastLoginDate, aspnet_Membership.LastPasswordChangedDate, aspnet_Membership.Comment, aspnet_Users.ApplicationId, aspnet_Users.UserName, aspnet_Users.MobileAlias, aspnet_Users.IsAnonymous, aspnet_Users.LastActivityDate
FROM aspnet_Membership INNER JOIN aspnet_Users ON aspnet_Membership.UserId=aspnet_Users.UserId;

CREATE VIEW [vw_aspnet_Profiles] AS SELECT aspnet_Profile.UserId, aspnet_Profile.LastUpdatedDate, (Len(aspnet_Profile.PropertyValuesString)+Len(aspnet_Profile.PropertyNames)) AS DataSize
FROM aspnet_Profile;

CREATE VIEW [vw_aspnet_Roles] AS SELECT [aspnet_Roles].[ApplicationId], [aspnet_Roles].[RoleId], [aspnet_Roles].[RoleName], [aspnet_Roles].[Description]
FROM aspnet_Roles;

CREATE VIEW [vw_aspnet_Users] AS SELECT [aspnet_Users].[ApplicationId], [aspnet_Users].[UserId], [aspnet_Users].[UserName], [aspnet_Users].[MobileAlias], [aspnet_Users].[IsAnonymous], [aspnet_Users].[LastActivityDate]
FROM aspnet_Users;

CREATE VIEW [vw_aspnet_UsersInRoles] AS SELECT [aspnet_UsersInRoles].[UserId], [aspnet_UsersInRoles].[RoleId]
FROM aspnet_UsersInRoles;

CREATE VIEW [vw_aspnet_WebPartState_Paths] AS SELECT aspnet_Paths.ApplicationId, aspnet_Paths.PathId, aspnet_Paths.Path
FROM aspnet_Paths;

CREATE VIEW [vw_aspnet_WebPartState_Shared] AS SELECT aspnet_PagePersonalizationAllUsers.PathId, Len(aspnet_PagePersonalizationAllUsers.PageSettings) AS DataSize, aspnet_PagePersonalizationAllUsers.LastUpdatedDate
FROM aspnet_PagePersonalizationAllUsers;

CREATE VIEW [vw_aspnet_WebPartState_User] AS SELECT aspnet_PagePersonalizationPerUser.PathId, aspnet_PagePersonalizationPerUser.UserId, Len(aspnet_PagePersonalizationPerUser.PageSettings) AS DataSize, aspnet_PagePersonalizationPerUser.LastUpdatedDate
FROM aspnet_PagePersonalizationPerUser;

INSERT INTO [AdminLevels] ([ID], [DisplayName], [AdminLevelTypeId], [ParentId], [EcoZoneId], [UrbanOrRural], [LatWho], [LngWho], [LatOther], [LngOther], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [AdminLevelTypes] ([ID], [AdminLevel], [DisplayName], [IsDistrict], [IsAggregatingLevel], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, 0, "Country", 0, 0, 0, 26, #06/27/2013 12:18:53#, 26, #10/03/2013 13:53:07#);
INSERT INTO [AdminLevelTypes] ([ID], [AdminLevel], [DisplayName], [IsDistrict], [IsAggregatingLevel], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, 1, "Region", 0, 0, 0, 26, #07/03/2013 15:39:33#, 26, #10/03/2013 13:53:07#);
INSERT INTO [AdminLevelTypes] ([ID], [AdminLevel], [DisplayName], [IsDistrict], [IsAggregatingLevel], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (3, 2, "District", -1, -1, 0, 26, #07/03/2013 15:39:33#, 26, #10/03/2013 13:53:07#);
INSERT INTO [aspnet_Applications] ([ApplicationName], [ApplicationId], [Description]) VALUES ("NadaNtd", 3, NULL);
INSERT INTO [aspnet_Users] ([ApplicationId], [UserId], [UserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (3, 26, "admin", NULL, 0, #12/10/2013 18:56:17#);
INSERT INTO [aspnet_Membership] ([UserId], [Password], [PasswordFormat], [MobilePIN], [Email], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [Comment], [PasswordSalt]) VALUES (26, "Dr65e6iprzwKEEad5jOa39g9X4Y=", 1, NULL, "admin@iotaink.com", "Hood", "Seattle", -1, #06/25/2013 15:12:14#, #12/10/2013 18:56:17#, #06/25/2013 15:12:14#, NULL, "sytFeaHmQhideB2xDBurYw==");
INSERT INTO [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [Description]) VALUES (3, 17, "RoleAdmin", NULL);
INSERT INTO [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [Description]) VALUES (3, 18, "RoleDataViewer", NULL);
INSERT INTO [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [Description]) VALUES (3, 19, "RoleDataEnterer", NULL);
INSERT INTO [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (26, 18);
INSERT INTO [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (26, 17);
INSERT INTO [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (26, 19);
INSERT INTO [Country] ([ID], [AdminLevelId], [MonthYearStarts], [HasReviewedDiseases], [UpdatedById], [UpdatedAt]) VALUES (1, 1, -1, 0, 26, #12/10/2013 18:48:47#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (28, 6, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 28, 26, #10/28/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (30, 6, 1, "CaseFindingStrategy", 4, 0, 0, 0, 0, 0, 0, 0, 30, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (31, 6, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (32, 7, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 32, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (33, 7, 5, "CaseFindingStrategy", 4, 0, 0, -1, 0, 0, 0, 0, 33, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (34, 7, 2, "TotalNumNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 34, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (35, 7, 2, "TotalNumMbCases", 1, 0, 0, -1, 0, 0, 0, 0, 35, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (36, 7, 2, "TotalNumChildNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 36, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (37, 7, 2, "TotalNumFemaleNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 37, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (38, 7, 2, "PrevalenceBeginningYear", 1, 0, 0, -1, 0, 0, 0, 0, 38, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (39, 7, 2, "MbCasesRegisteredMdtBeginning", 1, 0, 0, -1, 0, 0, 0, 0, 39, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (40, 7, 2, "MbCasesRegisteredMdtEnd", 1, 0, 0, 0, 0, 0, 0, 0, 40, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (41, 7, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (42, 8, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 42, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (43, 8, 1, "CaseFindingStrategy", 1, 0, 0, 0, 0, 0, 0, 0, 43, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (44, 8, 2, "TotalNumHat", 1, 0, 0, 0, 0, 0, 0, 0, 44, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (45, 8, 2, "TotalNumHat2", 1, 0, 0, 0, 0, 0, 0, 0, 45, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (46, 8, 2, "TotalNumHatChild", 1, 0, 0, 0, 0, 0, 0, 0, 46, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (47, 8, 2, "TotalNumHatFemale", 1, 0, 0, 0, 0, 0, 0, 0, 47, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (48, 8, 2, "TotalHatConfirmedLab", 1, 0, 0, 0, 0, 0, 0, 0, 48, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (50, 9, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 50, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (51, 9, 1, "CaseFindingStrategy", 4, 0, 0, 0, 0, 0, 0, 0, 51, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (52, 9, 2, "TotalNumNewCases", 1, 0, 0, 0, 0, 0, 0, 0, 52, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (53, 9, 2, "TotalNumVlCases", 1, 0, 0, 0, 0, 0, 0, 0, 53, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (54, 9, 2, "TotalNumClCases", 1, 0, 0, 0, 0, 0, 0, 0, 54, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (55, 9, 2, "TotalNumChildNewCases", 1, 0, 0, 0, 0, 0, 0, 0, 55, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (56, 9, 2, "TotalNumFemaleNewCases", 1, 0, 0, 0, 0, 0, 0, 0, 56, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (58, 9, 2, "PrevalenceRateEndOfYear", 1, 0, 0, 0, 0, 0, 0, 0, 58, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (59, 10, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 59, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (60, 11, 5, "EndemicityStatus", 2, 0, 0, -1, 0, 0, 0, 0, 60, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (61, 10, 5, "CaseFindingStrategy", 4, 0, 0, -1, 0, 0, 0, 0, 61, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (62, 11, 5, "CaseFindingStrategy", 4, 0, 0, -1, 0, 0, 0, 0, 62, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (63, 10, 2, "TotalNumNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 63, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (64, 11, 2, "TotalNumNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 64, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (65, 10, 2, "TotalUlcerativeCases", 1, 0, 0, 0, 0, 0, 0, 0, 65, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (66, 10, 2, "TotalNumChildNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 66, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (67, 10, 2, "TotalNumFemaleNewCases", 1, 0, 0, -1, 0, 0, 0, 0, 67, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (68, 10, 2, "TotalCat1Cases", 1, 0, 0, 0, 0, 0, 0, 0, 68, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (69, 10, 2, "TotalCat2Cases", 1, 0, 0, 0, 0, 0, 0, 0, 69, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (70, 10, 2, "TotalCat3Cases", 1, 0, 0, -1, 0, 0, 0, 0, 70, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (71, 10, 2, "TotalCasesConfirmedPcr", 1, 0, 0, -1, 0, 0, 0, 0, 71, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (72, 11, 2, "TotalCasesConfirmedLab", 1, 0, 0, -1, 0, 0, 0, 0, 72, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (73, 11, 2, "TotalNumChildNewCases", 1, 0, 0, 0, 0, 0, 0, 0, 73, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (74, 11, 2, "PreSchoolCases6", 1, 0, 0, -1, 0, 0, 0, 0, 74, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (75, 11, 2, "SchoolCases14", 1, 0, 0, -1, 0, 0, 0, 0, 75, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (76, 11, 2, "TotalNumFemaleNewCases", 1, 0, 0, 0, 0, 0, 0, 0, 76, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (77, 11, 2, "CasesFromRural", 1, 0, 0, 0, 0, 0, 0, 0, 77, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (78, 11, 2, "TotalCasesTargeted", 1, 0, 0, 0, 0, 0, 0, 0, 78, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (79, 11, 2, "TotalCasesContacted", 1, 0, 0, 0, 0, 0, 0, 0, 79, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (81, 11, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (82, 10, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (83, 9, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (84, 8, 4, "DateReported", 3, 0, 0, -1, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (85, 3, 5, "DDLFDiseaseDistributionPcInterventions", 3, 0, 0, -1, 0, 0, 0, 0, 85, 26, #10/28/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (86, 3, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (91, 3, 5, "DDLFNumPcRoundsYearRecommendedByWhoGuid", 3, 0, 0, 0, 0, 0, 0, -1, 91, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (92, 3, 5, "DDLFNumPcRoundsYearCurrentlyImplemented", 3, 0, 0, 0, 0, 0, 0, -1, 92, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (93, 3, 5, "DDLFObjectiveOfPlannedTas", 5, 0, 0, 0, 0, 0, 0, 0, 93, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (94, 3, 8, "DDLFMonthOfPlannedTas", 5, 0, 0, 0, 0, 0, 0, 0, 94, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (95, 3, 7, "DDLFYearOfPlannedTas", 5, 0, 0, 0, 0, 0, 0, 0, 95, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (96, 3, 7, "DDLFYearPcStarted", 2, 0, 0, 0, 0, 0, 0, 0, 96, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (97, 3, 7, "DDLFYearDeterminedThatAchievedCriteriaFo", 2, 0, 0, 0, 0, 0, 0, 0, 97, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (98, 3, 2, "DDLFPopulationAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 98, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (99, 3, 2, "DDLFPopulationRequiringPc", 1, 0, 0, 0, 0, 0, 0, 0, 99, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (100, 3, 2, "DDLFHighRiskAdultsAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 100, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (101, 3, 2, "DDLFPopulationLivingInTheDistrictsThatAc", 1, 0, 0, 0, 0, 0, 0, 0, 101, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (102, 4, 5, "DDOnchoDiseaseDistributionPcInterventio", 3, 0, 0, -1, 0, 0, 0, 0, 102, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (103, 4, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (105, 4, 5, "DDOnchoNumPcRoundsYearRecommendedByWhoG", 3, 0, 0, 0, 0, 0, 0, -1, 105, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (106, 4, 5, "DDOnchoNumPcRoundsYearCurrentlyImplemen", 3, 0, 0, 0, 0, 0, 0, -1, 106, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (107, 4, 5, "DDOnchoSaeRiskFromLoaLoa", 3, 0, 0, 0, 0, 0, 0, -1, 107, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (108, 4, 5, "DDOnchoMethodOfLoaLoaMapping", 4, 0, 0, 0, 0, 0, 0, -1, 108, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (109, 4, 7, "DDOnchoYearPcStarted", 2, 0, 0, 0, 0, 0, 0, 0, 109, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (110, 4, 7, "DDOnchoYearDeterminedThatAchievedCriteri", 2, 0, 0, 0, 0, 0, 0, 0, 110, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (111, 4, 2, "DDOnchoPopulationAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 111, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (112, 4, 2, "DDOnchoPopulationRequiringPc", 1, 0, 0, 0, 0, 0, 0, 0, 112, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (113, 4, 2, "DDOnchoPopulationLivingInTheDistrictsTha", 1, 0, 0, 0, 0, 0, 0, 0, 113, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (114, 4, 2, "DDOnchoTotalNumOfVillagesCommunitiesInT", 1, 0, 0, 0, 0, 0, 0, 0, 114, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (115, 4, 2, "DDOnchoTotalNumEndemicVillagesCommuniti", 1, 0, 0, 0, 0, 0, 0, 0, 115, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (116, 12, 5, "DDSchistoDiseaseDistributionPcIntervent", 3, 0, 0, -1, 0, 0, 0, 0, 116, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (120, 12, 5, "DDSchistoNumPcRoundsYearRecommendedByWh", 3, 0, 0, 0, 0, 0, 0, -1, 120, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (121, 12, 5, "DDSchistoNumPcRoundsYearCurrentlyImplem", 3, 0, 0, 0, 0, 0, 0, -1, 121, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (122, 12, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (123, 12, 7, "DDSchistoYearPcStarted", 2, 0, 0, 0, 0, 0, 0, 0, 123, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (124, 12, 7, "DDSchistoYearDeterminedThatAchievedCrite", 2, 0, 0, 0, 0, 0, 0, 0, 124, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (125, 12, 2, "DDSchistoPopulationAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 125, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (126, 12, 2, "DDSchistoPopulationRequiringPc", 1, 0, 0, 0, 0, 0, 0, 0, 126, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (166, 14, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (167, 15, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (168, 16, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (169, 17, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (170, 18, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (171, 19, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (172, 11, 13, "PercentNewChildren", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (173, 11, 13, "PercentNewFemales", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (174, 7, 13, "PercentNewChildren", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (175, 7, 13, "PercentNewFemales", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (176, 7, 13, "TotalNumCasesRegistered", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (177, 7, 13, "PercentNewMb", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (178, 7, 13, "PrevalenceRateEndOfYear", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (179, 7, 13, "RateNewGrade2", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (180, 7, 13, "DetectionRate100k", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (181, 9, 13, "PercentNewChildren", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (182, 9, 13, "PercentNewFemales", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (183, 8, 13, "PercentCasesHat2", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (184, 8, 13, "PercentNewChildren", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (185, 8, 13, "PercentNewFemales", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (186, 10, 13, "PercentNewChildren", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (187, 10, 13, "PercentNewFemales", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (188, 10, 13, "PercentUlcerativeCases", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (189, 10, 13, "PercentCatICases", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (190, 10, 13, "PercentCatIICases", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (191, 10, 13, "PercentPcrCases", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (192, 10, 13, "DetectionRate100k", 5, 0, 0, 0, 0, -1, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (193, 4, 13, "DDOnchoTotalPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (194, 3, 13, "DDLFTotalPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (195, 3, 13, "DDLFPsacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (196, 3, 13, "DDLFSacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (197, 3, 13, "DDLFAdultPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (198, 12, 13, "DDSchistoTotalPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (199, 12, 13, "DDSchistoSacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (200, 12, 13, "DDSchistoAdultPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (201, 5, 13, "DDSTHTotalPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (202, 5, 13, "DDSTHPsacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (203, 5, 13, "DDSTHSacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (204, 5, 13, "DDSTHAdultPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (205, 13, 13, "DDTraTotalPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (206, 13, 13, "DDTraSacPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (207, 13, 13, "DDTraAdultPopulation", 1, 0, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (127, 12, 2, "DDSchistoSacAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 127, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (128, 12, 2, "DDSchistoHighRiskAdultsAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 128, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (129, 12, 2, "DDSchistoPopulationLivingInTheDistrictsT", 1, 0, 0, 0, 0, 0, 0, 0, 129, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (130, 5, 5, "DDSTHDiseaseDistributionPcInterventions", 3, 0, 0, -1, 0, 0, 0, 0, 130, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (135, 5, 5, "DDSTHNumPcRoundsYearRecommendedByWhoGui", 3, 0, 0, 0, 0, 0, 0, -1, 135, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (136, 5, 5, "DDSTHNumPcRoundsYearCurrentlyImplemente", 3, 0, 0, 0, 0, 0, 0, -1, 136, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (137, 5, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (138, 5, 7, "DDSTHYearPcStarted", 2, 0, 0, 0, 0, 0, 0, 0, 138, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (139, 5, 7, "DDSTHYearDeterminedThatAchievedCriteriaF", 2, 0, 0, 0, 0, 0, 0, 0, 139, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (140, 5, 2, "DDSTHPopulationAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 140, 26, #01/01/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (141, 5, 2, "DDSTHPopulationRequiringPc", 1, 0, 0, 0, 0, 0, 0, 0, 141, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (142, 5, 2, "DDSTHPsacAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 142, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (143, 5, 2, "DDSTHSacAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 143, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (144, 5, 2, "DDSTHHighRiskAdultsAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 144, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (145, 5, 2, "DDSTHPopulationLivingInTheDistrictsThatA", 1, 0, 0, 0, 0, 0, 0, 0, 145, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (146, 13, 5, "DDTraDiseaseDistributionPcInterventions", 3, 0, 0, -1, 0, 0, 0, 0, 146, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (147, 13, 5, "DDTraDiseaseDistributionTrichiasisSurge", 3, 0, 0, 0, 0, 0, 0, -1, 147, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (151, 13, 5, "DDTraNumPcRoundsYearRecommendedByWhoGui", 3, 0, 0, 0, 0, 0, 0, -1, 151, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (152, 13, 5, "DDTraNumPcRoundsYearCurrentlyImplemente", 3, 0, 0, 0, 0, 0, 0, -1, 152, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (153, 13, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 1, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (154, 13, 8, "DDTraMonthOfPlannedTrachomaImpactSurvey", 5, 0, 0, 0, 0, 0, 0, 0, 154, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (155, 13, 7, "DDTraYearOfPlannedTrachomaImpactSurvey", 5, 0, 0, 0, 0, 0, 0, 0, 155, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (156, 13, 7, "DDTraYearPcStarted", 3, 0, 0, 0, 0, 0, 0, 0, 156, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (157, 13, 7, "DDTraYearDeterminedThatAchievedCriteriaF", 3, 0, 0, 0, 0, 0, 0, 0, 157, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (158, 13, 2, "DDTraTrichiasisSurgeryBacklog", 1, 0, 0, 0, 0, 0, 0, 0, 158, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (159, 13, 2, "DDTraUltimateInterventionGoalForTrichias", 1, 0, 0, 0, 0, 0, 0, 0, 159, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (160, 13, 2, "DDTraUltimateInterventionGoalForActiveTr", 1, 0, 0, 0, 0, 0, 0, 0, 160, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (161, 13, 2, "DDTraPopulationAtRisk", 1, 0, 0, 0, 0, 0, 0, 0, 161, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (162, 13, 2, "DDTraPopulationLivingInAreasDistrict", 1, 0, 0, 0, 0, 0, 0, 0, 162, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicators] ([ID], [DiseaseId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [IsMetaData], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (163, 13, 2, "DDTraPopulationLivingInAreasSubDistrict", 1, 0, 0, 0, 0, 0, 0, 0, 163, 26, #11/02/2013#);
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (15, 28, 2, "Endemic", 26, "2013-11-02 09:58:13");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (16, 30, 2, "456", 26, "2013-11-02 09:58:13");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (17, 31, 2, "2010", 26, "2013-11-02 09:58:13");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (18, 28, 3, "Endemic", 26, "2013-11-07 16:10:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (19, 30, 3, "Test", 26, "2013-11-07 16:10:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (20, 31, 3, "2010", 26, "2013-11-07 16:10:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (21, 28, 4, "Endemic", 26, "2013-11-07 16:11:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (22, 30, 4, "asf", 26, "2013-11-07 16:11:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (23, 31, 4, "2010", 26, "2013-11-07 16:11:48");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (24, 28, 5, "NotEndemic", 26, "2013-11-07 16:14:27");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (25, 30, 5, "20", 26, "2013-11-07 16:14:27");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (26, 31, 5, "2010", 26, "2013-11-07 16:14:27");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (27, 28, 6, "EndemicityTbv", 26, "2013-11-07 16:16:24");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (28, 30, 6, "asdf", 26, "2013-11-07 16:16:24");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (29, 31, 6, "2010", 26, "2013-11-07 16:16:24");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (30, 86, 7, "2012", 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (31, 85, 7, "40", 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (32, 91, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (33, 92, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (34, 93, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (35, 94, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (36, 95, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (37, 96, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (38, 97, 7, NULL, 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (39, 98, 7, "20", 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (40, 99, 7, "10", 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (41, 100, 7, "50", 26, "2013-12-10 18:50:01");
INSERT INTO [DiseaseDistributionIndicatorValues] ([ID], [IndicatorId], [DiseaseDistributionId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (42, 101, 7, "20", 26, "2013-12-10 18:50:01");
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (6, "Dracun", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (7, "Leprosy", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (8, "HAT", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (9, "Leishmaniasis", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (10, "BuruliUlcer", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (11, "YAWS", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (12, "Schisto", "PC", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (13, "Trachoma", "PC", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/22/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (14, "Chagas", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (15, "Dengue", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (16, "Rabies", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (17, "Echino", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (18, "Foodborne", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (19, "Taeniasis", "CM", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (20, "Custom", "CM", NULL, -1, 0, -1, 26, #12/10/2013 18:48:47#, 26, #10/01/2013#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (3, "LF", "PC", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, "Oncho", "PC", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Diseases] ([ID], [DisplayName], [DiseaseType], [UserDefinedName], [IsSelected], [IsDeleted], [IsHidden], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (5, "STH", "PC", NULL, 0, 0, 0, 26, #12/10/2013 18:48:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [ExportCmJrfQuestions] ([ID], [YearReporting], [CmHaveMasterPlan], [CmYearsMasterPlan], [CmBuget], [CmPercentFunded], [CmHaveAnnualOpPlan], [CmDiseaseSpecOrNtdIntegrated], [CmBuHasPlan], [CmGwHasPlan], [CmHatHasPlan], [CmLeishHasPlan], [CmLeprosyHasPlan], [CmYawsHasPlan], [CmAnySupplyFunds], [CmHasStorage], [CmStorageNtdOrCombined], [CmStorageSponsor1], [CmStorageSponsor2], [CmStorageSponsor3], [CmStorageSponsor4], [CmHasTaskForce], [CmHasMoh], [CmHasMosw], [CmHasMot], [CmHasMoe], [CmHasMoc], [CmHasUni], [CmHasNgo], [CmHasAnnualForum], [CmForumHasRegions], [CmForumHasTaskForce], [CmHasNtdReviewMeetings], [CmHasDiseaseSpecMeetings], [CmHasGwMeeting], [CmHasLeprosyMeeting], [CmHasHatMeeting], [CmHasLeishMeeting], [CmHasBuMeeting], [CmHasYawsMeeting], [CmHasWeeklyMech], [CmHasMonthlyMech], [CmHasQuarterlyMech], [CmHasSemesterMech], [CmOtherMechs]) VALUES (1, NULL, 0, NULL, NULL, NULL, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL);
INSERT INTO [ExportJrfQuestions] ([ID], [JrfYearReporting], [JrfEndemicLf], [JrfEndemicOncho], [JrfEndemicSth], [JrfEndemicSch]) VALUES (1, NULL, NULL, NULL, NULL, NULL);
INSERT INTO [IndicatorAggType] ([ID], [TypeName]) VALUES (1, "Sum");
INSERT INTO [IndicatorAggType] ([ID], [TypeName]) VALUES (2, "Min");
INSERT INTO [IndicatorAggType] ([ID], [TypeName]) VALUES (3, "Max");
INSERT INTO [IndicatorAggType] ([ID], [TypeName]) VALUES (4, "Combine");
INSERT INTO [IndicatorAggType] ([ID], [TypeName]) VALUES (5, "None");
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (1, 442, 3, 114, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (2, 442, 3, 113, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (3, 441, 3, 89, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (4, 441, 3, 88, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (5, 443, 3, 134, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (6, 443, 3, 133, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (9, 446, 3, 134, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (10, 446, 3, 140, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (11, 447, 3, 134, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (12, 447, 3, 142, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (13, 448, 3, 134, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (14, 448, 3, 144, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (15, 449, 3, 160, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (16, 449, 3, 161, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (17, 450, 3, 165, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (18, 450, 3, 166, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (19, 451, 3, 197, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (20, 451, 3, 198, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (21, 452, 3, 182, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (22, 452, 3, 183, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (23, 453, 3, 192, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (24, 453, 3, 193, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (25, 454, 3, 187, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (26, 454, 3, 188, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (27, 221, 3, 219, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (28, 221, 3, 220, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (29, 224, 3, 222, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (30, 224, 3, 223, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (31, 227, 3, 225, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (32, 227, 3, 226, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (33, 230, 3, 228, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (34, 230, 3, 229, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (35, 218, 3, 216, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (36, 218, 3, 217, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (37, 258, 3, 256, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (38, 258, 3, 257, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (39, 455, 3, 288, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (40, 455, 3, 291, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (41, 318, 3, 317, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (42, 318, 3, 316, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (45, 456, 3, 319, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (46, 456, 3, 317, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (47, 457, 3, 434, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (48, 457, 3, 317, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (49, 458, 3, 317, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (50, 458, 3, 436, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (51, 459, 3, 438, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (52, 459, 3, 317, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (53, 335, 3, 333, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (54, 335, 3, 334, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (55, 340, 3, 338, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (56, 340, 3, 339, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (57, 360, 3, 358, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (58, 360, 3, 359, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (59, 365, 3, 364, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (60, 365, 3, 363, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (61, 370, 3, 368, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (62, 370, 3, 369, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (63, 375, 3, 373, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (64, 375, 3, 374, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (65, 394, 3, 392, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (66, 394, 3, 393, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (67, 397, 3, 396, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (68, 397, 3, 395, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (69, 400, 3, 399, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (70, 400, 3, 398, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (71, 403, 3, 401, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (72, 403, 3, 402, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (73, 406, 3, 405, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (74, 406, 3, 404, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (75, 186, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (76, 187, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (77, 188, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (78, 189, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (79, 190, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (80, 191, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (81, 192, 1, 63, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (82, 186, 1, 66, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (83, 187, 1, 67, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (84, 188, 1, 65, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (85, 189, 1, 68, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (86, 190, 1, 70, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (87, 191, 1, 71, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (88, 183, 1, 44, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (89, 184, 1, 44, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (90, 185, 1, 44, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (91, 183, 1, 45, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (92, 184, 1, 46, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (93, 185, 1, 47, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (94, 181, 1, 52, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (95, 181, 1, 55, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (96, 182, 1, 52, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (97, 182, 1, 56, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (98, 172, 1, 64, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (99, 172, 1, 73, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (100, 173, 1, 64, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (101, 173, 1, 76, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (115, 180, 1, 34, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (116, 178, 1, 40, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (117, 177, 1, 34, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (118, 177, 1, 35, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (119, 174, 1, 34, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (120, 174, 1, 36, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (121, 175, 1, 34, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (122, 175, 1, 37, 1);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (123, 332, 2, 155, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (124, 333, 2, 73, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (125, 333, 2, 75, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (126, 334, 2, 67, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (127, 334, 2, 68, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (128, 335, 2, 69, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (129, 335, 2, 70, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (130, 336, 2, 71, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (131, 336, 2, 72, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (132, 337, 2, 73, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (133, 337, 2, 78, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (134, 338, 2, 80, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (135, 338, 2, 81, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (136, 361, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (137, 361, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (138, 361, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (139, 362, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (140, 362, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (141, 362, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (142, 362, 2, 127, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (144, 363, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (145, 363, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (146, 363, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (147, 364, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (148, 364, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (149, 364, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (153, 366, 2, 130, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (154, 366, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (155, 366, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (156, 366, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (157, 339, 2, 109, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (158, 339, 2, 110, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (159, 340, 2, 111, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (160, 340, 2, 110, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (161, 341, 2, 112, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (162, 341, 2, 110, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (163, 342, 2, 109, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (164, 342, 2, 113, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (165, 343, 2, 109, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (166, 343, 2, 114, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (150, 365, 2, 126, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (151, 365, 2, 128, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (152, 365, 2, 129, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (167, 344, 2, 118, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (168, 344, 2, 115, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (169, 345, 2, 115, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (170, 345, 2, 119, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (171, 346, 2, 115, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (172, 346, 2, 120, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (173, 347, 2, 109, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (174, 347, 2, 121, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (175, 348, 2, 109, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (176, 355, 2, 167, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (177, 355, 2, 168, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (178, 252, 2, 231, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (179, 252, 2, 238, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (180, 253, 2, 238, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (181, 253, 2, 238, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (182, 254, 2, 238, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (183, 254, 2, 239, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (184, 255, 2, 238, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (185, 255, 2, 240, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (186, 256, 2, 234, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (187, 256, 2, 241, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (188, 257, 2, 235, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (189, 257, 2, 242, 2);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (202, 445, 3, 136, 3);
INSERT INTO [IndicatorCalculations] ([ID], [IndicatorId], [EntityTypeId], [RelatedIndicatorId], [RelatedEntityTypeId]) VALUES (203, 445, 3, 134, 3);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (1, "Text", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (2, "Number", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (3, "Yes/No", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (4, "Date", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (5, "Dropdown", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (6, "Multiselect", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (7, "Year", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (8, "Month", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (9, "Partners", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (10, "SentinelSpotCheck", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (11, "EcologicalZone", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (12, "EvaluationUnit", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (13, "Calculated", 0);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (14, "EvalSubDistrict", -1);
INSERT INTO [IndicatorDataTypes] ([ID], [DataType], [HasValueList]) VALUES (15, "LargeText", 0);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (29, 61, 4, 23, "Trainers", "Trainers", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (30, 61, 4, 24, "RegionalLevelSup", "RegionalLevelSup", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (31, 61, 4, 25, "DistrictLevelSup", "DistrictLevelSup", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (32, 61, 4, 26, "DrugDistrib", "DrugDistrib", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (33, 61, 4, 27, "MandE", "MandE", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (34, 61, 4, 28, "FinanceAcct", "FinanceAcct", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (35, 85, 1, 1, "LfEndM", "LfEndM", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (36, 85, 1, 2, "LfEndNs", "LfEndNs", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (37, 85, 1, 3, "LfEnd0", "LfEnd0", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (38, 85, 1, 4, "LfEnd1", "LfEnd1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (39, 85, 1, 5, "LfEnd100", "LfEnd100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (40, 85, 1, 6, "LfEndPending", "LfEndPending", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (41, 91, 1, 7, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (42, 91, 1, 8, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (43, 91, 1, 9, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (44, 92, 1, 10, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (45, 92, 1, 11, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (46, 92, 1, 12, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (47, 105, 1, 13, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (48, 105, 1, 14, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (49, 105, 1, 15, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (50, 106, 1, 16, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (51, 106, 1, 17, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (52, 106, 1, 18, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (53, 151, 1, 19, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (54, 151, 1, 20, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (55, 152, 1, 21, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (56, 152, 1, 22, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (57, 120, 1, 4, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (58, 120, 1, 2, "xyear1every2", "xyear1every2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (59, 120, 1, 3, "xyear1every3", "xyear1every3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (60, 120, 1, 1, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (61, 120, 1, 5, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (62, 121, 1, 4, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (63, 121, 1, 2, "xyear1every2", "xyear1every2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (64, 121, 1, 3, "xyear1every3", "xyear1every3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (65, 121, 1, 1, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (66, 121, 1, 5, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (67, 135, 1, 1, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (68, 135, 1, 3, "xyear1every2", "xyear1every2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (69, 135, 1, 4, "xyear3", "xyear3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (70, 135, 1, 2, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (71, 135, 1, 5, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (72, 136, 1, 1, "xyear2", "xyear2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (73, 136, 1, 3, "xyear1every2", "xyear1every2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (74, 136, 1, 4, "xyear3", "xyear3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (75, 136, 1, 2, "xyear1", "xyear1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (76, 136, 1, 5, "None", "None", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (77, 93, 1, 17, "LfStopMdaTas", "LfStopMdaTas", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (78, 93, 1, 18, "LfPostMdaTas1", "LfPostMdaTas1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (79, 93, 1, 19, "LfPostMdaTas2", "LfPostMdaTas2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (80, 102, 1, 20, "OnchoM", "OnchoM", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (81, 102, 1, 21, "OnchoNs", "OnchoNs", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (82, 102, 1, 22, "Oncho0", "Oncho0", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (10, 50, 1, 10, "NotEndemic", "NotEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (11, 50, 1, 11, "UnknownEndemicity", "UnknownEndemicity", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (12, 59, 1, 12, "Endemic", "Endemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (13, 59, 1, 13, "NotEndemic", "NotEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (14, 59, 1, 14, "UnknownEndemicity", "UnknownEndemicity", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (15, 60, 1, 15, "Endemic", "Endemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (16, 60, 1, 16, "NotEndemic", "NotEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (17, 60, 1, 17, "UnknownEndemicity", "UnknownEndemicity", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (18, 61, 1, 18, "Active", "Active", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (19, 61, 1, 19, "Passive", "Passive", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (20, 61, 1, 20, "Combined", "Combined", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (21, 62, 1, 21, "Active", "Active", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (22, 62, 1, 22, "Passive", "Passive", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (23, 62, 1, 23, "Combined", "Combined", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, 28, 1, 1, "Endemic", "Endemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, 28, 1, 2, "NotEndemic", "NotEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (3, 28, 1, 3, "EndemicityTbv", "EndemicityTbv", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, 32, 1, 4, "Low", "Low", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (5, 32, 1, 5, "High", "High", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (6, 42, 1, 6, "Endemic", "Endemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (7, 42, 1, 7, "NotEndemic", "NotEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (8, 42, 1, 8, "FormerlyEndemic", "FormerlyEndemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (9, 50, 1, 9, "Endemic", "Endemic", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (24, 125, 2, 24, "Melarsoprol", "Melarsoprol", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (25, 125, 2, 25, "Lomidine", "Lomidine", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (26, 125, 2, 26, "NifurtimoxNECT", "NifurtimoxNECT", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (27, 125, 2, 27, "Fexinidazole", "Fexinidazole", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (28, 125, 2, 28, "Other", "Other", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (83, 102, 1, 23, "Oncho1", "Oncho1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (84, 102, 1, 24, "Oncho100", "Oncho100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (85, 102, 1, 25, "OnchoPending", "OnchoPending", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (86, 107, 1, 26, "Oncho2NotMapped", "Oncho2NotMapped", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (87, 107, 1, 27, "OnchoNotSuspect", "OnchoNotSuspect", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (88, 107, 1, 28, "OnchoSaeUnlikely", "OnchoSaeUnlikely", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (89, 107, 1, 29, "OnchoSaeLikely", "OnchoSaeLikely", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (90, 107, 1, 30, "OnchoSaeHigh", "OnchoSaeHigh", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (91, 108, 1, 31, "Oncho3Parasite", "Oncho3Parasite", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (92, 108, 1, 32, "OnchoRaploa", "OnchoRaploa", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (93, 116, 1, 1, "Scho0", "Scho0", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (94, 116, 1, 2, "Sch1", "Sch1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (95, 116, 1, 3, "Sch2", "Sch2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (96, 116, 1, 4, "Sch2a", "Sch2a", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (97, 116, 1, 5, "Sch2b", "Sch2b", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (98, 116, 1, 6, "Sch3", "Sch3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (99, 116, 1, 7, "Sch3a", "Sch3a", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (100, 116, 1, 8, "Sch3b", "Sch3b", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (101, 116, 1, 9, "Sch10", "Sch10", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (102, 116, 1, 10, "Sch20", "Sch20", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (103, 116, 1, 11, "Sch30", "Sch30", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (104, 116, 1, 12, "Sch40", "Sch40", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (105, 116, 1, 13, "Sch100", "Sch100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (106, 116, 1, 14, "SchPending", "SchPending", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (107, 130, 1, 15, "SthM", "SthM", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (108, 130, 1, 16, "SthNs", "SthNs", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (109, 130, 1, 17, "Sth0", "Sth0", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (110, 130, 1, 18, "Sth1", "Sth1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (111, 130, 1, 19, "Sth2", "Sth2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (112, 130, 1, 20, "Sth3", "Sth3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (113, 130, 1, 21, "Sth10", "Sth10", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (114, 130, 1, 22, "Sth20", "Sth20", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (115, 130, 1, 23, "Sth30", "Sth30", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (116, 130, 1, 24, "Sth40", "Sth40", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (117, 130, 1, 25, "Sth100", "Sth100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (118, 130, 1, 26, "SthPending", "SthPending", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (119, 146, 1, 1, "TraM", "TraM", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (120, 146, 1, 2, "TraNs", "TraNs", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (121, 146, 1, 3, "Tra0", "Tra0", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (122, 146, 1, 4, "Tra1", "Tra1", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (123, 146, 1, 5, "Tra2", "Tra2", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (124, 146, 1, 6, "Tra3", "Tra3", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (125, 146, 1, 7, "Tra4", "Tra4", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (126, 146, 1, 8, "Tra5", "Tra5", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (127, 146, 1, 9, "Tra100", "Tra100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (128, 146, 1, 10, "TraPending", "TraPending", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (129, 147, 1, 11, "Tra2M", "Tra2M", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (130, 147, 1, 12, "Tra2Ns", "Tra2Ns", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (131, 147, 1, 13, "Tra20", "Tra20", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (132, 147, 1, 14, "Tra21", "Tra21", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (133, 147, 1, 15, "Tra2100", "Tra2100", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (134, 33, 1, 1, "Active", "Active", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (135, 33, 1, 2, "Passive", "Passive", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (136, 33, 1, 3, "Combined", "Combined", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (138, 227, 2, 1, "0", NULL, NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (139, 227, 2, 2, "1", NULL, NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (140, 227, 2, 3, "2", NULL, NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (141, 227, 2, 4, "3", NULL, NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (142, 227, 2, 5, "4", NULL, NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (143, 249, 2, 6, "Yes", "Yes", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (144, 249, 2, 7, "No", "No", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (145, 249, 2, 8, "NA", "NA", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (146, 250, 2, 9, "DrpDec", "DrpDec", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (147, 250, 2, 10, "DrpDecAlb", "DrpDecAlb", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (148, 250, 2, 11, "DrpIvm", "DrpIvm", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (149, 250, 2, 12, "DrpIvmAlb", "DrpIvmAlb", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (150, 250, 2, 13, "DrpAlb", "DrpAlb", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (151, 251, 2, 14, "IntvDrp1day", "IntvDrp1day", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (152, 251, 2, 15, "IntvDrp3days", "IntvDrp3days", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (153, 251, 2, 16, "IntvDrp3weeks", "IntvDrp3weeks", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (154, 251, 2, 17, "IntvDrp1month", "IntvDrp1month", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (155, 251, 2, 18, "IntvDrpNotResolved", "IntvDrpNotResolved", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (156, 251, 2, 19, "IntvDrpNa", "IntvDrpNa", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (157, 226, 2, 1, "LF", "LF", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (158, 226, 2, 2, "Oncho", "Oncho", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (159, 226, 2, 3, "STH", "STH", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (160, 259, 2, 4, "LF", "LF", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (161, 259, 2, 5, "STH", "STH", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (162, 260, 2, 6, "Oncho", "Oncho", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (163, 261, 2, 7, "Schisto", "Schisto", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (164, 261, 2, 8, "STH", "STH", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (165, 262, 2, 9, "Schisto", "Schisto", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (166, 262, 2, 10, "STH", "STH", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (167, 263, 2, 11, "Schisto", "Schisto", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (168, 264, 2, 12, "STH", "STH", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (169, 265, 2, 13, "STH", "STH", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (170, 266, 2, 14, "LF", "LF", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (171, 267, 2, 15, "Oncho", "Oncho", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (172, 267, 2, 16, "Schisto", "Schisto", NULL, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (173, 268, 2, 17, "LF", "LF", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (174, 268, 2, 18, "Oncho", "Oncho", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (175, 268, 2, 19, "STH", "STH", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (176, 269, 2, 20, "Trachoma", "Trachoma", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (177, 270, 2, 21, "Trachoma", "Trachoma", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (178, 271, 2, 22, "Trachoma", "Trachoma", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (179, 272, 2, 1, "DrugIvm", "DrugIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (180, 272, 2, 2, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (181, 272, 2, 3, "DrugAlbIvm", "DrugAlbIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (182, 273, 2, 4, "DrugDec", "DrugDec", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (183, 273, 2, 5, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (184, 273, 2, 6, "DrugDecAlb", "DrugDecAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (185, 274, 2, 7, "DrugIvm", "DrugIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (186, 275, 2, 8, "DrugPzq", "DrugPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (187, 275, 2, 9, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (188, 275, 2, 10, "DrugPzqAlb", "DrugPzqAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (189, 277, 2, 11, "DrugPzq", "DrugPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (190, 278, 2, 12, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (191, 279, 2, 13, "DrugMbd", "DrugMbd", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (192, 280, 2, 14, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (193, 281, 2, 15, "DrugIvm", "DrugIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (194, 281, 2, 16, "DrugPzq", "DrugPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (195, 281, 2, 17, "DrugIvmPzq", "DrugIvmPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (196, 282, 2, 18, "DrugIvm", "DrugIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (197, 282, 2, 19, "DrugPzq", "DrugPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (198, 282, 2, 20, "DrugAlb", "DrugAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (199, 282, 2, 21, "DrugIvmPzq", "DrugIvmPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (200, 282, 2, 22, "DrugAlbIvm", "DrugAlbIvm", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (201, 282, 2, 23, "DrugPzqAlb", "DrugPzqAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (202, 282, 2, 24, "DrugIvmPzqAlb", "DrugIvmPzqAlb", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (203, 283, 2, 25, "DrugZithro", "DrugZithro", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (204, 284, 2, 26, "DrugTeo", "DrugTeo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (205, 285, 2, 27, "DrugZithro", "DrugZithro", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (206, 285, 2, 28, "DrugTeo", "DrugTeo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (207, 285, 2, 29, "DrugZirthroTeo", "DrugZirthroTeo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (208, 276, 2, 30, "DrugPzq", "DrugPzq", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (209, 276, 2, 31, "DrugMbd", "DrugMbd", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (210, 276, 2, 32, "DrugPzqMbd", "DrugPzqMbd", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (211, 295, 2, 1, "Adequate", "Adequate", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (212, 295, 2, 2, "TsInsufficient", "TsInsufficient", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (213, 296, 2, 3, "Adequate", "Adequate", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (214, 296, 2, 4, "TsInsufficient", "TsInsufficient", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (215, 297, 2, 5, "Adequate", "Adequate", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (216, 297, 2, 6, "TsInsufficient", "TsInsufficient", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (217, 298, 2, 7, "FreeOfCharge", "FreeOfCharge", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (218, 298, 2, 8, "PriceRecoverySystem", "PriceRecoverySystem", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (219, 299, 2, 9, "Yes", "Yes", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (220, 299, 2, 10, "No", "No", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (221, 300, 2, 11, "Yes", "Yes", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (222, 300, 2, 12, "No", "No", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (223, 301, 2, 1, "Yes", "Yes", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (224, 301, 2, 2, "No", "No", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (225, 302, 2, 1, "Yes", "Yes", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (226, 302, 2, 2, "No", "No", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (227, 308, 2, 1, "MoH", "MoH", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (228, 308, 2, 2, "MoE", "MoE", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (229, 308, 2, 3, "UNICEF", "UNICEF", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (230, 308, 2, 4, "WhoCountryOffice", "WhoCountryOffice", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (231, 309, 2, 1, "MoW", "MoW", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (232, 309, 2, 2, "MoA", "MoA", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (233, 101, 3, 1, "Culex", "Culex", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (234, 101, 3, 2, "Anopheles", "Anopheles", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (235, 101, 3, 3, "Aedes", "Aedes", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (236, 101, 3, 4, "Mansonia", "Mansonia", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (237, 102, 3, 5, "Wbancrofti", "Wbancrofti", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (238, 102, 3, 6, "Brugiaspp", "Brugiaspp", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (239, 102, 3, 7, "WbancroftiBrugia", "WbancroftiBrugia", 7, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (240, 105, 3, 8, "Baseline", "Baseline", 8, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (241, 105, 3, 9, "Midterm", "Midterm", 9, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (242, 105, 3, 10, "TasEligibility", "TasEligibility", 10, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (243, 106, 3, 11, "Mf", "Mf", 11, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (244, 106, 3, 12, "ICT", "ICT", 12, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (245, 119, 3, 13, "Phase1aEval", "Phase1aEval", 13, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (246, 119, 3, 14, "Phase2bEval", "Phase2bEval", 14, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (247, 119, 3, 15, "Phase2Eval", "Phase2Eval", 15, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (248, 119, 3, 16, "Phase3Eval", "Phase3Eval", 16, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (249, 171, 3, 17, "Ascaris", "Ascaris", 17, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (250, 171, 3, 18, "Trichuris", "Trichuris", 18, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (251, 171, 3, 19, "Necator", "Necator", 19, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (252, 171, 3, 20, "Ancylostoma", "Ancylostoma", 20, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (253, 206, 3, 21, "Prevalence", "Prevalence", 21, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (254, 206, 3, 22, "Tra", "Tra", 22, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (255, 206, 3, 23, "Other", "Other", 23, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (256, 208, 3, 24, "Tf", "Tf", 24, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (257, 208, 3, 25, "Tt", "Tt", 25, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (258, 208, 3, 26, "Ti", "Ti", 26, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (259, 208, 3, 27, "Ts", "Ts", 27, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (260, 208, 3, 28, "Co", "Co", 28, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (261, 233, 3, 29, "DistrictLevel", "DistrictLevel", 29, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (262, 233, 3, 30, "SubDistrictLevel", "SubDistrictLevel", 30, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (263, 233, 3, 31, "Targeted", "Targeted", 31, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (264, 233, 3, 32, "NoAneeded", "NoAneeded", 32, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (265, 233, 3, 33, "NonEndemic", "NonEndemic", 33, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (266, 243, 3, 34, "LfStopMdaTas", "LfStopMdaTas", 34, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (267, 243, 3, 35, "PostMdaTasI", "PostMdaTasI", 35, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (268, 243, 3, 36, "PostMdaTasII", "PostMdaTasII", 36, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (269, 249, 3, 37, "Cluster", "Cluster", 37, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (270, 249, 3, 38, "Systematic", "Systematic", 38, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (271, 249, 3, 39, "Census", "Census", 39, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (272, 280, 3, 40, "Mapping", "Mapping", 40, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (273, 280, 3, 41, "Remapping", "Remapping", 41, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (274, 300, 3, 42, "EndemicAbovePc", "EndemicAbovePc", 42, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (275, 300, 3, 43, "EndemicButPcNotReq", "EndemicButPcNotReq", 43, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (276, 300, 3, 44, "NonEndemic", "NonEndemic", 44, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (277, 344, 3, 45, "HighRiskComm", "HighRiskComm", 45, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (278, 344, 3, 46, "ModRiskComm", "ModRiskComm", 46, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (279, 344, 3, 47, "LowRiskComm", "LowRiskComm", 47, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (280, 344, 3, 48, "NonEndComm", "NonEndComm", 48, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (281, 382, 3, 49, "Prevalence", "Prevalence", 49, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (282, 382, 3, 50, "Tra", "Tra", 50, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (283, 382, 3, 51, "Other", "Other", 51, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (284, 383, 3, 52, "AdminLevel1", "AdminLevel1", 52, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (285, 383, 3, 53, "AdminLevel2", "AdminLevel2", 53, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (286, 383, 3, 54, "AdminLevel3", "AdminLevel3", 54, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (287, 383, 3, 55, "Other", "Other", 55, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (288, 409, 3, 56, "Endemic", "Endemic", 56, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (289, 409, 3, 57, "NonEndemic", "NonEndemic", 57, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (290, 410, 3, 58, "DistrictLevel", "DistrictLevel", 58, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (291, 410, 3, 59, "SubDistrictLevel", "SubDistrictLevel", 59, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (292, 410, 3, 60, "Targeted", "Targeted", 60, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (293, 410, 3, 61, "NoAneeded", "NoAneeded", 61, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (294, 410, 3, 62, "NonEndemic", "NonEndemic", 62, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (295, 120, 3, 63, "Yes", "Yes", 63, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (296, 120, 3, 64, "No", "No", 64, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (297, 127, 3, 65, "MfSkinSnip", "MfSkinSnip", 65, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (298, 127, 3, 66, "DecPatch", "DecPatch", 66, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (299, 127, 3, 67, "Ov16", "Ov16", 67, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (300, 127, 3, 68, "Entomological", "Entomological", 68, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (301, 127, 3, 69, "NodulePalpation", "NodulePalpation", 69, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (302, 149, 3, 70, "Shaematobium", "Shaematobium", 70, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (303, 149, 3, 71, "Smansoni", "Smansoni", 71, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (304, 149, 3, 72, "Sjaponicum", "Sjaponicum", 72, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (305, 149, 3, 73, "Smekongi", "Smekongi", 73, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (306, 149, 3, 74, "Sintercalatum", "Sintercalatum", 74, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (307, 149, 3, 75, "Sguineesis", "Sguineesis", 75, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (308, 152, 3, 76, "Baseline", "Baseline", 76, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (309, 152, 3, 77, "Midterm", "Midterm", 77, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (310, 152, 3, 78, "Evaluation", "Evaluation", 78, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (311, 153, 3, 79, "KatoKat", "KatoKat", 79, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (312, 153, 3, 80, "Questionnaire", "Questionnaire", 80, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (313, 153, 3, 81, "Cca", "Cca", 81, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (314, 153, 3, 82, "UrineFiltration", "UrineFiltration", 82, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (315, 153, 3, 83, "Dipstick", "Dipstick", 83, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (316, 207, 3, 84, "AdminLevel1", "AdminLevel1", 84, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (317, 207, 3, 85, "AdminLevel2", "AdminLevel2", 85, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (318, 207, 3, 86, "AdminLevel3", "AdminLevel3", 86, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (319, 207, 3, 87, "Other", "Other", 87, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (342, 174, 3, 1, "Baseline", "Baseline", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (343, 174, 3, 2, "Midterm", "Midterm", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (344, 174, 3, 3, "Evaluation", "Evaluation", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (345, 175, 3, 4, "KatoKat", "KatoKat", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (346, 239, 3, 5, "Wbancrofti", "Wbancrofti", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (347, 239, 3, 6, "Brugiaspp", "Brugiaspp", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (348, 239, 3, 7, "WbancroftiBrugia", "WbancroftiBrugia", 7, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (349, 277, 3, 8, "Culex", "Culex", 8, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (350, 277, 3, 9, "Anopheles", "Anopheles", 9, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (351, 277, 3, 10, "Aedes", "Aedes", 10, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (352, 277, 3, 11, "Mansonia", "Mansonia", 11, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (353, 279, 3, 12, "Mf", "Mf", 12, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (354, 279, 3, 13, "ICT", "ICT", 13, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (355, 303, 3, 14, "Yes", "Yes", 14, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (356, 303, 3, 15, "No", "No", 15, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (357, 310, 3, 16, "MfSkinSnip", "MfSkinSnip", 16, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (358, 310, 3, 17, "DecPatch", "DecPatch", 17, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (359, 310, 3, 18, "NodulePalpation", "NodulePalpation", 18, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (360, 322, 3, 19, "YesSentinelSite", "YesSentinelSite", 19, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (362, 322, 3, 21, "NotYetDetermined", "NotYetDetermined", 21, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (363, 322, 3, 22, "No", "No", 22, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (364, 325, 3, 23, "Shaematobium", "Shaematobium", 23, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (365, 325, 3, 24, "Smansoni", "Smansoni", 24, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (366, 325, 3, 25, "Sjaponicum", "Sjaponicum", 25, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (367, 325, 3, 26, "Smekongi", "Smekongi", 26, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (368, 325, 3, 27, "Sintercalatum", "Sintercalatum", 27, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (369, 325, 3, 28, "Sguineesis", "Sguineesis", 28, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (370, 343, 3, 29, "EndemicAbovePc", "EndemicAbovePc", 29, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (371, 343, 3, 30, "EndemicButPcNotReq", "EndemicButPcNotReq", 30, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (372, 343, 3, 31, "NonEndemic", "NonEndemic", 31, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (373, 350, 3, 32, "Ascaris", "Ascaris", 32, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (374, 350, 3, 33, "Trichuris", "Trichuris", 33, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (375, 350, 3, 34, "Necator", "Necator", 34, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (376, 350, 3, 35, "Ancylostoma", "Ancylostoma", 35, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (377, 379, 3, 36, "HighRiskComm", "HighRiskComm", 36, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (378, 379, 3, 37, "LowRiskComm", "LowRiskComm", 37, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (379, 379, 3, 38, "NonEndComm", "NonEndComm", 38, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (380, 384, 3, 39, "Tf", "Tf", 39, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (381, 384, 3, 40, "Tt", "Tt", 40, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (382, 384, 3, 41, "Ti", "Ti", 41, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (383, 384, 3, 42, "Ts", "Ts", 42, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (384, 384, 3, 43, "Co", "Co", 43, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (385, 389, 3, 44, "Females", "Females", 44, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (386, 389, 3, 45, "Males", "Males", 45, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (387, 389, 3, 46, "FemalesAndMales", "FemalesAndMales", 46, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (388, 351, 3, 4, "KatoKat", "KatoKat", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (389, 278, 3, 1, "Wbancrofti", "Wbancrofti", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (390, 278, 3, 2, "Brugiaspp", "Brugiaspp", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (391, 278, 3, 3, "WbancroftiBrugia", "WbancroftiBrugia", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (392, 326, 3, 4, "KatoKat", "KatoKat", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (393, 326, 3, 5, "Questionnaire", "Questionnaire", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (394, 326, 3, 6, "Cca", "Cca", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (395, 326, 3, 7, "UrineFiltration", "UrineFiltration", 7, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (396, 326, 3, 8, "Dipstick", "Dipstick", 8, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (397, 347, 3, 9, "YesSentinelSite", "YesSentinelSite", 9, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (399, 347, 3, 11, "NotYetDetermined", "NotYetDetermined", 11, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (400, 347, 3, 12, "No", "No", 12, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (401, 378, 3, 13, "EndemicAbovePc", "EndemicAbovePc", 13, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (402, 378, 3, 14, "EndemicButPcNotReq", "EndemicButPcNotReq", 14, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (403, 378, 3, 15, "NonEndemic", "NonEndemic", 15, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (404, 417, 3, -1, "NoEz", "NoEz", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (405, 418, 3, -1, "NoEz", "NoEz", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (406, 419, 3, -1, "NoEz", "NoEz", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (407, 420, 3, -1, "NoEz", "NoEz", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (408, 116, 1, -1, "SthM", "SthM", -1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (409, 116, 1, 0, "SthNs", "SthNs", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (410, 70, 4, 1, "SCMDropBottle", "SCMDropBottle", 1, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (411, 70, 4, 2, "SCMDropTablet", "SCMDropTablet", 2, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (412, 70, 4, 3, "SCMDropTube", "SCMDropTube", 3, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (413, 69, 4, 4, "ScmDrpIvm", "ScmDrpIvm", 4, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (414, 69, 4, 5, "ScmDrpDec", "ScmDrpDec", 5, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (415, 69, 4, 6, "ScmDrpAlb", "ScmDrpAlb", 6, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (416, 69, 4, 7, "ScmDrpMdb", "ScmDrpMdb", 7, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (417, 69, 4, 8, "ScmDrpPzq", "ScmDrpPzq", 8, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (418, 69, 4, 9, "ScmDrpAzirtho", "ScmDrpAzirtho", 9, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (419, 69, 4, 10, "ScmDrpAzPos", "ScmDrpAzPos", 10, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (420, 69, 4, 11, "ScmDrpTeo", "ScmDrpTeo", 11, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (421, 138, 4, 1, "SaeSae", "SaeSae", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (422, 138, 4, 2, "SaeAe", "SaeAe", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (423, 142, 4, 3, "SaeFemale", "SaeFemale", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (424, 142, 4, 4, "SaeMale", "SaeMale", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (425, 145, 4, 5, "SaeAlbendazole", "SaeAlbendazole", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (426, 145, 4, 6, "SaeAzithromycin", "SaeAzithromycin", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (427, 145, 4, 7, "SaeDiethylcarbamazine", "SaeDiethylcarbamazine", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (428, 145, 4, 8, "SaeIvermectin", "SaeIvermectin", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (429, 145, 4, 9, "SaeMebendazole", "SaeMebendazole", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (430, 145, 4, 10, "SaePraziquantel", "SaePraziquantel", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (431, 145, 4, 11, "SaeTetracycline", "SaeTetracycline", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (432, 145, 4, 12, "SaeOther", "SaeOther", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (433, 156, 4, 13, "SaeGood", "SaeGood", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (434, 156, 4, 14, "SaePoor", "SaePoor", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (435, 156, 4, 15, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (436, 157, 4, 16, "SaeLF", "SaeLF", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (437, 157, 4, 17, "SaeOncho", "SaeOncho", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (438, 157, 4, 18, "SaeSchisto", "SaeSchisto", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (439, 157, 4, 19, "SaeSTH", "SaeSTH", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (440, 157, 4, 20, "SaeTrachoma", "SaeTrachoma", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (320, 213, 3, 88, "Females", "Females", 88, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (321, 213, 3, 89, "Males", "Males", 89, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (322, 213, 3, 90, "FemalesAndMales", "FemalesAndMales", 90, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (323, 238, 3, 91, "Culex", "Culex", 91, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (324, 238, 3, 92, "Anopheles", "Anopheles", 92, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (325, 238, 3, 93, "Aedes", "Aedes", 93, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (326, 238, 3, 94, "Mansonia", "Mansonia", 94, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (327, 245, 3, 95, "ICT", "ICT", 95, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (328, 245, 3, 96, "BrugiaRapid", "BrugiaRapid", 96, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (329, 246, 3, 97, "School", "School", 97, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (330, 246, 3, 98, "Community", "Community", 98, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (331, 273, 3, 99, "YesSentinelSite", "YesSentinelSite", 99, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (333, 273, 3, 101, "NotYetDetermined", "NotYetDetermined", 101, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (334, 273, 3, 102, "No", "No", 102, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (335, 281, 3, 103, "Community", "Community", 103, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (336, 281, 3, 104, "School", "School", 104, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (337, 281, 3, 105, "Other", "Other", 105, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (338, 302, 3, 106, "Phase1aEval", "Phase1aEval", 106, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (339, 302, 3, 107, "Phase2bEval", "Phase2bEval", 107, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (340, 302, 3, 108, "Phase2Eval", "Phase2Eval", 108, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (341, 302, 3, 109, "Phase3Eval", "Phase3Eval", 109, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (441, 157, 4, 21, "SaeMalaria", "SaeMalaria", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (442, 157, 4, 22, "SaeLoiasis", "SaeLoiasis", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (443, 157, 4, 23, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (444, 157, 4, 24, "SaeYes", "SaeYes", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (445, 157, 4, 25, "SaeNo", "SaeNo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (446, 157, 4, 26, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (447, 168, 4, 27, "SaeYes", "SaeYes", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (448, 168, 4, 28, "SaeNo", "SaeNo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (449, 168, 4, 29, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (450, 162, 4, 30, "SaeYes", "SaeYes", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (451, 162, 4, 31, "SaeNo", "SaeNo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (452, 162, 4, 32, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (453, 183, 4, 33, "SaeFullrecovery", "SaeFullrecovery", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (454, 183, 4, 34, "SaeOngoingillness", "SaeOngoingillness", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (455, 183, 4, 35, "SaePersistentorsignificantdisabilityincapacity", "SaePersistentorsignificantdisabilityincapacity", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (456, 183, 4, 36, "SaeProlongationofexistinghospitalization", "SaeProlongationofexistinghospitalization", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (457, 183, 4, 37, "SaeLifethreatening", "SaeLifethreatening", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (458, 183, 4, 38, "SaeCongenitalanomalybirthdefect", "SaeCongenitalanomalybirthdefect", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (459, 183, 4, 39, "SaeDeath", "SaeDeath", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (460, 188, 4, 40, "SaeYes", "SaeYes", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (461, 188, 4, 41, "SaeNo", "SaeNo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (462, 190, 4, 42, "SaeYes", "SaeYes", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (463, 190, 4, 43, "SaeNo", "SaeNo", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [IndicatorDropdownValues] ([ID], [IndicatorId], [EntityType], [SortOrder], [DropdownValue], [TranslationKey], [WeightedValue], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (464, 190, 4, 44, "SaeUnknown", "SaeUnknown", 0, 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [InterventionDistributionMethods] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt]) VALUES (1, "Distribution A", 0, 26, #08/19/2013#);
INSERT INTO [InterventionDistributionMethods] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt]) VALUES (2, "DM b", 0, 26, #08/19/2013#);
INSERT INTO [InterventionDistributionMethods] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt]) VALUES (3, "test", 0, 26, #08/19/2013 20:01:07#);
INSERT INTO [InterventionDistributionMethods] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt]) VALUES (4, "e2", 0, 26, #08/19/2013 20:03:17#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (32, 0, 2, "LFMMDPNumLymphoedemaPatients", 1, 0, 0, 0, 0, 0, 0, 32, 26, #08/19/2013 18:28:16#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (33, 0, 2, "LFMMDPNumLymphoedemaPatientsTreated", 1, 0, 0, 0, 0, 0, 0, 33, 26, #08/19/2013 18:28:16#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (34, 0, 2, "LFMMDPNumHydroceleCases", 1, 0, 0, 0, 0, 0, 0, 34, 26, #08/19/2013 18:28:16#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (35, 0, 2, "LFMMDPNumHydroceleCasesTreated", 1, 0, 0, 0, 0, 0, 0, 35, 26, #08/19/2013 18:27:47#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (67, 4, 2, "NumVas", 1, 0, 0, -1, 0, 0, 0, 67, 26, #11/04/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (68, 4, 2, "VasReporting", 1, 0, 0, -1, 0, 0, 0, 68, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (259, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (260, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (261, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (262, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (263, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (264, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (265, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (266, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (267, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (268, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (269, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (270, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (271, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (272, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (273, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (274, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (275, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (276, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (277, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (278, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (279, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (280, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (281, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (282, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (283, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (284, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (285, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (286, 0, 2, "TSAnnualInterventionObjectiveSurgery", 5, 0, 0, 0, 0, 0, 0, 2, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (287, 0, 2, "TSNumberOfTrichiasisSurgeons", 1, 0, 0, 0, 0, 0, 0, 3, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (288, 0, 2, "TSNumberOfTrichiasisSurgeriesInYearPeop", 1, 0, 0, 0, 0, 0, 0, 4, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (289, 0, 2, "TSRecurrentTrichiasisNumber", 5, 0, 0, 0, 0, 0, 0, 5, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (290, 0, 2, "TSRecurrentTrichiasisPercent", 5, 0, 0, 0, 0, 0, 0, 6, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (291, 0, 2, "TSPerSurgeonsInRuralAreas", 4, 0, 0, 0, 0, 0, 0, 7, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (292, 0, 2, "TSNumberOfSurgeonsCertifiedForGoodQualit", 1, 0, 0, 0, 0, 0, 0, 8, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (293, 0, 2, "TSNumberOfTtSurgeonsInTraining", 1, 0, 0, 0, 0, 0, 0, 9, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (294, 0, 1, "TSWhereAreTtSurgeonsBeingTrained", 4, 0, 0, 0, 0, 0, 0, 10, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (295, 0, 5, "TSSurgicalInstruments", 3, 0, 0, 0, 0, 0, 0, 11, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (296, 0, 5, "TSConsumablesForSurgery", 3, 0, 0, 0, 0, 0, 0, 12, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (297, 0, 5, "TSDrugsForSurgery", 3, 0, 0, 0, 0, 0, 0, 13, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (298, 0, 5, "TSNationalPolicyForTtSurgery", 5, 0, 0, 0, 0, 0, 0, 14, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (299, 0, 5, "TSNationalPolicyArePrimaryHealthCarPh", 5, 0, 0, 0, 0, 0, 0, 15, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (300, 0, 5, "TSNationalPolicyDoPhcWorkersReferTtForS", 5, 0, 0, 0, 0, 0, 0, 16, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (301, 0, 5, "FEHealthPromotionActivitiesYN", 2, 0, 0, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (302, 0, 5, "FESchoolHealthEducationYN", 2, 0, 0, 0, 0, 0, 0, 2, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (303, 0, 1, "FEMethodSchoolEducationCommunityEducat", 4, 0, 0, 0, 0, 0, 0, 3, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (304, 0, 2, "FEPeopleWithAccessToCleanWaterPercent", 5, 0, 0, 0, 0, 0, 0, 4, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (305, 0, 1, "FESourceOfDataForPeopleWithAccessToClean", 5, 0, 0, 0, 0, 0, 0, 5, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (306, 0, 2, "FEPeopleWithAccessToFunctionalLatrinesPer", 5, 0, 0, 0, 0, 0, 0, 6, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (307, 0, 1, "FESourceOfDataForPeopleWithAccessToFunct", 5, 0, 0, 0, 0, 0, 0, 7, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (308, 0, 6, "FENpbpPartnersCurrentForFComponents", 4, 0, 0, 0, 0, 0, -1, 8, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (309, 0, 6, "FENpbpPartnersCurrentForEComponents", 4, 0, 0, 0, 0, 0, -1, 9, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (310, 2, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (311, 10, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (312, 11, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (313, 12, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (314, 13, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (315, 14, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (316, 15, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (317, 16, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (318, 17, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (319, 18, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (320, 19, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (321, 20, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (322, 21, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (323, 22, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (324, 23, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (325, 24, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (326, 25, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (332, 0, 13, "PercentCoverageBu", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (333, 0, 13, "NumImported", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (334, 0, 13, "PercentVas", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (335, 0, 13, "PercentIdsr", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (336, 0, 13, "PercentRumorsInvestigated", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (337, 0, 13, "PercentCasesContained", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (338, 0, 13, "PercentEndemicReporting", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (339, 0, 13, "PercentLabConfirmed", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (340, 0, 13, "PercentTGamb", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (341, 0, 13, "PercentTRhod", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (342, 0, 13, "PercentTGambTRhod", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (343, 0, 13, "PercentCasesActivelyFound", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (344, 0, 13, "CureRate", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (345, 0, 13, "PercentTreatmentFailure", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (346, 0, 13, "PercentSae", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (347, 0, 13, "FatalityRate", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (348, 0, 13, "DetectionRatePer100k", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (349, 9, 13, "PercentPsacYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (350, 0, 13, "PercentSacYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (351, 0, 13, "PercentScreenedYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (352, 0, 13, "PercentTreatedAmongDetected", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (353, 0, 13, "DetectRate100kYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (354, 0, 13, "PercentNewCasesLabYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (355, 0, 13, "PercentTreatedYw", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (356, 5, 13, "PercentNewGrade2", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (357, 0, 13, "PrevalenceDetectionRatio", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (358, 0, 13, "PercentCureRateMb", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (359, 0, 13, "PercentCureRatePb", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (360, 0, 13, "PercentCoverageMdt", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (361, 7, 13, "DetectRate100kLeish", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (362, 0, 13, "PercentLabConfirm", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (363, 0, 13, "PercentCl", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (364, 0, 13, "PercentVl", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (365, 0, 13, "PercentClVl", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (366, 0, 13, "PercentCasesActiveLeish", 1, 0, 0, 0, 0, -1, 0, 0, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (69, 4, 2, "NumIdsr", 1, 0, 0, -1, 0, 0, 0, 69, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (70, 4, 2, "NumIdsrReporting", 1, 0, 0, -1, 0, 0, 0, 70, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (71, 4, 2, "NumRumors", 1, 0, 0, -1, 0, 0, 0, 71, 26, #11/04/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (72, 4, 2, "NumRumorsInvestigated", 1, 0, 0, -1, 0, 0, 0, 72, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (73, 4, 2, "NumClinical", 1, 0, 0, -1, 0, 0, 0, 73, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (74, 4, 2, "NumLab", 1, 0, 0, 0, 0, 0, 0, 74, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (75, 4, 2, "NumIndigenous", 1, 0, 0, -1, 0, 0, 0, 75, 26, #11/04/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (77, 4, 2, "NumVillageWithImported", 1, 0, 0, -1, 0, 0, 0, 77, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (78, 4, 2, "NumCasesContained", 1, 0, 0, -1, 0, 0, 0, 78, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (79, 4, 2, "NumCasesLost", 1, 0, 0, -1, 0, 0, 0, 79, 26, #11/04/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (80, 4, 2, "NumEndemicVillages", 1, 0, 0, -1, 0, 0, 0, 80, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (81, 4, 2, "NumEndemicVillagesReporting", 1, 0, 0, -1, 0, 0, 0, 81, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (82, 4, 2, "NumEndemicVillageWater", 1, 0, 0, -1, 0, 0, 0, 82, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (83, 4, 2, "NumEndemicAbate", 1, 0, 0, -1, 0, 0, 0, 83, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (84, 4, 2, "NumVillagesSafeWater", 1, 0, 0, -1, 0, 0, 0, 84, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (85, 5, 2, "NumGrade2", 1, 0, 0, -1, 0, 0, 0, 85, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (86, 5, 2, "NumRelapses", 1, 0, 0, 0, 0, 0, 0, 86, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (87, 5, 2, "CasesReadmitted", 1, 0, 0, 0, 0, 0, 0, 87, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (88, 5, 2, "TotalWithdrawal", 1, 0, 0, -1, 0, 0, 0, 88, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (89, 5, 2, "NumFirstLine", 1, 0, 0, -1, 0, 0, 0, 89, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (90, 5, 2, "NumDistributionPoints", 1, 0, 0, 0, 0, 0, 0, 90, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (91, 5, 2, "NumMbAdult", 1, 0, 0, -1, 0, 0, 0, 91, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (92, 5, 2, "NumMbChild", 1, 0, 0, -1, 0, 0, 0, 92, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (93, 5, 2, "NumPbAdult", 1, 0, 0, -1, 0, 0, 0, 93, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (94, 5, 2, "NumPbChild", 1, 0, 0, -1, 0, 0, 0, 94, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (95, 5, 2, "ClofazAvail", 1, 0, 0, 0, 0, 0, 0, 95, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (96, 5, 1, "OtherMedsLeprosy", 1, 0, 0, 0, 0, 0, 0, 96, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (97, 5, 2, "MbAdultsEnd", 1, 0, 0, -1, 0, 0, 0, 97, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (98, 5, 2, "MbChildEnd", 1, 0, 0, -1, 0, 0, 0, 98, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (99, 5, 2, "PbAdultEnd", 1, 0, 0, -1, 0, 0, 0, 99, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (100, 5, 2, "PbChildEnd", 1, 0, 0, -1, 0, 0, 0, 100, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (101, 5, 2, "PatientsCuredMb", 1, 0, 0, 0, 0, 0, 0, 101, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (102, 5, 2, "PatientsCuredPb", 1, 0, 0, 0, 0, 0, 0, 102, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (103, 5, 2, "NumPatientsType1", 1, 0, 0, 0, 0, 0, 0, 103, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (104, 5, 2, "NumPatientsType2", 1, 0, 0, 0, 0, 0, 0, 104, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (105, 5, 2, "NumPatientsFootwear", 1, 0, 0, 0, 0, 0, 0, 105, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (106, 5, 2, "NumLeprosySelfCare", 1, 0, 0, 0, 0, 0, 0, 106, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (107, 5, 2, "NumLeprosySurgery", 1, 0, 0, 0, 0, 0, 0, 107, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (108, 5, 2, "NumLeprosyRehab", 1, 0, 0, 0, 0, 0, 0, 108, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (109, 6, 2, "NumClinicalCasesHat", 1, 0, 0, -1, 0, 0, 0, 109, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (110, 6, 2, "NumLabCases", 1, 0, 0, -1, 0, 0, 0, 110, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (111, 6, 2, "NumTGamb", 1, 0, 0, -1, 0, 0, 0, 111, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (112, 6, 2, "NumTRhod", 1, 0, 0, -1, 0, 0, 0, 112, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (113, 6, 2, "NumTGambTRhod", 1, 0, 0, -1, 0, 0, 0, 113, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (114, 6, 2, "NumProspection", 1, 0, 0, 0, 0, 0, 0, 114, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (115, 6, 2, "NumCasesTreated", 1, 0, 0, -1, 0, 0, 0, 115, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (116, 6, 2, "NumTreatedRef", 1, 0, 0, 0, 0, 0, 0, 116, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (117, 6, 2, "NumTreatedNect", 1, 0, 0, 0, 0, 0, 0, 117, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (118, 6, 2, "NumCasesCured", 1, 0, 0, -1, 0, 0, 0, 118, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (119, 6, 2, "NumTreatmentFailures", 1, 0, 0, 0, 0, 0, 0, 119, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (120, 6, 2, "NumCasesSaes", 1, 0, 0, 0, 0, 0, 0, 120, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (121, 6, 2, "NumDeaths", 1, 0, 0, 0, 0, 0, 0, 121, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (123, 6, 2, "CureRateStage2", 1, 0, 0, 0, 0, 0, 0, 123, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (124, 6, 2, "CureRateStage1", 1, 0, 0, 0, 0, 0, 0, 124, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (125, 6, 5, "AvailableHatMeds", 1, 0, 0, 0, 0, 0, 0, 125, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (126, 7, 2, "NumClCases", 1, 0, 0, -1, 0, 0, 0, 126, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (127, 7, 2, "NumLabClCases", 1, 0, 0, -1, 0, 0, 0, 127, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (128, 7, 2, "NumLabVlCases", 1, 0, 0, -1, 0, 0, 0, 128, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (129, 7, 2, "NumClVlCases", 1, 0, 0, -1, 0, 0, 0, 129, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (130, 7, 2, "NumCasesFoundActively", 1, 0, 0, 0, 0, 0, 0, 130, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (131, 7, 2, "NumCasesTreatedLeish", 1, 0, 0, -1, 0, 0, 0, 131, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (132, 7, 2, "NumCasesTreatedRef", 1, 0, 0, 0, 0, 0, 0, 132, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (133, 7, 2, "NumCasesNewMeds", 1, 0, 0, 0, 0, 0, 0, 133, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (134, 7, 2, "NumCasesCuredLeish", 1, 0, 0, -1, 0, 0, 0, 134, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (135, 7, 2, "NumTreatmentFail", 1, 0, 0, 0, 0, 0, 0, 135, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (136, 7, 2, "NumCasesSaesLeish", 1, 0, 0, 0, 0, 0, 0, 136, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (137, 7, 2, "NumDeathsLeish", 1, 0, 0, 0, 0, 0, 0, 137, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (140, 8, 2, "PrevBeginYear", 1, 0, 0, 0, 0, 0, 0, 140, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (141, 8, 2, "CasesRegisteredYear", 1, 0, 0, 0, 0, 0, 0, 141, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (142, 8, 2, "CasesTreatedBu", 1, 0, 0, 0, 0, 0, 0, 142, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (143, 8, 2, "PatientsCompletedTreatment", 1, 0, 0, 0, 0, 0, 0, 143, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (144, 8, 2, "PatientsFullyScared", 1, 0, 0, 0, 0, 0, 0, 144, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (145, 8, 2, "PatientsCuredWoDisability", 1, 0, 0, 0, 0, 0, 0, 145, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (146, 8, 2, "OtherWithdrawl", 1, 0, 0, 0, 0, 0, 0, 146, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (147, 8, 2, "AdultsAtEnd", 1, 0, 0, 0, 0, 0, 0, 147, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (148, 8, 2, "ChildrenAtEnd", 1, 0, 0, 0, 0, 0, 0, 148, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (149, 8, 2, "PatientsFullyScarred", 1, 0, 0, 0, 0, 0, 0, 149, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (150, 8, 2, "TotalNumCasesCat3", 1, 0, 0, 0, 0, 0, 0, 150, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (151, 8, 2, "TotalNumCasesWoDisability", 1, 0, 0, 0, 0, 0, 0, 151, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (152, 8, 2, "NumRelapsesCases", 1, 0, 0, 0, 0, 0, 0, 152, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (153, 8, 2, "NumCasesReadmitted", 1, 0, 0, 0, 0, 0, 0, 153, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (154, 8, 2, "TotalWithdrawalTreatment", 1, 0, 0, 0, 0, 0, 0, 154, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (155, 8, 2, "NumFacilitiesProvidingBu", 1, 0, 0, 0, 0, 0, 0, 155, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (156, 8, 2, "NumVolunteersBu", 1, 0, 0, 0, 0, 0, 0, 156, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (157, 8, 2, "NumPatientsComplementedBu", 1, 0, 0, 0, 0, 0, 0, 157, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (158, 8, 1, "NumSurgeryPerformed", 1, 0, 0, 0, 0, 0, 0, 158, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (159, 8, 2, "TotalTreatedWithPrevention", 1, 0, 0, 0, 0, 0, 0, 159, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (160, 8, 2, "NumGoodRifampicin", 1, 0, 0, 0, 0, 0, 0, 160, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (161, 8, 2, "NumGoodClarithromycin", 1, 0, 0, 0, 0, 0, 0, 161, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (162, 8, 2, "NumGoodStreptomycin", 1, 0, 0, 0, 0, 0, 0, 162, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (163, 8, 2, "CureRateBu", 1, 0, 0, 0, 0, 0, 0, 163, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (164, 8, 2, "NumAffectedBuSelfCare", 1, 0, 0, 0, 0, 0, 0, 164, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (165, 8, 2, "NumBuSurgery", 1, 0, 0, 0, 0, 0, 0, 165, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (166, 8, 2, "NumBuRehab", 1, 0, 0, 0, 0, 0, 0, 166, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (167, 9, 2, "NumCasesTreatedYaws", 1, 0, 0, 0, 0, 0, 0, 167, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (168, 9, 2, "NumContactsTreatedYw", 1, 0, 0, 0, 0, 0, 0, 168, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (169, 9, 2, "NumIneffectiveYw", 1, 0, 0, 0, 0, 0, 0, 169, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (170, 9, 2, "NumCasesCuredYw", 1, 0, 0, 0, 0, 0, 0, 170, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (171, 9, 2, "NumCentersTreatingYw", 1, 0, 0, 0, 0, 0, 0, 171, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (172, 9, 2, "NumVolunteersYw", 1, 0, 0, 0, 0, 0, 0, 172, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (173, 9, 2, "AvailOfStrep", 1, 0, 0, 0, 0, 0, 0, 173, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (174, 9, 2, "AvailOfAzithro", 1, 0, 0, 0, 0, 0, 0, 174, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (175, 9, 2, "NumCasesCuredYaws", 1, 0, 0, 0, 0, 0, 0, 175, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (176, 9, 1, "NumFullyHealed", 1, 0, 0, 0, 0, 0, 0, 176, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (177, 9, 2, "NumPersonsBenza", 1, 0, 0, 0, 0, 0, 0, 177, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (178, 9, 2, "NumSaesYw", 1, 0, 0, 0, 0, 0, 0, 178, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (179, 9, 2, "NumTreatedAzithro", 1, 0, 0, 0, 0, 0, 0, 179, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (180, 9, 2, "NumFacilitiesProvidingAntibiotics", 1, 0, 0, 0, 0, 0, 0, 180, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (181, 9, 2, "NumVillagesWithVolunteers", 1, 0, 0, 0, 0, 0, 0, 181, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (182, 9, 2, "BenzaVials", 1, 0, 0, 0, 0, 0, 0, 182, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (183, 9, 2, "Benza6Vials", 1, 0, 0, 0, 0, 0, 0, 183, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (184, 9, 2, "OtherAntibiotics", 1, 0, 0, 0, 0, 0, 0, 184, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (185, 4, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (186, 5, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (187, 6, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (188, 7, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (189, 8, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (190, 9, 4, "DateReported", 5, 0, 0, -1, 0, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (226, 0, 6, "PcIntvDiseases", 5, 0, 0, -1, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (227, 0, 5, "PcIntvNumberOfTreatmentRoundsPlannedForTheYear", 3, 0, 0, 0, 0, 0, -1, 40, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (228, 0, 2, "PcIntvRoundNumber", 5, 0, 0, -1, 0, 0, 0, 50, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (229, 0, 4, "PcIntvStartDateOfMda", 2, 0, 0, -1, 0, 0, 0, 60, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (230, 0, 4, "PcIntvEndDateOfMda", 3, 0, 0, 0, 0, 0, 0, 70, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (231, 0, 2, "PcIntvNumEligibleIndividualsTargeted", 1, 0, 0, -1, 0, 0, 0, 80, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (232, 0, 2, "PcIntvNumEligibleFemalesTargeted", 1, 0, 0, 0, 0, 0, 0, 90, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (233, 0, 2, "PcIntvNumEligibleMalesTargeted", 1, 0, 0, 0, 0, 0, 0, 100, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (234, 0, 2, "PcIntvNumPsacTargeted", 1, 0, 0, 0, 0, 0, 0, 110, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (235, 0, 2, "PcIntvNumSacTargeted", 1, 0, 0, 0, 0, 0, 0, 120, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (236, 0, 2, "PcIntvNumAdultsTargeted", 1, 0, 0, 0, 0, 0, 0, 130, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (237, 0, 2, "PcIntvOfTotalTargetedForOncho", 1, 0, 0, 0, 0, 0, 0, 140, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (238, 0, 2, "PcIntvNumIndividualsTreated", 1, 0, 0, -1, 0, 0, 0, 150, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (239, 0, 2, "PcIntvNumFemalesTreated", 1, 0, 0, 0, 0, 0, 0, 160, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (240, 0, 2, "PcIntvNumMalesTreated", 1, 0, 0, 0, 0, 0, 0, 170, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (241, 0, 2, "PcIntvPsacTreated", 1, 0, 0, 0, 0, 0, 0, 180, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (242, 0, 2, "PcIntvNumSacTreated", 1, 0, 0, 0, 0, 0, 0, 190, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (243, 0, 2, "PcIntvNumAdultsTreated", 1, 0, 0, 0, 0, 0, 0, 200, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (244, 0, 2, "PcIntvOfTotalTreatedForOncho", 1, 0, 0, 0, 0, 0, 0, 210, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (245, 0, 2, "PcIntvNumTreatedZx", 1, 0, 0, 0, 0, 0, 0, 220, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (246, 0, 2, "PcIntvNumTreatedZxPos", 1, 0, 0, 0, 0, 0, 0, 230, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (247, 0, 2, "PcIntvNumTreatedTeo", 1, 0, 0, 0, 0, 0, 0, 240, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (248, 0, 2, "PcIntvNumSeriousAdverseEventsReported", 1, 0, 0, 0, 0, 0, 0, 250, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (249, 0, 5, "PcIntvStockOutDuringMda", 2, 0, 0, 0, 0, 0, 0, 260, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (250, 0, 5, "PcIntvStockOutDrug", 4, 0, 0, 0, 0, 0, 0, 270, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (251, 0, 5, "PcIntvLengthOfStockOut", 3, 0, 0, 0, 0, 0, 0, 280, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (252, 0, 13, "PcIntvProgramCoverage", 5, 0, 0, 0, 0, -1, 0, 290, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (253, 0, 13, "PcIntvEpiCoverage", 5, 0, 0, 0, 0, -1, 0, 300, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (254, 0, 13, "PcIntvFemalesTreatedProportion", 5, 0, 0, 0, 0, -1, 0, 310, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (255, 0, 13, "PcIntvMalesTreatedProportion", 5, 0, 0, 0, 0, -1, 0, 320, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (256, 0, 13, "PcIntvPsacCoverage", 5, 0, 0, 0, 0, -1, 0, 330, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (257, 0, 13, "PcIntvSacCoverage", 5, 0, 0, 0, 0, -1, 0, 340, 26, #01/01/2013#);
INSERT INTO [InterventionIndicators] ([ID], [InterventionTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (258, 0, 9, "PcIntvPartner", 4, 0, 0, 0, 0, 0, -1, 350, 26, #01/01/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (10, "IntvIvmAlb", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (11, "IntvDecAlb", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (12, "IntvIvm", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (13, "IntvPzqAlb", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (14, "IntvPzqMbd", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (15, "IntvPzq", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (16, "IntvAlb", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (17, "IntvMbd", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (18, "IntvAlb2", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (19, "IntvIvmPzq", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (20, "IntvIvmPzqAlb", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (21, "IntvZithro", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (22, "IntvTeo", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (23, "IntvZithroTeo", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (24, "TsSurgeries", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (25, "TrachomaFaceClean", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, "LfMorbidityManagment", "PC", 26, #08/19/2013 18:27:47#, 26, #10/03/2013 13:53:07#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, "GuineaWormIntervention", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (5, "LeprosyIntervention", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (6, "HatIntervention", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (7, "LeishIntervention", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (8, "BuruliUlcerIntv", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes] ([ID], [InterventionTypeName], [DiseaseType], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (9, "YawsIntervention", "CM", 26, #11/04/2013#, 26, #11/04/2013#);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (2, 3);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (4, 6);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (5, 7);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (6, 8);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (7, 9);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (8, 10);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (9, 11);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (10, 3);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (10, 4);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (10, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (11, 3);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (11, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (12, 4);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (13, 12);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (13, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (14, 12);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (14, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (15, 12);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (16, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (17, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (18, 3);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (19, 4);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (19, 12);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (20, 3);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (20, 4);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (20, 5);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (21, 13);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (22, 13);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (23, 13);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (25, 13);
INSERT INTO [InterventionTypes_to_Diseases] ([InterventionTypeId], [DiseaseId]) VALUES (24, 13);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 67);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 68);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 69);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 70);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 71);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 72);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 73);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 74);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 75);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 77);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 78);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 79);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 80);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 81);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 82);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 83);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 84);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 85);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 86);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 87);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 88);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 89);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 90);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 91);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 92);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 93);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 94);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 95);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 96);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 97);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 98);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 99);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 100);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 101);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 102);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 103);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 104);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 105);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 106);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 107);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 108);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 109);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 110);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 111);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 112);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 113);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 114);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 115);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 116);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 117);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 118);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 119);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 120);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 121);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 123);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 124);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 125);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 126);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 127);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 128);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 129);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 130);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 131);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 132);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 133);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 134);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 135);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 136);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 137);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 140);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 141);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 142);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 143);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 144);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 145);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 146);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 147);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 148);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 150);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 151);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 152);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 153);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 154);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 155);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 156);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 157);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 158);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 159);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 160);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 161);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 162);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 163);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 164);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 165);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 166);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 167);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 168);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 169);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 170);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 171);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 172);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 173);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 174);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 175);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 176);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 177);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 178);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 179);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 180);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 181);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 182);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 183);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 184);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 185);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 186);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 187);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 188);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 189);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 190);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 226);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 237);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 244);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 272);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 259);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 234);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 241);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 273);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 256);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 260);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 274);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 261);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 275);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 262);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 276);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 263);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 277);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 264);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 234);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 241);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 278);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 256);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 234);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 241);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 279);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 256);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 265);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 266);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 234);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 241);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 280);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 256);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 267);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 281);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 268);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 235);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 236);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 237);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 242);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 243);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 244);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 282);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 257);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 269);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 245);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 246);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 283);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 270);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 247);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 284);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 271);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 227);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 228);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 229);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 230);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 231);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 232);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 233);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 238);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 239);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 240);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 245);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 246);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 247);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 248);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 249);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 285);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 251);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 252);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 253);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 254);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 255);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 258);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (2, 32);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (2, 33);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (2, 34);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (2, 35);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 286);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 287);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 288);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 289);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 290);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 291);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 292);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 293);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 294);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 295);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 296);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 297);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 298);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 299);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 300);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 301);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 302);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 303);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 304);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 305);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 306);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 307);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 308);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 309);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (2, 310);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (10, 311);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 312);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (12, 313);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (13, 314);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (14, 315);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (15, 316);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (16, 317);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (17, 318);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (18, 319);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (19, 320);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (20, 321);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (21, 322);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (22, 323);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (23, 324);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (24, 325);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (25, 326);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (8, 332);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 333);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 334);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 335);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 336);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 337);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (4, 338);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 339);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 340);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 341);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 342);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 343);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 344);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 345);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 346);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 347);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (6, 348);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 349);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 350);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 351);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 352);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 353);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 354);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (9, 355);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 356);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 357);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 358);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 359);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (5, 360);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 361);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 362);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 363);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 364);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 365);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (7, 366);
INSERT INTO [InterventionTypes_to_Indicators] ([InterventionTypeId], [IndicatorId]) VALUES (11, 257);
INSERT INTO [Languages] ([IsoCode], [DisplayName]) VALUES ("en-US", "English");
INSERT INTO [Languages] ([IsoCode], [DisplayName]) VALUES ("fr-FR", "français");
INSERT INTO [Medicines] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, "Medicine A", 0, 26, #08/21/2013 13:44:40#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Medicines] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, "Medicine c", -1, 26, #08/26/2013 10:18:23#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Partners] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, "Partner B", -1, 26, #08/21/2013 13:45:09#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Partners] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, "a", -1, 26, #08/26/2013 09:38:05#, 26, #10/03/2013 13:53:07#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (59, 6, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (60, 7, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (61, 7, 5, "PCTrainTrainingCategory", 5, 0, 0, -1, 0, -1, 2, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (62, 7, 2, "PCTrainNumIndividualsTargeted", 1, 0, 0, 0, 0, 0, 49, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (63, 7, 2, "PCTrainNumIndividualsTrainedTotal", 1, 0, 0, 0, 0, 0, 50, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (64, 7, 2, "PCTrainNumIndividualsTrainedFemales", 1, 0, 0, 0, 0, 0, 51, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (65, 7, 2, "PCTrainNumIndividualsTrainedMales", 1, 0, 0, 0, 0, 0, 52, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (66, 7, 9, "PCTrainFunders", 4, 0, 0, 0, 0, -1, 53, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (67, 8, 7, "SCMTimePeriodForInventory", 5, 0, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (68, 8, 4, "SCMDateOfInventory", 5, 0, 0, 0, 0, 0, 2, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (69, 8, 5, "SCMDrug", 5, 0, 0, -1, 0, -1, 3, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (70, 8, 5, "SCMUnit", 5, 0, 0, 0, 0, 0, 4, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (71, 8, 2, "SCMIfBottlesHowManyTabletsAreInEachBott", 5, 0, 0, 0, 0, 0, 5, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (72, 8, 2, "SCMAvailableUsableStock", 1, 0, 0, 0, 0, 0, 6, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (73, 8, 1, "SCMExpiryDatesForUsableStock", 5, 0, 0, 0, 0, 0, 7, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (74, 8, 2, "SCMExpiredDrugs", 1, 0, 0, 0, 0, 0, 8, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (75, 8, 2, "SCMRequested", 1, 0, 0, 0, 0, 0, 9, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (76, 8, 2, "SCMReceived", 1, 0, 0, 0, 0, 0, 10, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (77, 8, 2, "SCMUsedDistributed", 1, 0, 0, 0, 0, 0, 11, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (78, 8, 2, "SCMWasted", 1, 0, 0, 0, 0, 0, 12, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (79, 8, 2, "SCMUnusable", 1, 0, 0, 0, 0, 0, 13, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (80, 8, 2, "SCMLosses", 1, 0, 0, 0, 0, 0, 14, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (81, 8, 2, "SCMStolen", 1, 0, 0, 0, 0, 0, 15, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (82, 8, 2, "SCMTransferredToAnotherDistrict", 1, 0, 0, 0, 0, 0, 16, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (83, 8, 2, "SCMTransferredFromAnotherDisrict", 1, 0, 0, 0, 0, 0, 17, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (84, 8, 2, "SCMAdjustments", 1, 0, 0, 0, 0, 0, 18, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (85, 8, 2, "SCMRemaining", 1, 0, 0, 0, 0, 0, 19, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (86, 8, 2, "SCMNumRemainingSealedBottles", 1, 0, 0, 0, 0, 0, 20, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (87, 8, 2, "SCMNumRemainingOpenedBottles", 1, 0, 0, 0, 0, 0, 21, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (88, 8, 1, "SCMExpiryDatesForSealedBottles", 5, 0, 0, 0, 0, 0, 22, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (89, 8, 1, "SCMExpiryDatesForOpenedBottles", 5, 0, 0, 0, 0, 0, 23, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (90, 8, 1, "SCMBatchReferenceNumber", 5, 0, 0, 0, 0, 0, 24, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (91, 8, 4, "DateReported", 5, 0, 0, -1, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (92, 9, 4, "DateReported", 5, 0, 0, -1, 0, 0, -1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (137, 9, 4, "SaeDate", 2, 0, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (138, 9, 1, "SaeId", 2, 0, 0, 0, 0, 0, 2, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (139, 9, 5, "SaeType", 5, 0, 0, 0, 0, 0, 3, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (140, 9, 1, "SaePatientName", 5, 0, 0, 0, 0, 0, 4, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (141, 9, 1, "SaeContactDetails", 2, 0, 0, 0, 0, 0, 5, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (142, 9, 2, "SaeAge", 5, 0, 0, 0, 0, 0, 6, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (143, 9, 5, "SaeSex", 5, 0, 0, 0, 0, 0, 7, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (144, 9, 1, "SaeHeight", 2, 0, 0, 0, 0, 0, 8, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (145, 9, 1, "SaeWeight", 2, 0, 0, 0, 0, 0, 9, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (146, 9, 6, "SaeDrugs", 5, 0, 0, 0, 0, -1, 10, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (147, 9, 1, "SAENumberoftabletsadministered", 5, 0, 0, 0, 0, 0, 11, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (148, 9, 1, "SAEDoseadministered", 2, 0, 0, 0, 0, 0, 12, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (149, 9, 1, "SAEBrandandmanufacturername", 5, 0, 0, 0, 0, 0, 13, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (1, 1, 2, "GWTrainNumberOfTrainingSessionsHold", 2, 0, 0, 0, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (2, 1, 2, "GWTrainNumberOfParticipantsToTrainingSessions", 2, 0, 0, 0, 0, 0, 2, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (3, 1, 2, "GWTrainPercentageOfTrainedHealthWorkersAmongTot", 5, 0, 0, 0, 0, 0, 3, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (4, 1, 2, "GWTrainPercentageOfHealthFacilitiesHavingAtLeas", 5, 0, 0, 0, 0, 0, 4, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (5, 1, 2, "GWTrainNumberOfSupervisionVisitsScheduledPerYea", 2, 0, 0, 0, 0, 0, 5, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (6, 1, 2, "GWTrainPercentageOfSupervisionVisitsCarriedOutD", 5, 0, 0, 0, 0, 0, 6, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (7, 1, 2, "GWTrainPercentageOfHealthFacilityStaffSupervise", 5, 0, 0, 0, 0, 0, 7, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (8, 2, 2, "LeprosyTrainNumberOfTrainingSessionsHold", 2, 0, 0, 0, 0, 0, 8, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (9, 2, 2, "LeprosyTrainNumberOfParticipantsToTraini", 2, 0, 0, 0, 0, 0, 9, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (10, 2, 2, "LeprosyTrainPercentageOfTrainedHealthWor", 5, 0, 0, 0, 0, 0, 10, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (11, 2, 2, "LeprosyTrainPercentageOfHealthFacilities", 5, 0, 0, 0, 0, 0, 11, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (12, 2, 2, "LeprosyTrainNumberOfSupervisionVisitsSch", 2, 0, 0, 0, 0, 0, 12, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (13, 2, 2, "LeprosyTrainPercentageOfSupervisionVisit", 5, 0, 0, 0, 0, 0, 13, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (14, 2, 2, "LeprosyTrainPercentageOfHealthFacilitySt", 5, 0, 0, 0, 0, 0, 14, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (15, 3, 2, "HATTrainNumberOfTrainingSessionsForHatHo", 2, 0, 0, 0, 0, 0, 15, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (16, 3, 2, "HATTrainNumberOfParticipantsToHatTrainin", 2, 0, 0, 0, 0, 0, 16, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (17, 3, 2, "HATTrainPercentageOfTrainedHealthWorkers", 5, 0, 0, 0, 0, 0, 17, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (18, 3, 2, "HATTrainPercentageOfHealthFacilitiesHavi", 5, 0, 0, 0, 0, 0, 18, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (19, 3, 2, "HATTrainNumberOfSupervisionVisitsSchedul", 2, 0, 0, 0, 0, 0, 19, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (20, 3, 2, "HATTrainPercentageOfSupervisionVisitsCar", 5, 0, 0, 0, 0, 0, 20, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (21, 3, 2, "HATTrainPercentageOfHealthFacilityStaffS", 5, 0, 0, 0, 0, 0, 21, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (22, 4, 2, "LeishTrainNumberOfTrainingSessionsHold", 2, 0, 0, 0, 0, 0, 22, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (23, 4, 2, "LeishTrainNumberOfParticipantsToTraining", 2, 0, 0, 0, 0, 0, 23, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (24, 4, 2, "LeishTrainPercentageOfTrainedHealthWorke", 5, 0, 0, 0, 0, 0, 24, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (25, 4, 2, "LeishTrainPercentageOfHealthFacilitiesHa", 5, 0, 0, 0, 0, 0, 25, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (26, 4, 2, "LeishTrainNumberOfSupervisionVisitsSched", 2, 0, 0, 0, 0, 0, 26, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (27, 4, 2, "LeishTrainPercentageOfSupervisionVisitsC", 5, 0, 0, 0, 0, 0, 27, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (28, 4, 2, "LeishTrainPercentageOfHealthFacilityStaf", 5, 0, 0, 0, 0, 0, 28, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (29, 5, 2, "BUTrainNumberOfTrainingSessionsHold", 2, 0, 0, 0, 0, 0, 29, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (30, 5, 2, "BUTrainNumberOfParticipantsToTrainingSes", 2, 0, 0, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (31, 5, 2, "BUTrainPercentageOfTrainedHealthWorkersA", 5, 0, 0, 0, 0, 0, 31, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (32, 5, 2, "BUTrainPercentageOfHealthFacilitiesHavin", 5, 0, 0, 0, 0, 0, 32, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (44, 5, 2, "BUTrainNumberOfSupervisionVisitsSchedule", 2, 0, 0, 0, 0, 0, 44, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (45, 5, 2, "BUTrainPercentageOfSupervisionVisitsCarr", 5, 0, 0, 0, 0, 0, 45, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (46, 5, 2, "BUTrainPercentageOfHealthFacilityStaffSu", 5, 0, 0, 0, 0, 0, 46, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (47, 6, 2, "YawsTrainNumberOfTrainingSessionsHold", 2, 0, 0, 0, 0, 0, 47, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (48, 6, 2, "YawsTrainNumberOfParticipantsToTrainingS", 2, 0, 0, 0, 0, 0, 48, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (49, 6, 2, "YawsTrainPercentageOfTrainedHealthWorker", 5, 0, 0, 0, 0, 0, 49, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (50, 6, 2, "YawsTrainPercentageOfHealthFacilitiesHav", 5, 0, 0, 0, 0, 0, 50, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (51, 6, 2, "YawsTrainNumberOfSupervisionVisitsSchedu", 2, 0, 0, 0, 0, 0, 51, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (52, 6, 2, "YawsTrainPercentageOfSupervisionVisitsCa", 5, 0, 0, 0, 0, 0, 52, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (53, 6, 2, "YawsTrainPercentageOfHealthFacilityStaff", 5, 0, 0, 0, 0, 0, 53, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (54, 1, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (55, 2, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (56, 3, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (57, 4, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (58, 5, 4, "DateReported", 5, 0, 0, -1, 0, 0, 1, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (150, 9, 1, "SAEBatchnumber", 5, 0, 0, 0, 0, 0, 14, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (151, 9, 1, "SAEExpirydateofdrug", 2, 0, 0, 0, 0, 0, 15, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (152, 9, 1, "SAEDateoftreatment", 2, 0, 0, 0, 0, 0, 16, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (153, 9, 1, "SAETimeoftreatment", 5, 0, 0, 0, 0, 0, 17, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (154, 9, 1, "SAESourceoftreatmentPCClinicorphysiciantreatmentsOther", 5, 0, 0, 0, 0, 0, 18, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (155, 9, 1, "SAEWasthisthefirsttreatmentwiththisdrug", 2, 0, 0, 0, 0, 0, 19, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (156, 9, 15, "SAEIfthiswasnotthefirsttreatmentwiththisdrugcircumstancesofpasttreatments", 5, 0, 0, 0, 0, 0, 20, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (157, 9, 5, "SAEHealthstatusbeforetreatment", 5, 0, 0, 0, 0, 0, 21, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (158, 9, 6, "SAEWhichparasitichave", 2, 0, 0, 0, 0, 0, 22, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (159, 9, 1, "SAEStatusknown", 2, 0, 0, 0, 0, 0, 23, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (160, 9, 15, "SAEDetails", 5, 0, 0, 0, 0, 0, 24, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (161, 9, 2, "SAEIfLoiasisinfectionsmfmlblood", 5, 0, 0, 0, 0, 0, 25, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (162, 9, 2, "SAEIfLoiasisinfectionsmfmlCSF", 2, 0, 0, 0, 0, 0, 26, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (163, 9, 5, "SAEIspatientpregnant", 5, 0, 0, 0, 0, 0, 27, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (164, 9, 5, "SAEDidpatienttakealcoholwithin24hoursoftreatment", 5, 0, 0, 0, 0, 0, 28, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (165, 9, 1, "SAEOthermedicationsbeingtakencurrentlyorrecently", 2, 0, 0, 0, 0, 0, 29, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (166, 9, 4, "SAEDateofonset", 2, 0, 0, 0, 0, 0, 30, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (167, 9, 1, "SAETimeofonset", 5, 0, 0, 0, 0, 0, 31, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (168, 9, 1, "SAEClinicalsignsandsymptoms", 5, 0, 0, 0, 0, 0, 32, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (169, 9, 5, "SAEWashospitalizationrequired", 2, 0, 0, 0, 0, 0, 33, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (170, 9, 4, "SAEIfhospitalizationrequireddateofadmission", 5, 0, 0, 0, 0, 0, 34, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (171, 9, 1, "SAEIfhospitalizationrequiredreasonforadmission", 5, 0, 0, 0, 0, 0, 35, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (172, 9, 4, "SAEIfhospitalizationrequireddateofdischarge", 2, 0, 0, 0, 0, 0, 36, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (173, 9, 1, "SAEDrugsadministeredtomanageSAE", 2, 0, 0, 0, 0, 0, 37, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (174, 9, 1, "SAEDoseadministeredtomanageSAE", 5, 0, 0, 0, 0, 0, 38, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (175, 9, 15, "SAEDetails1", 5, 0, 0, 0, 0, 0, 39, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (176, 9, 1, "SAEClinicalcourse", 2, 0, 0, 0, 0, 0, 40, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (177, 9, 1, "SAELaboratorytestsperformed", 5, 0, 0, 0, 0, 0, 41, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (178, 9, 1, "SAEDateoftest", 5, 0, 0, 0, 0, 0, 42, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (179, 9, 1, "SAEResultoftest", 5, 0, 0, -1, 0, 0, 43, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (180, 9, 1, "SAEUnits", 5, 0, 0, -1, 0, 0, 44, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (181, 9, 1, "SAENormalRangeLow", 5, 0, 0, -1, 0, 0, 45, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (182, 9, 1, "SAENormalRangeHigh", 5, 0, 0, -1, 0, 0, 46, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (183, 9, 1, "SAEComments", 5, 0, 0, -1, 0, 0, 47, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (184, 9, 5, "SAEOutcomeattimeoflastobservation", 5, 0, 0, -1, 0, -1, 48, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (185, 9, 1, "SAEExtenuatingorcomplicatingcircumstances", 5, 0, 0, -1, 0, 0, 49, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (186, 9, 4, "SAEIfdeathdateofdeath", 5, 0, 0, -1, 0, 0, 50, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (187, 9, 1, "SAEIfdeathcauseofdeath", 1, 0, 0, 0, 0, 0, 51, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (188, 9, 1, "SAEIfdeathcircumstancesatthetimeofdeath", 1, 0, 0, 0, 0, 0, 52, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (189, 9, 5, "SAEWasthiscaseinvestigated", 1, 0, 0, 0, 0, 0, 53, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (190, 9, 1, "SAEResultofinvestigation", 1, 0, 0, 0, 0, 0, 54, 26, #01/01/2013#);
INSERT INTO [ProcessIndicators] ([ID], [ProcessTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsRequired], [IsDisplayed], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (191, 9, 5, "SAEWastheSAErelatedtothedrugs", 4, 0, 0, 0, 0, -1, 55, 26, #01/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, "GuineaWormTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, "LeprosyTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (3, "HatTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, "LeishTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (5, "BuruliUlcerTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (6, "YawsTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (7, "PcTraining", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (8, "SCM", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes] ([ID], [TypeName], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (9, "SAEs", 26, #11/01/2013#, 26, #11/01/2013#);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (1, 6);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (2, 7);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (3, 8);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (4, 9);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (5, 10);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (6, 11);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (7, 3);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (7, 4);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (7, 5);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (7, 12);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (7, 13);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (8, 3);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (8, 4);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (8, 5);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (8, 12);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (8, 13);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 3);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 4);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 5);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 6);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 7);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 8);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 9);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 10);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 11);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 12);
INSERT INTO [ProcessTypes_to_Diseases] ([ProcessTypeId], [DiseaseId]) VALUES (9, 13);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (73, 4, 2, "NumPopSurveyedCm", 5, 0, 0, 0, 0, 0, 0, 73, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (74, 4, 2, "NumPopScreenedCm", 5, 0, 0, 0, 0, 0, 0, 74, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (75, 4, 2, "NumCasesFoundCm", 5, 0, 0, 0, 0, 0, 0, 75, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (76, 5, 2, "NumPopSurveyedCm", 5, 0, 0, 0, 0, 0, 0, 76, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (77, 5, 2, "NumPopScreenedCm", 5, 0, 0, 0, 0, 0, 0, 77, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (78, 5, 2, "NumCasesDiagnosedCm", 5, 0, 0, 0, 0, 0, 0, 78, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (79, 5, 2, "NumCasesLeprosyCm", 5, 0, 0, 0, 0, 0, 0, 79, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (80, 6, 2, "NumPopSurveyedCm", 5, 0, 0, 0, 0, 0, 0, 80, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (81, 6, 2, "NumPopScreenedCm", 5, 0, 0, 0, 0, 0, 0, 81, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (82, 6, 2, "NumCasesDiagnosedCm", 5, 0, 0, 0, 0, 0, 0, 82, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (83, 7, 2, "NumPopSurveyedCm", 5, 0, 0, 0, 0, 0, 0, 83, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (84, 7, 2, "NumPopScreenedCm", 5, 0, 0, 0, 0, 0, 0, 84, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (85, 7, 2, "NumCasesDiagnosedCm", 5, 0, 0, 0, 0, 0, 0, 85, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (86, 8, 2, "NumPopSurveyedCm", 5, 0, 0, 0, 0, 0, 0, 86, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (87, 8, 2, "NumPopScreenedCm", 5, 0, 0, 0, 0, 0, 0, 87, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (88, 8, 2, "NumCasesDiagnosedCm", 5, 0, 0, 0, 0, 0, 0, 88, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (89, 8, 2, "NumCasesPcrCm", 5, 0, 0, 0, 0, 0, 0, 89, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (90, 9, 2, "NumVillagesScreenedCm", 5, 0, 0, 0, 0, 0, 0, 90, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (91, 9, 2, "NumSchoolsScreenedCm", 5, 0, 0, 0, 0, 0, 0, 91, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (92, 9, 2, "TotalNumPeopleScreenedCm", 5, 0, 0, 0, 0, 0, 0, 92, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (93, 9, 2, "NumCasesDiagnosedCm", 5, 0, 0, 0, 0, 0, 0, 93, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (94, 9, 2, "NumCasesContactsTreatedCm", 5, 0, 0, 0, 0, 0, 0, 94, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (95, 9, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (96, 4, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (97, 5, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (98, 6, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (99, 7, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (100, 8, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (101, 10, 6, "LFSurVector", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (102, 10, 5, "LFSurCausalAgent", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (103, 10, 7, "LFSurDateOfTheFirstRoundOfPc", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (104, 10, 2, "LFSurNumberOfRoundsOfPcCompletedPriorToS", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (105, 10, 5, "LFSurSurveyTiming", 5, 0, 0, 0, -1, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (106, 10, 5, "LFSurTestType", 5, 0, 0, 0, -1, 0, -1, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (107, 10, 4, "LFSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (108, 10, 4, "LFSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (109, 10, 2, "LFSurTargetSampleSize", 5, 0, 0, 0, 0, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (110, 10, 1, "LFSurAgeRange", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (111, 10, 2, "LFSurNumberOfIndividualsSampled", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (112, 10, 2, "LFSurNumberOfIndividualsWithNon", 5, 0, 0, 0, 0, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (113, 10, 2, "LFSurNumberOfIndividualsExamined", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (114, 10, 2, "LFSurNumberOfIndividualsPositive", 5, 0, 0, 0, -1, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (116, 10, 2, "LFSurMeanDensity", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (117, 10, 2, "LFSurCount", 5, 0, 0, 0, 0, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (118, 10, 2, "LFSurCommunityMfLoad", 5, 0, 0, 0, 0, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (119, 13, 5, "OnchoSurSurveyType", 5, 0, 0, 0, -1, 0, -1, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (120, 13, 5, "OnchoSurEntomologicalAssessmentConducted", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (121, 13, 1, "OnchoSurIfEcologicalAssessmentSite", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (122, 13, 1, "OnchoSurIfEcologicalAssessmentRiver", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (123, 13, 2, "OnchoSurLatitude", 5, 0, 0, 0, 0, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (124, 13, 2, "OnchoSurLongitude", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (125, 13, 4, "OnchoSurDateOfTheLastTreatment", 5, 0, 0, 0, -1, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (126, 13, 2, "OnchoSurNumberOfRoundsOfPcCompletedPrior", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (127, 13, 5, "OnchoSurTestType", 5, 0, 0, 0, -1, 0, -1, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (128, 13, 4, "OnchoSurStartDateOfSurvey", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (129, 13, 4, "OnchoSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (130, 13, 2, "OnchoSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (131, 13, 1, "OnchoSurAgeRange", 5, 0, 0, 0, 0, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (132, 13, 2, "OnchoSurReportedPopulation", 5, 0, 0, 0, 0, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (133, 13, 2, "OnchoSurRegistrationPopulation", 5, 0, 0, 0, -1, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (134, 13, 2, "OnchoSurNumberOfIndividualsExamined", 5, 0, 0, 0, -1, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (136, 13, 2, "OnchoSurNumberOfIndividualsPositive", 5, 0, 0, 0, 0, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (138, 13, 2, "OnchoSurStandardizedPrevelance", 5, 0, 0, 0, 0, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (139, 13, 2, "OnchoSurCmfl", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (140, 13, 2, "OnchoSurIfTestTypeIsNpNumDep", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (142, 13, 2, "OnchoSurIfTestTypeIsNpNumNod", 5, 0, 0, 0, 0, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (144, 13, 2, "OnchoSurIfTestTypeIsNpNumWri", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (146, 13, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (148, 10, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (149, 11, 6, "SCHSurCausalAgent", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (150, 11, 4, "SCHSurDateOfTheFirstRoundOfPcYear", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (151, 11, 2, "SCHSurNumberOfRoundsOfPcCompletedPriorTo", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (152, 11, 5, "SCHSurSurveyTiming", 5, 0, 0, 0, -1, 0, -1, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (153, 11, 6, "SCHSurTestType", 5, 0, 0, 0, -1, 0, -1, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (154, 11, 4, "SCHSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (155, 11, 4, "SCHSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (156, 11, 2, "SCHSurTargetSampleSize", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (157, 11, 1, "SCHSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (158, 11, 2, "SCHSurNumberOfIndividualsSampled", 5, 0, 0, 0, 0, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (159, 11, 2, "SCHSurNumberOfIndividualsWithNonResponse", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (160, 11, 2, "SCHSurNumberOfIndividualsExaminedForUrin", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (161, 11, 2, "SCHSurNumberOfIndividualsPositiveForHaem", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (163, 11, 2, "SCHSurProportionOfHeavyIntensityUrinaryS", 5, 0, 0, 0, 0, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (164, 11, 2, "SCHSurProportionOfModerateIntensityUrina", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (165, 11, 2, "SCHSurNumberOfIndividualsExaminedForInte", 5, 0, 0, 0, -1, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (166, 11, 2, "SCHSurNumberOfIndividualsPositiveForInte", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (168, 11, 2, "SCHSurProportionOfHeavyIntensityIntestin", 5, 0, 0, 0, 0, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (169, 11, 2, "SCHSurProportionOfModerateIntensityIntes", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (170, 11, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (171, 12, 6, "STHSurCausalAgent", 5, 0, 0, 0, -1, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (172, 12, 4, "STHSurDateOfTheFirstRoundOfPcYear", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (173, 12, 2, "STHSurNumberOfRoundsOfPcCompletedPriorTo", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (174, 12, 5, "STHSurSurveyTiming", 5, 0, 0, 0, -1, 0, -1, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (175, 12, 5, "STHSurTestType", 5, 0, 0, 0, -1, 0, -1, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (176, 12, 4, "STHSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (177, 12, 4, "STHSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (178, 12, 2, "STHSurTargetSampleSize", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (179, 12, 1, "STHSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (180, 12, 2, "STHSurNumberOfIndividualsSampled", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (181, 12, 2, "STHSurNumberOfIndividualsWithNonResponse", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (182, 12, 2, "STHSurNumberOfIndividualsExaminedAscaris", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (183, 12, 2, "STHSurNumberOfIndividualsPositiveAscaris", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (185, 12, 2, "STHSurProportionOfHeavyIntensityOfAsc", 5, 0, 0, 0, 0, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (186, 12, 2, "STHSurProportionOfModerateIntensityAsc", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (187, 12, 2, "STHSurNumberOfIndividualsExaminedHookwor", 5, 0, 0, 0, -1, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (188, 12, 2, "STHSurNumberOfIndividualsPositiveHookwor", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (190, 12, 2, "STHSurProportionOfHeavyIntensityHook", 5, 0, 0, 0, 0, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (191, 12, 2, "STHSurProportionOfModerateIntensityHook", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (192, 12, 2, "STHSurNumberOfIndividualsExaminedTrichur", 5, 0, 0, 0, -1, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (193, 12, 2, "STHSurNumberOfIndividualsPositiveTrichur", 5, 0, 0, 0, -1, 0, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (195, 12, 2, "STHSurProportionOfHeavyIntensityOfTri", 5, 0, 0, 0, 0, 0, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (196, 12, 2, "STHSurProportionOfModerateIntensityTri", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (197, 12, 2, "STHSurNumberOfIndividualsExaminedOverall", 5, 0, 0, 0, -1, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (198, 12, 2, "STHSurNumberOfIndividualsPositiveOverall", 5, 0, 0, 0, -1, 0, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (200, 12, 2, "STHSurOverallProportionOfHeavyIntensity", 5, 0, 0, 0, 0, 0, 0, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (201, 12, 2, "STHSurOverallProportionOfModerateIntens", 5, 0, 0, 0, 0, 0, 0, 31, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (202, 12, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 32, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (203, 14, 1, "TraSurSiteNames", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (204, 14, 4, "TraSurDateOfTheFirstRoundOfDistributionO", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (205, 14, 2, "TraSurNumberOfYearsOfSafePriorToSurveyIm", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (206, 14, 5, "TraSurSurveyType", 5, 0, 0, 0, -1, 0, -1, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (207, 14, 5, "TraSurSurveyPower", 5, 0, 0, 0, 0, 0, -1, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (208, 14, 6, "TraSurClinicalSignAssessed", 5, 0, 0, 0, -1, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (209, 14, 4, "TraSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (210, 14, 4, "TraSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (211, 14, 2, "TraSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (212, 14, 1, "TraSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (213, 14, 5, "TraSurGenderSurveyed", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (214, 14, 2, "TraSurNumberOfIndividualsSampled", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (215, 14, 2, "TraSurNumberOfIndividualsWithNonResponse", 5, 0, 0, 0, 0, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (216, 14, 2, "TraSurNumberOfIndividualsExaminedTf", 5, 0, 0, 0, -1, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (217, 14, 2, "TraSurNumberOfIndividualsWithCSTF", 5, 0, 0, 0, -1, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (218, 14, 13, "TraSurWithClinicalSignTf", 5, 0, 0, 0, 0, -1, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (219, 14, 2, "TraSurNumberOfIndividualsExaminedTt", 5, 0, 0, 0, -1, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (220, 14, 2, "TraSurNumberOfIndividualsWithCSTT", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (221, 14, 13, "TraSurWithClinicalSignTt", 5, 0, 0, 0, 0, -1, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (222, 14, 2, "TraSurNumberOfIndividualsExaminedTi", 5, 0, 0, 0, -1, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (223, 14, 2, "TraSurNumberOfIndividualsWithCSTI", 5, 0, 0, 0, -1, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (224, 14, 13, "TraSurWithClinicalSignTi", 5, 0, 0, 0, 0, -1, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (225, 14, 2, "TraSurNumberOfIndividualsExaminedTs", 5, 0, 0, 0, -1, 0, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (226, 14, 2, "TraSurNumberOfIndividualsWithCSTS", 5, 0, 0, 0, -1, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (227, 14, 13, "TraSurWithClinicalSignTs", 5, 0, 0, 0, 0, -1, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (228, 14, 2, "TraSurNumberOfIndividualsExaminedCo", 5, 0, 0, 0, -1, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (229, 14, 2, "TraSurNumberOfIndividualsWithClinicalSig", 5, 0, 0, 0, -1, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (230, 14, 13, "TraSurWithClinicalSignCo", 5, 0, 0, 0, 0, -1, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (231, 14, 2, "TraSurNumberOfIndividualsWithBlindnessDu", 5, 0, 0, 0, 0, 0, 0, 29, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (232, 14, 2, "TraSurOfNationalBlindness", 5, 0, 0, 0, 0, 0, 0, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (233, 14, 5, "TraSurLevelOfAntibioticTreatmentRequired", 5, 0, 0, 0, 0, 0, 0, 31, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (234, 14, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 32, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (235, 15, 2, "TASTotalEuPopulation", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (236, 15, 2, "TASTotalEuAreaKm2", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (237, 15, 1, "TASSiteName", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (238, 15, 6, "TASParasiteSpeciesInEu", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (239, 15, 6, "TASPredominateVectorInEu", 5, 0, 0, 0, 0, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (240, 15, 2, "TASNumOfEffectiveMdasCompleted", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (241, 15, 7, "TASYearOfFirstEffectiveMda", 5, 0, 0, 0, -1, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (242, 15, 7, "TASYearOfMostRecentEffectiveMda", 5, 0, 0, 0, -1, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (243, 15, 5, "TASTasObjective", 5, 0, 0, 0, -1, 0, -1, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (244, 15, 2, "TASEuNetPrimarySchoolEnrolmentRate", 5, 0, 0, 0, 0, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (245, 15, 6, "TASDiagnosticTest", 5, 0, 0, 0, -1, 0, -1, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (246, 15, 5, "TASLocationType", 5, 0, 0, 0, -1, 0, -1, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (247, 15, 4, "TASStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (248, 15, 4, "TASEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (249, 15, 5, "TASSurveyType", 5, 0, 0, 0, -1, 0, -1, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (250, 15, 1, "TASAgeRange", 5, 0, 0, 0, -1, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (251, 15, 2, "TASTargetNumberOfSchoolsOrEas", 5, 0, 0, 0, 0, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (252, 15, 2, "TASTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (253, 15, 2, "TASCriticalCutoffValue", 5, 0, 0, 0, 0, 0, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (254, 15, 2, "TASSamplingInterval", 5, 0, 0, 0, 0, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (255, 15, 2, "TASActualNumberOfSchoolsOrEasSurveyed", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (256, 15, 2, "TASActualSampleSizePositive", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (257, 15, 2, "TASActualSampleSizeNegative", 5, 0, 0, 0, 0, 0, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (258, 15, 13, "TASActualSampleSizeTotal", 5, 0, 0, 0, 0, -1, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (259, 15, 2, "TASActualNonResponseRateAbsent", 5, 0, 0, 0, 0, 0, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (260, 15, 2, "TASActualNonResponseRateRefusalOrNoConse", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (261, 15, 2, "TASActualNonResponseRateInabilityToPerfo", 5, 0, 0, 0, 0, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (262, 15, 2, "TASActualNonResponseRateTotal", 5, 0, 0, 0, 0, 0, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (263, 15, 4, "TASTasActualTimelineAndResourcesStartDat", 5, 0, 0, 0, 0, 0, 0, 29, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (264, 15, 2, "TASTasActualTimelineAndResourcesNuOfSurve", 5, 0, 0, 0, 0, 0, 0, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (265, 15, 2, "TASTasActualTimelineAndResourcesActualCo", 5, 0, 0, 0, 0, 0, 0, 31, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (266, 15, 2, "TASTasActualTimelineAndResourcesActualCu", 5, 0, 0, 0, 0, 0, 0, 32, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (267, 15, 1, "TASTasActualTimelineAndResourcesSourceOf", 5, 0, 0, 0, 0, 0, 0, 33, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (268, 15, 2, "TASTasActualTimelineAndResourcesOfIctOr", 5, 0, 0, 0, 0, 0, 0, 34, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (269, 15, 2, "TASTasActualTimelineAndResourcesTeams", 5, 0, 0, 0, 0, 0, 0, 35, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (270, 15, 2, "TASTasActualTimelineAndResourcesStaff", 5, 0, 0, 0, 0, 0, 0, 36, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (271, 15, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 37, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (272, 16, 1, "LFMapSurSiteNames", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (273, 16, 5, "LFMapSurWillTheSitesAlsoServeAsASentin", 5, 0, 0, 0, 0, 0, -1, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (274, 16, 2, "LFMapSurLatitude", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (275, 16, 2, "LFMapSurLongitude", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (276, 16, 2, "LFMapSurPopulationLivingInMappingSites", 5, 0, 0, 0, 0, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (277, 16, 6, "LFMapSurVector", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (278, 16, 5, "LFMapSurCausalAgent", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (279, 16, 5, "LFMapSurTestType", 5, 0, 0, 0, -1, 0, -1, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (280, 16, 5, "LFMapSurMappingType", 5, 0, 0, 0, 0, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (281, 16, 5, "LFMapSurMappingSiteLocation", 5, 0, 0, 0, 0, 0, -1, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (282, 16, 4, "LFMapSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (283, 16, 4, "LFMapSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (284, 16, 2, "LFMapSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (285, 16, 1, "LFMapSurAgeRange", 5, 0, 0, 0, -1, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (286, 16, 2, "LFMapSurNumberOfIndividualsSampled", 5, 0, 0, 0, 0, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (287, 16, 2, "LFMapSurNumberOfIndividualsWithNonRespon", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (288, 16, 2, "LFMapSurNumberOfIndividualsExamined", 5, 0, 0, 0, -1, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (289, 16, 2, "LFMapSurNumberOfFemalesExamined", 5, 0, 0, 0, 0, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (290, 16, 2, "LFMapSurNumberOfMalesExamined", 5, 0, 0, 0, 0, 0, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (291, 16, 2, "LFMapSurNumberOfIndividualsPositive", 5, 0, 0, 0, -1, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (293, 16, 2, "LFMapSurNumberOfFemalesPositive", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (294, 16, 2, "LFMapSurNumberOfMalesPositive", 5, 0, 0, 0, 0, 0, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (295, 16, 2, "LFMapSurMeanDensity", 5, 0, 0, 0, 0, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (296, 16, 2, "LFMapSurCount", 5, 0, 0, 0, 0, 0, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (297, 16, 2, "LFMapSurCommunityMfLoad", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (298, 16, 2, "LFMapSurNumberOfCasesOfLymphoedema", 5, 0, 0, 0, 0, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (299, 16, 2, "LFMapSurNumberOfCasesOfHydrocele", 5, 0, 0, 0, 0, 0, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (300, 16, 5, "LFMapSurEndemity", 5, 0, 0, 0, -1, 0, -1, 29, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (301, 16, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (302, 19, 5, "OnchoMapSurSurveyType", 5, 0, 0, 0, -1, 0, -1, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (303, 19, 5, "OnchoMapSurEntomologicalAssessmentConduc", 5, 0, 0, 0, 0, 0, 0, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (304, 19, 1, "OnchoMapSurIfEntomologicalAssessmentSite", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (305, 19, 1, "OnchoMapSurIfEntomologicalAssessmentRiver", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (306, 19, 2, "OnchoMapSurLatitude", 5, 0, 0, 0, 0, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (307, 19, 2, "OnchoMapSurLongitude", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (308, 19, 4, "OnchoMapSurDateOfTheLastTreatment", 5, 0, 0, 0, -1, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (309, 19, 2, "OnchoMapSurNumberOfRoundsOfPcCompletedPr", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (310, 19, 5, "OnchoMapSurTestType", 5, 0, 0, 0, -1, 0, -1, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (311, 19, 4, "OnchoMapSurStartDateOfSurvey", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (312, 19, 4, "OnchoMapSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (313, 19, 2, "OnchoMapSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (314, 19, 1, "OnchoMapSurAgeRange", 5, 0, 0, 0, 0, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (315, 19, 2, "OnchoMapSurReportedPopulation", 5, 0, 0, 0, 0, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (316, 19, 2, "OnchoMapSurRegistrationPopulation", 5, 0, 0, 0, -1, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (317, 19, 2, "OnchoMapSurNumberOfIndividualsExamined", 5, 0, 0, 0, -1, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (318, 19, 13, "OnchoMapSurAttendanceRate", 5, 0, 0, 0, 0, -1, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (319, 19, 2, "OnchoMapSurNumberOfIndividualsPositive", 5, 0, 0, 0, 0, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (321, 17, 1, "SCHMapSurSiteNames", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (322, 17, 5, "SCHMapSurWillSite", 5, 0, 0, 0, 0, 0, -1, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (323, 17, 2, "SCHMapSurLatitude", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (324, 17, 2, "SCHMapSurLongitude", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (325, 17, 6, "SCHMapSurCausalAgent", 5, 0, 0, 0, -1, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (326, 17, 6, "SCHMapSurTestType", 5, 0, 0, 0, -1, 0, -1, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (327, 17, 4, "SCHMapSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (328, 17, 4, "SCHMapSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (329, 17, 2, "SCHMapSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (330, 17, 1, "SCHMapSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (331, 17, 2, "SCHMapSurNumberOfIndividualsSampled", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (332, 17, 2, "SCHMapSurNumberOfIndividualsWithNonRespo", 5, 0, 0, 0, 0, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (333, 17, 2, "SCHMapSurNumberOfIndividualsExaminedForU", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (334, 17, 2, "SCHMapSurNumberOfIndividualsPositiveForH", 5, 0, 0, 0, -1, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (335, 17, 13, "SCHMapSurPrevalenceOfAnyHaemuaturiaOrPar", 5, 0, 0, 0, 0, -1, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (336, 17, 2, "SCHMapSurProportionOfHeavyIntensityUrina", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (337, 17, 2, "SCHMapSurProportionOfModerateIntensityUr", 5, 0, 0, 0, 0, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (338, 17, 2, "SCHMapSurNumberOfIndividualsExaminedForI", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (339, 17, 2, "SCHMapSurNumberOfIndividualsPositiveForI", 5, 0, 0, 0, -1, 0, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (340, 17, 13, "SCHMapSurPrevalenceOfIntestinalSchistoso", 5, 0, 0, 0, 0, -1, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (341, 17, 2, "SCHMapSurProportionOfHeavyIntensityIntes", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (342, 17, 2, "SCHMapSurProportionOfModerateIntensityIn", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (343, 17, 5, "SCHMapSurEndemity", 5, 0, 0, 0, -1, 0, -1, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (344, 17, 5, "SCHMapSurLevelOfEndemicity", 5, 0, 0, 0, 0, 0, -1, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (345, 17, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (346, 18, 1, "STHMapSurSurSiteNames", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (347, 18, 5, "STHMapSurSurWillTheSitesAlsoServeAsASentinelO", 5, 0, 0, 0, 0, 0, -1, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (348, 18, 2, "STHMapSurSurLatitude", 5, 0, 0, 0, 0, 0, 0, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (349, 18, 2, "STHMapSurSurLongitude", 5, 0, 0, 0, 0, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (350, 18, 6, "STHMapSurSurCausalAgent", 5, 0, 0, 0, -1, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (351, 18, 5, "STHMapSurSurTestType", 5, 0, 0, 0, -1, 0, -1, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (352, 18, 4, "STHMapSurSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (353, 18, 4, "STHMapSurSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (354, 18, 2, "STHMapSurSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (355, 18, 1, "STHMapSurSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (356, 18, 2, "STHMapSurSurNumberOfIndividualsSampled", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (357, 18, 2, "STHMapSurSurNumberOfIndividualsWithNonResponse", 5, 0, 0, 0, 0, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (358, 18, 2, "STHMapSurSurNumberOfIndividualsExaminedAscaris", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (359, 18, 2, "STHMapSurSurNumberOfIndividualsPositiveAscaris", 5, 0, 0, 0, -1, 0, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (360, 18, 13, "STHMapSurSurPerPositiveAscaris", 5, 0, 0, 0, 0, -1, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (361, 18, 2, "STHMapSurSurProportionOfHeavyIntensityOfInAS", 5, 0, 0, 0, 0, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (362, 18, 2, "STHMapSurSurProportionOfModerateIntensityOfInAS", 5, 0, 0, 0, 0, 0, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (363, 18, 2, "STHMapSurSurNumberOfIndividualsExaminedHookwor", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (364, 18, 2, "STHMapSurSurNumberOfIndividualsPositiveHookwor", 5, 0, 0, 0, -1, 0, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (365, 18, 13, "STHMapSurSurPerPositiveHookworm", 5, 0, 0, 0, 0, -1, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (366, 18, 2, "STHMapSurSurProportionOfHeavyIntensityOfInHook", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (367, 18, 2, "STHMapSurSurProportionOfModerateIntensityOfInHook", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (368, 18, 2, "STHMapSurSurNumberOfIndividualsExaminedTrichur", 5, 0, 0, 0, -1, 0, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (369, 18, 2, "STHMapSurSurNumberOfIndividualsPositiveTrichur", 5, 0, 0, 0, -1, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (370, 18, 13, "STHMapSurSurPerPositiveTrichuris", 5, 0, 0, 0, 0, -1, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (371, 18, 2, "STHMapSurSurProportionOfHeavyIntensityOfInfecti", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (372, 18, 2, "STHMapSurSurProportionOfModerateIntensityOfInfe", 5, 0, 0, 0, 0, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (373, 18, 2, "STHMapSurSurNumberOfIndividualsExaminedOverall", 5, 0, 0, 0, -1, 0, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (374, 18, 2, "STHMapSurSurNumberOfIndividualsPositiveOverall", 5, 0, 0, 0, -1, 0, 0, 29, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (375, 18, 13, "STHMapSurSurPerPositiveOverall", 5, 0, 0, 0, 0, -1, 0, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (376, 18, 2, "STHMapSurSurOverallProportionOfHeavyIntensity", 5, 0, 0, 0, 0, 0, 0, 31, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (377, 18, 2, "STHMapSurSurOverallProportionOfModerateIntensi", 5, 0, 0, 0, 0, 0, 0, 32, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (378, 18, 5, "STHMapSurSurEndemity", 5, 0, 0, 0, -1, 0, -1, 33, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (379, 18, 5, "STHMapSurSurLevelOfEndemicity", 5, 0, 0, 0, 0, 0, -1, 34, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (380, 18, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 35, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (381, 20, 1, "TraMapSurSiteNames", 5, 0, 0, 0, 0, 0, 0, 1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (382, 20, 5, "TraMapSurSurveyType", 5, 0, 0, 0, -1, 0, -1, 2, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (383, 20, 5, "TraMapSurSurveyPower", 5, 0, 0, 0, 0, 0, -1, 3, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (384, 20, 6, "TraMapSurClinicalSignAssessed", 5, 0, 0, 0, -1, 0, 0, 4, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (385, 20, 4, "TraMapSurStartDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 5, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (386, 20, 4, "TraMapSurEndDateOfSurvey", 5, 0, 0, 0, 0, 0, 0, 6, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (387, 20, 2, "TraMapSurTargetSampleSize", 5, 0, 0, 0, -1, 0, 0, 7, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (388, 20, 1, "TraMapSurAgeGroupSurveyed", 5, 0, 0, 0, -1, 0, 0, 8, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (389, 20, 5, "TraMapSurGenderSurveyed", 5, 0, 0, 0, 0, 0, 0, 9, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (390, 20, 2, "TraMapSurNumberOfIndividualsSampled", 5, 0, 0, 0, -1, 0, 0, 10, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (391, 20, 2, "TraMapSurNumberOfIndividualsWithNonResp", 5, 0, 0, 0, 0, 0, 0, 11, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (392, 20, 2, "TraMapSurNumberOfIndividualsExaminedTf", 5, 0, 0, 0, -1, 0, 0, 12, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (393, 20, 2, "TraMapSurNumberOfIndividualsWithClTF", 5, 0, 0, 0, -1, 0, 0, 13, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (394, 20, 13, "TraMapSurPerWithClinicalSignTf", 5, 0, 0, 0, 0, -1, 0, 14, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (395, 20, 2, "TraMapSurNumberOfIndividualsExaminedTt", 5, 0, 0, 0, -1, 0, 0, 15, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (396, 20, 2, "TraMapSurNumberOfIndividualsWithClTT", 5, 0, 0, 0, -1, 0, 0, 16, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (397, 20, 13, "TraMapSurPerWithClinicalSignTt", 5, 0, 0, 0, 0, -1, 0, 17, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (398, 20, 2, "TraMapSurNumberOfIndividualsExaminedTi", 5, 0, 0, 0, -1, 0, 0, 18, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (399, 20, 2, "TraMapSurNumberOfIndividualsWithClTI", 5, 0, 0, 0, -1, 0, 0, 19, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (400, 20, 13, "TraMapSurPerWithClinicalSignTi", 5, 0, 0, 0, 0, -1, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (401, 20, 2, "TraMapSurNumberOfIndividualsExaminedTs", 5, 0, 0, 0, -1, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (402, 20, 2, "TraMapSurNumberOfIndividualsWithClTS", 5, 0, 0, 0, -1, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (403, 20, 13, "TraMapSurPerWithClinicalSignTs", 5, 0, 0, 0, 0, -1, 0, 23, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (404, 20, 2, "TraMapSurNumberOfIndividualsExamined", 5, 0, 0, 0, -1, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (405, 20, 2, "TraMapSurNumberOfIndividualsWithClinical", 5, 0, 0, 0, -1, 0, 0, 25, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (406, 20, 13, "TraMapSurPerWithClinicalSignCo", 5, 0, 0, 0, 0, -1, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (407, 20, 2, "TraMapSurNumberOfIndividualsWithBlindnes", 5, 0, 0, 0, 0, 0, 0, 27, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (408, 20, 2, "TraMapSurPerOfNationalBlindness", 5, 0, 0, 0, 0, 0, 0, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (409, 20, 5, "TraMapSurEndemicity", 5, 0, 0, 0, -1, 0, 0, 29, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (410, 20, 5, "TraMapSurLevelOfAntibioticTreatmentRequi", 5, 0, 0, 0, 0, 0, 0, 30, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (411, 20, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 31, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (412, 10, 10, "SentinelSitePicker", 5, 0, 0, 0, 0, 0, -1, -99, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (413, 11, 10, "SentinelSitePicker", 5, 0, 0, 0, 0, 0, -1, -99, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (414, 12, 10, "SentinelSitePicker", 5, 0, 0, 0, 0, 0, -1, -99, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (415, 15, 12, "EuName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (416, 20, 5, "SuperDistrict", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (417, 11, 11, "EzName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (418, 12, 11, "EzName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (419, 17, 11, "EzName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (420, 18, 11, "EzName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (421, 10, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (422, 11, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (423, 12, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (424, 13, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (425, 14, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (426, 15, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (427, 16, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (428, 17, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (429, 18, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (430, 19, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (431, 20, 4, "DateReported", 5, 0, 0, 0, -1, 0, 0, -1, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (432, 19, 2, "OnchoMapSurStandardizedPrevelance", 5, 0, 0, 0, 0, 0, 0, 20, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (433, 19, 2, "OnchoMapSurCmfl", 5, 0, 0, 0, 0, 0, 0, 21, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (434, 19, 2, "OnchoMapSurIfTestTypeIsNpNumDep", 5, 0, 0, 0, 0, 0, 0, 22, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (436, 19, 2, "OnchoMapSurIfTestTypeIsNpNumNod", 5, 0, 0, 0, 0, 0, 0, 24, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (438, 19, 2, "OnchoMapSurIfTestTypeIsNpNumWri", 5, 0, 0, 0, 0, 0, 0, 26, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (440, 19, 9, "Partners", 5, 0, 0, 0, 0, 0, -1, 28, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (441, 8, 13, "PercentNewCasesPcrCm", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (442, 10, 13, "LFSurPositive", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (443, 13, 13, "OnchoSurAttendanceRate", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (445, 13, 13, "OnchoSurPercentPositive", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (446, 13, 13, "OnchoSurIfTestTypeIsNpPerDep", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (447, 13, 13, "OnchoSurIfTestTypeIsNpPerNod", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (448, 13, 13, "OnchoSurIfTestTypeIsNpPerWri", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (449, 11, 13, "SCHSurPrevalenceOfIntestinalSchistosomeI", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (450, 11, 13, "SCHSurPrevalenceOfAnyHaemuaturiaOrParasi", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (451, 12, 13, "STHSurPositiveOverall", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (452, 12, 13, "STHSurPositiveAscaris", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (453, 12, 13, "STHSurPositiveTrichuris", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (454, 12, 13, "STHSurPositiveHookworm", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (455, 16, 13, "LFMapSurPositive", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (456, 19, 13, "OnchoMapSurPercentPositive", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (457, 19, 13, "OnchoMapSurIfTestTypeIsNpPerDep", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (458, 19, 13, "OnchoMapSurIfTestTypeIsNpPerNod", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (459, 19, 13, "OnchoMapSurIfTestTypeIsNpPerWri", 5, 0, 0, 0, 0, -1, 0, 0, 26, #12/01/2013#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (466, 14, 14, "SubDistrictName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicators] ([ID], [SurveyTypeId], [DataTypeId], [DisplayName], [AggTypeId], [IsDisabled], [IsEditable], [IsDisplayed], [IsRequired], [IsCalculated], [CanAddValues], [SortOrder], [UpdatedById], [UpdatedAt]) VALUES (467, 20, 14, "SubDistrictName", 5, 0, 0, 0, -1, 0, -1, 0, 26, #10/17/2013 14:32:02#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (54, 53, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (55, 54, 2, "2010", 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (56, 55, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (57, 56, 2, "234", 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (58, 57, 2, "23", 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (59, 58, 2, "9.83", 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (60, 59, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (61, 60, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (62, 61, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (63, 62, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (64, 63, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (65, 64, 2, NULL, 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (66, 65, 2, "123", 26, #09/22/2013 15:53:11#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (67, 73, 3, "1", 26, #11/07/2013 16:12:02#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (68, 74, 3, "2", 26, #11/07/2013 16:12:02#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (69, 75, 3, "3", 26, #11/07/2013 16:12:02#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (70, 96, 3, "2010", 26, #11/07/2013 16:12:02#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (71, 73, 4, "5", 26, #11/07/2013 16:15:04#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (72, 74, 4, "5", 26, #11/07/2013 16:15:04#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (73, 75, 4, "5", 26, #11/07/2013 16:15:04#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (74, 96, 4, "2010", 26, #11/07/2013 16:15:04#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (75, 73, 5, "2", 26, #11/07/2013 16:16:45#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (76, 74, 5, "2", 26, #11/07/2013 16:16:45#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (77, 75, 5, "2", 26, #11/07/2013 16:16:45#);
INSERT INTO [SurveyIndicatorValues] ([ID], [IndicatorId], [SurveyId], [DynamicValue], [UpdatedById], [UpdatedAt]) VALUES (78, 96, 5, "2010", 26, #11/07/2013 16:16:45#);
INSERT INTO [Surveys_to_Partners] ([SurveyId], [PartnerId]) VALUES (2, 1);
INSERT INTO [Surveys_to_Vectors] ([SurveyId], [VectorId]) VALUES (2, 1);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (16, "SurLfMapping", 3, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (17, "SurSchMapping", 12, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (18, "SurSthMapping", 5, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (19, "SurOnchoMapping", 4, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (20, "SurTrachomaMapping", 13, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, "SurGwSurvey", 6, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (5, "SurLeprosySurvey", 7, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (6, "SurHatSurvey", 8, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (7, "SurLeishSurvey", 9, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (8, "SurBuSurvey", 10, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (9, "SurYawsSurvey", 11, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (10, "SurLfSentinelSpotCheckSurvey", 3, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (11, "SurSchSentinelSpotCheckSurvey", 12, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (12, "SurSthSentinelSpotCheckSurvey", 5, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (13, "SurOnchoAssesments", 4, 0, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (14, "SurImpactSurvey", 13, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [SurveyTypes] ([ID], [SurveyTypeName], [DiseaseId], [HasMultipleLocations], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (15, "SurTransAssessSurvey", 3, -1, 26, #08/16/2013 17:27:45#, 26, #08/16/2013 17:27:45#);
INSERT INTO [Vectors] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (1, "Culex", 0, 26, #09/15/2013#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Vectors] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (2, "Anopheles", 0, 26, #09/15/2013#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Vectors] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (3, "Aedes", 0, 26, #09/15/2013#, 26, #10/03/2013 13:53:07#);
INSERT INTO [Vectors] ([ID], [DisplayName], [IsDeleted], [UpdatedById], [UpdatedAt], [CreatedById], [CreatedAt]) VALUES (4, "Mansonia", 0, 26, #09/15/2013#, 26, #10/03/2013 13:53:07#);
