IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Site_Page')
BEGIN
	CREATE TABLE [dbo].[Site_Page](
		[PageId] [int] IDENTITY(1,1) NOT NULL,
		[SectionName] [varchar](50) NOT NULL,
		[AreaName] [varchar](50) NOT NULL,
		[createDate] [datetime] NOT NULL,
		[ApplicationId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_Site_Page] PRIMARY KEY CLUSTERED 
	(
		[PageId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]	
END

IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Site_PageContent')
BEGIN
	CREATE TABLE [dbo].[Site_PageContent](
		[ContentId] [int] IDENTITY(1,1) NOT NULL,
		[PageId] [int] NOT NULL,
		[ContentBody] [varchar](max) NOT NULL,
		[ContentTitle] [varchar](250) NULL,
		[ContentAuthor] [uniqueidentifier] NOT NULL,
		[PublishDate] [datetime] NULL,
		[CreateDate] [datetime] NULL,
	 CONSTRAINT [PK_Site_PageContent] PRIMARY KEY CLUSTERED 
	(
		[ContentId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Site_Gallery')
BEGIN
	CREATE TABLE [dbo].[Site_Gallery](
		[GalleryId] [int] NOT NULL,
		[GalleryName] [varchar](250) NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL,
		[ApplicationId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
	(
		[GalleryId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Site_Image')
BEGIN
	CREATE TABLE [dbo].[Site_Image](
		[ImageId] [int] IDENTITY(1,1) NOT NULL,
		[Width] [float] NULL,
		[Height] [float] NULL,
		[ImageUrl] [varchar](250) NULL,
		[Alt] [varchar](250) NULL,
		[GalleryId] [int] NULL,
		[ImageFormat] [varchar](10) NOT NULL,
		[ImageName] [varchar](250) NOT NULL,
		[ThumbNail] [image] NOT NULL,
		[FullImage] [image] NOT NULL,
	 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
	(
		[ImageId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END

IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'aspnet_AuthenticationSources')
BEGIN
	CREATE TABLE [dbo].[aspnet_AuthenticationSources](
		[UserId] [uniqueidentifier] NOT NULL,
		[AuthenticationSource] [varchar](50) NOT NULL,
		[ApplicationId] [uniqueidentifier] NOT NULL,
		[AuthenticationSourceID] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_aspnet_AuthenticationSource] PRIMARY KEY CLUSTERED 
	(
		[AuthenticationSourceID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[aspnet_AuthenticationSources] ADD  CONSTRAINT [DF_aspnet_AuthenticationSource_AuthenticationSourceID]  DEFAULT (newid()) FOR [AuthenticationSourceID]
END