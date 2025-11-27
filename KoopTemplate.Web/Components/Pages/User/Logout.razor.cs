namespace KoopTemplate.Web.Components.Pages.User;

public partial class Logout
{
    protected override async Task OnInitializedAsync()
    {
        await Http.PostAsync("/auth/logout", content: null);
        Nav.NavigateTo("/login", forceLoad: true);
    }
}