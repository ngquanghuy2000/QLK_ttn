create database QLK_ttn
go
use QLK_ttn
go
create table Suplier(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(200),
	Address_ nvarchar(max),
	Phone nvarchar(20),
	Mail nvarchar(100)
)
go
create table Customer(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(200),
	Address_ nvarchar(max),
	Phone nvarchar(20),
	Mail nvarchar(100)
)
go
create table ObjectType(
	ID int identity(1,1) primary key,
	DisplayName nvarchar(200)
)
go
create table Object_(
	ID nvarchar(128) primary key,
	DisplayName nvarchar(200),
	IDType int not null references ObjectType(ID),
	IDSuplier int not null references Suplier(ID),
	Quantity int default 0,
	Available bit,
	InputPrice float default 0,
	OutputPrice float default 0
)
go
create table User_(
	ID int identity(1,1),
	DisplayName nvarchar(200),
	UserName nvarchar(100) primary key,
	Password_ nvarchar(200),
	AdminRole bit
)
go
create table Input(
	ID nvarchar(128) primary key,
	InputDate datetime
)
go
create table InputInfo(
	IDObject nvarchar(128) not null references Object_(ID),
	IDInput nvarchar(128) not null references Input(ID),
	primary key(IDObject,IDInput),
	Count_ int
)
go
create table Output_(
	ID nvarchar(128) primary key,
	OutputDate datetime
)
go
create table OutputInfo(
	IDObject nvarchar(128) not null references Object_(ID),
	IDOutput nvarchar(128) not null references Output_(ID),
	primary key(IDObject,IDOutput),
	IDCustomer int not null references Customer(ID),
	Count_ int
)
go
insert into User_(DisplayName,UserName,Password_,AdminRole) values(N'Người quản trị','admin','admin',1)
insert into ObjectType(DisplayName) values (N'Loại 1')
insert into ObjectType(DisplayName) values (N'Loại 2')
insert into ObjectType(DisplayName) values (N'Loại 3')
insert into ObjectType(DisplayName) values (N'Loại 4')
insert into ObjectType(DisplayName) values (N'Loại 5')
insert into Suplier(DisplayName,Phone,Mail,Address_) values(N'Nhà cung cấp 1','9999-999-999','suplier1@email.com',N'Hà Nội')
insert into Suplier(DisplayName,Phone,Mail,Address_) values(N'Nhà cung cấp 2','8888-888-888','suplier2@email.com',N'Hà Nội')
insert into Suplier(DisplayName,Phone,Mail,Address_) values(N'Nhà cung cấp 3','7777-777-777','suplier3@email.com',N'TP. Hồ Chí Minh')
insert into Suplier(DisplayName,Phone,Mail,Address_) values(N'Nhà cung cấp 4','6666-666-666','suplier4@email.com',N'TP. Hồ Chí Minh')
insert into Suplier(DisplayName,Phone,Mail,Address_) values(N'Nhà cung cấp 5','5555-555-555','suplier5@email.com',N'Hà Nội')
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ001',N'Sản phẩm 1',1,1,1000,'true',100000,105000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ002',N'Sản phẩm 2',1,2,1000,'true',200000,210000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ003',N'Sản phẩm 3',3,1,1000,'true',150000,160000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ004',N'Sản phẩm 4',4,3,1000,'true',350000,355000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ005',N'Sản phẩm 5',5,1,1000,'true',240000,255000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ006',N'Sản phẩm 6',2,4,1000,'true',160000,175000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ007',N'Sản phẩm 7',4,3,1000,'true',250000,255000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ008',N'Sản phẩm 8',5,5,1000,'true',300000,305000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ009',N'Sản phẩm 9',2,1,1000,'false',400000,405000)
insert into Object_(ID,DisplayName,IDType,IDSuplier,Quantity,Available,InputPrice,OutputPrice) values('OBJ010',N'Sản phẩm 10',3,3,1000,'true',330000,345000)
insert into Customer(DisplayName,Phone,Mail,Address_) values(N'Khách hàng 1','1111-111-111','customer1@email.com',N'Hà Nội')
insert into Customer(DisplayName,Phone,Mail,Address_) values(N'Khách hàng 2','2222-222-222','customer2@email.com',N'Hà Nội')
insert into Customer(DisplayName,Phone,Mail,Address_) values(N'Khách hàng 3','3333-333-333','customer3@email.com',N'Hà Nội')
insert into Customer(DisplayName,Phone,Mail,Address_) values(N'Khách hàng 4','4444-444-444','customer1@email.com',N'Hà Nội')
