namespace Application
{
    public interface IMapper<TDto , TOutput>
    {
        public TOutput Map(TDto dto);
    }
}
