using Microsoft.AspNetCore.Components;
using NoteCraftMAUIBlazor.Helpers;
using NoteCraftMAUIBlazor.Services;
using NoteCraftModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftMAUIBlazor.Pages
{
    public partial class Shared
    {
        [Inject]
        public INoteService NoteService { get; set; }

        public List<Note> SharedNotes { get; set; } = new List<Note>();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SharedNotes = await NoteService.GetShared(StaticInfo.UserBasicDetails.Id);
            await base.OnInitializedAsync();
        }

        protected string SetImportance(string importance)
        {
            if (importance.Equals(NoteImportance.High.ToString()))
            {
                return "background-color:red";
            }
            else if (importance.Equals(NoteImportance.Medium.ToString()))
            {
                return "background-color:yellow";
            }
            else
            {
                return "background-color:green";
            }
        }

        private void Open(string noteId)
        {
            NavigationManager.NavigateTo($"/note_details/{noteId}");
        }

        private void Update_Note(string noteId)
        {
            NavigationManager.NavigateTo($"/update_note/{noteId}");
        }
    }
}
