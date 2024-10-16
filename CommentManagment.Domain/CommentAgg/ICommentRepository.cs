using _0_Framework.Domain;
using CommentManagement.Application.Contract.Comment;

namespace CommentManagment.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
