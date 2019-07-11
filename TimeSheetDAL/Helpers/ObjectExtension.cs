using System;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public static class ObjectExtension
    {
        public static object GetDBNull(this object val)
           => val == null ? DBNull.Value : val;
    }
}
