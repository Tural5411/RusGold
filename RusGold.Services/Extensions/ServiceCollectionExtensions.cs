using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RusGold.Data.Abstract.UnitOfWorks;
using RusGold.Data.Concrete.UnitOfWork;
using RusGold.Entities.Concrete;
using RusGold.Services.Abstract;
using RusGold.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RusGold.Data.Concrete.EntityFramework.Context;

namespace RusGold.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection,string connectionString)
        {
            serviceCollection.AddDbContext<RusGoldContext>(options=>options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            serviceCollection.AddIdentity<User, Role>(options => {
                //User Password Options
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                //User UserName and Email Options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<RusGoldContext>();
            serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(15);
            });
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IQuestionService, QuestionManager>();
            serviceCollection.AddScoped<ICarBrendModelService, CarBrendModelManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            serviceCollection.AddScoped<ISliderService, SliderManager>();
            serviceCollection.AddScoped<ICarService, CarManager>();
            serviceCollection.AddScoped<IRegisterService, RegisterManager>();
            serviceCollection.AddSingleton<IMailService, MailManager>();
            serviceCollection.AddScoped<IPhotoService, PhotoManager>();
            serviceCollection.AddScoped<ICreditService, CreditManager>();
            return serviceCollection;
        }
    }
}
