using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
    public partial class UpdateNote
    {
        [Parameter]
        public string NoteId { get; set; }

        [Inject]
        public INoteService NoteService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Note NoteToUpdate { get; set; } = new Note();

        private string spinnerClass = "";

        protected override async Task OnInitializedAsync()
        {
            NoteToUpdate = await NoteService.GetById(NoteId);
            await base.OnInitializedAsync();
        }

        public async void Update()
        {
            spinnerClass = "spinner-border spinner-border-sm";

            try
            {
                var response = await NoteService.UpdateNote(StaticInfo.UserBasicDetails.Id, NoteId, new NoteDto(
                    NoteToUpdate.Title,
                    NoteToUpdate.Description,
                    NoteToUpdate.Content,
                    NoteToUpdate.NoteType,
                    NoteToUpdate.NoteImportance,
                    NoteToUpdate.SharedUsers,
                    NoteToUpdate.Comments,
                    NoteToUpdate.OwnerId
                    ));
                if (response != null)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Note updated successfully!", "OK");
                    NavigationManager.NavigateTo($"/note_details/{NoteId}");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid data provided, please check the form.", "OK");
                }
            }
            catch(Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again.", "OK");
                NavigationManager.NavigateTo($"/update_note/{NoteToUpdate.Id}");
            }

            spinnerClass = "";
            this.StateHasChanged();
        }

        private async Task OnPictureFileChange(InputFileChangeEventArgs e)
        {
            var format = "image/png";
            var resizedImage = await e.File.RequestImageFileAsync(format, 200, 200);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
            NoteToUpdate.Content = imageData;
        }
    }
}
