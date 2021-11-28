
--		Table : dbo.Accounts
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Accounts') AND type in (N'U'))
	Begin
		ALTER TABLE Accounts SET ( SYSTEM_VERSIONING = OFF  )
		ALTER TABLE Accounts DROP PERIOD FOR SYSTEM_TIME;
		DROP TABLE Accounts
		DROP TABLE AccountsHistory
	End
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Accounts
(
	Id					UNIQUEIDENTIFIER NOT NULL ,
	Name				Varchar(50) NOT NULL,
	Status				varchar(15) NOT NULL,
	EffectiveFrom		datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),		
    EffectiveUntil		datetime2  NOT NULL DEFAULT CONVERT(DATETIME2, '9999-12-31 23:59:59.99999999')
	CONSTRAINT PK_Accounts  PRIMARY KEY (ID)
)
GO
ALTER TABLE Accounts ADD PERIOD FOR SYSTEM_TIME (EffectiveFrom,EffectiveUntil);
GO
ALTER TABLE Accounts SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.AccountsHistory));
GO

--		Table : PhoneNumber
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PhoneNumber') AND type in (N'U'))
	Begin
		ALTER TABLE PhoneNumber 	SET ( SYSTEM_VERSIONING = OFF  )
		ALTER TABLE PhoneNumber DROP PERIOD FOR SYSTEM_TIME;
		DROP TABLE PhoneNumber
		DROP TABLE PhoneNumberHistory

	End
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE PhoneNumber
(
	Id					UNIQUEIDENTIFIER NOT NULL ,
	AccountId			UNIQUEIDENTIFIER NULL , 
	EffectiveFrom		datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),		
    EffectiveUntil		datetime2  NOT NULL DEFAULT CONVERT(DATETIME2, '9999-12-31 23:59:59.99999999')
	CONSTRAINT PK_PhoneNumber  PRIMARY KEY (ID)
)
GO

CREATE INDEX IX_PhoneNumber_AccountId ON PhoneNumber (AccountId)
GO

ALTER TABLE PhoneNumber ADD PERIOD FOR SYSTEM_TIME (EffectiveFrom,EffectiveUntil);
GO
ALTER TABLE PhoneNumber SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.PhoneNumberHistory));
GO