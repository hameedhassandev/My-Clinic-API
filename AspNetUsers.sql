create table AspNetUsers
(
    Id                   nvarchar(450) not null
        constraint PK_AspNetUsers
            primary key,
    Name                 nvarchar(max) not null,
    UserName             nvarchar(256),
    NormalizedUserName   nvarchar(256),
    Email                nvarchar(256),
    NormalizedEmail      nvarchar(256),
    EmailConfirmed       bit           not null,
    PasswordHash         nvarchar(max),
    SecurityStamp        nvarchar(max),
    ConcurrencyStamp     nvarchar(max),
    PhoneNumber          nvarchar(max),
    PhoneNumberConfirmed bit           not null,
    TwoFactorEnabled     bit           not null,
    LockoutEnd           datetimeoffset,
    LockoutEnabled       bit           not null,
    AccessFailedCount    int           not null
)
go

create index EmailIndex
    on AspNetUsers (NormalizedEmail)
go

create unique index UserNameIndex
    on AspNetUsers (NormalizedUserName)
    where [NormalizedUserName] IS NOT NULL
go

INSERT INTO ChatAppDB.dbo.AspNetUsers (Id, Name, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'11b1cddf-c2f3-4865-9dfc-33d8952b795c', N'Amr Rizk', N'Amr1elmasry', N'AMR1ELMASRY', N'Amr.elmasry@gmail.com', N'AMR.ELMASRY@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJ49z62+u+OKO54qdBCKJ8AVtEfWgb1D0gRyZ597/GAzH2YKOmugv/omC5AohxnlRQ==', N'LTT4ODNWFBU2LQC2RPP5MS3CK5INMHTW', N'a0249e27-98ab-47b5-9e42-f5489db035e9', N'01151059974', 0, 0, null, 0, 0);
INSERT INTO ChatAppDB.dbo.AspNetUsers (Id, Name, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES (N'65e4bbce-d2d7-40da-ac02-7de72491b491', N'Hameed Hassan', N'Hameed2hassan', N'HAMEED2HASSAN', N'hameed2@gmail.com', N'HAMEED2@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKDExt8Lx7ATFblsVy6O6Z3aIvmb94xNuAiq4TuyhCpP5+pO+osm7MLSob92we3Hpw==', N'LFMMP6W6HU7FEY5B7UHRR6R43754PLE6', N'383525df-9530-4d99-85b9-d5e1f25e9764', N'0123456789', 0, 0, null, 0, 0);
