using PortalNominaLibs.Models.Securidad;

namespace PortalNomina.Data.Interfaces
{
    public class LoginState
    {
        public UserLogin User { get; set; }

        public event Action OnChange;

        public void SetLogin(UserLogin user)
        {
            User = user;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
