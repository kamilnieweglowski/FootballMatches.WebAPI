using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballMatches.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCodeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stadiums_CountryCodes_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "CountryCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    StadiumId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_CountryCodes_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "CountryCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HomeTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    StadiumId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_CountryCodes_CountryCodeId",
                        column: x => x.CountryCodeId,
                        principalTable: "CountryCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lineups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOnBench = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lineups_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lineups_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lineups_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 1, "AF", "Afghanistan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 2, "AX", "Åland Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 3, "AL", "Albania" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 4, "DZ", "Algeria" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 5, "AS", "American Samoa" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 6, "AD", "Andorra" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 7, "AO", "Angola" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 8, "AI", "Anguilla" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 9, "AQ", "Antarctica" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 10, "AG", "Antigua and Barbuda" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 11, "AR", "Argentina" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 12, "AM", "Armenia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 13, "AW", "Aruba" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 14, "AU", "Australia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 15, "AT", "Austria" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 16, "AZ", "Azerbaijan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 17, "BS", "Bahamas" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 18, "BH", "Bahrain" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 19, "BD", "Bangladesh" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 20, "BB", "Barbados" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 21, "BY", "Belarus" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 22, "BE", "Belgium" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 23, "BZ", "Belize" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 24, "BJ", "Benin" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 25, "BM", "Bermuda" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 26, "BT", "Bhutan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 27, "BO", "Bolivia (Plurinational State of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 28, "BQ", "Bonaire, Sint Eustatius and Saba" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 29, "BA", "Bosnia and Herzegovina" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 30, "BW", "Botswana" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 31, "BV", "Bouvet Island" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 32, "BR", "Brazil" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 33, "IO", "British Indian Ocean Territory" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 34, "BN", "Brunei Darussalam" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 35, "BG", "Bulgaria" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 36, "BF", "Burkina Faso" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 37, "BI", "Burundi" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 38, "CV", "Cabo Verde" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 39, "KH", "Cambodia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 40, "CM", "Cameroon" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 41, "CA", "Canada" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 42, "KY", "Cayman Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 43, "CF", "Central African Republic" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 44, "TD", "Chad" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 45, "CL", "Chile" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 46, "CN", "China" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 47, "CX", "Christmas Island" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 48, "CC", "Cocos (Keeling) Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 49, "CO", "Colombia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 50, "KM", "Comoros" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 51, "CG", "Congo" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 52, "CD", "Congo (Democratic Republic of the)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 53, "CK", "Cook Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 54, "CR", "Costa Rica" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 55, "CI", "Côte d'Ivoire" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 56, "HR", "Croatia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 57, "CU", "Cuba" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 58, "CW", "Curaçao" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 59, "CY", "Cyprus" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 60, "CZ", "Czechia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 61, "DK", "Denmark" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 62, "DJ", "Djibouti" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 63, "DM", "Dominica" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 64, "DO", "Dominican Republic" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 65, "EC", "Ecuador" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 66, "EG", "Egypt" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 67, "SV", "El Salvador" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 68, "GQ", "Equatorial Guinea" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 69, "ER", "Eritrea" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 70, "EE", "Estonia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 71, "ET", "Ethiopia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 72, "FK", "Falkland Islands (Malvinas)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 73, "FO", "Faroe Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 74, "FJ", "Fiji" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 75, "FI", "Finland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 76, "FR", "France" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 77, "GF", "French Guiana" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 78, "PF", "French Polynesia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 79, "TF", "French Southern Territories" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 80, "GA", "Gabon" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 81, "GM", "Gambia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 82, "GE", "Georgia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 83, "DE", "Germany" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 84, "GH", "Ghana" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 85, "GI", "Gibraltar" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 86, "GR", "Greece" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 87, "GL", "Greenland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 88, "GD", "Grenada" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 89, "GP", "Guadeloupe" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 90, "GU", "Guam" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 91, "GT", "Guatemala" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 92, "GG", "Guernsey" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 93, "GN", "Guinea" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 94, "GW", "Guinea-Bissau" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 95, "GY", "Guyana" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 96, "HT", "Haiti" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 97, "HM", "Heard Island and McDonald Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 98, "VA", "Holy See" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 99, "HN", "Honduras" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 100, "HK", "Hong Kong" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 101, "HU", "Hungary" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 102, "IS", "Iceland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 103, "IN", "India" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 104, "ID", "Indonesia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 105, "IR", "Iran (Islamic Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 106, "IQ", "Iraq" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 107, "IE", "Ireland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 108, "IM", "Isle of Man" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 109, "IL", "Israel" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 110, "IT", "Italy" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 111, "JM", "Jamaica" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 112, "JP", "Japan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 113, "JE", "Jersey" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 114, "JO", "Jordan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 115, "KZ", "Kazakhstan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 116, "KE", "Kenya" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 117, "KI", "Kiribati" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 118, "KP", "Korea (Democratic People's Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 119, "KR", "Korea (Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 120, "KW", "Kuwait" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 121, "KG", "Kyrgyzstan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 122, "LA", "Lao People's Democratic Republic" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 123, "LV", "Latvia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 124, "LB", "Lebanon" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 125, "LS", "Lesotho" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 126, "LR", "Liberia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 127, "LY", "Libya" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 128, "LI", "Liechtenstein" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 129, "LT", "Lithuania" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 130, "LU", "Luxembourg" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 131, "MO", "Macao" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 132, "MK", "Macedonia (the former Yugoslav Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 133, "MG", "Madagascar" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 134, "MW", "Malawi" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 135, "MY", "Malaysia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 136, "MV", "Maldives" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 137, "ML", "Mali" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 138, "MT", "Malta" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 139, "MH", "Marshall Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 140, "MQ", "Martinique" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 141, "MR", "Mauritania" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 142, "MU", "Mauritius" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 143, "YT", "Mayotte" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 144, "MX", "Mexico" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 145, "FM", "Micronesia (Federated States of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 146, "MD", "Moldova (Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 147, "MC", "Monaco" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 148, "MN", "Mongolia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 149, "ME", "Montenegro" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 150, "MS", "Montserrat" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 151, "MA", "Morocco" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 152, "MZ", "Mozambique" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 153, "MM", "Myanmar" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 154, "NA", "Namibia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 155, "NR", "Nauru" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 156, "NP", "Nepal" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 157, "NL", "Netherlands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 158, "NC", "New Caledonia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 159, "NZ", "New Zealand" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 160, "NI", "Nicaragua" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 161, "NE", "Niger" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 162, "NG", "Nigeria" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 163, "NU", "Niue" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 164, "NF", "Norfolk Island" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 165, "MP", "Northern Mariana Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 166, "NO", "Norway" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 167, "OM", "Oman" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 168, "PK", "Pakistan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 169, "PW", "Palau" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 170, "PS", "Palestine, State of" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 171, "PA", "Panama" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 172, "PG", "Papua New Guinea" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 173, "PY", "Paraguay" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 174, "PE", "Peru" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 175, "PH", "Philippines" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 176, "PN", "Pitcairn" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 177, "PL", "Poland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 178, "PT", "Portugal" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 179, "PR", "Puerto Rico" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 180, "QA", "Qatar" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 181, "RE", "Réunion" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 182, "RO", "Romania" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 183, "RU", "Russian Federation" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 184, "RW", "Rwanda" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 185, "BL", "Saint Barthélemy" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 186, "SH", "Saint Helena, Ascension and Tristan da Cunha" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 187, "KN", "Saint Kitts and Nevis" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 188, "LC", "Saint Lucia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 189, "MF", "Saint Martin (French part)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 190, "PM", "Saint Pierre and Miquelon" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 191, "VC", "Saint Vincent and the Grenadines" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 192, "WS", "Samoa" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 193, "SM", "San Marino" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 194, "ST", "Sao Tome and Principe" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 195, "SA", "Saudi Arabia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 196, "SN", "Senegal" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 197, "RS", "Serbia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 198, "SC", "Seychelles" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 199, "SL", "Sierra Leone" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 200, "SG", "Singapore" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 201, "SX", "Sint Maarten (Dutch part)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 202, "SK", "Slovakia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 203, "SI", "Slovenia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 204, "SB", "Solomon Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 205, "SO", "Somalia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 206, "ZA", "South Africa" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 207, "GS", "South Georgia and the South Sandwich Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 208, "SS", "South Sudan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 209, "ES", "Spain" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 210, "LK", "Sri Lanka" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 211, "SD", "Sudan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 212, "SR", "Suriname" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 213, "SJ", "Svalbard and Jan Mayen" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 214, "SZ", "Swaziland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 215, "SE", "Sweden" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 216, "CH", "Switzerland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 217, "SY", "Syrian Arab Republic" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 218, "TW", "Taiwan, Province of China[a]" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 219, "TJ", "Tajikistan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 220, "TZ", "Tanzania, United Republic of" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 221, "TH", "Thailand" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 222, "TL", "Timor-Leste" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 223, "TG", "Togo" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 224, "TK", "Tokelau" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 225, "TO", "Tonga" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 226, "TT", "Trinidad and Tobago" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 227, "TN", "Tunisia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 228, "TR", "Turkey" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 229, "TM", "Turkmenistan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 230, "TC", "Turks and Caicos Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 231, "TV", "Tuvalu" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 232, "UG", "Uganda" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 233, "UA", "Ukraine" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 234, "AE", "United Arab Emirates" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 235, "GB", "United Kingdom of Great Britain and Northern Ireland" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 236, "US", "United States of America" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 237, "UM", "United States Minor Outlying Islands" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 238, "UY", "Uruguay" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 239, "UZ", "Uzbekistan" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 240, "VU", "Vanuatu" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 241, "VE", "Venezuela (Bolivarian Republic of)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 242, "VN", "Viet Nam" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 243, "VG", "Virgin Islands (British)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 244, "VI", "Virgin Islands (U.S.)" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 245, "WF", "Wallis and Futuna" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 246, "EH", "Western Sahara" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 247, "YE", "Yemen" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 248, "ZM", "Zambia" });

            migrationBuilder.InsertData(
                table: "CountryCodes",
                columns: new[] { "Id", "Code", "Country" },
                values: new object[] { 249, "ZW", "Zimbabwe" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 1, "New Josianneshire", 41, "Rempel - Zboncak" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 2, "Lake Gwen", 64, "Breitenberg, Kling and Bashirian" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 3, "Cieloborough", 177, "Lemke Group" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 4, "Breitenbergberg", 25, "Aufderhar - Greenfelder" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 5, "Leannonberg", 122, "Nicolas - Homenick" });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "City", "CountryCodeId", "Name" },
                values: new object[] { 6, "Branthaven", 52, "Torp - Wisozk" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 1, "Schusterstad", 24, "Kulas, Stracke and Hyatt United", 1 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 2, "New Miltonshire", 230, "Mosciski, Sporer and Heathcote United", 2 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 3, "West Novellaville", 29, "Towne Inc City", 3 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 4, "Maggiotown", 33, "Fisher, Turcotte and Donnelly F.C.", 4 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 5, "North Joaniefurt", 202, "Hermiston - Smitham City", 5 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CountryCodeId", "Name", "StadiumId" },
                values: new object[] { 6, "Maramouth", 141, "Quitzon Group United", 6 });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayTeamId", "Date", "HomeTeamId", "StadiumId" },
                values: new object[] { 1, 2, new DateTime(2021, 7, 9, 10, 29, 18, 812, DateTimeKind.Local).AddTicks(3134), 1, 1 });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayTeamId", "Date", "HomeTeamId", "StadiumId" },
                values: new object[] { 2, 4, new DateTime(2021, 9, 17, 7, 55, 52, 70, DateTimeKind.Local).AddTicks(9719), 3, 2 });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayTeamId", "Date", "HomeTeamId", "StadiumId" },
                values: new object[] { 3, 6, new DateTime(2021, 9, 20, 23, 32, 25, 12, DateTimeKind.Local).AddTicks(7987), 5, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 1, 57, new DateTime(1992, 1, 11, 3, 19, 32, 233, DateTimeKind.Local).AddTicks(4422), "Angel", "Corwin", 1, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 2, 59, new DateTime(1990, 5, 7, 14, 1, 57, 450, DateTimeKind.Local).AddTicks(6809), "Don", "Herzog", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 3, 58, new DateTime(1990, 10, 4, 2, 15, 56, 331, DateTimeKind.Local).AddTicks(6222), "Noah", "Rowe", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 4, 165, new DateTime(1976, 12, 17, 18, 59, 1, 397, DateTimeKind.Local).AddTicks(2434), "Harvey", "Greenfelder", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 5, 155, new DateTime(1971, 9, 6, 4, 59, 15, 105, DateTimeKind.Local).AddTicks(1211), "Felipe", "Rath", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 6, 116, new DateTime(1964, 11, 21, 10, 55, 34, 983, DateTimeKind.Local).AddTicks(830), "Mario", "Spinka", 3, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 7, 96, new DateTime(1966, 4, 5, 18, 4, 54, 639, DateTimeKind.Local).AddTicks(3182), "Jake", "Bayer", 3, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 8, 179, new DateTime(1960, 3, 31, 17, 47, 28, 827, DateTimeKind.Local).AddTicks(2836), "Nicolas", "Torphy", 3, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 9, 38, new DateTime(1992, 3, 18, 11, 43, 38, 81, DateTimeKind.Local).AddTicks(5644), "Andre", "Swift", 3, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 10, 37, new DateTime(1977, 12, 7, 9, 5, 59, 617, DateTimeKind.Local).AddTicks(9034), "Dan", "Mayer", 4, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 11, 5, new DateTime(1980, 7, 17, 7, 5, 23, 702, DateTimeKind.Local).AddTicks(5763), "Ira", "Ryan", 4, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 12, 91, new DateTime(1963, 1, 26, 5, 16, 47, 825, DateTimeKind.Local).AddTicks(4692), "Clint", "Murray", 1, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 13, 245, new DateTime(1993, 8, 18, 5, 21, 28, 460, DateTimeKind.Local).AddTicks(542), "Derrick", "Gusikowski", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 14, 31, new DateTime(1971, 1, 28, 5, 24, 14, 98, DateTimeKind.Local).AddTicks(4398), "Clint", "Smitham", 1, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 15, 71, new DateTime(1981, 5, 29, 2, 53, 59, 903, DateTimeKind.Local).AddTicks(5525), "Brad", "Koepp", 3, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 16, 190, new DateTime(1960, 11, 29, 18, 55, 12, 204, DateTimeKind.Local).AddTicks(8734), "Jeremiah", "Lakin", 2, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 17, 87, new DateTime(1970, 2, 25, 5, 24, 48, 2, DateTimeKind.Local).AddTicks(8027), "Allen", "Renner", 1, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 18, 234, new DateTime(1976, 7, 19, 21, 34, 58, 636, DateTimeKind.Local).AddTicks(1880), "Greg", "Osinski", 1, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 19, 107, new DateTime(1961, 2, 4, 6, 16, 56, 106, DateTimeKind.Local).AddTicks(6146), "Aubrey", "Ernser", 1, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 20, 105, new DateTime(1996, 2, 17, 23, 45, 42, 999, DateTimeKind.Local).AddTicks(2040), "Johnathan", "Zemlak", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 21, 164, new DateTime(1976, 8, 25, 7, 2, 0, 179, DateTimeKind.Local).AddTicks(1172), "Dominic", "O'Conner", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 22, 115, new DateTime(1980, 4, 1, 18, 7, 53, 688, DateTimeKind.Local).AddTicks(55), "Rick", "Gibson", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 23, 230, new DateTime(1993, 11, 8, 4, 25, 54, 972, DateTimeKind.Local).AddTicks(7365), "Ben", "Robel", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 24, 221, new DateTime(1989, 6, 10, 14, 2, 27, 523, DateTimeKind.Local).AddTicks(5674), "Rolando", "Rohan", 3, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 25, 130, new DateTime(1993, 12, 5, 5, 37, 42, 872, DateTimeKind.Local).AddTicks(6524), "Dennis", "Lockman", 3, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 26, 191, new DateTime(1963, 1, 25, 13, 34, 22, 634, DateTimeKind.Local).AddTicks(7362), "Eric", "Steuber", 3, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 27, 133, new DateTime(1958, 7, 13, 6, 37, 34, 799, DateTimeKind.Local).AddTicks(8756), "Felix", "Bashirian", 3, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 28, 50, new DateTime(1994, 9, 26, 4, 56, 13, 849, DateTimeKind.Local).AddTicks(6046), "Eduardo", "Larkin", 4, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 29, 126, new DateTime(1957, 8, 21, 14, 7, 58, 691, DateTimeKind.Local).AddTicks(8694), "Stewart", "Bashirian", 4, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 30, 174, new DateTime(1979, 5, 23, 19, 51, 19, 828, DateTimeKind.Local).AddTicks(8294), "Bradley", "O'Kon", 1, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 31, 122, new DateTime(1976, 2, 11, 1, 32, 51, 521, DateTimeKind.Local).AddTicks(9065), "Ira", "Stanton", 3, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 32, 248, new DateTime(1958, 9, 11, 3, 4, 25, 805, DateTimeKind.Local).AddTicks(42), "Mario", "Corwin", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 33, 130, new DateTime(1970, 7, 26, 12, 21, 36, 707, DateTimeKind.Local).AddTicks(3662), "Fernando", "Harris", 1, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 34, 210, new DateTime(1966, 5, 22, 8, 57, 32, 295, DateTimeKind.Local).AddTicks(1018), "Allan", "Johns", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 35, 225, new DateTime(1957, 5, 11, 21, 28, 39, 850, DateTimeKind.Local).AddTicks(7262), "Greg", "Lynch", 1, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 36, 63, new DateTime(1960, 8, 15, 7, 47, 54, 553, DateTimeKind.Local).AddTicks(5124), "Gustavo", "Stiedemann", 2, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 37, 182, new DateTime(1963, 9, 21, 0, 0, 37, 437, DateTimeKind.Local).AddTicks(5326), "Rafael", "Dooley", 1, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 38, 103, new DateTime(1957, 11, 14, 7, 57, 23, 844, DateTimeKind.Local).AddTicks(5048), "Angelo", "Pagac", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 39, 141, new DateTime(1990, 4, 20, 12, 55, 10, 403, DateTimeKind.Local).AddTicks(7723), "Austin", "Kovacek", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 40, 84, new DateTime(1963, 6, 26, 3, 48, 45, 873, DateTimeKind.Local).AddTicks(7770), "Harold", "Okuneva", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 41, 23, new DateTime(1987, 5, 17, 10, 26, 16, 376, DateTimeKind.Local).AddTicks(1112), "Jerald", "Flatley", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 42, 38, new DateTime(1979, 4, 20, 23, 26, 12, 35, DateTimeKind.Local).AddTicks(6185), "Jay", "Zieme", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 43, 57, new DateTime(1961, 1, 10, 13, 26, 11, 519, DateTimeKind.Local).AddTicks(2790), "Larry", "Schinner", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 44, 181, new DateTime(1973, 9, 27, 5, 32, 30, 755, DateTimeKind.Local).AddTicks(2171), "Simon", "Donnelly", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 45, 226, new DateTime(1976, 9, 22, 16, 6, 51, 392, DateTimeKind.Local).AddTicks(3989), "Robin", "Schroeder", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 46, 189, new DateTime(1961, 1, 9, 17, 53, 7, 662, DateTimeKind.Local).AddTicks(5128), "Hugh", "Kozey", 4, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 47, 75, new DateTime(1989, 11, 15, 10, 45, 18, 765, DateTimeKind.Local).AddTicks(6739), "Neal", "Goldner", 4, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 48, 86, new DateTime(1976, 1, 11, 13, 16, 44, 361, DateTimeKind.Local).AddTicks(3555), "Alan", "Corkery", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 49, 40, new DateTime(1986, 7, 5, 21, 20, 25, 567, DateTimeKind.Local).AddTicks(5334), "Moses", "Carroll", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 50, 178, new DateTime(1992, 3, 14, 15, 6, 42, 394, DateTimeKind.Local).AddTicks(2130), "Bernard", "Hermann", 1, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 51, 24, new DateTime(1990, 8, 14, 3, 15, 35, 807, DateTimeKind.Local).AddTicks(7894), "Rogelio", "Runolfsdottir", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 52, 201, new DateTime(1979, 2, 11, 10, 40, 14, 81, DateTimeKind.Local).AddTicks(2353), "Michael", "Quitzon", 3, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 53, 173, new DateTime(1957, 7, 3, 13, 49, 23, 462, DateTimeKind.Local).AddTicks(2518), "Jeffrey", "Cole", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 54, 243, new DateTime(1960, 10, 14, 21, 25, 15, 167, DateTimeKind.Local).AddTicks(1578), "Clayton", "Kunze", 2, 3 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 55, 182, new DateTime(1974, 6, 24, 11, 10, 36, 690, DateTimeKind.Local).AddTicks(6157), "Allen", "Gorczany", 1, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 56, 174, new DateTime(1964, 10, 11, 22, 54, 58, 882, DateTimeKind.Local).AddTicks(1720), "Richard", "Schaefer", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 57, 119, new DateTime(1976, 5, 1, 0, 12, 29, 124, DateTimeKind.Local).AddTicks(4226), "Clayton", "Shields", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 58, 3, new DateTime(1969, 3, 26, 9, 48, 37, 836, DateTimeKind.Local).AddTicks(2792), "Felipe", "Boehm", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 59, 12, new DateTime(1980, 5, 16, 19, 28, 1, 290, DateTimeKind.Local).AddTicks(1731), "Raymond", "Hodkiewicz", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 60, 118, new DateTime(1983, 8, 1, 13, 22, 1, 715, DateTimeKind.Local).AddTicks(6859), "Clifton", "Hintz", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 61, 152, new DateTime(1980, 6, 9, 17, 41, 13, 594, DateTimeKind.Local).AddTicks(1658), "Clark", "Torp", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 62, 71, new DateTime(1971, 5, 10, 5, 15, 14, 333, DateTimeKind.Local).AddTicks(7712), "Adrian", "Murphy", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 63, 13, new DateTime(1967, 1, 24, 7, 58, 43, 692, DateTimeKind.Local).AddTicks(3024), "Carlos", "Wilkinson", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 64, 49, new DateTime(1987, 8, 23, 3, 55, 16, 702, DateTimeKind.Local).AddTicks(6838), "Eddie", "Considine", 4, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 65, 108, new DateTime(1963, 9, 5, 1, 33, 3, 371, DateTimeKind.Local).AddTicks(6672), "Todd", "Tillman", 4, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 66, 59, new DateTime(1974, 7, 18, 10, 57, 40, 534, DateTimeKind.Local).AddTicks(2637), "Jeremy", "Strosin", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 67, 241, new DateTime(1979, 11, 9, 23, 37, 46, 878, DateTimeKind.Local).AddTicks(270), "Herman", "Bayer", 1, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 68, 227, new DateTime(1965, 11, 15, 8, 7, 59, 678, DateTimeKind.Local).AddTicks(3096), "Gregory", "Barton", 1, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 69, 226, new DateTime(1964, 10, 19, 15, 50, 1, 460, DateTimeKind.Local).AddTicks(468), "Gabriel", "Hermann", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 70, 62, new DateTime(1959, 3, 20, 13, 46, 9, 367, DateTimeKind.Local).AddTicks(4846), "Geoffrey", "D'Amore", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 71, 42, new DateTime(1990, 7, 16, 17, 56, 23, 543, DateTimeKind.Local).AddTicks(3104), "Neil", "Simonis", 3, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 72, 7, new DateTime(1965, 5, 9, 21, 23, 54, 104, DateTimeKind.Local).AddTicks(4538), "Robin", "Funk", 2, 4 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 73, 243, new DateTime(1989, 11, 1, 22, 29, 0, 635, DateTimeKind.Local).AddTicks(7626), "Kristopher", "Labadie", 1, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 74, 247, new DateTime(1968, 5, 3, 10, 59, 11, 492, DateTimeKind.Local).AddTicks(6920), "Manuel", "McCullough", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 75, 93, new DateTime(1960, 7, 28, 9, 59, 46, 782, DateTimeKind.Local).AddTicks(3408), "Roland", "Skiles", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 76, 165, new DateTime(1979, 6, 28, 8, 8, 5, 697, DateTimeKind.Local).AddTicks(6801), "Jon", "Towne", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 77, 39, new DateTime(1962, 8, 26, 22, 19, 12, 771, DateTimeKind.Local).AddTicks(2120), "Manuel", "Mitchell", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 78, 141, new DateTime(1975, 1, 16, 18, 31, 43, 549, DateTimeKind.Local).AddTicks(8912), "Felipe", "Zboncak", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 79, 16, new DateTime(1977, 7, 29, 11, 37, 43, 296, DateTimeKind.Local).AddTicks(5837), "Dean", "Runolfsdottir", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 80, 11, new DateTime(1984, 5, 7, 19, 20, 52, 463, DateTimeKind.Local).AddTicks(6148), "Stuart", "Fay", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 81, 17, new DateTime(1966, 9, 7, 23, 6, 35, 603, DateTimeKind.Local).AddTicks(1342), "Duane", "Haley", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 82, 2, new DateTime(1993, 4, 4, 6, 23, 31, 4, DateTimeKind.Local).AddTicks(4028), "Javier", "Davis", 4, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 83, 14, new DateTime(1984, 2, 17, 1, 32, 54, 795, DateTimeKind.Local).AddTicks(9207), "Lynn", "Glover", 4, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 84, 196, new DateTime(1979, 5, 5, 20, 26, 3, 561, DateTimeKind.Local).AddTicks(3471), "Marion", "Hilpert", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 85, 183, new DateTime(1972, 8, 7, 11, 7, 28, 543, DateTimeKind.Local).AddTicks(26), "Nathan", "Hagenes", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 86, 185, new DateTime(1976, 1, 30, 2, 15, 20, 38, DateTimeKind.Local).AddTicks(7888), "Irvin", "Turcotte", 1, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 87, 163, new DateTime(1972, 12, 26, 19, 35, 27, 743, DateTimeKind.Local).AddTicks(6253), "Damon", "Herman", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 88, 111, new DateTime(1959, 7, 8, 19, 36, 16, 595, DateTimeKind.Local).AddTicks(7976), "Brendan", "Pollich", 2, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 89, 230, new DateTime(1984, 3, 22, 5, 24, 42, 350, DateTimeKind.Local).AddTicks(6482), "Ryan", "Yundt", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 90, 116, new DateTime(1960, 3, 20, 7, 25, 5, 287, DateTimeKind.Local).AddTicks(7178), "Kent", "Dare", 3, 5 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 91, 99, new DateTime(1975, 1, 12, 9, 35, 23, 706, DateTimeKind.Local).AddTicks(850), "Dan", "Lang", 1, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 92, 219, new DateTime(1983, 11, 30, 22, 21, 16, 208, DateTimeKind.Local).AddTicks(1419), "Warren", "O'Reilly", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 93, 62, new DateTime(1957, 7, 4, 19, 59, 38, 8, DateTimeKind.Local).AddTicks(5464), "Charles", "Veum", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 94, 38, new DateTime(1976, 9, 28, 22, 14, 40, 883, DateTimeKind.Local).AddTicks(8191), "Felipe", "Mitchell", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 95, 56, new DateTime(1993, 4, 14, 23, 29, 59, 708, DateTimeKind.Local).AddTicks(9707), "Edmund", "Quigley", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 96, 43, new DateTime(1993, 4, 27, 19, 40, 21, 735, DateTimeKind.Local).AddTicks(5322), "Arturo", "Adams", 3, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 97, 43, new DateTime(1958, 5, 15, 8, 46, 48, 901, DateTimeKind.Local).AddTicks(8300), "Byron", "Wintheiser", 3, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 98, 34, new DateTime(1988, 9, 27, 10, 7, 28, 414, DateTimeKind.Local).AddTicks(3212), "Pedro", "Murazik", 3, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 99, 220, new DateTime(1977, 8, 18, 23, 18, 45, 49, DateTimeKind.Local).AddTicks(4276), "Taylor", "Daniel", 3, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 100, 153, new DateTime(1986, 1, 28, 11, 53, 54, 248, DateTimeKind.Local).AddTicks(2789), "Arthur", "Hilll", 4, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 101, 229, new DateTime(1960, 1, 1, 8, 3, 45, 192, DateTimeKind.Local).AddTicks(5056), "Terence", "Kshlerin", 4, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 102, 228, new DateTime(1961, 2, 6, 2, 7, 5, 240, DateTimeKind.Local).AddTicks(408), "Terrence", "Pfeffer", 3, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 103, 55, new DateTime(1964, 5, 9, 12, 22, 31, 731, DateTimeKind.Local).AddTicks(4022), "Kirk", "Johnson", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 104, 195, new DateTime(1966, 4, 30, 3, 35, 31, 101, DateTimeKind.Local).AddTicks(8598), "Reginald", "Larkin", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 105, 109, new DateTime(1989, 1, 2, 14, 15, 27, 749, DateTimeKind.Local).AddTicks(7596), "Homer", "Boehm", 2, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 106, 169, new DateTime(1978, 11, 7, 21, 37, 44, 630, DateTimeKind.Local).AddTicks(3577), "Alan", "Jast", 1, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 107, 210, new DateTime(1981, 10, 30, 21, 56, 41, 491, DateTimeKind.Local).AddTicks(8810), "Larry", "Littel", 1, 6 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CountryCodeId", "DateOfBirth", "FirstName", "LastName", "Position", "TeamId" },
                values: new object[] { 108, 212, new DateTime(1990, 8, 18, 22, 49, 47, 838, DateTimeKind.Local).AddTicks(7541), "Tomas", "Gibson", 3, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 1, false, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 2, false, 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 3, false, 1, 3, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 4, false, 1, 4, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 5, false, 1, 5, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 6, false, 1, 6, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 7, false, 1, 7, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 8, false, 1, 8, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 9, false, 1, 9, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 10, false, 1, 10, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 11, false, 1, 11, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 12, true, 1, 12, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 13, true, 1, 13, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 14, true, 1, 14, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 15, true, 1, 15, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 16, true, 1, 16, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 17, true, 1, 17, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 18, true, 1, 18, 1 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 19, false, 1, 19, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 20, false, 1, 20, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 21, false, 1, 21, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 22, false, 1, 22, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 23, false, 1, 23, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 24, false, 1, 24, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 25, false, 1, 25, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 26, false, 1, 26, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 27, false, 1, 27, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 28, false, 1, 28, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 29, false, 1, 29, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 30, true, 1, 30, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 31, true, 1, 31, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 32, true, 1, 32, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 33, true, 1, 33, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 34, true, 1, 34, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 35, true, 1, 35, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 36, true, 1, 36, 2 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 37, false, 2, 37, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 38, false, 2, 38, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 39, false, 2, 39, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 40, false, 2, 40, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 41, false, 2, 41, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 42, false, 2, 42, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 43, false, 2, 43, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 44, false, 2, 44, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 45, false, 2, 45, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 46, false, 2, 46, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 47, false, 2, 47, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 48, true, 2, 48, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 49, true, 2, 49, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 50, true, 2, 50, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 51, true, 2, 51, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 52, true, 2, 52, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 53, true, 2, 53, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 54, true, 2, 54, 3 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 55, false, 2, 55, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 56, false, 2, 56, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 57, false, 2, 57, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 58, false, 2, 58, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 59, false, 2, 59, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 60, false, 2, 60, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 61, false, 2, 61, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 62, false, 2, 62, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 63, false, 2, 63, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 64, false, 2, 64, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 65, false, 2, 65, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 66, true, 2, 66, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 67, true, 2, 67, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 68, true, 2, 68, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 69, true, 2, 69, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 70, true, 2, 70, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 71, true, 2, 71, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 72, true, 2, 72, 4 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 73, false, 3, 73, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 74, false, 3, 74, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 75, false, 3, 75, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 76, false, 3, 76, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 77, false, 3, 77, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 78, false, 3, 78, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 79, false, 3, 79, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 80, false, 3, 80, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 81, false, 3, 81, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 82, false, 3, 82, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 83, false, 3, 83, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 84, true, 3, 84, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 85, true, 3, 85, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 86, true, 3, 86, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 87, true, 3, 87, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 88, true, 3, 88, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 89, true, 3, 89, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 90, true, 3, 90, 5 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 91, false, 3, 91, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 92, false, 3, 92, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 93, false, 3, 93, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 94, false, 3, 94, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 95, false, 3, 95, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 96, false, 3, 96, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 97, false, 3, 97, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 98, false, 3, 98, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 99, false, 3, 99, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 100, false, 3, 100, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 101, false, 3, 101, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 102, true, 3, 102, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 103, true, 3, 103, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 104, true, 3, 104, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 105, true, 3, 105, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 106, true, 3, 106, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 107, true, 3, 107, 6 });

            migrationBuilder.InsertData(
                table: "Lineups",
                columns: new[] { "Id", "IsOnBench", "MatchId", "PlayerId", "TeamId" },
                values: new object[] { 108, true, 3, 108, 6 });

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_MatchId",
                table: "Lineups",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_PlayerId",
                table: "Lineups",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lineups_TeamId",
                table: "Lineups",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StadiumId",
                table: "Matches",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryCodeId",
                table: "Players",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_CountryCodeId",
                table: "Stadiums",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryCodeId",
                table: "Teams",
                column: "CountryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lineups");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropTable(
                name: "CountryCodes");
        }
    }
}
