using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Interfaces
{
    public interface IProductsCollectionUpdate
    {
        Action UpdateCollection { get; set; }
    }
}
