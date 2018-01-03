using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Repositories.CurrencyExchangeRepository;

namespace Presenter.IPresenters
{
    interface IMasterPresenter
    {
        bool IsUserLoggedIn();
        void SetCurrenciesListSource();
    }
}