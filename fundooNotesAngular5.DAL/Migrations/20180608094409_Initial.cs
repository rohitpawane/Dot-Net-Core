using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace fundooNotesAngular5.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tblUserCollaborates_collaboratorId",
                table: "tblUserCollaborates",
                column: "collaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserCollaborates_tblCollaborator_collaboratorId",
                table: "tblUserCollaborates",
                column: "collaboratorId",
                principalTable: "tblCollaborator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserCollaborates_tblCollaborator_collaboratorId",
                table: "tblUserCollaborates");

            migrationBuilder.DropIndex(
                name: "IX_tblUserCollaborates_collaboratorId",
                table: "tblUserCollaborates");
        }
    }
}
