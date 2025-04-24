using Base.Domain;

namespace WebApp.ViewModels;

public class RoomCreateEditViewModel : BaseEntity
{
    public string Name { get; set; } = default!;
}