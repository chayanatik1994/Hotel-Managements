using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M_08_Hotel.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelRoomAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "date", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "money", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });
            //1
            string sql = @"CREATE PROCEDURE InsertRoom
	                        @r  NVARCHAR(50),
	                        @rd Date,
                            @p Decimal,
	                        @hid INT
                        AS
	                        INSERT INTO Room (RoomType, ReservationDate,PricePerNight ,HotelId) VALUES (@r, @rd,@p, @hid)
                        RETURN 0";
            migrationBuilder.Sql(sql);
            //2
            sql = @"CREATE PROCEDURE DeleteRoomOfHotel
	                @hid INT
                AS
	                DELETE Room WHERE HotelId = @hid
                RETURN 0";
            migrationBuilder.Sql(sql);
            //2
            sql = @"CREATE PROCEDURE DeleteHotel
	                @hid INT
                AS
	                DELETE Hotels WHERE Hotel = @hid
                RETURN 0";

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");
            migrationBuilder.Sql("DROP PROCEDURE InsertRoom");
            migrationBuilder.Sql("DROP PROCEDURE DeleteRoomOfHotel");
            migrationBuilder.Sql("DROP PROCEDURE DeleteHotel");
        }
    }
}
