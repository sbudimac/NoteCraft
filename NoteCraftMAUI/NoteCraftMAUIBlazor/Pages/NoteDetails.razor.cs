using Microsoft.AspNetCore.Components;
using NoteCraftMAUIBlazor.Services;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Pages
{
    public partial class NoteDetails
    {
        [Parameter]
        public string NoteId { get; set; }

        [Inject]
        public INoteService NoteService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Note NoteToDisplay { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NoteToDisplay = await NoteService.GetById(NoteId);
            await base.OnInitializedAsync();
        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/notes");
        }
    }
}
