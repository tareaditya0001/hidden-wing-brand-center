using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Slug = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "themes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Slug = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    PrimaryColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    SecondaryColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    AccentColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    BackgroundColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    SurfaceColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TextColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MutedTextColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    BorderColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FontFamily = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    BorderRadius = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Slug = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ApplicationUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "brand_assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand_assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_brand_assets_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_brand_assets_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "product_brandings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThemeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    ShortName = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Tagline = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: true),
                    LogoUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LogoDarkUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    LogoLightUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IconUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FaviconUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PrimaryColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    SecondaryColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    AccentColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    BackgroundColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    SurfaceColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TextColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    MutedTextColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    BorderColor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    FontFamily = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    SupportEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PrivacyUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TermsUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_brandings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_brandings_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_brandings_themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Description", "IsActive", "Name", "Slug", "UpdatedAt" },
                values: new object[] { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), "Hidden Wing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Technology, products, solutions and digital ventures built under Hidden Wing.", true, "Hidden Wing", "hidden-wing", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "themes",
                columns: new[] { "Id", "AccentColor", "BackgroundColor", "BorderColor", "BorderRadius", "CreatedAt", "FontFamily", "IsDefault", "MutedTextColor", "Name", "PrimaryColor", "SecondaryColor", "Slug", "SurfaceColor", "TextColor", "UpdatedAt" },
                values: new object[] { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), "#C6A15B", "#F4EFE6", "#D9D2C5", "8px", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Inter, system-ui, sans-serif", true, "#5C6B7A", "Hidden Wing Default", "#1C3353", "#3E536B", "hidden-wing-default", "#FFFFFF", "#1A1F29", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "brand_assets",
                columns: new[] { "Id", "BrandId", "CreatedAt", "FileSize", "IsActive", "MimeType", "Name", "ProductId", "Type", "UpdatedAt", "Url" },
                values: new object[,]
                {
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000041"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/svg+xml", "Hidden Wing Logo", null, "Logo", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/logo.svg" },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000042"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/svg+xml", "Hidden Wing Logo Dark", null, "LogoDark", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/logo-dark.svg" },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000043"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/svg+xml", "Hidden Wing Logo Light", null, "LogoLight", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/logo-light.svg" },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000044"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/svg+xml", "Hidden Wing Icon", null, "Icon", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/icon.svg" },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000045"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/svg+xml", "Hidden Wing Favicon", null, "Favicon", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/favicon.svg" },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000046"), new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L, true, "image/png", "Hidden Wing OG Image", null, "OgImage", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "/assets/hidden-wing/og-image.png" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "ApplicationUrl", "BrandId", "CreatedAt", "Description", "IsActive", "Name", "Slug", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000021"), null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Internal operations and company headquarters workspace.", true, "Hidden Wing HQ", "hq", "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000022"), null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Commerce and catalog experience for Hidden Wing products.", true, "Hidden Wing Store", "store", "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000023"), null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Project delivery and collaboration workspace.", true, "Hidden Wing Projects", "projects", "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000024"), null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Data platform and reporting workspace.", true, "Hidden Wing Data", "data", "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000025"), null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Administrative console for Hidden Wing operators.", true, "Hidden Wing Admin", "admin", "Active", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "product_brandings",
                columns: new[] { "Id", "AccentColor", "BackgroundColor", "BorderColor", "CreatedAt", "DisplayName", "FaviconUrl", "FontFamily", "IconUrl", "IsEnabled", "LogoDarkUrl", "LogoLightUrl", "LogoUrl", "MutedTextColor", "PrimaryColor", "PrivacyUrl", "ProductId", "SecondaryColor", "ShortName", "SupportEmail", "SurfaceColor", "Tagline", "TermsUrl", "TextColor", "ThemeId", "UpdatedAt", "WebsiteUrl" },
                values: new object[,]
                {
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000031"), null, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hidden Wing HQ", null, null, null, true, null, null, null, null, null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000021"), null, "HQ", null, null, "Company operations, in one place.", null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000032"), null, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hidden Wing Store", null, null, null, true, null, null, null, null, null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000022"), null, "Store", null, null, "Products and commerce under Hidden Wing.", null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000033"), null, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hidden Wing Projects", null, null, null, true, null, null, null, null, null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000023"), null, "Projects", null, null, "Plan and deliver Hidden Wing work.", null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000034"), null, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hidden Wing Data", null, null, null, true, null, null, null, null, null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000024"), null, "Data", null, null, "Insight from Hidden Wing systems.", null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000035"), null, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hidden Wing Admin", null, null, null, true, null, null, null, null, null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000025"), null, "Admin", null, null, "Operate Hidden Wing platforms.", null, null, new Guid("8a0f1c2e-3b4d-4e5f-9a01-000000000010"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_brand_assets_BrandId",
                table: "brand_assets",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_brand_assets_ProductId",
                table: "brand_assets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_brands_Slug",
                table: "brands",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_brandings_ProductId",
                table: "product_brandings",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_brandings_ThemeId",
                table: "product_brandings",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_products_BrandId",
                table: "products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_products_Slug",
                table: "products",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_themes_Slug",
                table: "themes",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brand_assets");

            migrationBuilder.DropTable(
                name: "product_brandings");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "themes");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
