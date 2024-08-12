﻿namespace LingYan.SaaSMultiTenantDbSharding.DynamicDbContext
{
    public enum DatabaseType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        SqlServer,

        /// <summary>
        /// MySql数据库 
        /// </summary>
        MySql,

        /// <summary>
        /// Oracle数据库
        /// </summary>
        Oracle,

        /// <summary>
        /// PostgreSql数据库
        /// </summary>
        PostgreSql,

        /// <summary>
        /// SQLite数据库
        /// </summary>
        SQLite 
    }
}
