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
    public partial class CreateNote
    {
        [Inject]
        public INoteService NoteService { get; set; }

        public NoteDto NoteToCreate { get; set; } = new NoteDto();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private string spinnerClass = "";

        protected override void OnInitialized()
        {
            NoteToCreate.OwnerId = StaticInfo.UserBasicDetails.Id;
            base.OnInitialized();
        }

        public async void Create()
        {
            spinnerClass = "spinner-border spinner-border-sm";

            try
            {
                var response = await NoteService.CreateNote(NoteToCreate);
                if (response != null)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Note created successfully!", "OK");
                    NavigationManager.NavigateTo("/notes");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid data provided, please check the form.", "OK");
                }
            }
            catch(Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again.", "OK");
                NavigationManager.NavigateTo("/create_note");
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
            NoteToCreate.Content = imageData;
        }
    }
}
