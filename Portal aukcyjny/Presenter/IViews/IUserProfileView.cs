using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface IUserProfileView
    {
        string UsernameField { get; set; }
        string EmailField { get; set; }
        string SoldItemsNumField { get; set; }
        string RegistrationDateField { get; set; }
    }
}
