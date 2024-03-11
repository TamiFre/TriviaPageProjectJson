using LoginPage.ViewModels;
namespace LoginPage.Views;

public partial class ApproveQuestionsPageView : ContentPage
{
	public ApproveQuestionsPageView(ApproveQuestionsPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
		
    }
}