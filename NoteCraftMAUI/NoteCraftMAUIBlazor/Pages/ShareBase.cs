using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Pages
{
    public class ShareBase : ComponentBase
    {
        public bool ShowSharing { get; set; }

        [Parameter]
        public string Username { get; set; }

        [Parameter]
        public string SharingTitle { get; set; } = "Share Note";

        public void Show()
        {
            ShowSharing = true;
            StateHasChanged();
        }

        public void Hide()
        {
            ShowSharing = false;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<string> SharingChanged { get; set; }

        //[Parameter]
        //public Action<bool, string> Confirm_Share { get; set; }

        protected async Task OnSharingChange(string username)
        {
            ShowSharing = false;
            await SharingChanged.InvokeAsync(username);
        }
    }
}
