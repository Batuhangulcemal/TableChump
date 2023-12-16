namespace AsepStudios.TableChump.API
{
    public class ActionResult<T> : ActionResult
    {
        public T Data { get; set; }
    }

    public class ActionResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
