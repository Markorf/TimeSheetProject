using System;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public static class ObjectExtension
    {
        public static object GetDBNull(this object value)
           => value == null ? DBNull.Value : value;
    }
}
