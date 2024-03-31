

using HandmadeByDoniApp.Web.ViewModels.Decanter;
using HandmadeByDoniApp.Web.ViewModels.Glass;

namespace HandmadeByDoniApp.Web.ViewModels.Set
{
    public class SetAddFormModel
    {
        public SetAddFormModel()
        {
            this.FormModel =new SetFormModel();
            this.Glasses= new HashSet<AllNotInSetViewModel>();
            this.Decanters = new HashSet<AllNotInSetViewModel>();
        }

        public SetFormModel FormModel { get; set; }

        public ICollection<AllNotInSetViewModel> Glasses { get; set; }

        public ICollection<AllNotInSetViewModel> Decanters{ get;set; }

    }
}
