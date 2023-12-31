USE [master]
GO
/****** Object:  Database [Ownership]    Script Date: 10/25/2023 3:50:36 PM ******/
CREATE DATABASE [Ownership]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ownership', FILENAME = N'D:\Databases\Ownership.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ownership_log', FILENAME = N'D:\Databases\Ownership_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Ownership] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ownership].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ownership] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ownership] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ownership] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ownership] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ownership] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ownership] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ownership] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ownership] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ownership] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ownership] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ownership] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ownership] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ownership] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ownership] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ownership] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ownership] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ownership] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ownership] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ownership] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ownership] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ownership] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ownership] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ownership] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Ownership] SET  MULTI_USER 
GO
ALTER DATABASE [Ownership] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ownership] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ownership] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ownership] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ownership] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Ownership] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Ownership] SET QUERY_STORE = OFF
GO
USE [Ownership]
GO
/****** Object:  Table [dbo].[Owner]    Script Date: 10/25/2023 3:50:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owner](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OwnerType] [varchar](30) NULL,
	[Name] [nvarchar](150) NULL,
	[OwnerId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Owner] ON 

INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (1, N'Team', N'Global Support', N'18875cd6-c069-4ba9-b762-d2cc13c8aeb4', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (2, N'Team', N'Customer Support', N'd0d806be-75cb-4fa1-878b-bb317205eb90', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (3, N'Team', N'Accountant', N'90c567d7-fedd-420a-a204-9ed425961b58', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (4, N'Queue', N'Queue 1', N'c19ce968-d4d3-43e5-b067-3ca61aa3905e', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (5, N'Queue', N'Queue 2', N'e7deb0dd-4478-41dc-953b-622f2e4423e2', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (6, N'Queue', N'Queue 3', N'ef2cab66-c302-4435-ac5b-07a89f3ccda7', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (7, N'User', N'User 1', N'770fa5b9-6ae0-4018-a89d-31e388fd040c', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (8, N'User', N'User 2', N'e19da973-13e6-49f6-a4c6-5a2f20c50cfe', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
INSERT [dbo].[Owner] ([Id], [OwnerType], [Name], [OwnerId], [CreatedDate], [UpdatedDate]) VALUES (9, N'User', N'User 3', N'32099909-50bb-48ce-bd29-7aa7b7ad8265', CAST(N'2023-10-23T16:36:11.833' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Owner] OFF
GO
USE [master]
GO
ALTER DATABASE [Ownership] SET  READ_WRITE 
GO
