USE [master]
GO
/****** Object:  Database [DatVeXemPhim]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE DATABASE [DatVeXemPhim]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DatVeXemPhim', FILENAME = N'C:\Users\TUF\DatVeXemPhim.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DatVeXemPhim_log', FILENAME = N'C:\Users\TUF\DatVeXemPhim_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DatVeXemPhim] SET COMPATIBILITY_LEVEL = 170
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DatVeXemPhim].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DatVeXemPhim] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ARITHABORT OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DatVeXemPhim] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DatVeXemPhim] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DatVeXemPhim] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DatVeXemPhim] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DatVeXemPhim] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DatVeXemPhim] SET  MULTI_USER 
GO
ALTER DATABASE [DatVeXemPhim] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DatVeXemPhim] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DatVeXemPhim] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DatVeXemPhim] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DatVeXemPhim] SET OPTIMIZED_LOCKING = OFF 
GO
ALTER DATABASE [DatVeXemPhim] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DatVeXemPhim] SET QUERY_STORE = ON
GO
ALTER DATABASE [DatVeXemPhim] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DatVeXemPhim]
GO
/****** Object:  Table [dbo].[Combos]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combos](
	[ComboId] [int] IDENTITY(1,1) NOT NULL,
	[ComboName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ComboId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Phone] [varchar](20) NULL,
	[LoyaltyPoint] [int] NOT NULL,
	[MembershipRank] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreId] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieGenres]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieGenres](
	[MovieId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_MovieGenres] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC,
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Duration] [int] NOT NULL,
	[PosterUrl] [nvarchar](500) NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[PaymentStatus] [nvarchar](50) NOT NULL,
	[TransactionCode] [varchar](100) NULL,
	[PaymentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewId] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ApprovedBy] [int] NULL,
	[ApprovedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](100) NOT NULL,
	[TotalSeats] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[RowLabel] [varchar](5) NOT NULL,
	[ColumnNumber] [int] NOT NULL,
	[SeatType] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Showtimes]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Showtimes](
	[ShowtimeId] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[TicketPrice] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShowtimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShowtimeSeats]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShowtimeSeats](
	[ShowtimeSeatId] [int] IDENTITY(1,1) NOT NULL,
	[ShowtimeId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[HoldExpiredAt] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShowtimeSeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketCombos]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketCombos](
	[TicketId] [int] NOT NULL,
	[ComboId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TicketCombos] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC,
	[ComboId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketDetails]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketDetails](
	[TicketDetailId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[ShowtimeSeatId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ShowtimeId] [int] NOT NULL,
	[VoucherId] [int] NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[ConfirmedAt] [datetime] NULL,
	[CancelledAt] [datetime] NULL,
	[RefundAmount] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Phone] [varchar](20) NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 12/07/2026 5:20:21 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[DiscountType] [nvarchar](20) NOT NULL,
	[DiscountValue] [decimal](18, 2) NOT NULL,
	[MinOrderAmount] [decimal](18, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[UsageLimit] [int] NOT NULL,
	[UsedCount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Combos] ON 

INSERT [dbo].[Combos] ([ComboId], [ComboName], [Description], [Price], [IsActive]) VALUES (1, N'Combo 1: Bắp lớn + Nước lớn', N'1 bắp rang bơ lớn + 1 nước ngọt lớn', CAST(89000.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Combos] ([ComboId], [ComboName], [Description], [Price], [IsActive]) VALUES (2, N'Combo 2: Bắp nhỏ + 2 Nước', N'1 bắp rang bơ nhỏ + 2 nước ngọt vừa', CAST(79000.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[Combos] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerId], [FullName], [Email], [PasswordHash], [Phone], [LoyaltyPoint], [MembershipRank], [IsActive], [CreatedAt]) VALUES (1, N'Lê Văn An', N'levanan@gmail.com', N'$2a$hash_cust01', N'0911111111', 150, N'Thành viên Bạc', 1, CAST(N'2026-07-09T15:25:08.577' AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [FullName], [Email], [PasswordHash], [Phone], [LoyaltyPoint], [MembershipRank], [IsActive], [CreatedAt]) VALUES (2, N'Phạm Thị Bình', N'phambinh@gmail.com', N'$2a$hash_cust02', N'0922222222', 0, N'Thành viên mới', 1, CAST(N'2026-07-09T15:25:08.577' AS DateTime))
INSERT [dbo].[Customers] ([CustomerId], [FullName], [Email], [PasswordHash], [Phone], [LoyaltyPoint], [MembershipRank], [IsActive], [CreatedAt]) VALUES (3, N'Hoàng Minh Châu', N'hoangchau@gmail.com', N'$2a$hash_cust03', N'0933333333', 500, N'Thành viên Vàng', 1, CAST(N'2026-07-09T15:25:08.577' AS DateTime))
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (5, N'Hài hước')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (1, N'Hành động')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (4, N'Hoạt hình')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (3, N'Kinh dị')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (7, N'Tài liệu')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (2, N'Tình cảm')
INSERT [dbo].[Genres] ([GenreId], [GenreName]) VALUES (6, N'Viễn tưởng')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (1, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (2, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (3, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (4, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (5, 1)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (6, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (7, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (8, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (9, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (10, 2)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (11, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (12, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (13, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (14, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (15, 3)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (16, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (17, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (18, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (19, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (20, 4)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (21, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (22, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (23, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (24, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (25, 5)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (26, 6)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (27, 6)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (28, 6)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (29, 6)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (30, 6)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (31, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (32, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (33, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (34, 7)
INSERT [dbo].[MovieGenres] ([MovieId], [GenreId]) VALUES (35, 7)
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (1, N'Mission: Impossible – The Final Reckoning', N'Ethan Hunt và đội IMF đối mặt nhiệm vụ nguy hiểm nhất sự nghiệp trong phần cuối của loạt phim gián điệp hành động kinh điển.', 170, N'/posters/mission-impossible-the-final-reckoning.jpg', CAST(N'2026-08-07T00:00:00.000' AS DateTime), CAST(N'2026-09-10T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (2, N'Bad Boys: Ride or Die', N'Hai cảnh sát Miami Mike Lowrey và Marcus Burnett phải chạy đua để minh oan cho người chỉ huy quá cố của mình.', 115, N'/posters/bad-boys-ride-or-die.jpg', CAST(N'2026-08-15T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (3, N'The Fall Guy', N'Một diễn viên đóng thế phải điều tra vụ mất tích của ngôi sao điện ảnh trong lúc cố gắng hàn gắn chuyện tình cũ.', 126, N'/posters/the-fall-guy.jpg', CAST(N'2026-07-04T00:00:00.000' AS DateTime), CAST(N'2026-08-13T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (4, N'Furiosa: A Mad Max Saga', N'Câu chuyện về tuổi trẻ của Furiosa trong thế giới hậu tận thế khắc nghiệt của vũ trụ Mad Max.', 148, N'/posters/furiosa-a-mad-max-saga.jpg', CAST(N'2026-08-28T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (5, N'Twisters', N'Một nhóm thợ săn bão liều lĩnh đối đầu với những cơn lốc xoáy ngày càng khốc liệt ở vùng Trung Tây nước Mỹ.', 122, N'/posters/twisters.jpg', CAST(N'2026-06-23T00:00:00.000' AS DateTime), CAST(N'2026-07-22T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (6, N'Anyone but You', N'Hai người từng có một đêm hẹn hò tuyệt vời rồi trở mặt bất ngờ, buộc phải giả vờ yêu nhau tại một đám cưới ở Úc.', 103, N'/posters/anyone-but-you.jpg', CAST(N'2026-06-29T00:00:00.000' AS DateTime), CAST(N'2026-08-15T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (7, N'It Ends with Us', N'Một người phụ nữ trẻ phải đối mặt với những lựa chọn khó khăn khi tình yêu và quá khứ đau buồn đan xen.', 130, N'/posters/it-ends-with-us.jpg', CAST(N'2026-09-22T00:00:00.000' AS DateTime), CAST(N'2026-11-10T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (8, N'We Live in Time', N'Một cặp đôi cùng nhau trải qua những cột mốc vui buồn của cuộc sống, tình yêu và bệnh tật.', 108, N'/posters/we-live-in-time.jpg', CAST(N'2026-07-31T00:00:00.000' AS DateTime), CAST(N'2026-09-09T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (9, N'The Idea of You', N'Một người mẹ đơn thân bất ngờ nảy sinh tình cảm với chàng ca sĩ trẻ của một ban nhạc nổi tiếng.', 115, N'/posters/the-idea-of-you.jpg', CAST(N'2026-09-13T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (10, N'Past Lives', N'Hai người bạn thời thơ ấu tái ngộ sau nhiều năm xa cách, đối diện với những gì có thể đã xảy ra.', 106, N'/posters/past-lives.jpg', CAST(N'2026-08-29T00:00:00.000' AS DateTime), CAST(N'2026-10-24T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (11, N'The Substance', N'Một ngôi sao đang lụi tàn sử dụng loại thuốc bí ẩn để tạo ra phiên bản trẻ trung hơn của chính mình, với cái giá khủng khiếp.', 141, N'/posters/the-substance.jpg', CAST(N'2026-08-17T00:00:00.000' AS DateTime), CAST(N'2026-09-17T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (12, N'Smile 2', N'Một ngôi sao nhạc pop phải đối mặt với những sự kiện ngày càng đáng sợ khi thực tại bắt đầu sụp đổ quanh cô.', 127, N'/posters/smile-2.jpg', CAST(N'2026-06-15T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (13, N'Terrifier 3', N'Gã hề sát nhân Art the Clown trở lại gieo rắc kinh hoàng trong đêm Giáng sinh.', 125, N'/posters/terrifier-3.jpg', CAST(N'2026-07-11T00:00:00.000' AS DateTime), CAST(N'2026-09-03T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (14, N'Beetlejuice Beetlejuice', N'Ba thế hệ trong gia đình Deetz vô tình mở lại cánh cổng dẫn đến thế giới của hồn ma Beetlejuice.', 105, N'/posters/beetlejuice-beetlejuice.jpg', CAST(N'2026-08-09T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (15, N'Longlegs', N'Một đặc vụ FBI điều tra loạt án mạng liên quan đến các manh mối huyền bí đầy ám ảnh.', 101, N'/posters/longlegs.jpg', CAST(N'2026-08-06T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (16, N'Inside Out 2', N'Riley bước vào tuổi dậy thì và phải đối mặt với những cảm xúc mới phức tạp hơn trong tâm trí mình.', 96, N'/posters/inside-out-2.jpg', CAST(N'2026-08-16T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (17, N'Moana 2', N'Moana lên đường trong một chuyến hải trình mới đầy thử thách cùng những người bạn cũ và mới.', 100, N'/posters/moana-2.jpg', CAST(N'2026-08-31T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (18, N'Despicable Me 4', N'Gru phải bảo vệ gia đình mới của mình trước một kẻ thù cũ đầy nguy hiểm.', 94, N'/posters/despicable-me-4.jpg', CAST(N'2026-08-13T00:00:00.000' AS DateTime), CAST(N'2026-10-03T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (19, N'The Wild Robot', N'Một robot bị mắc kẹt trên hòn đảo hoang phải học cách sinh tồn và trở thành người mẹ nuôi của một chú ngỗng con.', 102, N'/posters/the-wild-robot.jpg', CAST(N'2026-06-21T00:00:00.000' AS DateTime), CAST(N'2026-07-31T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (20, N'Kung Fu Panda 4', N'Po phải tìm người kế nhiệm làm Rồng Chiến Binh trong khi đối mặt với một pháp sư biến hình nguy hiểm.', 94, N'/posters/kung-fu-panda-4.jpg', CAST(N'2026-07-16T00:00:00.000' AS DateTime), CAST(N'2026-08-22T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (21, N'Barbie', N'Barbie rời khỏi thế giới hoàn hảo của mình để khám phá thế giới thực đầy bất ngờ.', 114, N'/posters/barbie.jpg', CAST(N'2026-09-26T00:00:00.000' AS DateTime), CAST(N'2026-11-17T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (22, N'No Hard Feelings', N'Một phụ nữ được thuê để giúp một chàng trai nhút nhát tự tin hơn trước khi vào đại học.', 103, N'/posters/no-hard-feelings.jpg', CAST(N'2026-07-13T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (23, N'Argylle', N'Một nữ tiểu thuyết gia phát hiện cốt truyện trong sách của mình đang trở thành sự thật ngoài đời.', 139, N'/posters/argylle.jpg', CAST(N'2026-09-16T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (24, N'Y2K', N'Một nhóm bạn trẻ phải sống sót qua đêm giao thừa thiên niên kỷ khi máy móc nổi loạn.', 93, N'/posters/y2k.jpg', CAST(N'2026-08-15T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (25, N'Am I OK?', N'Một phụ nữ ở độ tuổi 30 bắt đầu hành trình khám phá lại chính bản thân mình.', 96, N'/posters/am-i-ok.jpg', CAST(N'2026-06-14T00:00:00.000' AS DateTime), CAST(N'2026-08-01T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (26, N'Dune: Part Two', N'Paul Atreides hợp lực cùng người Fremen trên hành trình trả thù và định đoạt số phận cả vũ trụ.', 166, N'/posters/dune-part-two.jpg', CAST(N'2026-09-17T00:00:00.000' AS DateTime), CAST(N'2026-10-29T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (27, N'Godzilla x Kong: The New Empire', N'Hai quái vật huyền thoại Godzilla và Kong buộc phải bắt tay chống lại một mối đe dọa ẩn giấu.', 115, N'/posters/godzilla-x-kong-the-new-empire.jpg', CAST(N'2026-06-24T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (28, N'Alien: Romulus', N'Một nhóm người trẻ khai thác trạm vũ trụ bỏ hoang chạm trán sinh vật ngoài hành tinh nguy hiểm bậc nhất vũ trụ.', 119, N'/posters/alien-romulus.jpg', CAST(N'2026-07-30T00:00:00.000' AS DateTime), CAST(N'2026-09-28T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (29, N'The Creator', N'Trong cuộc chiến giữa loài người và trí tuệ nhân tạo, một cựu binh phát hiện vũ khí bí mật mang hình hài đứa trẻ.', 133, N'/posters/the-creator.jpg', CAST(N'2026-06-13T00:00:00.000' AS DateTime), CAST(N'2026-07-19T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (30, N'Poor Things', N'Một phụ nữ trẻ được hồi sinh bởi khoa học kỳ lạ và bắt đầu hành trình khám phá thế giới theo cách riêng của mình.', 141, N'/posters/poor-things.jpg', CAST(N'2026-07-07T00:00:00.000' AS DateTime), CAST(N'2026-08-21T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (31, N'Free Solo', N'Ghi lại hành trình leo núi El Capitan không dây bảo hộ đầy mạo hiểm của vận động viên Alex Honnold.', 100, N'/posters/free-solo.jpg', CAST(N'2026-07-29T00:00:00.000' AS DateTime), CAST(N'2026-08-31T00:00:00.000' AS DateTime), N'Ngừng chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (32, N'My Octopus Teacher', N'Một nhà làm phim xây dựng mối quan hệ đặc biệt với một con bạch tuộc hoang dã ngoài khơi Nam Phi.', 85, N'/posters/my-octopus-teacher.jpg', CAST(N'2026-06-20T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (33, N'13th', N'Bộ phim tài liệu phân tích mối liên hệ giữa chế độ nô lệ và hệ thống nhà tù ở nước Mỹ hiện đại.', 100, N'/posters/13th.jpg', CAST(N'2026-09-18T00:00:00.000' AS DateTime), NULL, N'Sắp chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (34, N'Won''t You Be My Neighbor?', N'Chân dung về cuộc đời và di sản của Fred Rogers, người dẫn chương trình truyền hình thiếu nhi huyền thoại.', 94, N'/posters/won-t-you-be-my-neighbor.jpg', CAST(N'2026-07-24T00:00:00.000' AS DateTime), CAST(N'2026-09-01T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [Duration], [PosterUrl], [ReleaseDate], [EndDate], [Status], [CreatedAt]) VALUES (35, N'Fyre: The Greatest Party That Never Happened', N'Câu chuyện có thật đằng sau lễ hội âm nhạc xa hoa sụp đổ thảm hại trên mạng xã hội.', 97, N'/posters/fyre-the-greatest-party-that-never-happened.jpg', CAST(N'2026-07-23T00:00:00.000' AS DateTime), CAST(N'2026-09-21T00:00:00.000' AS DateTime), N'Đang chiếu', CAST(N'2026-07-12T17:13:55.597' AS DateTime))
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Staff')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([RoomId], [RoomName], [TotalSeats], [IsActive]) VALUES (1, N'Phòng chiếu 1', 15, 1)
INSERT [dbo].[Rooms] ([RoomId], [RoomName], [TotalSeats], [IsActive]) VALUES (2, N'Phòng chiếu 2', 8, 1)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Seats] ON 

INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (1, 1, N'A', 1, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (2, 1, N'A', 2, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (3, 1, N'A', 3, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (4, 1, N'A', 4, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (5, 1, N'A', 5, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (6, 1, N'B', 1, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (7, 1, N'B', 2, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (8, 1, N'B', 3, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (9, 1, N'B', 4, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (10, 1, N'B', 5, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (11, 1, N'C', 1, N'VIP')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (12, 1, N'C', 2, N'VIP')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (13, 1, N'C', 3, N'VIP')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (14, 1, N'C', 4, N'VIP')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (15, 1, N'C', 5, N'VIP')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (16, 2, N'A', 1, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (17, 2, N'A', 2, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (18, 2, N'A', 3, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (19, 2, N'A', 4, N'Thường')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (20, 2, N'B', 1, N'Đôi')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (21, 2, N'B', 2, N'Đôi')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (22, 2, N'B', 3, N'Đôi')
INSERT [dbo].[Seats] ([SeatId], [RoomId], [RowLabel], [ColumnNumber], [SeatType]) VALUES (23, 2, N'B', 4, N'Đôi')
SET IDENTITY_INSERT [dbo].[Seats] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [PasswordHash], [FullName], [Email], [Phone], [RoleId], [IsActive], [CreatedAt]) VALUES (1, N'admin01', N'$2a$hash_admin01', N'Nguyễn Văn Quản', N'admin01@rapphim.vn', N'0900000001', 2, 1, CAST(N'2026-07-09T15:25:08.577' AS DateTime))
INSERT [dbo].[Users] ([UserId], [Username], [PasswordHash], [FullName], [Email], [Phone], [RoleId], [IsActive], [CreatedAt]) VALUES (2, N'staff01', N'$2a$hash_staff01', N'Trần Thị Nhân Viên', N'staff01@rapphim.vn', N'0900000002', 1, 1, CAST(N'2026-07-09T15:25:08.577' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vouchers] ON 

INSERT [dbo].[Vouchers] ([VoucherId], [Code], [DiscountType], [DiscountValue], [MinOrderAmount], [StartDate], [EndDate], [UsageLimit], [UsedCount], [IsActive]) VALUES (1, N'SUMMER10', N'Phần trăm', CAST(10.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), CAST(N'2026-06-01T00:00:00.000' AS DateTime), CAST(N'2026-08-31T00:00:00.000' AS DateTime), 100, 5, 1)
INSERT [dbo].[Vouchers] ([VoucherId], [Code], [DiscountType], [DiscountValue], [MinOrderAmount], [StartDate], [EndDate], [UsageLimit], [UsedCount], [IsActive]) VALUES (2, N'GIAM20K', N'Số tiền cố định', CAST(20000.00 AS Decimal(18, 2)), CAST(150000.00 AS Decimal(18, 2)), CAST(N'2026-07-01T00:00:00.000' AS DateTime), CAST(N'2026-07-31T00:00:00.000' AS DateTime), 50, 0, 1)
SET IDENTITY_INSERT [dbo].[Vouchers] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A9D10534137D7CCE]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Customers] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Genres__BBE1C3394B604BC5]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Genres] ADD UNIQUE NONCLUSTERED 
(
	[GenreName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Movies_Status]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Movies_Status] ON [dbo].[Movies]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Reviews_Movie_Customer]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Reviews] ADD  CONSTRAINT [UQ_Reviews_Movie_Customer] UNIQUE NONCLUSTERED 
(
	[MovieId] ASC,
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Reviews_MovieId_Status]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_MovieId_Status] ON [dbo].[Reviews]
(
	[MovieId] ASC,
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Roles__8A2B61604159D905]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Roles] ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Rooms__6B500B5505914500]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Rooms] ADD UNIQUE NONCLUSTERED 
(
	[RoomName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Seats_Room_Position]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Seats] ADD  CONSTRAINT [UQ_Seats_Room_Position] UNIQUE NONCLUSTERED 
(
	[RoomId] ASC,
	[RowLabel] ASC,
	[ColumnNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Showtimes_MovieId]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Showtimes_MovieId] ON [dbo].[Showtimes]
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Showtimes_StartTime]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Showtimes_StartTime] ON [dbo].[Showtimes]
(
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Showtimes_Room_StartTime]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Showtimes_Room_StartTime] ON [dbo].[Showtimes]
(
	[RoomId] ASC,
	[StartTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_ShowtimeSeats_Showtime_Seat]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[ShowtimeSeats] ADD  CONSTRAINT [UQ_ShowtimeSeats_Showtime_Seat] UNIQUE NONCLUSTERED 
(
	[ShowtimeId] ASC,
	[SeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ShowtimeSeats_Status]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_ShowtimeSeats_Status] ON [dbo].[ShowtimeSeats]
(
	[Status] ASC,
	[HoldExpiredAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_TicketDetails_ShowtimeSeat]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[TicketDetails] ADD  CONSTRAINT [UQ_TicketDetails_ShowtimeSeat] UNIQUE NONCLUSTERED 
(
	[ShowtimeSeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tickets_CustomerId]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Tickets_CustomerId] ON [dbo].[Tickets]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Tickets_Status]    Script Date: 12/07/2026 5:20:21 CH ******/
