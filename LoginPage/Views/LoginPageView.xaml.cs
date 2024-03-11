using LoginPage.ViewModels;

namespace LoginPage.Views;

public partial class LoginPageView : ContentPage
{
	public LoginPageView()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageViewModel();
	}
}