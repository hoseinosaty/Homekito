using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barayand.DAL.Migrations
{
    public partial class TransferSystemToBarayandSubDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    A_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_UserId = table.Column<int>(type: "int", nullable: false),
                    A_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.A_Id);
                });

            migrationBuilder.CreateTable(
                name: "AmazingRequests",
                columns: table => new
                {
                    A_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_UserId = table.Column<int>(type: "int", nullable: false),
                    A_ProductId = table.Column<int>(type: "int", nullable: false),
                    A_NotificationType = table.Column<int>(type: "int", nullable: false),
                    A_Type = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmazingRequests", x => x.A_Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    A_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    A_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A_Type = table.Column<int>(type: "int", nullable: false),
                    A_SortField = table.Column<int>(type: "int", nullable: false),
                    A_UseInSearch = table.Column<bool>(type: "bit", nullable: false),
                    A_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    A_Status = table.Column<bool>(type: "bit", nullable: false),
                    A_ShowInDetail = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.A_Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeAnswer",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_CatAttrId = table.Column<int>(type: "int", nullable: false),
                    X_Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X_Sort = table.Column<int>(type: "int", nullable: false),
                    X_Status = table.Column<bool>(type: "bit", nullable: false),
                    X_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeAnswer", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowdUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BetterPriceRequest",
                columns: table => new
                {
                    B_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    B_ProductId = table.Column<int>(type: "int", nullable: false),
                    B_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_StoreWebAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_StoreOwnCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_StoreType = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetterPriceRequest", x => x.B_Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    B_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    B_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_SortField = table.Column<int>(type: "int", nullable: false),
                    B_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Status = table.Column<bool>(type: "bit", nullable: false),
                    B_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    B_ShowInIndex = table.Column<bool>(type: "bit", nullable: false),
                    B_SiteLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.B_Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    C_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_CatId = table.Column<int>(type: "int", nullable: false),
                    C_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_SortField = table.Column<int>(type: "int", nullable: false),
                    C_Status = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.C_Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAttribute",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_CatId = table.Column<int>(type: "int", nullable: false),
                    X_AttrId = table.Column<int>(type: "int", nullable: false),
                    X_Status = table.Column<bool>(type: "bit", nullable: false),
                    X_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAttribute", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    C_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_HexColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    C_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.C_Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    C_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_ParentId = table.Column<int>(type: "int", nullable: false),
                    C_UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    C_Rate = table.Column<int>(type: "int", nullable: false),
                    C_Type = table.Column<int>(type: "int", nullable: false),
                    C_EntityId = table.Column<int>(type: "int", nullable: false),
                    C_Status = table.Column<int>(type: "int", nullable: false),
                    C_Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.C_Id);
                });

            migrationBuilder.CreateTable(
                name: "Coppon",
                columns: table => new
                {
                    CP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CP_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CP_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CP_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CP_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CP_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CP_Type = table.Column<int>(type: "int", nullable: false),
                    CP_Status = table.Column<bool>(type: "bit", nullable: false),
                    CP_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coppon", x => x.CP_Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    D_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    D_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D_SortField = table.Column<int>(type: "int", nullable: false),
                    D_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D_Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    D_Status = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.D_Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPagesContent",
                columns: table => new
                {
                    D_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageSeo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageOtherSetting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPagesContent", x => x.D_Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyUsage",
                columns: table => new
                {
                    E_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    E_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_Status = table.Column<bool>(type: "bit", nullable: false),
                    E_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_Sort = table.Column<int>(type: "int", nullable: false),
                    E_Type = table.Column<int>(type: "int", nullable: false),
                    E_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    E_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyUsage", x => x.E_Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertReview",
                columns: table => new
                {
                    E_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    E_ProductId = table.Column<int>(type: "int", nullable: false),
                    E_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_Sort = table.Column<int>(type: "int", nullable: false),
                    E_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_Status = table.Column<bool>(type: "bit", nullable: false),
                    E_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertReview", x => x.E_Id);
                });

            migrationBuilder.CreateTable(
                name: "Faq",
                columns: table => new
                {
                    FA_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FA_CatId = table.Column<int>(type: "int", nullable: false),
                    FA_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FA_SortField = table.Column<int>(type: "int", nullable: false),
                    FA_Status = table.Column<bool>(type: "bit", nullable: false),
                    FA_Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FA_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.FA_Id);
                });

            migrationBuilder.CreateTable(
                name: "FaqCategories",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_SortField = table.Column<int>(type: "int", nullable: false),
                    F_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_Status = table.Column<bool>(type: "bit", nullable: false),
                    F_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqCategories", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_UserId = table.Column<int>(type: "int", nullable: false),
                    F_EntityId = table.Column<int>(type: "int", nullable: false),
                    F_EntityType = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "FestivalOffer",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    F_Type = table.Column<int>(type: "int", nullable: false),
                    F_EndLevelCategoryId = table.Column<int>(type: "int", nullable: false),
                    F_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    F_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalOffer", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Formula",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_Formula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_Status = table.Column<bool>(type: "bit", nullable: false),
                    F_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    F_CatId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formula", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryCategory",
                columns: table => new
                {
                    GC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GC_Titles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GC_SortField = table.Column<int>(type: "int", nullable: false),
                    GC_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GC_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GC_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GC_Status = table.Column<bool>(type: "bit", nullable: false),
                    GC_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    GC_Type = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryCategory", x => x.GC_Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftProduct",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_MainProdId = table.Column<int>(type: "int", nullable: false),
                    X_ProdId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftProduct", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "HeaderNotifications",
                columns: table => new
                {
                    H_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    H_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H_BgColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H_BtnColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    H_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderNotifications", x => x.H_Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageGallery",
                columns: table => new
                {
                    IG_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IG_CatId = table.Column<int>(type: "int", nullable: false),
                    IG_Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IG_SortField = table.Column<int>(type: "int", nullable: false),
                    IG_Status = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IG_ImageUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGallery", x => x.IG_Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexSections",
                columns: table => new
                {
                    I_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    I_SecId = table.Column<int>(type: "int", nullable: false),
                    I_Pid = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexSections", x => x.I_Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexSectionsInfo",
                columns: table => new
                {
                    I_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    I_SecId = table.Column<int>(type: "int", nullable: false),
                    I_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_Sort = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexSectionsInfo", x => x.I_Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    I_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_UserId = table.Column<int>(type: "int", nullable: false),
                    I_PaymentType = table.Column<int>(type: "int", nullable: false),
                    I_RecipientInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_CopponId = table.Column<int>(type: "int", nullable: false),
                    I_CopponDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    I_ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    I_ShippingMethod = table.Column<int>(type: "int", nullable: false),
                    I_PaymentInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    I_TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    I_TotalProductAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    I_DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_Status = table.Column<int>(type: "int", nullable: false),
                    I_BoxWrapperId = table.Column<int>(type: "int", nullable: false),
                    I_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    I_RequestedPOS = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ManufacturingCuntry",
                columns: table => new
                {
                    M_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    M_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_Sort = table.Column<int>(type: "int", nullable: false),
                    M_Status = table.Column<bool>(type: "bit", nullable: false),
                    M_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingCuntry", x => x.M_Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsLetter",
                columns: table => new
                {
                    NL_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NL_Entity = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NL_Type = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetter", x => x.NL_Id);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    N_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N_Type = table.Column<int>(type: "int", nullable: false),
                    N_CId = table.Column<int>(type: "int", nullable: false),
                    N_Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    N_Sup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    N_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    N_ShamsiDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_Sort = table.Column<int>(type: "int", nullable: false),
                    N_Status = table.Column<bool>(type: "bit", nullable: false),
                    N_Image = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    N_Seo = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    N_Media = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    N_MediaType = table.Column<int>(type: "int", nullable: false),
                    N_Summary = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    N_Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    N_BannerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_IsSlideShow = table.Column<int>(type: "int", nullable: false),
                    N_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    N_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    N_Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.N_Id);
                });

            migrationBuilder.CreateTable(
                name: "NoticesCategory",
                columns: table => new
                {
                    NC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NC_Type = table.Column<int>(type: "int", nullable: false),
                    NC_Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NC_SortField = table.Column<int>(type: "int", nullable: false),
                    NC_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NC_SeoUrl = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    NC_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NC_Status = table.Column<bool>(type: "bit", nullable: false),
                    NC_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticesCategory", x => x.NC_Id);
                });

            migrationBuilder.CreateTable(
                name: "OfflineRequest",
                columns: table => new
                {
                    O_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    O_User = table.Column<int>(type: "int", nullable: false),
                    O_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    O_DeliverDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_Status = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineRequest", x => x.O_Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    O_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    O_Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.O_Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    O_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    O_ReciptId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    O_ProductId = table.Column<int>(type: "int", nullable: false),
                    O_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    O_ColorId = table.Column<int>(type: "int", nullable: false),
                    O_ProductManuualId = table.Column<int>(type: "int", nullable: false),
                    O_ProductType = table.Column<int>(type: "int", nullable: false),
                    O_GiftId = table.Column<int>(type: "int", nullable: false),
                    O_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    O_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    O_DiscountType = table.Column<int>(type: "int", nullable: false),
                    O_Quantity = table.Column<int>(type: "int", nullable: false),
                    O_Version = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.O_Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfectProduct",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_MainProdId = table.Column<int>(type: "int", nullable: false),
                    X_ProdId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfectProduct", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Perm_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Perm_Urlid = table.Column<int>(type: "int", nullable: false),
                    Perm_Uid = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Perm_Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Type = table.Column<int>(type: "int", nullable: false),
                    P_MainCatId = table.Column<int>(type: "int", nullable: false),
                    P_EndLevelCatId = table.Column<int>(type: "int", nullable: false),
                    P_BrandId = table.Column<int>(type: "int", nullable: false),
                    P_MCuntryId = table.Column<int>(type: "int", nullable: false),
                    P_EnergyId = table.Column<int>(type: "int", nullable: false),
                    P_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_EnTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManualRate = table.Column<int>(type: "int", nullable: false),
                    P_DetailsTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_DetailsSubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Status = table.Column<bool>(type: "bit", nullable: false),
                    P_CompanyWarranty = table.Column<bool>(type: "bit", nullable: false),
                    P_ImmediateSend = table.Column<bool>(type: "bit", nullable: false),
                    P_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Sort = table.Column<int>(type: "int", nullable: false),
                    P_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_ImgGallery = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    P_Video = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    P_Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    P_TechnicalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Manuals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    P_BestOffer = table.Column<bool>(type: "bit", nullable: false),
                    P_AvailableCount = table.Column<int>(type: "int", nullable: false),
                    P_SaleCount = table.Column<int>(type: "int", nullable: false),
                    P_Isbn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Audio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_DownloadAbleAudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_PriceType = table.Column<int>(type: "int", nullable: false),
                    P_PriceFormulaId = table.Column<int>(type: "int", nullable: false),
                    P_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_DiscountType = table.Column<int>(type: "int", nullable: false),
                    P_ExternalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_PageCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_GiftBin = table.Column<int>(type: "int", nullable: false),
                    P_BinPrice = table.Column<int>(type: "int", nullable: false),
                    P_PrintAbleVersion = table.Column<bool>(type: "bit", nullable: false),
                    P_PrintAbleVerPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_PrintAbleVerPriceType = table.Column<int>(type: "int", nullable: false),
                    P_PrintAbleVerFormulaId = table.Column<int>(type: "int", nullable: false),
                    P_DiscountPeriodTime = table.Column<int>(type: "int", nullable: false),
                    P_PeriodDiscountPriceType = table.Column<int>(type: "int", nullable: false),
                    P_PeriodDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_PriodDiscountFormulaId = table.Column<int>(type: "int", nullable: false),
                    P_PeriodPrintablePriceType = table.Column<int>(type: "int", nullable: false),
                    P_PeriodPrintablePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    P_PeriodPrintableFomrulaId = table.Column<int>(type: "int", nullable: false),
                    P_DownloadLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_PublishDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P_Author = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.P_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeAnswer",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_PId = table.Column<int>(type: "int", nullable: false),
                    X_AId = table.Column<int>(type: "int", nullable: false),
                    X_AnswerId = table.Column<int>(type: "int", nullable: false),
                    X_AnswerTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X_EntityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeAnswer", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    PC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PC_ParentId = table.Column<int>(type: "int", nullable: false),
                    PC_Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PC_ImgTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_PrefixCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_OrderField = table.Column<int>(type: "int", nullable: false),
                    PC_Status = table.Column<bool>(type: "bit", nullable: false),
                    PC_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PC_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_Type = table.Column<int>(type: "int", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.PC_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombine",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_ProductId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_Sort = table.Column<int>(type: "int", nullable: false),
                    X_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    X_Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    X_DiscountType = table.Column<int>(type: "int", nullable: false),
                    X_Default = table.Column<bool>(type: "bit", nullable: false),
                    X_Status = table.Column<bool>(type: "bit", nullable: false),
                    X_AvailableCount = table.Column<int>(type: "int", nullable: false),
                    X_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombine", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeedbacks",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Topic = table.Column<int>(type: "int", nullable: false),
                    F_DuplicateUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    F_ProductId = table.Column<int>(type: "int", nullable: false),
                    F_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeedbacks", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLabel",
                columns: table => new
                {
                    L_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    L_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L_HexCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L_DisplayOnProduct = table.Column<bool>(type: "bit", nullable: false),
                    L_Status = table.Column<bool>(type: "bit", nullable: false),
                    L_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLabel", x => x.L_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLabelRelation",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_LabelId = table.Column<int>(type: "int", nullable: false),
                    X_Pid = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLabelRelation", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductManual",
                columns: table => new
                {
                    M_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    M_ProductId = table.Column<int>(type: "int", nullable: false),
                    M_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    M_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManual", x => x.M_Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionBoxProducts",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_SectionId = table.Column<int>(type: "int", nullable: false),
                    X_ProdId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    X_DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    X_DiscountType = table.Column<bool>(type: "bit", nullable: false),
                    X_StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    X_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    X_Status = table.Column<bool>(type: "bit", nullable: false),
                    X_ShowInIndex = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionBoxProducts", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionBoxs",
                columns: table => new
                {
                    B_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    B_EntityId = table.Column<int>(type: "int", nullable: false),
                    B_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    B_SectionId = table.Column<int>(type: "int", nullable: false),
                    B_LoadType = table.Column<int>(type: "int", nullable: false),
                    B_Type = table.Column<int>(type: "int", nullable: false),
                    B_ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionBoxs", x => x.B_Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amar_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PublicForms",
                columns: table => new
                {
                    F_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_Type = table.Column<int>(type: "int", nullable: false),
                    F_MsgType = table.Column<int>(type: "int", nullable: false),
                    F_MsgTopic = table.Column<int>(type: "int", nullable: false),
                    F_MsgSubTopic = table.Column<int>(type: "int", nullable: false),
                    F_User = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    F_UserPhone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    F_UserEmail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    F_Msg = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    F_Status = table.Column<bool>(type: "bit", nullable: false),
                    F_Response = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicForms", x => x.F_Id);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    R_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    R_Rate = table.Column<int>(type: "int", nullable: false),
                    R_EntityId = table.Column<int>(type: "int", nullable: false),
                    R_Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R_Type = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.R_Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatedProduct",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_MainProdId = table.Column<int>(type: "int", nullable: false),
                    X_ProdId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedProduct", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    S_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Sort = table.Column<int>(type: "int", nullable: false),
                    S_Status = table.Column<bool>(type: "bit", nullable: false),
                    S_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_ImageGallery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Type = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.S_Id);
                });

            migrationBuilder.CreateTable(
                name: "SetProduct",
                columns: table => new
                {
                    X_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X_MainProdId = table.Column<int>(type: "int", nullable: false),
                    X_ProdId = table.Column<int>(type: "int", nullable: false),
                    X_ColorId = table.Column<int>(type: "int", nullable: false),
                    X_WarrantyId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetProduct", x => x.X_Id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    S_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S_Titles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_OrderField = table.Column<int>(type: "int", nullable: false),
                    S_Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_AltTag = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_TooltipTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_DesktopFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_MobileFileLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    S_Status = table.Column<bool>(type: "bit", nullable: false),
                    S_ShowAnimation = table.Column<bool>(type: "bit", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.S_Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaTitles",
                columns: table => new
                {
                    SM_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SM_Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SM_Url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SM_Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaTitles", x => x.SM_Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<int>(type: "int", nullable: false),
                    amar_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    S_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_KeeperId = table.Column<int>(type: "int", nullable: false),
                    S_ProvinceId = table.Column<int>(type: "int", nullable: false),
                    S_CityId = table.Column<int>(type: "int", nullable: false),
                    S_Status = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.S_Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    T_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T_ParentId = table.Column<int>(type: "int", nullable: false),
                    T_Cid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    T_Userid = table.Column<int>(type: "int", nullable: false),
                    T_Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Status = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.T_Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    T_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_MainCatId = table.Column<int>(type: "int", nullable: false),
                    T_EndLevelCatId = table.Column<int>(type: "int", nullable: false),
                    T_Level = table.Column<int>(type: "int", nullable: false),
                    T_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Status = table.Column<bool>(type: "bit", nullable: false),
                    T_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_ImgGallery = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    T_Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    T_Summary = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    T_Cost = table.Column<double>(type: "float", nullable: false),
                    T_Discount = table.Column<double>(type: "float", nullable: false),
                    T_DiscountType = table.Column<int>(type: "int", nullable: false),
                    T_SaleCount = table.Column<int>(type: "int", nullable: false),
                    T_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.T_Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSeasons",
                columns: table => new
                {
                    S_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S_TId = table.Column<int>(type: "int", nullable: false),
                    S_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S_Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    S_Sort = table.Column<int>(type: "int", nullable: false),
                    S_VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSeasons", x => x.S_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    U_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    U_Family = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    U_Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    U_Avatar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    U_Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    U_HomeTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_IdentityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_CreditCardNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_Wallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    U_Coupon = table.Column<int>(type: "int", nullable: false),
                    U_Status = table.Column<int>(type: "int", nullable: false),
                    U_SuspendReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_Role = table.Column<int>(type: "int", nullable: false),
                    U_RoleId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.U_Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoGallery",
                columns: table => new
                {
                    VG_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VG_CatId = table.Column<int>(type: "int", nullable: false),
                    VG_Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VG_Status = table.Column<bool>(type: "bit", nullable: false),
                    VG_SortField = table.Column<int>(type: "int", nullable: false),
                    VG_UrlType = table.Column<int>(type: "int", nullable: false),
                    VG_VideoUrl = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VG_VideoImage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VG_Keywords = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VG_Seo = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    VG_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VG_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGallery", x => x.VG_Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistory",
                columns: table => new
                {
                    WH_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WH_SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WH_CustomerId = table.Column<int>(type: "int", nullable: false),
                    WH_AdderId = table.Column<int>(type: "int", nullable: false),
                    WH_TransactionType = table.Column<int>(type: "int", nullable: false),
                    WH_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WH_PayType = table.Column<int>(type: "int", nullable: false),
                    WH_PayBankRecip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistory", x => x.WH_id);
                });

            migrationBuilder.CreateTable(
                name: "Warranty",
                columns: table => new
                {
                    W_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    W_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    W_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    W_SortField = table.Column<int>(type: "int", nullable: false),
                    W_Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    W_Status = table.Column<bool>(type: "bit", nullable: false),
                    W_IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranty", x => x.W_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "AmazingRequests");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "AttributeAnswer");

            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "BetterPriceRequest");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CategoryAttribute");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Coppon");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DynamicPagesContent");

            migrationBuilder.DropTable(
                name: "EnergyUsage");

            migrationBuilder.DropTable(
                name: "ExpertReview");

            migrationBuilder.DropTable(
                name: "Faq");

            migrationBuilder.DropTable(
                name: "FaqCategories");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "FestivalOffer");

            migrationBuilder.DropTable(
                name: "Formula");

            migrationBuilder.DropTable(
                name: "GalleryCategory");

            migrationBuilder.DropTable(
                name: "GiftProduct");

            migrationBuilder.DropTable(
                name: "HeaderNotifications");

            migrationBuilder.DropTable(
                name: "ImageGallery");

            migrationBuilder.DropTable(
                name: "IndexSections");

            migrationBuilder.DropTable(
                name: "IndexSectionsInfo");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "ManufacturingCuntry");

            migrationBuilder.DropTable(
                name: "NewsLetter");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "NoticesCategory");

            migrationBuilder.DropTable(
                name: "OfflineRequest");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "PerfectProduct");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductAttributeAnswer");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductCombine");

            migrationBuilder.DropTable(
                name: "ProductFeedbacks");

            migrationBuilder.DropTable(
                name: "ProductLabel");

            migrationBuilder.DropTable(
                name: "ProductLabelRelation");

            migrationBuilder.DropTable(
                name: "ProductManual");

            migrationBuilder.DropTable(
                name: "PromotionBoxProducts");

            migrationBuilder.DropTable(
                name: "PromotionBoxs");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "PublicForms");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "RelatedProduct");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SetProduct");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "SocialMediaTitles");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "TrainingSeasons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VideoGallery");

            migrationBuilder.DropTable(
                name: "WalletHistory");

            migrationBuilder.DropTable(
                name: "Warranty");
        }
    }
}
