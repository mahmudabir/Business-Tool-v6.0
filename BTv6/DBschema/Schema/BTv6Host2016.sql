USE [BusinessToolDB]
GO
ALTER TABLE [dbo].[sales] DROP CONSTRAINT [FK_sales_products]
GO
ALTER TABLE [dbo].[sales] DROP CONSTRAINT [FK_sales_employees]
GO
ALTER TABLE [dbo].[profile_images] DROP CONSTRAINT [FK_profile_images_employees]
GO
ALTER TABLE [dbo].[products] DROP CONSTRAINT [FK_products_employees]
GO
ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK_orders_products]
GO
ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK_orders_employees]
GO
ALTER TABLE [dbo].[orders] DROP CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[notes] DROP CONSTRAINT [FK_notes_employees]
GO
ALTER TABLE [dbo].[log_in] DROP CONSTRAINT [FK_log_in_status]
GO
ALTER TABLE [dbo].[employees] DROP CONSTRAINT [FK_employees_log_in2]
GO
ALTER TABLE [dbo].[employees] DROP CONSTRAINT [FK_employees_log_in1]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_log_in]
GO
ALTER TABLE [dbo].[complains] DROP CONSTRAINT [FK_complains_customers]
GO
ALTER TABLE [dbo].[chats] DROP CONSTRAINT [FK_chats_employees2]
GO
ALTER TABLE [dbo].[chats] DROP CONSTRAINT [FK_chats_employees1]
GO
ALTER TABLE [dbo].[sales] DROP CONSTRAINT [DF__sales__Sell_SDat__6383C8BA]
GO
ALTER TABLE [dbo].[products] DROP CONSTRAINT [DF__products__Add_PD__60A75C0F]
GO
ALTER TABLE [dbo].[products] DROP CONSTRAINT [DF__products__AVAILA__5FB337D6]
GO
ALTER TABLE [dbo].[orders] DROP CONSTRAINT [DF__orders__delivery__5DCAEF64]
GO
ALTER TABLE [dbo].[orders] DROP CONSTRAINT [DF__orders__ord_date__5CD6CB2B]
GO
ALTER TABLE [dbo].[log_in] DROP CONSTRAINT [DF__log_in__PASS__6D0D32F4]
GO
ALTER TABLE [dbo].[employees] DROP CONSTRAINT [DF__employees__JOIN___6C190EBB]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [DF__customers__reg_d__6B24EA82]
GO
ALTER TABLE [dbo].[chats] DROP CONSTRAINT [DF__chats__ATTACHMEN__52593CB8]
GO
ALTER TABLE [dbo].[chats] DROP CONSTRAINT [DF__chats__SUB__5165187F]
GO
ALTER TABLE [dbo].[chats] DROP CONSTRAINT [DF__chats__DATE__5070F446]
GO
/****** Object:  Table [dbo].[status]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[status]') AND type in (N'U'))
DROP TABLE [dbo].[status]
GO
/****** Object:  Table [dbo].[sales]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sales]') AND type in (N'U'))
DROP TABLE [dbo].[sales]
GO
/****** Object:  Table [dbo].[profile_images]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[profile_images]') AND type in (N'U'))
DROP TABLE [dbo].[profile_images]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[products]') AND type in (N'U'))
DROP TABLE [dbo].[products]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[orders]') AND type in (N'U'))
DROP TABLE [dbo].[orders]
GO
/****** Object:  Table [dbo].[notices]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[notices]') AND type in (N'U'))
DROP TABLE [dbo].[notices]
GO
/****** Object:  Table [dbo].[notes]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[notes]') AND type in (N'U'))
DROP TABLE [dbo].[notes]
GO
/****** Object:  Table [dbo].[log_in]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[log_in]') AND type in (N'U'))
DROP TABLE [dbo].[log_in]
GO
/****** Object:  Table [dbo].[employees]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[employees]') AND type in (N'U'))
DROP TABLE [dbo].[employees]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[customers]') AND type in (N'U'))
DROP TABLE [dbo].[customers]
GO
/****** Object:  Table [dbo].[complains]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[complains]') AND type in (N'U'))
DROP TABLE [dbo].[complains]
GO
/****** Object:  Table [dbo].[chats]    Script Date: 12/3/2020 1:12:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[chats]') AND type in (N'U'))
DROP TABLE [dbo].[chats]
GO
/****** Object:  Table [dbo].[chats]    Script Date: 12/3/2020 1:12:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chats](
	[MSG_ID] [int] IDENTITY(1,1) NOT NULL,
	[DATE] [datetime2](0) NOT NULL,
	[SUB] [varchar](250) NULL,
	[SENDER] [varchar](50) NOT NULL,
	[TEXT] [varchar](max) NOT NULL,
	[ATTACHMENT] [varchar](300) NULL,
	[RECEIVER] [varchar](50) NOT NULL,
	[STATUS] [int] NOT NULL,
 CONSTRAINT [PK__chats__825DA51C17C6F92B] PRIMARY KEY CLUSTERED 
(
	[MSG_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[complains]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[complains](
	[cID] [int] IDENTITY(1,1) NOT NULL,
	[sub] [varchar](50) NULL,
	[OwnerID] [varchar](50) NOT NULL,
	[Text] [varchar](max) NOT NULL,
 CONSTRAINT [PK__complain__D830D457DFE272A9] PRIMARY KEY CLUSTERED 
(
	[cID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[cusid] [varchar](50) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[design] [varchar](30) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[mobile] [varchar](50) NOT NULL,
	[reg_date] [datetime2](0) NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employees]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees](
	[EmpID] [varchar](50) NOT NULL,
	[E_NAME] [varchar](50) NOT NULL,
	[DID] [int] NOT NULL,
	[SAL] [float] NOT NULL,
	[E_MOB] [varchar](14) NOT NULL,
	[E_MAIL] [varchar](50) NOT NULL,
	[JOIN_DATE] [datetime2](0) NOT NULL,
	[ADDED_BY] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[log_in]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log_in](
	[LID] [varchar](50) NOT NULL,
	[SID] [int] NOT NULL,
	[PASS] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[LID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notes]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notes](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[NoteName] [varchar](50) NULL,
	[OwnerID] [varchar](50) NOT NULL,
	[Text] [varchar](max) NOT NULL,
 CONSTRAINT [PK__notes__EACE357F70163F49] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[notices]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notices](
	[noticeID] [int] IDENTITY(1,1) NOT NULL,
	[noteSub] [varchar](100) NOT NULL,
	[noticetext] [varchar](max) NOT NULL,
 CONSTRAINT [PK__notices__4ED12E4E979D4467] PRIMARY KEY CLUSTERED 
(
	[noticeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[orderid] [int] IDENTITY(1,1) NOT NULL,
	[prodid] [varchar](15) NOT NULL,
	[quant] [int] NOT NULL,
	[ammout] [float] NOT NULL,
	[stat] [varchar](50) NOT NULL,
	[ord_date] [datetime2](0) NULL,
	[deliveryby] [varchar](50) NOT NULL,
	[orderby] [varchar](50) NOT NULL,
 CONSTRAINT [PK__orders__080E37755F40D859] PRIMARY KEY CLUSTERED 
(
	[orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[PID] [varchar](15) NOT NULL,
	[P_NAME] [varchar](50) NOT NULL,
	[P_IMG] [varchar](50) NULL,
	[TYPE] [varchar](20) NOT NULL,
	[AVAILABILITY] [varchar](20) NOT NULL,
	[QUANTITY] [int] NOT NULL,
	[BUY_PRICE] [float] NOT NULL,
	[SELL_PRICE] [float] NOT NULL,
	[MOD_BY] [varchar](50) NOT NULL,
	[Add_PDate] [datetime2](0) NULL,
 CONSTRAINT [PK__products__C577552077360BE9] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[profile_images]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profile_images](
	[UID] [varchar](50) NOT NULL,
	[IMAGE] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sales]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sales](
	[SLID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [varchar](15) NOT NULL,
	[QUANT] [int] NOT NULL,
	[OB_AMMOUNT] [float] NOT NULL,
	[PROFIT] [float] NOT NULL,
	[C_NAME] [varchar](25) NOT NULL,
	[C_MOB] [varchar](14) NOT NULL,
	[SOLD_BY] [varchar](50) NOT NULL,
	[Sell_SDate] [datetime2](0) NOT NULL,
 CONSTRAINT [PK__sales__A43D35CF8823EBD3] PRIMARY KEY CLUSTERED 
(
	[SLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[status]    Script Date: 12/3/2020 1:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status](
	[SID] [int] NOT NULL,
	[DESIGNATION] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[complains] ON 

INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (7, N'123', N'5', N'123123123')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (8, N'123', N'5', N'asfdcczxvczx')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (9, N'234', N'5', N'234243234234234234234')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (10, N'dxzfgzb', N'5', N'bcxvbcxvbcvbx')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (11, N'afgsd', N'5', N'fgdfgfdgsdfsgsgsdfg')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (12, N'bcvcvb', N'5', N'cvbcvbxcvxcrdfzgf')
INSERT [dbo].[complains] ([cID], [sub], [OwnerID], [Text]) VALUES (13, N'a', N'5', N'aasd')
SET IDENTITY_INSERT [dbo].[complains] OFF
GO
INSERT [dbo].[customers] ([cusid], [name], [design], [email], [mobile], [reg_date], [status]) VALUES (N'5', N'Abir', N'asd', N'asd@asd', N'231123', CAST(N'2020-11-18T21:04:51.0000000' AS DateTime2), 0)
GO
INSERT [dbo].[employees] ([EmpID], [E_NAME], [DID], [SAL], [E_MOB], [E_MAIL], [JOIN_DATE], [ADDED_BY]) VALUES (N'1', N'Tanvir Tanjum', 1, 10000, N'3242343244', N'tanvir@gmail.com', CAST(N'2020-08-10T19:28:06.0000000' AS DateTime2), N'1')
INSERT [dbo].[employees] ([EmpID], [E_NAME], [DID], [SAL], [E_MOB], [E_MAIL], [JOIN_DATE], [ADDED_BY]) VALUES (N'2', N'2222', 1, 11111, N'111111111', N'sfgv@dasf.asd', CAST(N'2020-11-23T01:23:14.0000000' AS DateTime2), N'1')
INSERT [dbo].[employees] ([EmpID], [E_NAME], [DID], [SAL], [E_MOB], [E_MAIL], [JOIN_DATE], [ADDED_BY]) VALUES (N'3', N'3333', 1, 1111, N'111111111', N'asdsa@ada.asd', CAST(N'2020-11-23T01:22:38.0000000' AS DateTime2), N'1')
INSERT [dbo].[employees] ([EmpID], [E_NAME], [DID], [SAL], [E_MOB], [E_MAIL], [JOIN_DATE], [ADDED_BY]) VALUES (N'4', N'4444', 1, 11111, N'111111111', N'svcf@af.fgh', CAST(N'2020-11-23T01:22:54.0000000' AS DateTime2), N'1')
GO
INSERT [dbo].[log_in] ([LID], [SID], [PASS]) VALUES (N'1', 1, N'1111')
INSERT [dbo].[log_in] ([LID], [SID], [PASS]) VALUES (N'2', 2, N'2222')
INSERT [dbo].[log_in] ([LID], [SID], [PASS]) VALUES (N'3', 3, N'3333')
INSERT [dbo].[log_in] ([LID], [SID], [PASS]) VALUES (N'4', 4, N'4444')
INSERT [dbo].[log_in] ([LID], [SID], [PASS]) VALUES (N'5', 5, N'5555')
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (2, N'1', 1, 100, N'1', CAST(N'2020-11-19T17:21:51.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (3, N'1', 1, 11111, N'2', CAST(N'2020-11-19T17:39:01.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (6, N'2', 5, 75, N'0', CAST(N'2020-11-20T20:51:08.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (7, N'2', 5, 75, N'0', CAST(N'2020-11-20T20:51:25.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (9, N'2', 5, 75, N'0', CAST(N'2020-11-20T20:54:40.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (11, N'2', 10, 150, N'0', CAST(N'2020-11-20T21:01:06.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (14, N'2', 27, 405, N'1', CAST(N'2020-11-22T11:34:05.0000000' AS DateTime2), N'1', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (16, N'1', 11, 220000, N'2', CAST(N'2020-11-27T04:46:10.0000000' AS DateTime2), N'4', N'5')
INSERT [dbo].[orders] ([orderid], [prodid], [quant], [ammout], [stat], [ord_date], [deliveryby], [orderby]) VALUES (17, N'2', 100, 1500, N'2', CAST(N'2020-11-30T01:30:26.0000000' AS DateTime2), N'4', N'5')
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'1', N'Asus Laptop', N'~/Assets/image/product/default.png', N'Laptop', N'AVAILABLE', 990, 15000, 20000, N'1', CAST(N'2020-08-13T15:18:23.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'2', N'Matador', N'~/Assets/image/product/default.png', N'Pen', N'AVAILABLE', 1800, 10, 15, N'1', CAST(N'2020-11-21T16:42:10.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'3', N'Xiaomi', N'~/Assets/image/product/default.png', N'Mobile', N'AVAILABLE', 1000, 10000, 15000, N'1', CAST(N'2020-11-19T00:40:54.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'4', N'Samsung', N'~/Assets/image/product/default.png', N'Mobile', N'AVAILABLE', 1000, 12000, 15000, N'1', CAST(N'2020-11-23T12:24:21.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'5', N'Energypac', N'~/Assets/image/product/default.png', N'Bulb', N'AVAILABLE', 123431, 180, 200, N'1', CAST(N'2020-11-23T12:27:31.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'6', N'Huawei', N'~/Assets/image/product/default.png', N'Mobile', N'AVAILABLE', 1212, 16000, 21000, N'1', CAST(N'2020-11-23T12:28:32.0000000' AS DateTime2))
INSERT [dbo].[products] ([PID], [P_NAME], [P_IMG], [TYPE], [AVAILABILITY], [QUANTITY], [BUY_PRICE], [SELL_PRICE], [MOD_BY], [Add_PDate]) VALUES (N'7', N'Walton', N'~/Assets/image/product/default.png', N'Mobile', N'AVAILABLE', 1500, 11000, 12000, N'1', CAST(N'2020-11-26T13:47:58.0000000' AS DateTime2))
GO
INSERT [dbo].[profile_images] ([UID], [IMAGE]) VALUES (N'1', N'~/Assets/image/profile/default.png')
GO
SET IDENTITY_INSERT [dbo].[sales] ON 

INSERT [dbo].[sales] ([SLID], [PID], [QUANT], [OB_AMMOUNT], [PROFIT], [C_NAME], [C_MOB], [SOLD_BY], [Sell_SDate]) VALUES (1, N'1', 1, 1, 1, N'5', N'1', N'1', CAST(N'2020-11-22T14:03:17.0000000' AS DateTime2))
INSERT [dbo].[sales] ([SLID], [PID], [QUANT], [OB_AMMOUNT], [PROFIT], [C_NAME], [C_MOB], [SOLD_BY], [Sell_SDate]) VALUES (2, N'1', 1, 1, 1, N'55', N'12', N'1', CAST(N'2020-11-22T14:03:28.0000000' AS DateTime2))
INSERT [dbo].[sales] ([SLID], [PID], [QUANT], [OB_AMMOUNT], [PROFIT], [C_NAME], [C_MOB], [SOLD_BY], [Sell_SDate]) VALUES (4, N'2', 40, 600, 200, N'Abir', N'123123', N'3', CAST(N'2020-11-23T01:24:28.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[sales] OFF
GO
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (0, N'RESTRICTED')
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (1, N'ADMIN')
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (2, N'MANAGER')
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (3, N'SALESMAN')
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (4, N'DELIVERYMAN')
INSERT [dbo].[status] ([SID], [DESIGNATION]) VALUES (5, N'CUSTOMER')
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF__chats__DATE__5070F446]  DEFAULT (getdate()) FOR [DATE]
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF__chats__SUB__5165187F]  DEFAULT (NULL) FOR [SUB]
GO
ALTER TABLE [dbo].[chats] ADD  CONSTRAINT [DF__chats__ATTACHMEN__52593CB8]  DEFAULT (NULL) FOR [ATTACHMENT]
GO
ALTER TABLE [dbo].[customers] ADD  DEFAULT (getdate()) FOR [reg_date]
GO
ALTER TABLE [dbo].[employees] ADD  DEFAULT (getdate()) FOR [JOIN_DATE]
GO
ALTER TABLE [dbo].[log_in] ADD  DEFAULT (NULL) FOR [PASS]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF__orders__ord_date__5CD6CB2B]  DEFAULT (getdate()) FOR [ord_date]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF__orders__delivery__5DCAEF64]  DEFAULT (NULL) FOR [deliveryby]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF__products__AVAILA__5FB337D6]  DEFAULT ('AVAILABLE') FOR [AVAILABILITY]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF__products__Add_PD__60A75C0F]  DEFAULT (getdate()) FOR [Add_PDate]
GO
ALTER TABLE [dbo].[sales] ADD  CONSTRAINT [DF__sales__Sell_SDat__6383C8BA]  DEFAULT (getdate()) FOR [Sell_SDate]
GO
ALTER TABLE [dbo].[chats]  WITH CHECK ADD  CONSTRAINT [FK_chats_employees1] FOREIGN KEY([SENDER])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[chats] CHECK CONSTRAINT [FK_chats_employees1]
GO
ALTER TABLE [dbo].[chats]  WITH CHECK ADD  CONSTRAINT [FK_chats_employees2] FOREIGN KEY([RECEIVER])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[chats] CHECK CONSTRAINT [FK_chats_employees2]
GO
ALTER TABLE [dbo].[complains]  WITH CHECK ADD  CONSTRAINT [FK_complains_customers] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[customers] ([cusid])
GO
ALTER TABLE [dbo].[complains] CHECK CONSTRAINT [FK_complains_customers]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_log_in] FOREIGN KEY([cusid])
REFERENCES [dbo].[log_in] ([LID])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_log_in]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_log_in1] FOREIGN KEY([EmpID])
REFERENCES [dbo].[log_in] ([LID])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_log_in1]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_log_in2] FOREIGN KEY([EmpID])
REFERENCES [dbo].[log_in] ([LID])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_log_in2]
GO
ALTER TABLE [dbo].[log_in]  WITH CHECK ADD  CONSTRAINT [FK_log_in_status] FOREIGN KEY([SID])
REFERENCES [dbo].[status] ([SID])
GO
ALTER TABLE [dbo].[log_in] CHECK CONSTRAINT [FK_log_in_status]
GO
ALTER TABLE [dbo].[notes]  WITH CHECK ADD  CONSTRAINT [FK_notes_employees] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[notes] CHECK CONSTRAINT [FK_notes_employees]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([orderby])
REFERENCES [dbo].[customers] ([cusid])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_employees] FOREIGN KEY([deliveryby])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_employees]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_products] FOREIGN KEY([prodid])
REFERENCES [dbo].[products] ([PID])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_products]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_employees] FOREIGN KEY([MOD_BY])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_employees]
GO
ALTER TABLE [dbo].[profile_images]  WITH CHECK ADD  CONSTRAINT [FK_profile_images_employees] FOREIGN KEY([UID])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[profile_images] CHECK CONSTRAINT [FK_profile_images_employees]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_employees] FOREIGN KEY([SOLD_BY])
REFERENCES [dbo].[employees] ([EmpID])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_employees]
GO
ALTER TABLE [dbo].[sales]  WITH CHECK ADD  CONSTRAINT [FK_sales_products] FOREIGN KEY([PID])
REFERENCES [dbo].[products] ([PID])
GO
ALTER TABLE [dbo].[sales] CHECK CONSTRAINT [FK_sales_products]
GO
