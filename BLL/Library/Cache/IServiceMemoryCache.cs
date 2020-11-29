using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Library.Cache
{
    public interface IServiceMemoryCache<TItem>
    {
        Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem);
    }
}
