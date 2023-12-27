using UserManagement.Data.Dto.AppSetting;
using UserManagement.Helper;

namespace UserManagement.Domain.Model.App
{
    public class AddAppSettingModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
