/*
*	FILE        : 
*	PROJECT		: 
*	PROGRAMMER  : 
*	DESCRIPTION :  
*/
USE [master];
GO

DROP DATABASE IF EXISTS [HCV];
GO
CREATE DATABASE 		[HCV];
GO
ALTER DATABASE 			[HCV] SET MULTI_USER;
GO
USE [HCV];
BEGIN TRY
	BEGIN TRANSACTION


	-- 
	CREATE TABLE [dbo].[HCVPatient]
	(
		[ID]				[smallint]		NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED ([ID] ASC),
		[HealthCardNumber]	[nchar] (10)	NOT NULL,
		[VCode]				[nchar](2)		NOT NULL,
		[PostalCode]		[nchar](7)		NOT NULL
	) ON [PRIMARY]
	INSERT INTO [dbo].[HCVPatient] ([HealthCardNumber],[VCode], [PostalCode])
	VALUES
		('1234567980', 'AA', 'N2H4J3'),		-- 
		('1234567981', 'AB', 'N2H4J4'),		--
		('1234567982', 'AC', 'N2H3J5'),		--
		('1234567983', 'AD', 'N2H4J6'),		-- 
		('1234567984', 'AE', 'N2H4J7')		--



	COMMIT TRANSACTION
END TRY
BEGIN CATCH  

	-- If an error is detected, roll back the changes, and display the error
	ROLLBACK TRANSACTION;
	SELECT ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_SEVERITY() AS ErrorSeverity,
	ERROR_STATE() AS ErrorState;

END CATCH
GO
