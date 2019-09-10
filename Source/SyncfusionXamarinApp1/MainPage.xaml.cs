#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Backdrop;
using Xamarin.Forms;
using Syncfusion.Buttons.XForms;
using Syncfusion.Core.XForms;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.PopupLayout;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace SyncfusionXamarinApp1
{
    public partial class MainPage : SfBackdropPage
    {
        bool EncSelect;
        bool DecSelect;
        bool RealSelect;
        bool FileSelect;
        Encryption MyCode = new Encryption();
        SfPopupLayout MissingKeyPop;
        DataTemplate MissingKeyData;
        Label MissingKeyMess;

        SfPopupLayout FilePop;
        DataTemplate FilePopData;
        Label FilePopMess;

        FileData fileData;
        public string FileEncInName;
        string FileEncInPath;
        string FileEncInCont;

        string FileDecInName;
        string FileDecInPath;
        string FileDecInCont;

        string OutPath;
        StringBuilder OutCont;



        private void SetPage()
        {
            if (EncSelect == true && RealSelect == true)
            {
                this.Title = "Real-time Encryption";
                TabTab.SelectedIndex = 0;
                return;
            }

            if (EncSelect == true && FileSelect == true)
            {
                this.Title = "File Encryption";
                TabTab.SelectedIndex = 2;
                return;
            }

            if (DecSelect == true && RealSelect == true)
            {
                this.Title = "Real-time Decryption";
                TabTab.SelectedIndex = 1;
                return;
            }

            if (DecSelect == true && FileSelect == true)
            {
                this.Title = "File Decryption";
                TabTab.SelectedIndex = 3;
                return;
            }
        }

        void ClickFunc()
        {
            fileEncIn.GestureRecognizers.Add(new TapGestureRecognizer()
            {

                Command = new Command(() =>
                {
                    EncStartSelect();
                })
            });

            FileDecIn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DecStartSelect();
                })


            });
        }


        public MainPage()
        {
            InitializeComponent();
            ClickFunc();

            var assembly = typeof(MainPage);
            FileEncIm.Source = ImageSource.FromResource("SyncfusionXamarinApp1.Assets.encryption.png", assembly);
            FileDecIm.Source = ImageSource.FromResource("SyncfusionXamarinApp1.Assets.decryption.png", assembly);



            MissingKeyPop = new SfPopupLayout();
            MissingKeyData = new DataTemplate(() =>
            {
                MissingKeyMess = new Label();
                MissingKeyMess.Text = "Missing Key.\nPlease enter your key to decrypt";
                MissingKeyMess.HorizontalTextAlignment = TextAlignment.Center;
                MissingKeyMess.VerticalTextAlignment = TextAlignment.Center;
                MissingKeyMess.BackgroundColor = Color.FromHex("#363636");
                MissingKeyMess.TextColor = Color.White;
                MissingKeyMess.FontSize = 16;
                return MissingKeyMess;
            });
            MissingKeyPop.PopupView.ContentTemplate = MissingKeyData;
            MissingKeyPop.PopupView.HeaderTitle = "Missing Key!";

            MissingKeyPop.PopupView.PopupStyle.CornerRadius = 10;

            MissingKeyPop.PopupView.PopupStyle.HeaderBackgroundColor = Color.FromHex("#363636");
            MissingKeyPop.PopupView.PopupStyle.BorderThickness = 0;

            MissingKeyPop.PopupView.PopupStyle.HeaderFontSize = 20;
            MissingKeyPop.PopupView.PopupStyle.HeaderTextColor = Color.White;
            MissingKeyPop.PopupView.PopupStyle.HeaderFontAttribute = FontAttributes.Bold;

            MissingKeyPop.PopupView.PopupStyle.FooterBackgroundColor = Color.FromHex("#363636");
            MissingKeyPop.PopupView.PopupStyle.AcceptButtonBackgroundColor = Color.FromHex("#363636");
            MissingKeyPop.PopupView.PopupStyle.AcceptButtonTextColor = Color.White;

            MissingKeyPop.PopupView.AcceptButtonText = "Close";
            MissingKeyPop.PopupView.ShowCloseButton = false;




            FilePop = new SfPopupLayout();
            FilePopData = new DataTemplate(() =>
            {
                FilePopMess = new Label();
                FilePopMess.Text = "File Created.\nWould you like to view the file?";
                FilePopMess.HorizontalTextAlignment = TextAlignment.Center;
                FilePopMess.VerticalTextAlignment = TextAlignment.Center;
                FilePopMess.BackgroundColor = Color.FromHex("#363636");
                FilePopMess.TextColor = Color.White;
                FilePopMess.FontSize = 16;
                return FilePopMess;
            });
            FilePop.PopupView.AppearanceMode = AppearanceMode.TwoButton;
            FilePop.PopupView.ContentTemplate = FilePopData;
            FilePop.PopupView.HeaderTitle = "Process Done";

            FilePop.PopupView.PopupStyle.CornerRadius = 10;

            FilePop.PopupView.PopupStyle.HeaderBackgroundColor = Color.FromHex("#363636");
            FilePop.PopupView.PopupStyle.BorderThickness = 0;
            
            FilePop.PopupView.PopupStyle.HeaderFontSize = 20;
            FilePop.PopupView.PopupStyle.HeaderTextColor = Color.White;
            FilePop.PopupView.PopupStyle.HeaderFontAttribute = FontAttributes.Bold;

            FilePop.PopupView.PopupStyle.FooterBackgroundColor = Color.FromHex("#363636");
            FilePop.PopupView.PopupStyle.AcceptButtonBackgroundColor = Color.FromHex("#363636");
            FilePop.PopupView.PopupStyle.AcceptButtonTextColor = Color.White;
            

            FilePop.PopupView.PopupStyle.DeclineButtonBackgroundColor = Color.FromHex("#363636");
            FilePop.PopupView.PopupStyle.DeclineButtonTextColor = Color.White;

            FilePop.PopupView.AcceptButtonText = "View";
            FilePop.PopupView.AcceptCommand = new Command(() => { VFile(); });
            FilePop.PopupView.ShowCloseButton = false;
            FilePop.PopupView.DeclineCommand = new Command(() => { FilePop.Dismiss(); });
            FilePop.PopupView.DeclineButtonText = "Close";



            EncSelect = true;
            RealSelect = true;
            RealEnc_Key.Text = MyCode.NewKey();
            SetPage();





        }

        private void DecChip_Clicked(object sender, EventArgs e)
        {
            if (DecSelect == true)
            {
                return;
            }

            else
            {
                DecSelect = true;
                EncSelect = false;
                DecChip.BorderColor = Color.White;
                EncChip.BorderColor = Color.FromHex("#292929");
                SetPage();
                return;
            }

        }

        private void EncChip_Clicked(object sender, EventArgs e)
        {
            if (EncSelect == true)
            {
                return;
            }
            else
            {
                EncSelect = true;
                DecSelect = false;
                EncChip.BorderColor = Color.White;
                DecChip.BorderColor = Color.FromHex("#292929");
                SetPage();
                return;
            }
        }

        private void Real_chip_Clicked(object sender, EventArgs e)
        {
            if (RealSelect == true)
            {
                return;
            }

            else
            {
                RealSelect = true;
                FileSelect = false;
                Real_chip.BorderColor = Color.White;
                file_chip.BorderColor = Color.FromHex("#292929");
                SetPage();
                return;
            }

        }

        private void File_chip_Clicked(object sender, EventArgs e)
        {
            if (FileSelect == true)
            {
                return;
            }

            else
            {
                FileSelect = true;
                RealSelect = false;
                file_chip.BorderColor = Color.White;
                Real_chip.BorderColor = Color.FromHex("#292929");
                SetPage();
                return;
            }
        }

        private void RealEnc_Star_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RealEnc_Key.Text == null || RealEnc_Key.Text == "")
            {
                return;
            }

            else
            {
                string oldtext = RealEnc_Star.Text;
                string Key = RealEnc_Key.Text;
                string newText = MyCode.Encrypt(oldtext, Key);
                RealEnc_Fin.Text = newText;
                return;
            }
        }

        private void RealEnc_Rand_Clicked(object sender, EventArgs e)
        {
            RealEnc_Key.Text = MyCode.NewKey();
            return;
        }

        private void RealDec_Star_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (RealDec_Key.Text == null || RealDec_Key.Text == "")
            {
                MissingKeyPop.Show();
                return;
            }

            else
            {
                string oldtext = RealDec_Star.Text;
                string Key = RealDec_Key.Text;
                string newstr = MyCode.Decrypt(oldtext, Key);
                RealDec_Fin.Text = newstr;
                return;
            }
        }
        

        private void TabTab_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            if (TabTab.SelectedIndex == 0)
            {
                if (EncSelect == true && RealSelect == true)
                {
                    return;
                }
                else
                {
                    EncSelect = true;
                    RealSelect = true;
                    FileSelect = false;
                    DecSelect = false;
                    EncChip.BorderColor = Color.White;
                    Real_chip.BorderColor = Color.White;
                    DecChip.BorderColor = Color.FromHex("#292929");
                    file_chip.BorderColor = Color.FromHex("#292929");
                    this.Title = "Real-time Encryption";
                    return;
                }
            }

            if (TabTab.SelectedIndex == 2)
            {
                if (EncSelect == true && FileSelect == true)
                {
                    return;
                }
                else
                {
                    EncSelect = true;
                    FileSelect = true;
                    RealSelect = false;
                    DecSelect = false;
                    EncChip.BorderColor = Color.White;
                    file_chip.BorderColor = Color.White;
                    DecChip.BorderColor = Color.FromHex("#292929");
                    Real_chip.BorderColor = Color.FromHex("#292929");
                    this.Title = "File Encryption";
                    return;
                }
            }

            if (TabTab.SelectedIndex == 1)
            {
                if (DecSelect == true && RealSelect == true)
                {
                    return;
                }
                else
                {
                    DecSelect = true;
                    RealSelect = true;
                    FileSelect = false;
                    EncSelect = false;
                    DecChip.BorderColor = Color.White;
                    Real_chip.BorderColor = Color.White;
                    EncChip.BorderColor = Color.FromHex("#292929");
                    file_chip.BorderColor = Color.FromHex("#292929");
                    this.Title = "Real-time Decryption";
                    return;
                }
            }

            if (TabTab.SelectedIndex == 3)
            {
                if (DecSelect == true && FileSelect == true)
                {
                    return;
                }
                else
                {
                    EncSelect = false;
                    RealSelect = false;
                    FileSelect = true;
                    DecSelect = true;
                    DecChip.BorderColor = Color.White;
                    file_chip.BorderColor = Color.White;
                    EncChip.BorderColor = Color.FromHex("#292929");
                    Real_chip.BorderColor = Color.FromHex("#292929");
                    this.Title = "File Decryption";
                    return;
                }
            }
        }

        

        private async void EncStartSelect()
        {
            fileEncIn.TextColor = Color.FromHex("#B5B5B5");
            try
            {
                fileData = await CrossFilePicker.Current.PickFile(new string[] { "text/plain" });
                if (fileData == null)
                    return; // user canceled file picking

                

            }
            catch (Exception ex)
            {
                return;
            }

            if (fileData.FileName.Substring(fileData.FileName.Length - 3) != "txt")
            {
                fileEncIn.TextColor = Color.Red;
                fileEncIn.Text = "Please choose a .txt file";
                return;
            }

            FileEncInPath = fileData.FilePath;
            FileEncInName = fileData.FileName;
            FileEncInCont = System.Text.Encoding.UTF8.GetString(fileData.DataArray);
            fileEncIn.Text = FileEncInName;


        }

        private async void DecStartSelect()
        {
            FileDecIn.TextColor = Color.FromHex("#B5B5B5");
            try
            {
                fileData = await CrossFilePicker.Current.PickFile(new string[] { "text/plain" });
                if (fileData == null)
                    return; // user canceled file picking



            }
            catch (Exception ex)
            {
                return;
            }

            if (fileData.FileName.Substring(fileData.FileName.Length - 3) != "txt")
            {
                FileDecIn.TextColor = Color.Red;
                FileDecIn.Text = "Please choose a .txt file";
                return;
            }

            FileDecInPath = fileData.FilePath;
            FileDecInName = fileData.FileName;
            FileDecInCont = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

            FileDecIn.Text = FileDecInName;
        }


        private void FileEnc_Clicked(object sender, EventArgs e)
        {
            if (FileEncInPath == null)
            {
                fileEncIn.TextColor = Color.Red;
                return;
            }

            else
            {
                //Gets a new key
                string newKey = MyCode.NewKey();
                //Initializes the variable to store the content of the file
                OutCont = new StringBuilder();

                //Adds the data
                char len = (char)((newKey.Length) + 32);
                OutCont.Append(len);
                OutCont.Append(MyCode.Encrypt(FileEncInCont, newKey));
                OutCont.Append(newKey);

                //initializes a new strinbuilder for the name of the file
                StringBuilder name = new StringBuilder();
                name.Append(FileEncInName.Substring(0, FileEncInName.Length - 4));
                name.Append("_Encrypted.txt");
                //Makes a new name

                //Makes a new path
                StringBuilder newpath = new StringBuilder();
                newpath.Append(FileEncInPath.Substring(0, (FileEncInPath.Length - FileEncInName.Length)));
                newpath.Append(name.ToString());
                //Makes a new path

                //changes the path from stringbuilder to string
                OutPath = newpath.ToString();

                //writes the file
                File.WriteAllText(OutPath, OutCont.ToString());
                FilePop.Show();
            }
        }

        private void FileDec_Clicked(object sender, EventArgs e)
        {
            if (FileDecInPath == null)
            {
                FileDecIn.TextColor = Color.Red;
                return;
            }

            else
            {
                //Gets get length
                char chr = FileDecInCont[0];
                int length = ((int)chr) - 32;

                //Gets key from content
                string Key = FileDecInCont.Substring( (FileDecInCont.Length - length) , length);
                

                //Get the actual content
                string cipher = FileDecInCont.Substring(1, (FileDecInCont.Length - (length + 1)));

                //Initializes the variable to store the content of the file
                OutCont = new StringBuilder();

                //Adds the data
                string normal = MyCode.Decrypt(cipher, Key);
                OutCont.Append(normal);
                


                //initializes a new strinbuilder for the name of the file
                StringBuilder name = new StringBuilder();
                name.Append(FileDecInName.Substring(0, FileDecInName.Length - 4));
                name.Append("_Decrypted.txt");
                //Makes a new name

                //Makes a new path
                StringBuilder newpath = new StringBuilder();
                newpath.Append(FileDecInPath.Substring(0, (FileDecInPath.Length - FileDecInName.Length)));
                newpath.Append(name.ToString());
                //Makes a new path

                //changes the path from stringbuilder to string
                OutPath = newpath.ToString();

                //writes the file
                File.WriteAllText(OutPath, OutCont.ToString());
                FilePop.Show();
            }
        }

        private void VFile()
        {
            Navigation.PushAsync(new ViewFile(OutPath,OutCont.ToString()));
            
        }

        
    }
}
