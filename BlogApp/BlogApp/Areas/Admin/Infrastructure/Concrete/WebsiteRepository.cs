using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;

namespace BlogApp.Areas.Admin.Infrastructure.Concrete
{
    public class WebsiteRepository : GenericRepository<Website>, IDisposable
    {
    }
}