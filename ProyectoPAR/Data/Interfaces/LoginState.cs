using ProyectoPAR.Models;
using ProyectoParLibs.Models.Securidad;

namespace ProyectoPAR.Data.Interfaces
{
    public class LoginState
    {
        public UserData User { get; set; }

        public event Action OnChange;

        public void SetLogin(UserData user)
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
