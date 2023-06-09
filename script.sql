USE [master]
GO
/****** Object:  Database [BudgetManager]    Script Date: 24/06/2023 18:59:18 ******/
CREATE DATABASE [BudgetManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BudgetManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BudgetManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BudgetManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BudgetManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BudgetManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BudgetManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BudgetManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BudgetManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BudgetManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BudgetManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BudgetManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [BudgetManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BudgetManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BudgetManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BudgetManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BudgetManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BudgetManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BudgetManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BudgetManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BudgetManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BudgetManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BudgetManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BudgetManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BudgetManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BudgetManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BudgetManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BudgetManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BudgetManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BudgetManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BudgetManager] SET  MULTI_USER 
GO
ALTER DATABASE [BudgetManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BudgetManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BudgetManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BudgetManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BudgetManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BudgetManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BudgetManager] SET QUERY_STORE = OFF
GO
USE [BudgetManager]
GO
/****** Object:  Table [dbo].[Allowance_table]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Allowance_table](
	[Department] [nvarchar](50) NOT NULL,
	[Allowance] [decimal](15, 2) NOT NULL,
	[Date_from] [nvarchar](50) NOT NULL,
	[Date_to] [nvarchar](50) NOT NULL,
	[Money_left] [decimal](15, 2) NULL,
	[Total_spent] [decimal](15, 2) NULL,
 CONSTRAINT [PK_Allowance_table] PRIMARY KEY CLUSTERED 
(
	[Department] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense_table]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense_table](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Department] [nvarchar](50) NOT NULL,
	[Expense] [decimal](15, 2) NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[Date] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Expense_table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager_login]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager_login](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manager_login] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager_notifications]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager_notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manager_notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager_profile]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager_profile](
	[Username] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Manager_profile] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager_session]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager_session](
	[Username] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team_login]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team_login](
	[Department] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Team_login] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team_notifications]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team_notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Department] [varbinary](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Team_notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team_profile]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team_profile](
	[Department] [nvarchar](50) NOT NULL,
	[Supervisor] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Team_profile] PRIMARY KEY CLUSTERED 
(
	[Department] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team_session]    Script Date: 24/06/2023 18:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team_session](
	[Department] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [BudgetManager] SET  READ_WRITE 
GO
