using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace Base.BLL;

public class BaseBLL<TUOW> : IBaseBLL 
where TUOW : IBaseUOW
{
    protected readonly TUOW BLLUOW;

    public BaseBLL(TUOW uow)
    {
        BLLUOW = uow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await BLLUOW.SaveChangesAsync();
    }
}