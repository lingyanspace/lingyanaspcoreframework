﻿namespace LingYan.SaaSMultiTenantDbSharding.ShardingProvider
{
    internal class ShardingRule
    {
        public Type EntityType { get; set; } 
        public ShardingType ShardingType { get; set; }
        public string ShardingField { get; set; }
        public int Mod { get; set; }
        public ExpandByDateMode? ExpandByDateMode { get; set; }
        public string GetTableSuffixByField(object fieldValue)
        {
            switch (ShardingType)
            {
                case ShardingType.HashMod:
                    long suffix;
                    if (fieldValue.GetType() == typeof(int) || fieldValue.GetType() == typeof(long))
                    {
                        long longValue = (long)fieldValue;
                        if (longValue < 0)
                            throw new Exception($"字段{ShardingField}不能小于0");

                        suffix = longValue % Mod;
                    }
                    else
                    {
                        suffix = Math.Abs(fieldValue.ToString().GetStableHashCode()) % Mod;
                    }
                    return suffix.ToString();
                case ShardingType.Date:
                    string format = ExpandByDateMode switch
                    {
                        ShardingProvider.ExpandByDateMode.PerMinute => "yyyyMMddHHmm",
                        ShardingProvider.ExpandByDateMode.PerHour => "yyyyMMddHH",
                        ShardingProvider.ExpandByDateMode.PerDay => "yyyyMMdd",
                        ShardingProvider.ExpandByDateMode.PerMonth => "yyyyMM",
                        ShardingProvider.ExpandByDateMode.PerYear => "yyyy",
                        _ => throw new Exception("ExpandByDateMode无效")
                    };

                    return ((DateTime)fieldValue).ToString(format);
                default: throw new Exception("ShardingType无效");
            }
        }
        public string GetTableSuffixByEntity(object entity)
        {
            var property = entity.GetPropertyValue(ShardingField);

            return GetTableSuffixByField(property);
        }
    }
}
