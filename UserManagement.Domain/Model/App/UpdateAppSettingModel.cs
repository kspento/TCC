using System;

namespace UserManagement.Domain.Model.App
{
    public class UpdateAppSettingModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
