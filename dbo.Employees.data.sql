SET IDENTITY_INSERT [dbo].[Employees] ON
INSERT INTO [dbo].[Employees] ([Id], [FirstName], [LastName], [BirthDate],
[Salary], [JobType], [Position], [ShelterId]) VALUES (1, 'Timofte' , 'EuTimofte' ,
'1-May-2020', 2.99, 'full-full-time' , 'pleb', 1)
SET IDENTITY_INSERT [dbo].[Employees] OFF
