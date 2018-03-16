CREATE TABLE [dbo].[Currency](
	[Cid] [int] not null constraint [UQ_Currency] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_Currency] primary key default newid(),
	[Code] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Symbol] [nvarchar](20) NOT NULL,

	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,
);

CREATE TABLE [dbo].[Language](
	[Cid] [int] not null constraint [UQ_Language] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_Language] primary key default newid(),
	[Iso639_1] [nvarchar](max) NOT NULL,
	[Iso639_2] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[NativeName] [nvarchar](150) NULL,

	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,
);

CREATE TABLE [dbo].[Country](
	[Cid] [int] not null constraint [UQ_Country] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_Country] primary key default newid(),
	[Name] [nvarchar](150) NOT NULL,
	[Alpha2Code] [nvarchar](max) NULL,
	[Alpha3Code] [nvarchar](max) NULL,
	[Capital] [nvarchar](150) NULL,
	[Region] [nvarchar](150) NULL,
	[Subregion] [nvarchar](150) NULL,
	[Population] [bigint] NULL,
	[Demonym] [nvarchar](max) NULL,
	[Area] [decimal](18, 2) NULL,
	[Gini] [decimal](18, 2) NULL,
	[NativeName] [nvarchar](150) NULL,
	[NumericCode] [nvarchar](150) NULL,
	[Flag] [nvarchar](max) NULL,
	[Cioc] [nvarchar](max) NULL,
	[Latlng] [nvarchar](max) NULL,
	[TopLevelDomain] [nvarchar](max) NULL,
	[CallingCodes] [nvarchar](max) NULL,
	[AltSpellings] [nvarchar](max) NULL,
	[Timezones] [nvarchar](max) NULL,
	[Borders] [nvarchar](max) NULL,

	[SearchCount] bigint NOT NULL,

	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,
)

CREATE TABLE [dbo].[CountryLanguage](
	[Cid] [int] not null constraint [UQ_CountryLanguage] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_CountryLanguage] primary key default newid(),
	[CountryId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,

	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,

	constraint [FK_CountryLanguage_LanguageId] foreign key([LanguageId]) references [Language] ([Id]),
	constraint [FK_CountryLanguage_CountryId] foreign key([CountryId]) references [Country] ([Id]),
);

CREATE TABLE [dbo].[CoutryCurrency](
	[Cid] [int] not null constraint [UQ_CoutryCurrency] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_CoutryCurrency] primary key default newid(),
	[CountryId] [uniqueidentifier] NOT NULL,
	[CurrencyId] [uniqueidentifier] NOT NULL,

	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,

	constraint [FK_CoutryCurrency_CurrencyId] foreign key([CurrencyId]) references [Currency] ([Id]),
	constraint [FK_CoutryCurrency_CountryId] foreign key([CountryId]) references [Country] ([Id])
);

create table [FavoriteUserCountry](
	[Cid] [int] not null constraint [UQ_FevoriteUserCountry] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_FevoriteUserCountry] primary key default newid(),
	[UserId] [nvarchar](128) NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[IsFavorite] bit NOT NULL,

	[CreatedBy] [nvarchar](max) null,
	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,

	constraint [FK_FevoriteUserCountry_UserId] foreign key([UserId]) references [User] ([Id]),
	constraint [FK_FevoriteUserCountry_CountryId] foreign key([CountryId]) references [Country] ([Id]),
);

create table [Log](
	[Cid] [int] not null constraint [UQ_Log] unique clustered identity,
	[Id] [uniqueidentifier] not null constraint [PK_Log] primary key default newid(),

	[ControllerName] [nvarchar](128) NOT NULL,
	[ActionName][nvarchar](128) NOT NULL,
	[Parameter] [nvarchar](max) NOT NULL,
	[RequestUrl] [nvarchar](max) NOT NULL,
	[Ip] [nvarchar](128) NOT NULL,
	[Activity] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](128) NOT NULL,
	[LogInOn] [datetime2] null,
	[LogOutOn] [datetime2] null,

	[UserId] [nvarchar](128) NULL,
	[CountryId] [uniqueidentifier] NULL,

	[CreatedBy] [nvarchar](max) null,
	[CreatedOn] [datetime2] not null default getdate(),
	[ModifiedBy] [nvarchar](max) null,
	[ModifiedOn] [datetime2] null,
	[DeletedBy] [nvarchar](max) null,
	[DeletedOn] [datetime2] null,

	constraint [FK_Log_UserId] foreign key([UserId]) references [User] ([Id]),
	constraint [FK_Log_CountryId] foreign key([CountryId]) references [Country] ([Id]),
);

ALTER TABLE [User] ADD Activate bit default 1;

INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'0257EB6D-4288-4A93-9BF0-F93244ABD80E', N'Administrator')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'B4BC18D3-81FC-42C1-8326-AA0C06225485', N'Editor')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (N'CCF73D8C-0729-4CC7-8631-539A5AF62467', N'User')

USE [CMS-Db]
GO
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'0b819055-118f-48ae-a528-845de4d2c7cc', N'editor2018@cms.com', 0, N'AD7mBxW/cn103auKFQxSnt/SIPS1NHJmY5URci+XF1l9uzUnA+3JN6FJUWTfeTg2zA==', N'65a4622e-de07-4822-8864-e79f8f96bb18', NULL, 0, 0, NULL, 0, 0, N'editor2018@cms.com', N'User', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'86c6940a-0d67-4496-8f29-908b4cd24270', N'Mahesh@m.com', 0, N'ABNpRm0GAIY5+TqqqrRt1KPDnObvwXLJvrdE/+AdbE6Tp5nhkX5miKisoKEQ6EUEQQ==', N'bf5ae5c2-3d31-42a0-b069-d2aafe1eb5f1', NULL, 0, 0, NULL, 0, 0, N'Mahesh@m.com', N'User', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'897fa43e-3d2a-4e94-88af-768748e684fb', N'mahesh2018@gmail.com', 0, N'AI0zOZ7kXyQnpKWbchYkfsoyW5ROTYSIVQbRRyWq2mnV2uug++lWDoLHVS+Fgyu+JQ==', N'a8ce3ef5-d44c-461a-9448-5796cc1f448b', NULL, 0, 0, NULL, 0, 0, N'mahesh2018@gmail.com', N'User', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'b71f3734-c116-4b98-af88-420257453366', N'mahesh1@mahesh.com', 0, N'AGuhNswlpKwy4dqyu2pFyanRDvllzMy3smKyyA9YkSJentk7+b+n67/skyv7BgwElQ==', N'7f9ab704-5164-435f-9b5f-5e22642d150f', NULL, 0, 0, NULL, 0, 0, N'mahesh1@mahesh.com', N'User', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'e9ec9170-ef16-496f-8e18-2090776e6f3c', N'admin2018@cms.com', 0, N'AHbQknUfOwC2LxG0LeEodUG0njkMbLyWhsOFeFfsOb9jjPnDpOF3eus1WO5Vtqkh3Q==', N'0e9f61b6-b660-4091-89ac-b97d8afaeba5', NULL, 0, 0, NULL, 0, 0, N'admin2018@cms.com', N'User', 1)
