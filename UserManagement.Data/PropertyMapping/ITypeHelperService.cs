namespace UserManagement.Data.PropertyMapping
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
