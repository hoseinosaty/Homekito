
using Barayand.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using Barayand.DAL.Interfaces;
using Barayand.DAL.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Barayand.OutModels.Models;
using Barayand.Models.Entity;
using Barayand.Services.Interfaces;
using Barayand.Services.Services;

namespace Barayand
{
    public class Startup
    {
        readonly string AllowSites = "http://localhost:8080";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
              
                options.AddPolicy("CorsPolicy",
                 builder => builder
                     .AllowAnyMethod()
                     .AllowCredentials()
                     .SetIsOriginAllowed((host) => true)
                     .AllowAnyHeader());
            });
           
            services.AddDbContext<BarayandContext>( config => config.UseSqlServer(Configuration.GetConnectionString("BarayandDatabase"),sqlServerOptionsAction:sqlOptions=> {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount:10,
                    maxRetryDelay:TimeSpan.FromSeconds(30),
                    errorNumbersToAdd:null);
            })/*,ServiceLifetime.Singleton*/);
            var MapperConfiguration = new MapperConfiguration(mc=> {
                mc.AddProfile(new Common.MapperProfiles.ProductCategoryProfiler());
                mc.AddProfile(new Common.MapperProfiles.ColorProfile());
                mc.AddProfile(new Common.MapperProfiles.AttributeProfiler());
                mc.AddProfile(new Common.MapperProfiles.WarrantyProfiler());
                mc.AddProfile(new Common.MapperProfiles.BrandProfiler());
                mc.AddProfile(new Common.MapperProfiles.ProductLabelProfiler());
                mc.AddProfile(new Common.MapperProfiles.DynamicPageContentProfiler());
                mc.AddProfile(new Common.MapperProfiles.GalleryCatProfiler());
                mc.AddProfile(new Common.MapperProfiles.ImageGalleryProfiler());
                mc.AddProfile(new Common.MapperProfiles.VideoGalleryProfiler());
                mc.AddProfile(new Common.MapperProfiles.NoticesCategoryProfiler());
                mc.AddProfile(new Common.MapperProfiles.NoticesProfiler());
                mc.AddProfile(new Common.MapperProfiles.ProductProfiler());
                mc.AddProfile(new Common.MapperProfiles.TrainingProfiler());
                mc.AddProfile(new Common.MapperProfiles.UserProfiler());
                mc.AddProfile(new Common.MapperProfiles.CopponProfiler());
                mc.AddProfile(new Common.MapperProfiles.PublicFormProfiler());
                mc.AddProfile(new Common.MapperProfiles.NewsletterProfiler());
                mc.AddProfile(new Common.MapperProfiles.FormulaProfiler());
                mc.AddProfile(new Common.MapperProfiles.MCountryProfiler());
                mc.AddProfile(new Common.MapperProfiles.EnergyGiftWrapProfiler());
                mc.AddProfile(new Common.MapperProfiles.ProductCombineProfiler());
                mc.AddProfile(new Common.MapperProfiles.ExpertReviewProfiler());
                mc.AddProfile(new Common.MapperProfiles.ProductManualProfile());
                mc.AddProfile(new Common.MapperProfiles.PromotionBoxProfiler());
                mc.AddProfile(new Common.MapperProfiles.PromotionBoxProductProfiler());
                mc.AddProfile(new Common.MapperProfiles.FestivalProfiler());
                mc.AddProfile(new Common.MapperProfiles.HeaderNotificationProfiler());
                mc.AddProfile(new Common.MapperProfiles.FaqCategoryProfiler());
                mc.AddProfile(new Common.MapperProfiles.FaqProfiler());
                mc.AddProfile(new Common.MapperProfiles.AmazingRequestProfiler());
                mc.AddProfile(new Common.MapperProfiles.BetterPriceProfiler());
                mc.AddProfile(new Common.MapperProfiles.ProdFeedbackProfiler());
                mc.AddProfile(new Common.MapperProfiles.CatalogProfiler());
            });
            IMapper mapper = MapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));
            #region Add Services
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IAttributeAnswerRepository),typeof(AttributeAnswerRepository));
            
            services.AddScoped(typeof(IAddressRepository),typeof(AddressRepository));
            services.AddScoped(typeof(IPCRepository),typeof(PCRepository));
            services.AddScoped(typeof(IPRRepository),typeof(RelationProductRepository));
            services.AddScoped(typeof(IPerfectProductRepository),typeof(PerfectProductRepository));
            services.AddScoped(typeof(ISetProductRepository),typeof(SetProductRepository));
            services.AddScoped(typeof(IGiftProductRepository),typeof(GiftProductRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IFormulaRepository), typeof(FormulaRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IReportService), typeof(ReportService));
            services.AddScoped(typeof(IPromotionRepository), typeof(PromotionRepository));
            services.AddScoped(typeof(IFestivalRepository), typeof(FestivalRepository));
            services.AddScoped(typeof(IFileAccessService), typeof(Barayand.Services.Services.FileAccessSerivce));
            services.AddScoped(typeof(IWalletHistoryRepository), typeof(WalletHistoryRepository));
            services.AddScoped(typeof(ISmsService), typeof(Barayand.Services.Services.SmsService));
            services.AddScoped(typeof(IPublicMethodRepsoitory<GalleryCategoryModel>),typeof(GalleryCatRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductCategoryModel>),typeof(PCRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<WarrantyModel>),typeof(WarrantyRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductLabelModel>),typeof(ProductLabelRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ColorModel>),typeof(ColorRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CatAttrRelationModel>),typeof(CatAttrRelationRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<BrandModel>),typeof(BrandRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AttributeModel>),typeof(AttributeRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AttrAnswerModel>),typeof(AttributeAnswer));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ImageGalleryModel>),typeof(ImageGalleryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<VideoGalleryModel>),typeof(VideoGalleryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<NoticesCategoryModel>),typeof(NoticesCatRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<NoticesModel>),typeof(NoticesRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductModel>),typeof(ProductRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<TrainingModel>),typeof(TrainingRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<UserModel>), typeof(UserRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CopponModel>), typeof(CopponRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<DynamicPagesContent>), typeof(DynamicPagesRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<PublicFormsModel>), typeof(PublicFormRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<NewsletterModel>), typeof(NewsLetterRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<SocialMediaTitlesModel>), typeof(SocialMediaTitleRepository));

            services.AddScoped(typeof(IPublicMethodRepsoitory<InvoiceModel>), typeof(InvoiceRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<OrderModel>), typeof(OrderRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<RateModel>), typeof(RateRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CommentModel>), typeof(CommentRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<FormulaModel>), typeof(FormulaRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<DepartmentModel>), typeof(DepartmentRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<IndexBoxProductRelModel>), typeof(IndexSectionRepo));
            services.AddScoped(typeof(IPublicMethodRepsoitory<IndexBoxInfoModel>), typeof(IndexBoxInfoRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<SliderModel>), typeof(SliderRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<OfflinRequestModel>), typeof(OfflineRequestRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<Province>), typeof(ProvinceRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<States>), typeof(StateRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<OptionsModel>), typeof(OptionRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<TicketModel>), typeof(TicketRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ManufacturContryModel>), typeof(ManufacturCuntryRepsitory));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ServiceModel>), typeof(ServiceProfitsRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<EnergyUsageModel>), typeof(EnergyGiftWrapRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductCombineModel>), typeof(ProductCombineRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<HeaderNotificationModel>), typeof(HeaderNotifiRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<FaqCategoryModel>), typeof(FaqCategoryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AmazingRequestModel>), typeof(AmazingRequestRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<BetterPriceFoundModel>), typeof(BetterPriceRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProdFeedbackModel>), typeof(ProdFeedbackRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CatalogModel>), typeof(CatalogRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<FaqModel>), typeof(FaqRepository));
            services.AddScoped(typeof(IProductManualRepository), typeof(ProductManualRepository));
            services.AddScoped(typeof(IExpertReviewRepository), typeof(ExpertReviewRespository));
            services.AddScoped(typeof(IPromotionBoxProdRepository), typeof(PromotionBoxProdRepository));
            services.AddScoped(typeof(IPCalcRepository), typeof(PCalcRepository));
            services.AddScoped(typeof(IProductAttrAnswerRepository), typeof(ProductAttrAnswerRepository));
            #endregion
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
