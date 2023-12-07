USE [MyShopDB_9]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/25/2023 12:41:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](50) NULL,
	[CatIcon] [nvarchar](50) NULL,
	[CatDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 4/25/2023 12:41:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[CusID] [int] IDENTITY(1,1) NOT NULL,
	[CusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[CusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 4/25/2023 12:41:25 PM ******/
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
	[Desc] [nvarchar](300) NULL,
	[FullDes] [nvarchar](1000) NULL,
	[Price] [money] NULL,
	[ImagePath] [text] NULL,
	[Trademark] [text] NULL,
	[BatteryCapacity] [int] NULL,
	[CatID] [int] NULL,
	[Quantity] [int] NULL,
	[PromoCode] [nvarchar](150) NULL,
	[Block] [int] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[promotions]    Script Date: 4/25/2023 12:41:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotions](
	[IdPromo] [int] IDENTITY(1,1) NOT NULL,
	[PromoCode] [nvarchar](50) NULL,
	[DiscountPercent] [int] NULL,
 CONSTRAINT [pk_promo] PRIMARY KEY CLUSTERED 
(
	[IdPromo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase]    Script Date: 4/25/2023 12:41:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase](
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProID] [int] NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shop_order]    Script Date: 4/25/2023 12:41:25 PM ******/
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
/****** Object:  Table [dbo].[user]    Script Date: 4/25/2023 12:41:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [text] NULL,
	[Gender] [nchar](15) NULL,
	[Address] [text] NULL,
	[Tel] [nchar](15) NULL,
	[AvatarPath] [text] NULL,
	[IsHide] [tinyint] NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (1, N'Android', N'Android', N'Các thiết bị điện thoại chạy hệ điều hành Android')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (2, N'Iphone', N'Apple', N'Các thiết bị điện thoại chạy hệ điều hành IOS của hãng Apple')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (11, N'Window Phone', N'Windows', N'Các thiết bị điện thoại của Microsoft')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (13, N'điện thoại cục gạch', N'MobilePhone', N'Các thiết bị điện thoại cứng như đá')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (1, N'Nguyễn Thái Hiệp')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (2, N'Nguyễn Thị Hạnh Nhân')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (3, N'Dương Vũ Thái')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (4, N'Đình Văn Vũ')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (5, N'Lê Văn Nam')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (6, N'Nguyễn Thị Ngọc Hải')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (7, N'Nguyễn Trần Nhật Thi')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (8, N'Lê Bảo Bảo')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (9, N'Bảo Châu')
INSERT [dbo].[customer] ([CusID], [CusName]) VALUES (10, N'Hà Anh Tuấn')
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3013, N'Samsung Galaxy S23', 6, 256, 6.1, N'Samsung Galaxy S23 5G 128GB chắc hẳn không còn là cái tên 
quá xa lạ đối với các tín độ công nghệ hiện nay, được xem là một trong những gương mặt chủ 
chốt đến từ nhà Samsung với cấu hình mạnh mẽ bậc nhất, camera trứ danh hàng đầu cùng 
lối hoàn thiện vô cùng sang trọng và hiện đại.', NULL, 20990000.0000, N'Assets/Images/sp/7d9c9403-abba-41e0-88c2-f55785a41f0e.png', N'Samsung', 3900, 1, 1, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3014, N'iPhone 14 Pro Max', 6, 256, 6.7, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.', NULL, 27290000.0000, N'Assets/Images/sp/3014.png', N'Apple', 4323, 2, 27, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3015, N'iPhone 14 Pro', 6, 256, 6.7, N'iPhone 14 Pro 128GB - Mẫu smartphone đến từ nhà Apple được mong đợi nhất năm 2022, lần này nhà Táo mang 
đến cho chúng ta một phiên bản với kiểu thiết kế hình notch mới, cấu hình mạnh mẽ nhờ con chip Apple A16 
Bionic và cụm camera có độ phân giải được nâng cấp lên đến 48 MP.', NULL, 25290000.0000, N'Assets/Images/sp/3015.png', N'Apple', 3200, 2, 10, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3016, N'Samsung Galaxy S21 FE', 6, 128, 6.4, N'Samsung Galaxy S21 FE 5G (6GB/128GB) được Samsung ra mắt với dáng dấp thời thượng, cấu hình khỏe,
chụp ảnh đẹp với bộ 3 camera chất lượng, thời lượng pin
đủ dùng hằng ngày và còn gì nữa? Mời bạn khám phá qua 
nội dung sau ngay.', NULL, 10990000.0000, N'Assets/Images/sp/3016.png', N'Samsung', 4500, 1, 60, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3017, N'Xiaomi Redmi 12C', 4, 64, 6.71, N'Xiaomi Redmi 12C 64GB là một thiết bị di động tầm trung được
giới thiệu bởi Xiaomi, với cấu hình vượt trội 
so với các đối thủ trong cùng phân khúc', NULL, 2890000.0000, N'Assets/Images/sp/599469c0-df4b-4910-aa3a-2b2abd15b378.png', N'Xiaomi', 5000, 1, 66, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3018, N'Samsung Galaxy S23 Ultra', 8, 128, 6.8, N'Cuối cùng thì chiếc điện thoại Samsung Galaxy S23
cũng đã chính thức ra mắt tại sự kiện Galaxy Unpacked 
vào đầu tháng 2 năm 2023, máy nổi bật với camera 200 MP 
chất lượng, hiệu năng mạnh mẽ.', NULL, 26990000.0000, N'Assets/Images/sp/3018.png', N'Samsung', 5000, 1, 1, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3019, N'iPhone 11 64GB', 4, 6, 6.1, N'Apple đã chính thức trình làng bộ 3 siêu phẩm iPhone 11, trong đó phiên bản iPhone 11 64GB có mức giá rẻ nhất nhưng vẫn được nâng
cấp mạnh mẽ như iPhone Xr ra mắt trước đó.', NULL, 10590000.0000, N'Assets/Images/sp/3019.png', N'Apple', 3110, 2, 0, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3020, N'Điện thoại Vivo Y35', 8, 128, 6.58, N'Tiếp nối sự thành công của các mẫu smartphone tầm trung tại thị trường Việt Nam thì mới đây Vivo đã chính thức cho ra mắt điện thoại Vivo Y35. 
Máy sở hữu cho mình khả năng sạc siêu nhanh 44 W', NULL, 6290000.0000, N'Assets/Images/sp/3020.png', N'Vivo', 5000, 1, 1, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3021, N'OPPO Reno8 T 5G', 8, 64, 6.7, N'Tiếp nối sự thành công rực rỡ đến từ các thế hệ trước đó thì chiếc OPPO Reno8 T 5G 256GB cuối cùng
đã được giới thiệu chính thức tại Việt Nam', NULL, 10990000.0000, N'Assets/Images/sp/3021.png', N'OPPO', 4800, 1, 1, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3022, N'Samsung Galaxy A23', 4, 128, 6.6, N'Samsung Galaxy A23 4GB sở hữu bộ thông số kỹ thuật khá ấn tượng trong phân khúc, máy có một hiệu năng ổn định, cụm 4 camera thông minh cùng một diện mạo trẻ 
trung phù hợp cho mọi đối tượng người dùng.', NULL, 4790000.0000, N'Assets/Images/sp/3022.png', N'Samsung', 5000, 1, 8, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3025, N'Samsung Galaxy S20 FE', 8, 256, 6.5, N'Samsung đã giới thiệu đến người dùng thành viên mới của dòng S20 Series đó chính là điện thoại Samsung Galaxy S20 FE. Đây là mẫu flagship cao cấp quy tụ nhiều tính năng mà Samfan yêu thích, hứa hẹn sẽ mang lại trải nghiệm cao cấp của dòng Galaxy S với mức giá dễ tiếp cận hơn.', NULL, 8650000.0000, N'Assets/Images/sp/3025.png', N'Samsung', 4500, 1, 18, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3026, N'iPhone 14 128GB', 6, 128, 6.1, N'iPhone 14 128GB được xem là mẫu smartphone bùng nổ của nhà táo trong năm 2022, ấn tượng với ngoại hình trẻ trung, màn hình chất lượng đi kèm với những cải tiến về hệ điều hành và thuật toán xử lý hình ảnh, giúp máy trở thành cái tên thu hút được đông đảo người dùng quan tâm tại thời điểm ra mắt.', NULL, 19590000.0000, N'Assets/Images/sp/3026.png', N'Apple', 3279, 2, 28, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3027, N'iPhone 14 Pro Max', 6, 128, 6.6999998092651367, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022', NULL, 27090000.0000, N'Assets/Images/sp/2d6cb8fe-710f-40f6-b749-fb8ed4d2b3ae.png', N'Apple', 4323, 2, 27, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3028, N'Samsung Galaxy A34', 8, 256, 6.5999999046325684, N'Samsung Galaxy A34 5G là mẫu điện thoại thông minh tầm trung mới của Samsung được ra mắt vào tháng 03/2023', NULL, 8630000.0000, NULL, N'Samsung', 5000, 1, 23, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3029, N'iPhone 14 Pro Max', 6, 128, 6.6999998092651367, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022', NULL, 27090000.0000, NULL, N'Apple', 4323, 2, 14, NULL, 0)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID], [Quantity], [PromoCode], [Block]) VALUES (3030, N'Samsung Galaxy A34', 8, 256, 6.5999999046325684, N'Samsung Galaxy A34 5G là mẫu điện thoại thông minh tầm trung mới của Samsung được ra mắt vào tháng 03/2023', NULL, 8630000.0000, NULL, N'Samsung', 5000, 1, 25, NULL, 0)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[promotions] ON 

INSERT [dbo].[promotions] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (1, N'O12', 20)
INSERT [dbo].[promotions] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (2, N'A123', 10)
SET IDENTITY_INSERT [dbo].[promotions] OFF
GO
SET IDENTITY_INSERT [dbo].[purchase] ON 

INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (63, 61, 3025, 2, 18511000.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (61, 59, 3028, 3, 27702300.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (62, 60, 3026, 6, 125767800.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (64, 61, 3027, 2, 57972600.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (65, 62, 3026, 2, 41922600.0000)
SET IDENTITY_INSERT [dbo].[purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[shop_order] ON 

--dữ liệu phake 

INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (1, 1, CAST(N'2022-03-1' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (2, 1, CAST(N'2022-03-1' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (3, 7, CAST(N'2022-03-2' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (4, 1, CAST(N'2022-03-2' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (5, 1, CAST(N'2022-03-3' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (6, 7, CAST(N'2022-03-4' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (7, 1, CAST(N'2022-03-5' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (8, 1, CAST(N'2022-03-10' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (9, 7, CAST(N'2022-03-13' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (10, 1, CAST(N'2022-03-18' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (11, 1, CAST(N'2022-03-19' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (12, 7, CAST(N'2022-03-25' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (13, 1, CAST(N'2022-03-25' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (14, 1, CAST(N'2022-03-25' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (15, 7, CAST(N'2022-03-25' AS Date), 76483600.0000, 5003600.0000)


INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (16, 1, CAST(N'2023-03-1' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (17, 1, CAST(N'2023-03-1' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (18, 7, CAST(N'2023-03-2' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (19, 1, CAST(N'2023-03-2' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (20, 1, CAST(N'2023-03-3' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (21, 7, CAST(N'2023-03-4' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (22, 1, CAST(N'2023-03-5' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (23, 1, CAST(N'2023-03-10' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (24, 7, CAST(N'2023-03-13' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (25, 1, CAST(N'2023-03-18' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (26, 1, CAST(N'2023-03-19' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (27, 7, CAST(N'2023-03-25' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (28, 1, CAST(N'2023-03-25' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (29, 1, CAST(N'2023-03-25' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (30, 7, CAST(N'2023-03-25' AS Date), 76483600.0000, 5003600.0000)

INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (59, 1, CAST(N'2023-04-25' AS Date), 27702300.0000, 1812300.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (60, 1, CAST(N'2023-04-25' AS Date), 125767800.0000, 8227800.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (61, 7, CAST(N'2023-04-25' AS Date), 76483600.0000, 5003600.0000)
INSERT [dbo].[shop_order] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (62, 6, CAST(N'2023-04-25' AS Date), 41922600.0000, 2742600.0000)
SET IDENTITY_INSERT [dbo].[shop_order] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname], [Gender], [Address], [Tel], [AvatarPath], [IsHide], [RoleID]) VALUES (1, N'thaihiepnguyen', N'pFgk0Pg+KwewkxxcU9jjgg==', N'Nguyen Thai Hiep', N'Male           ', N'123 Nguyen Van Cu, Quan 1, TP HCM', N'0977328391     ', N'path/to/avatar.jpg', 0, 1)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CatID])
REFERENCES [dbo].[category] ([CatID])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
ALTER TABLE [dbo].[shop_order]  WITH CHECK ADD  CONSTRAINT [FK_shop_order_customer] FOREIGN KEY([CusID])
REFERENCES [dbo].[customer] ([CusID])
GO
ALTER TABLE [dbo].[shop_order] CHECK CONSTRAINT [FK_shop_order_customer]
GO
