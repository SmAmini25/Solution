using WebApp.Infrastructure.Comon;
using NLog.Web;
using NLog;
using System.Globalization;


namespace WebApp.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddRazorPages();

        //builder.Logging.ClearProviders();//.AddEventLog();



      builder.Services.AddScoped<IApiClient, ApiClient>();


      #region MyRegion
      // تعریف پیکربندی NLog
      var config = new NLog.Config.LoggingConfiguration();


      // تبدیل تاریخ میلادی به شمسی
      //var persianCalendar = new PersianCalendar();
      //var now = DateTime.Now;
      //string persianYear = persianCalendar.GetYear(now).ToString();
      //string persianMonth = persianCalendar.GetMonth(now).ToString("00");
      //string persianDay = persianCalendar.GetDayOfMonth(now).ToString("00");

      //// مقداردهی به Event Context
      //MappedDiagnosticsLogicalContext.Set("PersianYear", persianYear);
      //MappedDiagnosticsLogicalContext.Set("PersianMonth", persianMonth);
      //MappedDiagnosticsLogicalContext.Set("PersianDay", persianDay);

      // تنظیم مسیر فایل لاگ
      //var logfile = new NLog.Targets.FileTarget("logfile")
      //{
      //  //FileName = "${basedir}/logs/${PersianYear}/${event-context:PersianMonth}/${event-context:PersianYear}-${event-context:PersianMonth}-${event-context:PersianDay}.log",
      //  FileName = "${basedir}/logs/PersianYear${event-context:PersianYear}/TextLog.log",
      //  //Layout = "${longdate} ${uppercase:${level}} ${message} >>>>>"
      //};

      // تعریف قوانین لاگ‌نویسی
      // config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logfile);

      builder.Logging.AddNLog(config);
      // اعمال تنظیمات
      //NLog.LogManager.Configuration = config;

      // نوشتن لاگ
      //var logger = NLog.LogManager.GetCurrentClassLogger();
      //logger.Info("این یک پیام تست است.");
      #endregion


      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }


     // app.UseNLog();
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapRazorPages();

      app.Run();
    }
  }
}
