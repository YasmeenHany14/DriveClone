namespace DriveClone.Domain.Shared.ResourceParameters;

public abstract class BaseResourceParameters
{
    // public string? SearchQuery { get; set; }
    // filters
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 5;
    private const int MaxPageSize = 200;
    
    public virtual int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
    public string? OrderBy { get; set; }
}
