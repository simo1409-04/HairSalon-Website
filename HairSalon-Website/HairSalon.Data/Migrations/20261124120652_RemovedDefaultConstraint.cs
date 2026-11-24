using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalon.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDefaultConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DECLARE @ConstraintName nvarchar(200);
                SELECT @ConstraintName = dc.name
                FROM sys.default_constraints dc
                JOIN sys.columns c
                    ON dc.parent_object_id = c.object_id
                   AND dc.parent_column_id = c.column_id
                WHERE dc.parent_object_id = OBJECT_ID(N'[dbo].[HairDressers]')
                  AND c.name = N'Email';

                IF @ConstraintName IS NOT NULL
                    EXEC(N'ALTER TABLE [dbo].[HairDressers] DROP CONSTRAINT [' + @ConstraintName + N']');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.default_constraints dc
                    JOIN sys.columns c
                        ON dc.parent_object_id = c.object_id
                       AND dc.parent_column_id = c.column_id
                    WHERE dc.parent_object_id = OBJECT_ID(N'[dbo].[HairDressers]')
                      AND c.name = N'Email'
                )
                BEGIN
                    ALTER TABLE [dbo].[HairDressers]
                    ADD DEFAULT (N'') FOR [Email];
                END
            ");
        }
    }
}
