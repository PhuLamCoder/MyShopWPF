USE [master]
GO
/****** Object:  Database [MyShopDB]    Script Date: 12/18/2023 10:52:17 AM ******/
CREATE DATABASE [MyShopDB]

GO
ALTER DATABASE [MyShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MyShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyShopDB', N'ON'
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShopDB]
GO
/****** Object:  Table [dbo].[category]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](50) NULL,
	[CatDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[CusID] [int] IDENTITY(1,1) NOT NULL,
	[CusName] [nvarchar](50) NULL,
	[Gender] [nvarchar](5) NULL,
	[DOB] [date] NULL,
	[Address] [ntext] NULL,
	[Tel] [nvarchar](15) NULL,
	[Block] [int] NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[CusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[ProID] [int] IDENTITY(1,1) NOT NULL,
	[ProName] [nvarchar](150) NULL,
	[Ram] [float] NULL,
	[Rom] [int] NULL,
	[ScreenSize] [float] NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [money] NULL,
	[ImagePath] [text] NULL,
	[Trademark] [text] NULL,
	[BatteryCapacity] [int] NULL,
	[CatID] [int] NULL,
	[Quantity] [int] NULL,
	[Block] [int] NULL,
	[PromoID] [int] NULL,
	[PromotionPrice] [money] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[promotion]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotion](
	[IdPromo] [int] IDENTITY(1,1) NOT NULL,
	[PromoCode] [nvarchar](50) NULL,
	[DiscountPercent] [int] NULL,
 CONSTRAINT [pk_promo] PRIMARY KEY CLUSTERED 
(
	[IdPromo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase](
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProID] [int] NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [money] NULL,
 CONSTRAINT [PK_purchase] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shop_order]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shop_order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CusID] [int] NULL,
	[CreateAt] [date] NULL,
	[FinalTotal] [money] NULL,
	[ProfitTotal] [money] NULL,
 CONSTRAINT [PK_shop_order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 12/18/2023 10:52:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [ntext] NULL,
	[Gender] [nvarchar](5) NULL,
	[Address] [ntext] NULL,
	[Tel] [nvarchar](15) NULL,
	[IsHide] [tinyint] NULL,
 CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([CatID], [CatName], [CatDescription]) VALUES (1, N'Android', N'Các thiết bị điện thoại chạy hệ điều hành Android')
INSERT [dbo].[category] ([CatID], [CatName], [CatDescription]) VALUES (2, N'Iphone', N'Các thiết bị điện thoại chạy hệ điều hành IOS của hãng Apple')
INSERT [dbo].[category] ([CatID], [CatName], [CatDescription]) VALUES (3, N'Đời tống', N'Ngoài nghe gọi còn có thể chọi')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (1, N'Trần Mỹ Phú Lâm', N'Nam', CAST(N'2003-03-20' AS Date), N'95 Trần Não, Phường Bình An, Quận 2, TP.HCM', N'0749008546', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (2, N'Tạ Ngọc Duy Khiêm', N'Nam', CAST(N'1999-01-10' AS Date), N'48Bis Xuân Thủy, Phường Thảo Điền, Quận 2, TP.HCM', N'0888353622', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (3, N'Đàm Hồng Hưng', N'Nam', CAST(N'2000-02-24' AS Date), N'554 Nguyễn Thị Định, Phường Cát Lái, Quận 2, TP.HCM', N'0938997864', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (4, N'Ngô Quốc Huy', N'Nam', CAST(N'2006-04-05' AS Date), N'367 Đường An Dương Vương, Phường 3, quận 5, TP.HCM', N'0349009587', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (5, N'Vương Huỳnh Khải', N'Nam', CAST(N'2001-09-09' AS Date), N'162 Nguyễn Văn Lượng, Phường 17, Quận Gò Vấp, TP.HCM', N'0453627483', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (6, N'Nguyễn Võ Nhật Huy', N'Nam', CAST(N'1999-06-17' AS Date), N'47 Song Hành, Phường An Phú, Quận 2, TP. HCM', N'0193836233', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (7, N'Trần Nguyễn Gia Huy', N'Nam', CAST(N'2003-12-10' AS Date), N'159 Xa lộ Hà Nội, Phường Thảo Điền, Quận 2, TP. HCM', N'080808080', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (8, N'Đoàn Đức Hữu', N'Nam', CAST(N'2004-02-10' AS Date), N'122/80 Khu Phố Tân Lập, Bình Dương', N'04758383632', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (9, N'Quách Đức Huy', N'Nam', CAST(N'2005-08-13' AS Date), N'501 Kha Vạn Cân, TP Thủ Đức', N'0143526374', 0)
INSERT [dbo].[customer] ([CusID], [CusName], [Gender], [DOB], [Address], [Tel], [Block]) VALUES (10, N'Nguyễn Quốc Hưng', N'Nam', CAST(N'2002-12-11' AS Date), N'Tòa nhà Sun Avenue Mai Chí Thọ, Quận 10', N'0909867453', 0)
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (1, N'Samsung Galaxy S23', 6, 256, 6.1, N'Samsung Galaxy S23 5G 128GB chắc hẳn không còn là cái tên quá xa lạ đối với các tín độ công nghệ hiện nay, được xem là một trong những gương mặt chủ chốt đến từ nhà Samsung với cấu hình mạnh mẽ bậc nhất, camera trứ danh hàng đầu cùng lối hoàn thiện vô cùng sang trọng và hiện đại.', 20990000.0000, N'Assets/Images/sp/7d9c9403-abba-41e0-88c2-f55785a41f0e.png', N'Samsung', 3900, 1, 3, 0, NULL, 20990000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (2, N'iPhone 14 Pro Max', 6, 256, 6.7, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.', 27290000.0000, N'Assets/Images/sp/3014.png', N'Apple', 4323, 2, 24, 0, 3, 16374000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (3, N'iPhone 14 Pro', 6, 256, 6.7, N'iPhone 14 Pro 128GB - Mẫu smartphone đến từ nhà Apple được mong đợi nhất năm 2022, lần này nhà Táo mang đến cho chúng ta một phiên bản với kiểu thiết kế hình notch mới, cấu hình mạnh mẽ nhờ con chip Apple A16 Bionic và cụm camera có độ phân giải được nâng cấp lên đến 48 MP.', 25290000.0000, N'Assets/Images/sp/3015.png', N'Apple', 3200, 2, 7, 0, NULL, 25290000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (4, N'Samsung Galaxy S21 FE', 6, 128, 6.4, N'Samsung Galaxy S21 FE 5G (6GB/128GB) được Samsung ra mắt với dáng dấp thời thượng, cấu hình khỏe, chụp ảnh đẹp với bộ 3 camera chất lượng, thời lượng pin đủ dùng hằng ngày và còn gì nữa? Mời bạn khám phá qua nội dung sau ngay.', 10990000.0000, N'Assets/Images/sp/3016.png', N'Samsung', 4500, 1, 59, 0, 1, 9891000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (5, N'Xiaomi Redmi 12C', 4, 64, 6.71, N'Xiaomi Redmi 12C 64GB là một thiết bị di động tầm trung được giới thiệu bởi Xiaomi, với cấu hình vượt trội so với các đối thủ trong cùng phân khúc', 2890000.0000, N'Assets/Images/sp/fbc8c87c-4474-4c37-b2ba-706037ca87a1.png', N'Xiaomi', 5000, 1, 49, 0, 1, 2601000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (6, N'Samsung Galaxy S23 Ultra', 8, 128, 6.8, N'Cuối cùng thì chiếc điện thoại Samsung Galaxy S23 cũng đã chính thức ra mắt tại sự kiện Galaxy Unpacked vào đầu tháng 2 năm 2023, máy nổi bật với camera 200 MP chất lượng, hiệu năng mạnh mẽ.', 26990000.0000, N'Assets/Images/sp/3018.png', N'Samsung', 5000, 1, 1, 0, 3, 16194000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (7, N'iPhone 11 64GB', 4, 6, 6.1, N'Apple đã chính thức trình làng bộ 3 siêu phẩm iPhone 11, trong đó phiên bản iPhone 11 64GB có mức giá rẻ nhất nhưng vẫn được nâng cấp mạnh mẽ như iPhone Xr ra mắt trước đó.', 10590000.0000, N'Assets/Images/sp/3019.png', N'Apple', 3110, 2, 4, 0, 3, 6354000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (8, N'Điện thoại Vivo Y35', 8, 128, 6.58, N'Tiếp nối sự thành công của các mẫu smartphone tầm trung tại thị trường Việt Nam thì mới đây Vivo đã chính thức cho ra mắt điện thoại Vivo Y35. Máy sở hữu cho mình khả năng sạc siêu nhanh 44 W', 6290000.0000, N'Assets/Images/sp/3020.png', N'Vivo', 5000, 1, 2, 0, NULL, 6290000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (9, N'OPPO Reno8 T 5G', 8, 64, 6.7, N'Tiếp nối sự thành công rực rỡ đến từ các thế hệ trước đó thì chiếc OPPO Reno8 T 5G 256GB cuối cùng đã được giới thiệu chính thức tại Việt Nam', 10990000.0000, N'Assets/Images/sp/3021.png', N'OPPO', 4800, 1, 1, 0, 4, 9891000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (10, N'Samsung Galaxy A23', 4, 128, 6.6, N'Samsung Galaxy A23 4GB sở hữu bộ thông số kỹ thuật khá ấn tượng trong phân khúc, máy có một hiệu năng ổn định, cụm 4 camera thông minh cùng một diện mạo trẻ trung phù hợp cho mọi đối tượng người dùng.', 4790000.0000, N'Assets/Images/sp/3022.png', N'Samsung', 5000, 1, 19, 0, NULL, 4790000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (11, N'Samsung Galaxy S20 FE', 8, 256, 6.5, N'Samsung đã giới thiệu đến người dùng thành viên mới của dòng S20 Series đó chính là điện thoại Samsung Galaxy S20 FE. Đây là mẫu flagship cao cấp quy tụ nhiều tính năng mà Samfan yêu thích, hứa hẹn sẽ mang lại trải nghiệm cao cấp của dòng Galaxy S với mức giá dễ tiếp cận hơn.', 8650000.0000, N'Assets/Images/sp/3025.png', N'Samsung', 4500, 1, 15, 0, NULL, 8650000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (12, N'iPhone 14 128GB', 6, 128, 6.1, N'iPhone 14 128GB được xem là mẫu smartphone bùng nổ của nhà táo trong năm 2022, ấn tượng với ngoại hình trẻ trung, màn hình chất lượng đi kèm với những cải tiến về hệ điều hành và thuật toán xử lý hình ảnh, giúp máy trở thành cái tên thu hút được đông đảo người dùng quan tâm tại thời điểm ra mắt.', 19590000.0000, N'Assets/Images/sp/3026.png', N'Apple', 3279, 2, 23, 0, 4, 17631000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (13, N'iPhone 14 Pro Max', 6, 128, 6.7, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022', 27090000.0000, N'Assets/Images/sp/2d6cb8fe-710f-40f6-b749-fb8ed4d2b3ae.png', N'Apple', 4323, 2, 26, 0, 3, 16254000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (14, N'Samsung Galaxy A34', 8, 256, 6.6, N'Samsung Galaxy A34 5G là mẫu điện thoại thông minh tầm trung mới của Samsung được ra mắt vào tháng 03/2023', 8630000.0000, N'Assets/Images/sp/8a8cbf04-4d71-4582-a2b2-b3477fe1f0e5.png', N'Samsung', 5000, 1, 22, 0, NULL, 8630000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (15, N'iPhone 14 Pro Max', 6, 128, 6.7, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022', 27090000.0000, NULL, N'Apple', 4323, 2, 12, 1, NULL, 27090000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (16, N'Samsung Galaxy A34', 8, 256, 6.6, N'Samsung Galaxy A34 5G là mẫu điện thoại thông minh tầm trung mới của Samsung được ra mắt vào tháng 03/2023', 8630000.0000, N'Assets/Images/sp/eebe1110-5b9f-4df1-8af8-0aef0f06c2c9.png', N'Samsung', 5000, 1, 22, 1, NULL, 8630000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (17, N'iPhone 12 Pro Max', 6, 128, 6.6, N'iPhone 12 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2020', 17090000.0000, N'Assets/Images/sp/22dde313-d0fe-4b39-86c4-7a3bb7ef9d51.png', N'Apple', 3323, 2, 25, 0, 4, 15381000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (18, N'Vivo Y16 64GB', 4, 64, 6.5, N'Vivo Y16 64GB tiếp tục sẽ là cái tên được hãng bổ sung cho bộ sưu tập điện thoại Vivo dòng Y trong thời điểm cuối năm 2022', 3290000.0000, N'Assets/Images/sp/444a25f8-7a1b-4a81-8227-3233bfd06d9e.png', N'Vivo ', 5000, 1, 38, 0, NULL, 3290000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (19, N'OPPO Reno8 Pro 5G', 12, 256, 6.7, N'Lần này nhà OPPO mang đến cho chúng ta một chiếc điện thoại có thiết kế đặc biệt, máy sở hữu một diện mạo khác hẳn với những chiếc điện thoại OPPO Reno trước đây', 17990000.0000, N'Assets/Images/sp/b5ec601d-473a-4fdf-b2c1-5415b38f2a43.png', N'OPPO', 4500, 1, 27, 0, NULL, 17990000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (20, N'Điện thoại OPPO A55', 4, 64, 6.5, N'OPPO A55 4G có 3 phiên bản màu độc đáo là xanh lá, xanh dương và màu đen. Thiết kế cong 3D cùng kích thước mỏng nhẹ, OPPO A55 4G vừa vặn trong tay người cầm, cho từng thao tác trải nghiệm thoải mái và chắc chắn.', 3690000.0000, N'Assets/Images/sp/f8237f5c-9e82-4f32-9106-15f93fb34f02.png', N'OPPO', 5000, 1, 20, 0, NULL, 3690000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (21, N'iPhone 13 mini 64GB', 4, 64, 5.4, N'iPhone 13 mini được trang bị bộ vi xử lý A15 Bionic sản xuất trên tiến trình 5 nm giúp cải thiện hiệu suất và giảm điện năng tiêu thụ đến 15% so với phiên bản A14 Bionic trước đó, đáp ứng hoàn hảo trong công việc cũng như trong giải trí của người dùng tốt hơn.', 13999000.0000, N'Assets/Images/sp/382734b4-d941-49a2-9407-6e4726b99bb9.png', N'Apple', 2438, 2, 46, 0, 4, 12599100.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (22, N'iPhone 14 Pro Max', 8, 256, 6.67, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2020', 37090000.0000, N'Assets/Images/sp/fbedb6f0-f591-4142-94ab-463d55eaa582.png', N'Apple', 4323, 2, 27, 0, 4, 33381000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (23, N'Vivo Y02s 32GB', 3, 32, 6.51, N'Vivo Y02s được hoàn thiện với hai mặt và các cạnh vát phẳng giúp cho thân hình của chiếc máy trở nên vuông vắn và cực kỳ hợp xu hướng hiện nay. Nổi bật hơn hết là cụm camera được Vivo thiết kế với hai cụm tròn to nổi khá ấn tượng.', 2790000.0000, N'Assets/Images/sp/3c8ca825-3eea-4f94-b5d9-ee4576f7790a.png', N'Vivo', 5000, 1, 46, 0, 4, 2790000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (24, N'iPhone 13 mini 128GB', 4, 128, 5.4, N'iPhone 13 mini được trang bị bộ vi xử lý A15 Bionic sản xuất trên tiến trình 5 nm giúp cải thiện hiệu suất và giảm điện năng tiêu thụ đến 15% so với phiên bản A14 Bionic trước đó, đáp ứng hoàn hảo trong công việc cũng như trong giải trí của người dùng tốt hơn.', 16990000.0000, N'Assets/Images/sp/ae4fe396-0a7e-4d78-8df7-773035b137e5.png', N'Apple', 2438, 2, 46, 0, NULL, 16990000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (25, N'Điện thoại Nokia 8210', 1, 12, 2.8, N'Nokia 8210 4G có lẽ là một lựa chọn phù hợp với những ai cần cho mình một chiếc điện thoại phổ thông phục vụ cho nhu cầu nghe gọi. Máy có giá thành rẻ và vừa có độ bền cao', 1590000.0000, N'Assets/Images/sp/46277ded-2dba-45e2-af05-d23585cf0312.png', N'Nokia', 1450, 3, 27, 0, 4, 1431000.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Description], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (26, N'Điện thoại Mobell Rock 4', 1, 5, 2.4, N'Mobell Rock 4 được xem là một trong những chiếc điện thoại bền bỉ nhất mà nhà Mobell chính thức cho ra mắt trên thị trường, sở hữu diện mạo cứng cáp cùng viên pin cực trâu giúp máy có thể đồng hành cùng bạn trong suốt một thời gian dài.', 810000.0000, N'Assets/Images/sp/8c1be437-a63b-44bc-8bec-f74a3edd8cbb.png', N'Mobell', 3250, 3, 56, 0, 3, 486000.0000)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[promotion] ON 

INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (1, N'TMPL', 40)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (2, N'SieuCap', 70)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (3, N'TNDK', 25)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (4, N'DHH', 10)
SET IDENTITY_INSERT [dbo].[promotion] OFF
GO
SET IDENTITY_INSERT [dbo].[purchase] ON 

INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (1, 1, 26, 3, 1458000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (2, 2, 25, 1, 1431000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (3, 3, 20, 2, 7380000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (4, 4, 3, 1, 25290000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (5, 5, 12, 1, 17631000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (6, 6, 2, 1, 16374000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (7, 7, 5, 1, 2601000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (8, 8, 12, 1, 17631000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (9, 9, 22, 1, 33381000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (10, 10, 2, 1, 16374000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (11, 11, 13, 1, 16254000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (12, 12, 21, 1, 12599100.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (13, 13, 25, 2, 2862000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (14, 14, 17, 1, 15381000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (15, 15, 5, 2, 5202000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (16, 16, 23, 2, 5580000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (17, 17, 20, 1, 3690000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (18, 18, 26, 2, 972000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (19, 19, 11, 1, 8650000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (20, 20, 18, 1, 3290000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (21, 21, 20, 1, 3690000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (22, 22, 4, 1, 9891000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (23, 23, 5, 1, 2601000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (24, 24, 19, 1, 17990000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (25, 25, 21, 1, 12599100.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (26, 26, 24, 2, 33980000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (27, 27, 10, 1, 4790000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (28, 28, 22, 1, 33381000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (29, 29, 3, 1, 25290000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (30, 30, 5, 1, 2601000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (31, 31, 23, 1, 2790000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (32, 32, 25, 1, 1431000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (33, 33, 21, 2, 25198200.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (34, 34, 2, 1, 16374000.0000)
SET IDENTITY_INSERT [dbo].[purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[shop_order] ON 

INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (1, 3, CAST(N'2021-1-1' AS Date), 1458000.0000, 102060.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (2, 8, CAST(N'2021-3-12' AS Date), 1431000.0000, 100170.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (3, 4, CAST(N'2021-5-13' AS Date), 7380000.0000, 516600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (4, 10, CAST(N'2021-7-14' AS Date), 25290000.0000, 1770300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (5, 6, CAST(N'2021-9-15' AS Date), 17631000.0000, 1234170.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (6, 3, CAST(N'2021-11-21' AS Date), 16374000.0000, 1146180.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (7, 9, CAST(N'2021-12-22' AS Date), 2601000.0000, 182070.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (8, 2, CAST(N'2022-1-24' AS Date), 17631000.0000, 1234170.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (9, 5, CAST(N'2022-2-11' AS Date), 33381000.0000, 2336670.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (10, 7, CAST(N'2022-3-14' AS Date), 16374000.0000, 1146180.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (11, 1, CAST(N'2022-4-18' AS Date), 16254000.0000, 1137780.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (12, 8, CAST(N'2022-5-25' AS Date), 12599100.0000, 881937.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (13, 6, CAST(N'2022-6-21' AS Date), 2862000.0000, 200340.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (14, 3, CAST(N'2022-7-22' AS Date), 15381000.0000, 1076670.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (15, 8, CAST(N'2022-8-17' AS Date), 5202000.0000, 364140.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (16, 2, CAST(N'2022-9-14' AS Date), 5580000.0000, 390600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (17, 4, CAST(N'2022-10-13' AS Date), 3690000.0000, 258300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (18, 6, CAST(N'2022-11-19' AS Date), 972000.0000, 68040.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (19, 5, CAST(N'2022-12-11' AS Date), 8650000.0000, 605500.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (20, 10, CAST(N'2023-1-1' AS Date), 3290000.0000, 230300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (21, 9, CAST(N'2023-2-20' AS Date), 3690000.0000, 258300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (22, 6, CAST(N'2023-3-16' AS Date), 9891000.0000, 692370.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (23, 9, CAST(N'2023-4-29' AS Date), 2601000.0000, 182070.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (24, 1, CAST(N'2023-5-27' AS Date), 17990000.0000, 1259300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (25, 6, CAST(N'2023-6-25' AS Date), 12599100.0000, 881937.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (26, 5, CAST(N'2023-7-19' AS Date), 33980000.0000, 2378600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (27, 4, CAST(N'2023-11-1' AS Date), 4790000.0000, 335300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (28, 4, CAST(N'2023-8-12' AS Date), 33381000.0000, 2336670.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (29, 3, CAST(N'2023-9-18' AS Date), 25290000.0000, 1770300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (30, 4, CAST(N'2023-10-11' AS Date), 2601000.0000, 182070.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (31, 2, CAST(N'2023-11-03' AS Date), 2790000.0000, 195300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (32, 8, CAST(N'2023-12-18' AS Date), 1431000.0000, 100170.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (33, 2, CAST(N'2023-12-19' AS Date), 25198200.0000, 1763874.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (34, 2, CAST(N'2023-12-20' AS Date), 16374000.0000, 1146180.0000)
SET IDENTITY_INSERT [dbo].[shop_order] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname], [Gender], [Address], [Tel], [IsHide]) VALUES (1, N'tmpl', N'Q0Ly+JsXiAI=', N'Trần Mỹ Phú Lâm', N'Nam', N'Dĩ An, Bình Dương', N'0983636254', NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CatID])
REFERENCES [dbo].[category] ([CatID])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_promotion] FOREIGN KEY([PromoID])
REFERENCES [dbo].[promotion] ([IdPromo])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_promotion]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_product] FOREIGN KEY([ProID])
REFERENCES [dbo].[product] ([ProID])
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_product]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_shop_order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[shop_order] ([OrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_shop_order]
GO
ALTER TABLE [dbo].[shop_order]  WITH CHECK ADD  CONSTRAINT [FK_shop_order_customer] FOREIGN KEY([CusID])
REFERENCES [dbo].[customer] ([CusID])
GO
ALTER TABLE [dbo].[shop_order] CHECK CONSTRAINT [FK_shop_order_customer]
GO
USE [master]
GO
ALTER DATABASE [MyShopDB] SET  READ_WRITE 
GO
