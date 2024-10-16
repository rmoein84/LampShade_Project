using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using CommentManagment.Domain.CommentAgg;
using DomainManagement.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string ConnectionString)
        {
            /* Comment Configuration */
            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            /* DbContxt Configuration */
            services.AddDbContext<CommentContext>(x => x.UseSqlServer(ConnectionString));
        }
    }
}
