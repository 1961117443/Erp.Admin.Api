using System;

namespace FreeSql
{
    public class DbLocatorAttribute : Attribute
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private readonly Type type;

        public DbLocatorAttribute(Type type)
        {
            this.type = type;
        }

        public Type GetDbLocator()
        {
            return type;
        }
    }
}
