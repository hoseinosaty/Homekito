using System;
using System.Collections.Generic;
using System.Linq;

using Barayand.DAL.Context;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Barayand.DAL.Interfaces;
using Barayand.DAL.Repositories;
using Barayand.Common;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Barayand.OutModels.Models;
using Barayand.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using Stripe;
using Barayand.Services.Interfaces;
using Barayand.Services.Services;
using System.Globalization;
using Barayand.Common.Services;
using System.Threading;

namespace HomeKito
{
    public class Startup
    {
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
            services.AddDetection();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(12);
            });

            services.AddControllersWithViews();
            services.AddDbContext<BarayandContext>(config => config.UseSqlServer(Configuration.GetConnectionString("BarayandDatabase"), sqlServerOptionsAction: sqlOptions => {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            })/*,ServiceLifetime.Singleton*/);
            var MapperConfiguration = new MapperConfiguration(mc => {
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProductCategoryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ColorProfile());
                mc.AddProfile(new Barayand.Common.MapperProfiles.AttributeProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.WarrantyProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.BrandProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProductLabelProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.DynamicPageContentProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.GalleryCatProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ImageGalleryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.VideoGalleryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.NoticesCategoryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.NoticesProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProductProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.TrainingProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.UserProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.CopponProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.PublicFormProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.NewsletterProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.FormulaProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.MCountryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.EnergyGiftWrapProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProductCombineProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ExpertReviewProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProductManualProfile());
                mc.AddProfile(new Barayand.Common.MapperProfiles.PromotionBoxProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.PromotionBoxProductProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.HeaderNotificationProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.FaqCategoryProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.FaqProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.AmazingRequestProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.BetterPriceProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.ProdFeedbackProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.CatalogProfiler());
                mc.AddProfile(new Barayand.Common.MapperProfiles.FilterPriceProdiler());
            }); IMapper mapper = MapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));

            #region Add Services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAttributeAnswerRepository), typeof(AttributeAnswerRepository));
            services.AddScoped(typeof(IPCRepository), typeof(PCRepository));
            services.AddScoped(typeof(IPromotionRepository), typeof(PromotionRepository));
            services.AddScoped(typeof(IPRRepository), typeof(RelationProductRepository));
            services.AddScoped(typeof(IPerfectProductRepository), typeof(PerfectProductRepository));
            services.AddScoped(typeof(IReportService), typeof(ReportService));
            services.AddScoped(typeof(ISetProductRepository), typeof(SetProductRepository));
            services.AddScoped(typeof(IGiftProductRepository), typeof(GiftProductRepository));
            services.AddScoped(typeof(ILocalizationService), typeof(LocalizationService));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IFavoriteRepository), typeof(FavoriteRepository));
            services.AddScoped(typeof(IWalletHistoryRepository), typeof(WalletHistoryRepository));

            services.AddScoped(typeof(IFormulaRepository), typeof(FormulaRepository));
            services.AddScoped(typeof(IRateRepository), typeof(RateRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
            services.AddScoped(typeof(IFileAccessService), typeof(Barayand.Services.Services.FileAccessSerivce));
            services.AddScoped(typeof(ISmsService), typeof(Barayand.Services.Services.SmsService));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductCombineModel>), typeof(ProductCombineRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<GalleryCategoryModel>), typeof(GalleryCatRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductCategoryModel>), typeof(PCRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<WarrantyModel>), typeof(WarrantyRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductLabelModel>), typeof(ProductLabelRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ColorModel>), typeof(ColorRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CatAttrRelationModel>), typeof(CatAttrRelationRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<BrandModel>), typeof(BrandRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AttributeModel>), typeof(AttributeRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AttrAnswerModel>), typeof(AttributeAnswer));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ImageGalleryModel>), typeof(ImageGalleryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<VideoGalleryModel>), typeof(VideoGalleryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<NoticesCategoryModel>), typeof(NoticesCatRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<NoticesModel>), typeof(NoticesRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProductModel>), typeof(ProductRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<TrainingModel>), typeof(TrainingRepository));
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
            services.AddScoped(typeof(IPublicMethodRepsoitory<ServiceModel>), typeof(ServiceProfitsRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ManufacturContryModel>), typeof(ManufacturCuntryRepsitory));
            services.AddScoped(typeof(IPublicMethodRepsoitory<EnergyUsageModel>), typeof(EnergyGiftWrapRepository));
            services.AddScoped(typeof(IExpertReviewRepository), typeof(ExpertReviewRespository));
            services.AddScoped(typeof(IProductManualRepository), typeof(ProductManualRepository));
            services.AddScoped(typeof(IPromotionBoxProdRepository), typeof(PromotionBoxProdRepository));
            services.AddScoped(typeof(IPCalcRepository), typeof(PCalcRepository));
            services.AddScoped(typeof(IFestivalRepository), typeof(FestivalRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<HeaderNotificationModel>), typeof(HeaderNotifiRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<FaqCategoryModel>), typeof(FaqCategoryRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<FaqModel>), typeof(FaqRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<AmazingRequestModel>), typeof(AmazingRequestRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<BetterPriceFoundModel>), typeof(BetterPriceRepository));
            services.AddScoped(typeof(IPublicMethodRepsoitory<CatalogModel>), typeof(CatalogRepository));
            services.AddScoped(typeof(IAddressRepository), typeof(AddressRepository));
            services.AddScoped(typeof(IBasketService), typeof(Barayand.Services.Services.BasketService));
            services.AddScoped(typeof(IPublicMethodRepsoitory<ProdFeedbackModel>), typeof(ProdFeedbackRepository));
            services.AddScoped(typeof(IProductManualRepository), typeof(ProductManualRepository));
          
            services.AddScoped(typeof(IProductAttrAnswerRepository), typeof(ProductAttrAnswerRepository));
            //services.AddScoped(typeof(IPaymentService), typeof(BpmPaymentService));
            services.AddScoped(typeof(IPublicMethodRepsoitory<LogModel>), typeof(LogRepository));
            services.AddScoped(typeof(ILogService), typeof(LogService));

            #endregion
            services.AddSingleton<IViewRenderer, ViewRenderer>();
            services.AddScoped(typeof(IPublicMethodRepsoitory<TicketModel>), typeof(TicketRepository));
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization().AddNewtonsoftJson();


            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
               // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            var cultures = new List<CultureInfo> {
                new CultureInfo("en"),
                new CultureInfo("fa")
            };
            app.UseCors("CorsPolicy");
            app.UseRequestLocalization(options => {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(Configuration["Language"]);
                options.SupportedCultures = cultures;
                options.RequestCultureProviders.Clear();
                options.SupportedUICultures = cultures;
            });


            app.UseDetection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Configuration["Language"]);
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
