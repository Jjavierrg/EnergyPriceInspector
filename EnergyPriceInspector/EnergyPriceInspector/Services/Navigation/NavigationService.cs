namespace EnergyPriceInspector.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService: INavigationService
    {
        public void RegisterRouteComponent<T>()
        {
            var name = typeof(T).FullName;
            Routing.RegisterRoute(name, typeof(T));
        }
        public Task NavigateToAsync<T>() => Shell.Current.GoToAsync(typeof(T).FullName);
        public Task NavigateBackAsync<T>() => Shell.Current.GoToAsync("..");
    }
}
