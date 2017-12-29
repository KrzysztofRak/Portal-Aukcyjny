using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class UserProfilePresenter : MasterPresenter, IUserProfilePresenter
    {
        private IUserProfileView view;

        public UserProfilePresenter(IUserProfileView view)
        {
            this.view = view;
        }

        public bool LoadUserProfile(Guid userId)
        {
            var user = usersRepo.GetUserProfile(userId);

            if (user.Username == null)
                return false;

            view.UsernameField = user.Username;
            view.EmailField = user.Email;
            view.SoldItemsNumField = user.SoldItemsNum.ToString();
            view.RegistrationDateField = user.RegistrationDate.ToShortDateString();

            return true;
        }
    }
}