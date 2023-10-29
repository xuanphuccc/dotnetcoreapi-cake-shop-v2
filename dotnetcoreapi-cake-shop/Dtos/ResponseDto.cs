namespace dotnetcoreapi_cake_shop.Dtos
{
    public class ResponseDto
    {
        private int status = 200;
        private int totalPage = 1;

        public string? Title { get; set; }
        public int Status { get => status; set => status = value; }
        public dynamic? Data { get; set; }
        public int TotalPage { get => totalPage; set => totalPage = value; }
    }
}
