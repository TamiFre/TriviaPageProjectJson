using LoginPage.ViewModels;

namespace LoginPage.Views;

public partial class UserQuestionsPageView : ContentPage
{
	public UserQuestionsPageView( UserQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}