namespace _01_LampShade.Query.Contracts.Comment
{
    public class CommentQueryModel
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
    }
}
