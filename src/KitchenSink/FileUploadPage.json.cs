using System;
using System.IO;
using Starcounter;

namespace KitchenSink
{
    partial class FileUploadPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.SessionId = Session.Current.SessionId;
        }

        public string GetFileSizeString(long Size)
        {
            string[] sizes = new string[] {"Bytes", "KB", "MB", "GB", "TB"};

            if (Size == 0)
            {
                return "0 Byte";
            }

            int i = (int) (Math.Floor(Math.Log(Size)/Math.Log(1024)));
            string size = Math.Round(Size/Math.Pow(1024, i), 2) + " " + sizes[i];

            return size;
        }

        [FileUploadPage_json.Files]
        partial class FileUploadFilePage : Json
        {
            static FileUploadFilePage()
            {
                DefaultTemplate.FileSizeString.Bind = "FileSizeStringBind";
            }

            public FileUploadPage ParentPage
            {
                get { return this.Parent.Parent as FileUploadPage; }
            }

            public string FileSizeStringBind
            {
                get { return this.ParentPage.GetFileSizeString(this.FileSize); }
            }

            void Handle(Input.DeleteTrigger Action)
            {
                if (File.Exists(this.FilePath))
                {
                    File.Delete(this.FilePath);
                }

                this.ParentPage.Files.Remove(this);
            }
        }

        [FileUploadPage_json.Tasks]
        partial class FileUploadTaskPage : Json
        {
            static FileUploadTaskPage()
            {
                DefaultTemplate.FileSizeString.Bind = "FileSizeStringBind";
            }

            public FileUploadPage ParentPage
            {
                get { return this.Parent.Parent as FileUploadPage; }
            }

            public string FileSizeStringBind
            {
                get { return this.ParentPage.GetFileSizeString(this.FileSize); }
            }
        }
    }
}