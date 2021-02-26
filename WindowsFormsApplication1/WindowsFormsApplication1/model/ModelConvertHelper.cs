using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

namespace WindowsFormsApplication1.model
{
    /// <summary>
    /// 实体转换辅助类
    /// </summary>
    public class ModelConvertHelper<T> where T : new()
    {
        public static IList<T> ConvertToModel(DataTable tb)
        {
            //定义集合
            IList<T> ts = new List<T>();

            //获取此类型的类型
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in tb.Rows)
            {
                T t = new T();
                //获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//检查datatable是否包含此列、
                    if (tb.Columns.Contains(tempName))
                    {
                        //判断此属性是否存在setter
                        if (!pi.CanWrite)
                        {
                            continue;
                        }
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }

                }
                ts.Add(t);

            }
            return ts;
        }
    }
}