CREATE NONCLUSTERED INDEX [IX_Tickets_Status] ON [dbo].[Tickets]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__536C85E49A8FBED1]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534C9C85093]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vouchers__A25C5AA7EBC0A972]    Script Date: 12/07/2026 5:20:21 CH ******/
ALTER TABLE [dbo].[Vouchers] ADD UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Combos] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT ((0)) FOR [LoyaltyPoint]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT (N'Thành viên mới') FOR [MembershipRank]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customers] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Movies] ADD  DEFAULT (N'Sắp chiếu') FOR [Status]
GO
ALTER TABLE [dbo].[Movies] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (N'Chờ xử lý') FOR [PaymentStatus]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (N'Chờ duyệt') FOR [Status]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT ((0)) FOR [TotalSeats]
GO
ALTER TABLE [dbo].[Rooms] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Seats] ADD  DEFAULT (N'Thường') FOR [SeatType]
GO
ALTER TABLE [dbo].[Showtimes] ADD  DEFAULT (N'Sắp chiếu') FOR [Status]
GO
ALTER TABLE [dbo].[ShowtimeSeats] ADD  DEFAULT (N'Trống') FOR [Status]
GO
ALTER TABLE [dbo].[TicketCombos] ADD  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT (N'Chờ thanh toán') FOR [Status]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT (getdate()) FOR [BookingDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT ((0)) FOR [MinOrderAmount]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT ((0)) FOR [UsageLimit]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT ((0)) FOR [UsedCount]
GO
ALTER TABLE [dbo].[Vouchers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MovieGenres]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenres_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([GenreId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieGenres] CHECK CONSTRAINT [FK_MovieGenres_Genre]
GO
ALTER TABLE [dbo].[MovieGenres]  WITH CHECK ADD  CONSTRAINT [FK_MovieGenres_Movie] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MovieGenres] CHECK CONSTRAINT [FK_MovieGenres_Movie]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Tickets] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([TicketId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Tickets]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Customers]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Movies]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Users] FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Users]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD  CONSTRAINT [FK_Seats_Rooms] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seats] CHECK CONSTRAINT [FK_Seats_Rooms]
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD  CONSTRAINT [FK_Showtimes_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
GO
ALTER TABLE [dbo].[Showtimes] CHECK CONSTRAINT [FK_Showtimes_Movies]
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD  CONSTRAINT [FK_Showtimes_Rooms] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
GO
ALTER TABLE [dbo].[Showtimes] CHECK CONSTRAINT [FK_Showtimes_Rooms]
GO
ALTER TABLE [dbo].[ShowtimeSeats]  WITH CHECK ADD  CONSTRAINT [FK_ShowtimeSeats_Seat] FOREIGN KEY([SeatId])
REFERENCES [dbo].[Seats] ([SeatId])
GO
ALTER TABLE [dbo].[ShowtimeSeats] CHECK CONSTRAINT [FK_ShowtimeSeats_Seat]
GO
ALTER TABLE [dbo].[ShowtimeSeats]  WITH CHECK ADD  CONSTRAINT [FK_ShowtimeSeats_Showtime] FOREIGN KEY([ShowtimeId])
REFERENCES [dbo].[Showtimes] ([ShowtimeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShowtimeSeats] CHECK CONSTRAINT [FK_ShowtimeSeats_Showtime]
GO
ALTER TABLE [dbo].[TicketCombos]  WITH CHECK ADD  CONSTRAINT [FK_TicketCombos_Combo] FOREIGN KEY([ComboId])
REFERENCES [dbo].[Combos] ([ComboId])
GO
ALTER TABLE [dbo].[TicketCombos] CHECK CONSTRAINT [FK_TicketCombos_Combo]
GO
ALTER TABLE [dbo].[TicketCombos]  WITH CHECK ADD  CONSTRAINT [FK_TicketCombos_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([TicketId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TicketCombos] CHECK CONSTRAINT [FK_TicketCombos_Ticket]
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketDetails_ShowtimeSeat] FOREIGN KEY([ShowtimeSeatId])
REFERENCES [dbo].[ShowtimeSeats] ([ShowtimeSeatId])
GO
ALTER TABLE [dbo].[TicketDetails] CHECK CONSTRAINT [FK_TicketDetails_ShowtimeSeat]
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_TicketDetails_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([TicketId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TicketDetails] CHECK CONSTRAINT [FK_TicketDetails_Ticket]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Customers]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Showtimes] FOREIGN KEY([ShowtimeId])
REFERENCES [dbo].[Showtimes] ([ShowtimeId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Showtimes]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Vouchers] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Vouchers] ([VoucherId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Vouchers]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Combos]  WITH CHECK ADD CHECK  (([Price]>=(0)))
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD CHECK  (([Duration]>(0)))
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD CHECK  (([Status]=N'Sắp chiếu' OR [Status]=N'Ngừng chiếu' OR [Status]=N'Đang chiếu'))
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD  CONSTRAINT [CK_Movies_DateRange] CHECK  (([EndDate] IS NULL OR [EndDate]>=[ReleaseDate]))
GO
ALTER TABLE [dbo].[Movies] CHECK CONSTRAINT [CK_Movies_DateRange]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([Amount]>=(0)))
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([PaymentMethod]=N'Thanh toán online' OR [PaymentMethod]=N'Tiền mặt tại quầy'))
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD CHECK  (([PaymentStatus]=N'Đã hoàn tiền' OR [PaymentStatus]=N'Thất bại' OR [PaymentStatus]=N'Thành công' OR [PaymentStatus]=N'Chờ xử lý'))
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD CHECK  (([Status]=N'Đã ẩn' OR [Status]=N'Đã duyệt' OR [Status]=N'Chờ duyệt'))
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD CHECK  (([SeatType]=N'Đôi' OR [SeatType]=N'VIP' OR [SeatType]=N'Thường'))
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD CHECK  (([Status]=N'Đã hủy' OR [Status]=N'Đã chiếu' OR [Status]=N'Đang chiếu' OR [Status]=N'Sắp chiếu'))
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD CHECK  (([TicketPrice]>=(0)))
GO
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD  CONSTRAINT [CK_Showtimes_Time] CHECK  (([EndTime]>[StartTime]))
GO
ALTER TABLE [dbo].[Showtimes] CHECK CONSTRAINT [CK_Showtimes_Time]
GO
ALTER TABLE [dbo].[ShowtimeSeats]  WITH CHECK ADD CHECK  (([Status]=N'Đã đặt' OR [Status]=N'Đang giữ chỗ' OR [Status]=N'Trống'))
GO
ALTER TABLE [dbo].[TicketCombos]  WITH CHECK ADD CHECK  (([Quantity]>(0)))
GO
ALTER TABLE [dbo].[TicketDetails]  WITH CHECK ADD CHECK  (([Price]>=(0)))
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD CHECK  (([Status]=N'Đã hủy' OR [Status]=N'Đã thanh toán' OR [Status]=N'Chờ thanh toán'))
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD CHECK  (([TotalAmount]>=(0)))
GO
ALTER TABLE [dbo].[Vouchers]  WITH CHECK ADD CHECK  (([DiscountType]=N'Số tiền cố định' OR [DiscountType]=N'Phần trăm'))
GO
ALTER TABLE [dbo].[Vouchers]  WITH CHECK ADD CHECK  (([DiscountValue]>=(0)))
GO
ALTER TABLE [dbo].[Vouchers]  WITH CHECK ADD  CONSTRAINT [CK_Vouchers_DateRange] CHECK  (([EndDate]>=[StartDate]))
GO
ALTER TABLE [dbo].[Vouchers] CHECK CONSTRAINT [CK_Vouchers_DateRange]
GO
ALTER TABLE [dbo].[Vouchers]  WITH CHECK ADD  CONSTRAINT [CK_Vouchers_Usage] CHECK  (([UsedCount]<=[UsageLimit]))
GO
ALTER TABLE [dbo].[Vouchers] CHECK CONSTRAINT [CK_Vouchers_Usage]
GO
USE [master]
GO
ALTER DATABASE [DatVeXemPhim] SET  READ_WRITE 
GO
