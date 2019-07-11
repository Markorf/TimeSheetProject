using System;
using System.Data;

namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public static class IDataRecordExtension
    {
        public static Guid GetSafeGuid(this IDataRecord record, int colIndex)
        {
            if (!record.IsDBNull(colIndex))
                return record.GetGuid(colIndex);
            return Guid.Empty;
        }

        public static string GetSafeString(this IDataRecord record, int colIndex)
        {
            if (!record.IsDBNull(colIndex))
                return record.GetString(colIndex);
            return null;
        }
    }
}
