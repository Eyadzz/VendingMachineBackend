namespace Application.Mappings;

public abstract record BaseDto<TDto, TEntity> : IRegister where TDto : class where TEntity : class
{
    private TypeAdapterConfig Config { get; set; } = null!;

    protected virtual void AddCustomMappings() { }
    
    protected TypeAdapterSetter<TDto, TEntity> SetCustomMappings()
        => Config.ForType<TDto, TEntity>().IgnoreNullValues(true);

    protected TypeAdapterSetter<TEntity, TDto> SetCustomMappingsInverse()
        => Config.ForType<TEntity, TDto>().IgnoreNullValues(true);

    public void Register(TypeAdapterConfig config)
    {
        Config = config;
        AddCustomMappings();
    }
}