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