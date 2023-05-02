CREATE TABLE [dbo].[Nguoi] (
    [MaNguoi]  INT            IDENTITY (1, 1) NOT NULL,
    [HoTen]    NVARCHAR (50)  NOT NULL,
    [Gioitinh] NVARCHAR (10)  NOT NULL,
    [NgaySinh] DATE           NOT NULL,
    [DiaChi]   NVARCHAR (100) NOT NULL,
    [Sdt]      CHAR (10)      NULL,
    PRIMARY KEY CLUSTERED ([MaNguoi] ASC)
);



CREATE TABLE [dbo].[LoaiTK] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO
INSERT INTO LoaiTK(
	id,
	name
) VALUES (
	0,
	N'Admin'
),(
	1,
	N'Nhân viên'
)

CREATE TABLE [dbo].[NhanVien] (
    [MaNV]    VARCHAR (10)  NOT NULL,
    [Chucvu]  NVARCHAR (50) NULL,
    [MaNguoi] INT           NULL,
    PRIMARY KEY CLUSTERED ([MaNV] ASC),
    CONSTRAINT [MaNguoi1_FK] FOREIGN KEY ([MaNguoi]) REFERENCES [dbo].[Nguoi] ([MaNguoi])
);

CREATE TABLE [dbo].[TaiKhoan] (
    [TaiKhoan] VARCHAR (50) NOT NULL,
    [MatKhau]  VARCHAR (50) NOT NULL,
    [LoaiTK]   INT          DEFAULT ((0)) NULL,
    [MaNV]     VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([TaiKhoan] ASC),
    CONSTRAINT [LoaiTK_FK] FOREIGN KEY ([LoaiTK]) REFERENCES [dbo].[LoaiTK] ([id]),
    CONSTRAINT [NhanVien_FK] FOREIGN KEY ([MaNV]) REFERENCES [dbo].[NhanVien] ([MaNV])
);

CREATE TABLE [dbo].[LoaiDG] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
GO
INSERT INTO LoaiDG(
	id,
	name
) VALUES (
	0,
	N'Normal'
),(
	1,
	N'Premium'
),(
	2,
	N'Pro'
)

GO

CREATE TABLE [dbo].[DocGia] (
    [MaDG]       INT           IDENTITY (1, 1) NOT NULL,
    [NgheNghiep] NVARCHAR (10) NOT NULL,
    [LoaiDG]     INT           NOT NULL,
    [MaNguoi]    INT           NULL,
    PRIMARY KEY CLUSTERED ([MaDG] ASC),
    CONSTRAINT [MaNguoi_FK2] FOREIGN KEY ([MaNguoi]) REFERENCES [dbo].[Nguoi] ([MaNguoi]),
    CONSTRAINT [LoaiDG_FK] FOREIGN KEY ([LoaiDG]) REFERENCES [dbo].[LoaiDG] ([id])
);

CREATE TABLE [dbo].[TheLoai] (
    [MaTL]  INT           IDENTITY (1, 1) NOT NULL,
    [TenTL] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaTL] ASC)
);

CREATE TABLE [dbo].[Sach] (
    [MaSach]    INT           IDENTITY (1, 1) NOT NULL,
    [TenSach]   NVARCHAR (50) NOT NULL,
    [TenTG]     NVARCHAR (50) NOT NULL,
    [SoLuong]   INT           NOT NULL,
    [SoLuongTT] INT           NOT NULL,
    [MaTL]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([MaSach] ASC),
    CONSTRAINT [MaTL_FK] FOREIGN KEY ([MaTL]) REFERENCES [dbo].[TheLoai] ([MaTL])
);


CREATE TABLE [dbo].[TheThuVien] (
    [MaDG]    INT  NOT NULL,
    [ThoiHan] DATE NOT NULL,
    CONSTRAINT [MaDG_PK] PRIMARY KEY CLUSTERED ([MaDG] ASC),
    CONSTRAINT [MaDG_FK] FOREIGN KEY ([MaDG]) REFERENCES [dbo].[DocGia] ([MaDG])
);

CREATE TABLE [dbo].[LuaChon] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[TrangThai] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

GO
INSERT INTO LuaChon(
	id ,
	name
) VALUES (
	0,
	N'Mượn đọc tại chỗ'
),(
	1,
	N'Mượn mang về'
)

INSERT INTO TrangThai(
	id ,
	name
) VALUES (
	0,
	N'Đang mượn'
),(
	1,
	N'Đã hoàn thành'
) , (
	2,
	N'Quá hạn'
)

CREATE TABLE [dbo].[PhieuMuon] (
    [MaPM]      INT          IDENTITY (1, 1) NOT NULL,
    [MaDG]      INT          NULL,
    [MaNV]      VARCHAR (10) NULL,
    [NgayMuon]  DATE         NOT NULL,
    [NgayTra]   DATE         NOT NULL,
    [LuaChon]   INT          NOT NULL,
    [TrangThai] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([MaPM] ASC),
    CONSTRAINT [MaDG_FK1] FOREIGN KEY ([MaDG]) REFERENCES [dbo].[TheThuVien] ([MaDG]),
    CONSTRAINT [MaNV_FK] FOREIGN KEY ([MaNV]) REFERENCES [dbo].[NhanVien] ([MaNV]),
    CONSTRAINT [Luachon_FK] FOREIGN KEY ([LuaChon]) REFERENCES [dbo].[LuaChon] ([id]),
    CONSTRAINT [Trangthai_FK] FOREIGN KEY ([TrangThai]) REFERENCES [dbo].[TrangThai] ([id])
);

CREATE TABLE [dbo].[ChiTietPhieuMuon] (
    [MaCT]    INT IDENTITY (1, 1) NOT NULL,
    [MaPM]    INT NULL,
    [SoLuong] INT NOT NULL,
    [MaSach]  INT NULL,
    PRIMARY KEY CLUSTERED ([MaCT] ASC),
    CONSTRAINT [MaPM_FK] FOREIGN KEY ([MaPM]) REFERENCES [dbo].[PhieuMuon] ([MaPM]),
    CONSTRAINT [MaSach_FK] FOREIGN KEY ([MaSach]) REFERENCES [dbo].[Sach] ([MaSach])
);

CREATE TABLE [dbo].[PhieuPhat] (
    [MaPP] INT            IDENTITY (1, 1) NOT NULL,
    [MaPM] INT            NULL,
    [LyDo] NVARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([MaPP] ASC),
    CONSTRAINT [MaPM_FK1] FOREIGN KEY ([MaPM]) REFERENCES [dbo].[PhieuMuon] ([MaPM])
);