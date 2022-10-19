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
    public partial class Notes
    {
        [Inject]
        public INoteService NoteService { get; set; }

        public List<Note> UserNotes { get; set; } = new List<Note>();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Color { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<int> OnNoteDeleted { get; set; }

        public string CurrentId { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            UserNotes = await NoteService.GetByUser(StaticInfo.UserBasicDetails.Id);
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

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Note(string noteId)
        {
            CurrentId = noteId;
            DeleteConfirmation.Show();
        }

        protected async Task Confirm_Delete(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await NoteService.DeleteNote(StaticInfo.UserBasicDetails.Id, CurrentId);
                UserNotes = await NoteService.GetByUser(StaticInfo.UserBasicDetails.Id);
            }
        }
    }
}
