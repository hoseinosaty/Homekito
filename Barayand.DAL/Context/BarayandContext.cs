 using Barayand.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Context
{
    public class BarayandContext:DbContext
    {
        public BarayandContext(DbContextOptions<BarayandContext> options = null)

        {
           
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<ProductCategoryModel> ProductCategory { get; set; }
        public DbSet<AuthenticationModel> Authentication { get; set; }
        public DbSet<ColorModel> Color { get; set; }
        public DbSet<AttributeModel> Attribute { get; set; }
        public DbSet<WarrantyModel> Warranty { get; set; }
        public DbSet<ProductLabelModel> ProductLabel { get; set; }
        public DbSet<CatAttrRelationModel> CategoryAttribute { get; set; }
        public DbSet<AttrAnswerModel> AttributeAnswer { get; set; }
        public DbSet<DynamicPagesContent> DynamicPagesContent { get; set; }
        public DbSet<GalleryCategoryModel> GalleryCategory { get; set; }
        public DbSet<ImageGalleryModel> ImageGallery { get; set; }
        public DbSet<VideoGalleryModel> VideoGallery { get; set; }
        public DbSet<NoticesCategoryModel> NoticesCategory { get; set; }
        public DbSet<NoticesModel> Notices { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<ProductLabelRelationModel> ProductLabelRelation { get; set; }
        public DbSet<ProductAttributeModel> ProductAttributeAnswer { get; set; }
        public DbSet<GiftProductModel> GiftProduct { get; set; }
        public DbSet<SetProductsModel> SetProduct { get; set; }
        public DbSet<PerfectProductModel> PerfectProduct { get; set; }
        public DbSet<RelatedProductModel> RelatedProduct { get; set; }
        public DbSet<PromotionBoxProductsModel> PromotionBoxProducts { get; set; }
        public DbSet<TrainingModel> Trainings { get; set; }
        public DbSet<TrainingSeasonsModel> TrainingSeasons { get; set; }
        public DbSet<CommentModel> Comment { get; set; }
        public DbSet<RateModel> Rate { get; set; }
        public DbSet<InvoiceModel> Invoice { get; set; }
        public DbSet<OrderModel> Order { get; set; }
        public DbSet<CopponModel> Coppon { get; set; }
        public DbSet<PublicFormsModel> PublicForms { get; set; }
        public DbSet<NewsletterModel> NewsLetter { get; set; }
        public DbSet<SocialMediaTitlesModel> SocialMediaTitles { get; set; }
        public DbSet<FavoriteModel> Favorites { get; set; }
        public DbSet<WalletHistoryModel> WalletHistory { get; set; }
        public DbSet<FormulaModel> Formula { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<IndexBoxProductRelModel> IndexSections { get; set; }
        public DbSet<IndexBoxInfoModel> IndexSectionsInfo { get; set; }
        public DbSet<SliderModel> Slider { get; set; }
        public DbSet<OfflinRequestModel> OfflineRequest { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<PermissionModel> Permissions { get; set; }
        public DbSet<OptionsModel> Options { get; set; }
        public DbSet<TicketModel> Ticket { get; set; }
        public DbSet<ServiceModel> Services { get; set; }//combined with profits(مزیت)  
        public DbSet<EnergyUsageModel> EnergyUsage { get; set; }//combined with BoxGiftWrapper(کادوپیچ)
        public DbSet<ManufacturContryModel> ManufacturingCuntry { get; set; }
        public DbSet<ProductCombineModel> ProductCombine { get; set; }//combine warranty and color (ترکیب گارانتی و رنگ - جدول قیمت محصول)
        public DbSet<ExpertReviewModel> ExpertReview { get; set; }
        public DbSet<ProductManualModel> ProductManual { get; set; }
        public DbSet<PromotionBoxModel> PromotionBoxs { get; set; }
        public DbSet<FestivalOfferModel> FestivalOffer { get; set; }
        public DbSet<HeaderNotificationModel> HeaderNotifications { get; set; }
        public DbSet<FaqCategoryModel> FaqCategories { get; set; }
        public DbSet<FaqModel> Faq { get; set; }
        public DbSet<AmazingRequestModel> AmazingRequests { get; set; }
        public DbSet<BetterPriceFoundModel> BetterPriceRequest { get; set; }
        public DbSet<ProdFeedbackModel> ProductFeedbacks { get; set; }
        public DbSet<CatalogModel> Catalog { get; set; }
        public DbSet<AddressModel> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=barayand.net;Database=barayand_valhalladb;Trusted_Connection=false;Password=jG9zm5*9;User=barayand_valhallauser;");
            //optionsBuilder.UseSqlServer("Data Source=valhallaplanet.art;Database=valhalla_db;Trusted_Connection=false;Password=4o53^nxU;User=valhalla_user;");
            //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=HomeKito;Trusted_Connection=True;");
            // optionsBuilder.UseSqlServer("Data Source=185.10.73.229;Database=homekitodb;Trusted_Connection=false;Password=HomEKitOUseR1234$;User=homekitouser;MultipleActiveResultSets=true;");
            //optionsBuilder.UseSqlServer("Data Source=homekito.barayand.net;Database=barayand_homekitodb;Trusted_Connection=false;Password=q6xB5_c1;User=barayand_homekitouser;MultipleActiveResultSets=true;");
            //optionsBuilder.UseSqlServer("Data Source=homekito.com;Database=homekito_db;Trusted_Connection=false;Password=4L82gr_j;User=homekito_user;MultipleActiveResultSets=true;");
            optionsBuilder.UseSqlServer("Data Source=.;Database=homekito_db;Trusted_Connection=true;MultipleActiveResultSets=true;");


        }


    }
}
