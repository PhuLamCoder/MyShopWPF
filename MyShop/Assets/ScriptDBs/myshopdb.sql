USE [MyShopDB]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/13/2023 10:45:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](200) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 4/13/2023 10:45:13 AM ******/
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
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 4/13/2023 10:45:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](10) NULL,
 CONSTRAINT [pk_role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 4/13/2023 10:45:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [text] NULL,
	[Password] [text] NULL,
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

INSERT [dbo].[category] ([CatID], [CatName]) VALUES (1, N'Điện thoại Android')
INSERT [dbo].[category] ([CatID], [CatName]) VALUES (2, N'Điện thoại Iphone (IOS)')
INSERT [dbo].[category] ([CatID], [CatName]) VALUES (3, N'Điện thoại phổ thông')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3013, N'Samsung Galaxy S23', 8, 128, 6.1, N'Samsung Galaxy S23 5G 128GB chắc hẳn không còn là cái tên 
quá xa lạ đối với các tín độ công nghệ hiện nay, được xem là một trong những gương mặt chủ 
chốt đến từ nhà Samsung với cấu hình mạnh mẽ bậc nhất, camera trứ danh hàng đầu cùng 
lối hoàn thiện vô cùng sang trọng và hiện đại.', NULL, 20990000.0000, N'Assets/Images/sp/1.png', N'Samsung', 3900, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3014, N'iPhone 14 Pro Max', 6, 128, 6.7, N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi 
kèm theo đó là thiết kế hình màn hình mới, hứa hẹn mang lại nhữn
g trải nghiệm đầy mới mẻ cho người dùng iPhone.', NULL, 27290000.0000, N'Assets/Images/sp/2.png', N'Apple', 4323, 2)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3015, N'iPhone 14 Pro', 6, 128, 6.7, N'iPhone 14 Pro 128GB - Mẫu smartphone đến từ nhà Apple được mong đợi nhất năm 2022, lần này nhà Táo mang 
đến cho chúng ta một phiên bản với kiểu thiết kế hình notch mới, cấu hình mạnh mẽ nhờ con chip Apple A16 
Bionic và cụm camera có độ phân giải được nâng cấp lên đến 48 MP.', NULL, 25290000.0000, N'Assets/Images/sp/3.png', N'Apple', 3200, 2)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3016, N'Samsung Galaxy S21 FE', 6, 128, 6.4, N'Samsung Galaxy S21 FE 5G (6GB/128GB) được Samsung ra mắt với dáng dấp thời thượng, cấu hình khỏe,
chụp ảnh đẹp với bộ 3 camera chất lượng, thời lượng pin
đủ dùng hằng ngày và còn gì nữa? Mời bạn khám phá qua 
nội dung sau ngay.', NULL, 10990000.0000, N'Assets/Images/sp/4.png', N'Samsung', 4500, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3017, N'Xiaomi Redmi 12C', 4, 64, 6.71, N'Xiaomi Redmi 12C 64GB là một thiết bị di động tầm trung được
giới thiệu bởi Xiaomi, với cấu hình vượt trội 
so với các đối thủ trong cùng phân khúc', NULL, 2890000.0000, N'Assets/Images/sp/5.png', N'Xiaomi', 5000, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3018, N'Samsung Galaxy S23 Ultra', 8, 265, 6.8, N'Cuối cùng thì chiếc điện thoại Samsung Galaxy S23
cũng đã chính thức ra mắt tại sự kiện Galaxy Unpacked 
vào đầu tháng 2 năm 2023, máy nổi bật với camera 200 MP 
chất lượng, hiệu năng mạnh mẽ.', NULL, 26990000.0000, N'Assets/Images/sp/6.png', N'Samsung', 5000, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3019, N'iPhone 11 64GB', 4, 6, 6.1, N'Apple đã chính thức trình làng bộ 3 siêu phẩm iPhone 11, trong đó phiên bản iPhone 11 64GB có mức giá rẻ nhất nhưng vẫn được nâng
cấp mạnh mẽ như iPhone Xr ra mắt trước đó.', NULL, 10590000.0000, N'Assets/Images/sp/7.png', N'Apple', 3110, 2)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3020, N'Điện thoại Vivo Y35', 8, 128, 6.58, N'Tiếp nối sự thành công của các mẫu smartphone tầm trung tại thị trường Việt Nam thì mới đây Vivo đã chính thức cho ra mắt điện thoại Vivo Y35. 
Máy sở hữu cho mình khả năng sạc siêu nhanh 44 W', NULL, 6290000.0000, N'Assets/Images/sp/8.png', N'Vivo', 5000, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3021, N'OPPO Reno8 T 5G', 8, 256, 6.7, N'Tiếp nối sự thành công rực rỡ đến từ các thế hệ trước đó thì chiếc OPPO Reno8 T 5G 256GB cuối cùng
đã được giới thiệu chính thức tại Việt Nam', NULL, 10990000.0000, N'Assets/Images/sp/9.png', N'OPPO', 4800, 1)
INSERT [dbo].[product] ([ProID], [ProName], [Ram], [Rom], [ScreenSize], [Desc], [FullDes], [Price], [ImagePath], [Trademark], [BatteryCapacity], [CatID]) VALUES (3022, N'Samsung Galaxy A23', 4, 128, 6.6, N'Samsung Galaxy A23 4GB sở hữu bộ thông số kỹ thuật khá ấn tượng trong phân khúc, máy có một hiệu năng ổn định, cụm 4 camera thông minh cùng một diện mạo trẻ 
trung phù hợp cho mọi đối tượng người dùng.', NULL, 4790000.0000, N'Assets/Images/sp/10.png', N'Samsung', 5000, 1)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CatID])
REFERENCES [dbo].[category] ([CatID])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [fk_user_role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[role] ([RoleID])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [fk_user_role]
GO
