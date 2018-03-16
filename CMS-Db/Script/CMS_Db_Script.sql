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
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'e9ec9170-ef16-496f-8e18-2090776e6f3c', N'admin2018@cms.com', 0, N'AHbQknUfOwC2LxG0LeEodUG0njkMbLyWhsOFeFfsOb9jjPnDpOF3eus1WO5Vtqkh3Q==', N'0e9f61b6-b660-4091-89ac-b97d8afaeba5', NULL, 0, 0, NULL, 0, 0, N'admin2018@cms.com', N'User', 1)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'5dda7914-039c-4521-8afe-b6c94b245d69', N'user2018@cms.com', 0, N'AJwOfcHE2Gyht4KPYgfB9C874NDagAXbr6bphfaxCb9TTEVGwd7yLNdHIWqS/vXGKg==', N'436aaee9-a84e-4f60-8681-0161e23fbf6a', NULL, 0, 0, NULL, 0, 0, N'user2018@cms.com', N'User', 0)
INSERT [dbo].[User] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Discriminator], [Activate]) VALUES (N'0b819055-118f-48ae-a528-845de4d2c7cc', N'editor2018@cms.com', 0, N'AD7mBxW/cn103auKFQxSnt/SIPS1NHJmY5URci+XF1l9uzUnA+3JN6FJUWTfeTg2zA==', N'65a4622e-de07-4822-8864-e79f8f96bb18', NULL, 0, 0, NULL, 0, 0, N'editor2018@cms.com', N'User', 1)
