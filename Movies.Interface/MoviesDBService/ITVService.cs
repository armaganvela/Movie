using Movies.Core;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interface
{
    public interface ITVService
    {
        TVResult GetTopRatedTVs(int pageNumber);

        TVResult GetPopularTVs(int pageNumber);

        TV GetTV(int id);

        Credit GetTVCredits(int id);
    }
}
