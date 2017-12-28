﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Model.Repositories.CurrencyExchangeRepository;

namespace Presenter.IPresenters
{
    interface IMasterPresenter
    {
        bool IsUserLoggedIn();
        void LoadLanguagesList();
        List<Currency> GetCurrencyList();
    }
}