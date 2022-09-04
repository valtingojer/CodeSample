using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EnumCustomAttribute.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["Pending"] = EnumHelper.GetEnumInfo(PaymentStatus.Pending);
            ViewData["Completed"] = EnumHelper.GetEnumInfo(PaymentStatus.Completed);
            ViewData["Failed"] = EnumHelper.GetEnumInfo(PaymentStatus.Failed);
        }
    }
}

public class InfoAttribute : Attribute
{
    public string Info { get; set; }
    public InfoAttribute(string info)
    {
        Info = info;
    }
}

public enum PaymentStatus : int
{
    [Info("Payment is pending")]
    Pending = 1,
    [Info("Payment is completed")]
    Completed = 2,
    [Info("Payment is failed")]
    Failed = 3
}

public static class EnumHelper
{
    public static string GetEnumInfo(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var enumField = enumType.GetField(enumValue.ToString());
        var enumInfo = (InfoAttribute[])enumField.GetCustomAttributes(typeof(InfoAttribute), false);
        var result = enumInfo.Length > 0 ? enumInfo[0].Info : string.Empty;
        return result;
    }
}
