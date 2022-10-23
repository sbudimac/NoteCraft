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
    public partial class NoteDetails
    {
        [Parameter]
        public string NoteId { get; set; }

        [Inject]
        public INoteService NoteService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Note NoteToDisplay { get; set; }

        public Comment NewComment { get; set; } = new Comment();

        private string spinnerClass = "";

        protected override async Task OnInitializedAsync()
        {
            NoteToDisplay = await NoteService.GetById(NoteId);
            await base.OnInitializedAsync();
        }

        protected void Back()
        {
            NavigationManager.NavigateTo("/notes");
        }

        protected async void Comment()
        {
            if (NewComment.Content.Length <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again.", "OK");
                return;
            }

            spinnerClass = "spinner-border spinner-border-sm";

            NewComment.Author = StaticInfo.UserBasicDetails.Username;
            NoteToDisplay.Comments.Add(NewComment);

            try
            {
                var response = await NoteService.UpdateNote(StaticInfo.UserBasicDetails.Id, NoteId, new NoteDto(
                    NoteToDisplay.Title,
                    NoteToDisplay.Description,
                    NoteToDisplay.Content,
                    NoteToDisplay.NoteType,
                    NoteToDisplay.NoteImportance,
                    NoteToDisplay.SharedUsers,
                    NoteToDisplay.Comments,
                    NoteToDisplay.OwnerId
                    ));
                NoteToDisplay = response;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again.", "OK");
                NavigationManager.NavigateTo($"/update_note/{NoteToDisplay.Id}");
            }

            spinnerClass = "";
            this.StateHasChanged();
        }
    }
}
