CREATE PROCEDURE [dbo].[Account_Insert] 
    @Account AccountType READONLY 
AS
    INSERT INTO
        [dbo].[ApplicationUser] (
            [Username],
            [NormalizedUsername],
            [Email],
            [NormalizedEmail],
            [Fullname],
            [PasswordHash]
        )
    SELECT
        [Username],
        [NormalizedUsername],
        [Email],
        [NormalizedEmail],
        [Fullname],
        [PasswordHash]
    FROM
        @Account;

    SELECT
        CAST(SCOPE_IDENTITY() AS INT);

RETURN 0