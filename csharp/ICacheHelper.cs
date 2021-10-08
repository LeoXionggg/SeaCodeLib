using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaCodeLib.Common
{
    public interface ICacheHelper
    {
        T Get<T>(string key);

        void Set<T>(string key, T t);

        void Set<T>(string key, T t, double timeoutseconds);

        void ClearALL();

        void Clear(string key);

    }
}
