using System;
using RestierExample.Data;
using Microsoft.Restier.EntityFrameworkCore;

namespace RestierExample.Controllers
{
    public partial class RestierExampleApi : EntityFrameworkApi<RestierExampleDbContext>
    {
        public RestierExampleApi(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
